using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;//使用串口
using System.Runtime.InteropServices;//隐藏光标的
using System.Management;

using System.IO;
using System.Threading;     //使用线程
using System.Net;
using System.Net.Sockets;

//为变量定义别名
using u64 = System.UInt64;
using u32 = System.UInt32;
using u16 = System.UInt16;
using u8 = System.Byte;
using s64 = System.Int64;
using s32 = System.Int32;
using s16 = System.Int16;
using s8 = System.SByte;

namespace KCOM
{
    public partial class Form1
    {
		TcpListener Listener;
        TcpClient remoteClient;                     //Server用，远端的client
        TcpClient remoteServer;						//Client用，远端的Server
		NetworkStream network_stream_client;
        BinaryReader bw_client_read_from_server;
        BinaryWriter bw_client_write_to_server;

		NetworkStream network_stream_server;
		BinaryReader br_server_read_from_client;
        BinaryWriter bw_server_write_to_client;

        string[] net_recv_str_array = new string[256];
		u32 net_recv_top;
		u32 net_recv_bottom;		
		Thread BEThread;

        private void Func_NetCom_Init()
        {
            u32 i;

            for(i = 0; i < net_recv_str_array.Length; i++)
            {
				net_recv_str_array[i] = null;
            }
			net_recv_top = 0;
			net_recv_bottom = 0;

            Func_TextFont_Change();

            Func_NetCom_ChangeFont(Properties.Settings.Default._netcom_is_server);

            textBox_IP1.Text = Properties.Settings.Default._netcom_ip1.ToString();
            textBox_IP2.Text = Properties.Settings.Default._netcom_ip2.ToString();
            textBox_IP3.Text = Properties.Settings.Default._netcom_ip3.ToString();
            textBox_IP4.Text = Properties.Settings.Default._netcom_ip4.ToString();

            button_NetSend.Enabled = false;

            /**********************创建线程****************************/
            string strInfo = string.Empty;
            BEThread = new Thread(new ThreadStart(BEThreadEntry));   //实例化Thread线程对象

            strInfo = "\n线程唯一标识符：" + BEThread.ManagedThreadId;
            strInfo += "\n线程名字：" + BEThread.Name;
            strInfo += "\n线程状态：" + BEThread.ThreadState.ToString();
            strInfo += "\n线程优先级：" + BEThread.Priority.ToString();
            strInfo += "\n是否后台线程：" + BEThread.IsBackground;
            Console.WriteLine(strInfo);

            //BEThread.Abort("退出");     //结束线程
            BEThread.IsBackground = true;//设置为后台程序，它的主线程结束，它也一起结束                                       
            BEThread.Start();                                               //启动线程 
            /**********************创建线程****************************/
        }

        private void Func_NetCom_ChangeFont(bool is_server)
        {
            if(is_server == false)
            {
                this.Text += "(Client)";
                button_NetPoint.ForeColor = System.Drawing.Color.Blue;
				button_NetPoint.Text = "I am Client";
                button_NetRun.Text = "Connect to Server";
                label_IP.Text = "Server IP:";                
            }
            else
            {
                this.Text += "(Server)";
                button_NetPoint.ForeColor = System.Drawing.Color.Red;
				button_NetPoint.Text = "I am Server";
                button_NetRun.Text = "Wait for Clients";
                label_IP.Text = "Local IP:";
            }
        }

        private void button_NetPoint_Click(object sender, EventArgs e)
        {
            this.Text = "KCOM V" + _VersionMSB.ToString() + "." + _VersionLSB.ToString();
            if(Properties.Settings.Default._netcom_is_server == true)
            {
                Properties.Settings.Default._netcom_is_server = false;
            }
            else
            {
                Properties.Settings.Default._netcom_is_server = true;
            }
            Func_NetCom_ChangeFont(Properties.Settings.Default._netcom_is_server);
        }

        private void button_NetRun_Click(object sender, EventArgs e)
        {
            String IP_Str = "";

            IP_Str = textBox_IP1.Text + "." + textBox_IP2.Text + "." + textBox_IP3.Text + "." + textBox_IP4.Text;

			//服务器：真正串口要接收东西的那个
            //textBox_Netcom.Text += "Server is listening...\r\n";
            if(Properties.Settings.Default._netcom_is_server == true)
            {
                textBox_NetRecv.Text += "等待客户端连接...\r\n";

                IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(IP_Str), 6666); //本地端地址  
                Listener = new TcpListener(localEP);						//建立监听类，并绑定到指定的端地址  

                try
                {
                    Listener.Start();                                     //开始监听
                }
                catch
                {
                    MessageBox.Show("本地IP设置错误!", "提示");
                    return;
                }

				s32 minute_mark;
				s32 second_mark;
				s32 listener_pending_mark;
				s32 listener_pending_timeout;

				minute_mark = DateTime.Now.Minute;
				second_mark = DateTime.Now.Second;
				listener_pending_mark = minute_mark * 60 + second_mark;				

				while(true)
				{
                    if(Listener.Pending() == false)
                    {
                        //为了避免每次都被tcpListener.AcceptTcpClient()阻塞线程，添加了此判断，  
                        //no connection requests have arrived
                        //当没有连接请求时，什么也不做，有了请求再执行到tcpListener.AcceptTcpClient() 

                        minute_mark = DateTime.Now.Minute;
                        second_mark = DateTime.Now.Second;
                        listener_pending_timeout = minute_mark * 60 + second_mark;
                        //Console.WriteLine("listener pending{0}:{1}....", listener_pending_mark, listener_pending_timeout); 
                        if(listener_pending_timeout - listener_pending_mark >= 10)
                        {
                            textBox_NetRecv.Text += "没有发现任何客户端" + "\r\n";
                            MessageBox.Show("没有发现任何客户端", "提示");

                            Listener.Stop();
                            return;
                        }
                    }
                    else
                    {
						remoteClient = Listener.AcceptTcpClient();			//等待连接（阻塞）

						network_stream_server = remoteClient.GetStream();
						br_server_read_from_client = new BinaryReader(network_stream_server);
                        bw_server_write_to_client = new BinaryWriter(network_stream_server);

						textBox_NetRecv.Text += "客户端连接成功!\r\nClient:"   //打印客户端连接信息；
						+ remoteClient.Client.RemoteEndPoint.ToString() + "\r\n";

						break;
					}
				}
            }
            else
            {
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(IP_Str), 6666);    //远程服务器端地址；
				remoteServer = new TcpClient();

                try
                {
                    remoteServer.Connect(remoteEP);                         //调用connect方法连接远端服务器；  
                }
				catch(Exception ex)
                {
                    textBox_NetRecv.Text += "无法连接到服务器:" + IP_Str + "\r\n";
					MessageBox.Show("无法连接到服务器:" + IP_Str + "\r\n" + ex.Message, "提示");
                    return;
                }

				network_stream_client = remoteServer.GetStream();
                bw_client_read_from_server = new BinaryReader(network_stream_client);
				bw_client_write_to_server = new BinaryWriter(network_stream_client);

                textBox_NetRecv.Text += "连接服务器成功:" + IP_Str + "\r\n";
                Console.WriteLine("I'm using {0}.", remoteServer.Client.LocalEndPoint); //打印自己使用的端地址；  
            }

            button_NetSend.Enabled = true;  
            button_NetRun.Enabled = false;
        }

		private void Func_NetCom_SendData(string str)
		{
			if(button_NetRun.Enabled == false)
			{
				if(str.Length == 0)
				{
					MessageBox.Show("发送长度为空", "Error!");
				}
				else
				{
					if(Properties.Settings.Default._netcom_is_server == true)//服务器往客户端发送
					{
						try
						{
                            bw_server_write_to_client.Write(str);
						}
						catch
						{
							MessageBox.Show("客户端失去连接", "写入失败");
							this.Invoke((EventHandler)(delegate
							{
								button_NetRun.Enabled = true;
							}));
                            bw_server_write_to_client.Close();
                            network_stream_server.Close();
							Listener.Stop();
						}
					}
					else                                                    //客户端往服务器端发送
					{
						try
						{
							bw_client_write_to_server.Write(str);			//向服务器发送字符串
						}
						catch
						{
                            MessageBox.Show("服务器失去连接", "写入失败");
							this.Invoke((EventHandler)(delegate
							{
								button_NetRun.Enabled = true;
							}));
							bw_client_write_to_server.Close();
                            network_stream_client.Close();
							remoteServer.Close();
						}
					}
				}
			}		
		}

        private void button_NetSend_Click(object sender, EventArgs e)
        {
			Func_NetCom_SendData(textBox_NetSend.Text);
        }

        private void button_CleanNetRec_Click(object sender, EventArgs e)
        {
            textBox_NetRecv.Text = "";
        }

        private void button_CleanNetSnd_Click(object sender, EventArgs e)
        {
            textBox_NetSend.Text = "";
        }

        public void BEThreadEntry()                                         //线程入口
        {
            string recvmsg = null;                                          //接收消息
			string recv_data_buffer = "";

            while(true)
            {
                if(button_NetRun.Enabled == false)
                {
                    //客户端等待服务器数据
                    if(Properties.Settings.Default._netcom_is_server == false)
                    {
                        try
                        {
                            recvmsg = bw_client_read_from_server.ReadString();//接受服务器发送过来的消息 
                        }
                        catch
                        {
                            MessageBox.Show("服务器失去连接", "读取失败");
                            this.Invoke((EventHandler)(delegate
                            {
                                button_NetRun.Enabled = true;
                            }));
                            bw_client_read_from_server.Close();
                            network_stream_client.Close();
                            remoteServer.Close();
                        }
                    }
                    else    //服务器等待客户端数据
                    {
                        try
                        {
							recvmsg = br_server_read_from_client.ReadString();  //接收服务器发送的数据  
                        }
                        catch
                        {
                            MessageBox.Show("客户端失去连接", "读取失败");
                            this.Invoke((EventHandler)(delegate
                            {
                                button_NetRun.Enabled = true;
                            }));
                            br_server_read_from_client.Close();
                            network_stream_server.Close();
							Listener.Stop();
                        }
                    }					

                    if(recvmsg != null)
                    {
						u32 net_recv_pending;

						net_recv_pending = net_recv_top - net_recv_bottom;
						if(net_recv_pending > net_recv_str_array.Length * 10)
						{
							net_recv_pending = 0;
							MessageBox.Show("数据发送阻塞!", "警告");
						}
						Console.WriteLine("A:{0}|{1}|{2}", net_recv_top, net_recv_bottom, net_recv_pending);

						/*
						this.Invoke((EventHandler)(delegate
						{
							//textBox_NetRecv.Text += recvmsg;
							textBox_NetRecv.AppendText(recvmsg);    //使用AppendText可以让文件光标随着文本走，而+=不行
						}));
						 */
						
						if(net_recv_pending >= net_recv_str_array.Length - 100)
						{
							recv_data_buffer += recvmsg;
						}
						else
						{
							if(recv_data_buffer.Length > 0)
							{
								recvmsg = recv_data_buffer + recvmsg;
								recv_data_buffer = "";
							}
							net_recv_str_array[net_recv_top % net_recv_str_array.Length] = recvmsg;
							net_recv_top++;
						}

						/*
						while(true)
						{
							if(net_recv_pending >= net_recv_str_array.Length - 100)
							{
								//Console.WriteLine("数据发送阻塞. rw:{0}|{1}|{2}", net_recv_top, net_recv_bottom, net_recv_pending);
								MessageBox.Show("数据发送阻塞!", "警告");
								Thread.Sleep(1000);
							}
							else
							{
								net_recv_str_array[net_recv_top % net_recv_str_array.Length] = recvmsg;
								net_recv_top++;
								break;
							}
						}
						*/
                    }
                }
            }
        }
    }
}
