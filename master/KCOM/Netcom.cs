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
    public partial class FormMain
    {
		TcpListener Listener;
        TcpClient remoteClient;                     //Server用，远端的client, 从Listener中获得
        TcpClient remoteServer;						//Client用，远端的Server，从IP地址中获得
		NetworkStream network_stream_client;
        BinaryReader bw_client_read_from_server;
        BinaryWriter bw_client_write_to_server;

		NetworkStream network_stream_server;
		BinaryReader br_server_read_from_client;
        BinaryWriter bw_server_write_to_client;
		
		Thread thread_net;

		bool netcom_is_connected = false;

        private void Func_NetCom_Init()
        {
            Func_TextFont_Change();

            Func_NetCom_ChangeFont(param1.netcom_is_server);

            textBox_IP1.Text = Properties.Settings.Default._netcom_ip1.ToString();
            textBox_IP2.Text = Properties.Settings.Default._netcom_ip2.ToString();
            textBox_IP3.Text = Properties.Settings.Default._netcom_ip3.ToString();
            textBox_IP4.Text = Properties.Settings.Default._netcom_ip4.ToString();

            label_ShowIP.Text = "Local IP:\r\n";
            string name = Dns.GetHostName();  
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);  
            foreach (IPAddress ipa in ipadrlist)  
            {
                if(ipa.AddressFamily == AddressFamily.InterNetwork)
                {
                    //Console.WriteLine(ipa.ToString());
                    label_ShowIP.Text += ipa.ToString() + "\r\n";
                }                
            } 
        }

		private void Func_NetCom_Close()
		{
			if(param1.netcom_is_server == true)	//服务器往客户端发送
			{
				Func_Server_Close();
			}
			else														//客户端往服务器端发送
			{
				Func_Clint_Close();
			}
		}

        private void Func_NetCom_ChangeFont(bool is_server)
        {
            if(is_server == false)
            {
				Func_Set_Form_Text("(Client)", "");
                button_NetPoint.ForeColor = System.Drawing.Color.Blue;
				button_NetPoint.Text = "I am Client";
                button_NetRun.Text = "Connect to Server";
                label_IP.Text = "Server IP:";                
            }
            else
            {
				Func_Set_Form_Text("(Server)", "");
                button_NetPoint.ForeColor = System.Drawing.Color.Red;
				button_NetPoint.Text = "I am Server";
                button_NetRun.Text = "Wait for Clients";
                label_IP.Text = "Local IP:";
            }			
        }

        private void button_NetPoint_Click(object sender, EventArgs e)
        {
            this.Text = "KCOM V" + Parameter._VersionMSB.ToString() + "." + Parameter._VersionLSB.ToString();
            if(param1.netcom_is_server == true)
            {
                param1.netcom_is_server = false;
            }
            else
            {
                param1.netcom_is_server = true;
            }
            Func_NetCom_ChangeFont(param1.netcom_is_server);
        }

        bool first_run_init_done = false;

        private void button_NetRun_Click(object sender, EventArgs e)
        {
            if(first_run_init_done == false)
            {
                first_run_init_done = true;                

                /**********************创建线程****************************/
                string strInfo = string.Empty;
                thread_net = new Thread(ThreadEntry_Net);					//实例化Thread线程对象
				
                strInfo = "\nThe managed Thread ID:" + thread_net.ManagedThreadId;
                strInfo += "\nThread Name:" + thread_net.Name;
                strInfo += "\nThread State:" + thread_net.ThreadState.ToString();
                strInfo += "\nThread Priority:" + thread_net.Priority.ToString();
                strInfo += "\nIs Backgroud" + thread_net.IsBackground;
                Console.WriteLine(strInfo);

                thread_net.IsBackground = true;//设置为后台程序，它的主线程结束，它也一起结束                                       
                thread_net.Start();                                         //启动线程 
                /**********************创建线程****************************/
            }

			if(netcom_is_connected == false)
			{
				String IP_Str = "";

				IP_Str = textBox_IP1.Text + "." + textBox_IP2.Text + "." + textBox_IP3.Text + "." + textBox_IP4.Text;

				//服务器：真正串口要接收东西的那个
				//textBox_Netcom.Text += "Server is listening...\r\n";
				if(param1.netcom_is_server == true)
				{
					textBox_ComRec.Text += "Waiting for the Client...\r\n";

					IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(IP_Str), 6666); //本地端地址  
					Listener = new TcpListener(localEP);					//建立监听类，并绑定到指定的端地址  

					try
					{
						Listener.Start();                                   //开始监听
					}
					catch(Exception ex)
					{
						MessageBox.Show(ex.Message, "Error local IP setup!");
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
								textBox_ComRec.Text += "Can't find any client" + "\r\n";
								MessageBox.Show("Can't find any client", Func_GetStack("Attention!"));

								Listener.Stop();
								return;
							}
						}
						else
						{
							remoteClient = Listener.AcceptTcpClient();		//等待连接（阻塞）

							network_stream_server = remoteClient.GetStream();
							br_server_read_from_client = new BinaryReader(network_stream_server);
							bw_server_write_to_client = new BinaryWriter(network_stream_server);

							textBox_ComRec.Text += "Client connect successfully!\r\nClient:"   //打印客户端连接信息；
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
						textBox_ComRec.Text += "Can't connect to server:" + IP_Str + "\r\n";
						MessageBox.Show("Can't connect to server:" + IP_Str + "\r\n" + ex.Message, "Warning");
						return;
					}

					network_stream_client = remoteServer.GetStream();
					bw_client_read_from_server = new BinaryReader(network_stream_client);
					bw_client_write_to_server = new BinaryWriter(network_stream_client);

					textBox_ComRec.Text += "Connect to server successfully:" + IP_Str + "\r\n";
					Console.WriteLine("I'm using {0}.", remoteServer.Client.LocalEndPoint); //打印自己使用的端地址；  
				}

				netcom_is_connected = true;
				button_NetRun.Text = "Break the NetCom";
			}
			else
			{
				//客户端等待服务器数据
                if(param1.netcom_is_server == false)
                {
					Func_Clint_Close();
                }
                else    //服务器等待客户端数据
                {
					Func_Server_Close();
                }	

				MessageBox.Show("Break the NetCom", Func_GetStack("Warning!"));
			}
        }

		private void Func_Server_Close()
		{
			netcom_is_connected = false;
			this.Invoke((EventHandler)(delegate
			{
				button_NetRun.Text = "Wait for Clients";
			}));
			bw_server_write_to_client.Close();
			br_server_read_from_client.Close();
			network_stream_server.Close();
			remoteClient.Close();
			Listener.Stop();
		}

		private void Func_Clint_Close()
		{
			netcom_is_connected = false;
			this.Invoke((EventHandler)(delegate
			{
				button_NetRun.Text = "Connect to Server";
			}));
			bw_client_write_to_server.Close();
			bw_client_read_from_server.Close();
			network_stream_client.Close();
			remoteServer.Close();
		}

		private void Func_NetCom_SendData(string str)
		{
			if(netcom_is_connected == true)
			{
				if(str.Length == 0)
				{
					MessageBox.Show("Empty send length!", Func_GetStack("Error!"));
				}
				else
				{
					if(param1.netcom_is_server == true)//服务器往客户端发送
					{
						try
						{
                            bw_server_write_to_client.Write(str);
						}
						catch(Exception ex)
						{
							if(netcom_is_connected == true)
							{
								MessageBox.Show(ex.Message, "Client lost, Write Fail!");
								Func_Server_Close();
							}
						}
					}
					else                                                    //客户端往服务器端发送
					{
						try
						{
							bw_client_write_to_server.Write(str);			//向服务器发送字符串
						}
						catch(Exception ex)
						{
							if(netcom_is_connected == true)
							{
								MessageBox.Show(ex.Message, "Server lost, Write Fail!");
								Func_Clint_Close();
							}
						}
					}
				}
			}		
		}

        public void ThreadEntry_Net()                                       //线程入口
        {
            string recvmsg = null;                                          //接收消息

            while(true)
			{
				if(netcom_is_connected == true)
                {
                    //客户端等待服务器数据
                    if(param1.netcom_is_server == false)
                    {
                        try
                        {
							//接受服务器发送过来的消息,注意Client已经把数据处理成字符串了！
                            recvmsg = bw_client_read_from_server.ReadString();
                        }
                        catch(Exception ex)
                        {
							if(netcom_is_connected == true)
							{
								MessageBox.Show(ex.Message, "Server lost, Read fail!");
								Func_Clint_Close();
							}
                        }
                    }
                    else    //服务器等待客户端数据
                    {
                        try
                        {
							//接收客户端发送的数据  
							recvmsg = br_server_read_from_client.ReadString();
                        }
                        catch(Exception ex)
                        {
							if(netcom_is_connected == true)
							{
								MessageBox.Show(ex.Message, "Client lost, Read fail!");
								Func_Server_Close();
							}
                        }
                    }					

                    if(recvmsg != null)
                    {
						this.Invoke((EventHandler)(delegate
                        {	
							textBox_ComRec.AppendText(recvmsg);
                        }));
                    }
                }
            }
        }
    }
}
