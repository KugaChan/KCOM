//#define SUPPORT_SHOW_DATA
//#define SUPPORT_SHOW_LEN

//#define WARNING_USE_MESSAGEBOX

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Threading;     //使用线程
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms; //使用MessageBox
using System.Collections.Concurrent;    //使用ConcurrentQueue

namespace KCOM
{
    class eTCP
    {
        public class tyNode
        {
            public byte[] buffer;
            public int length;
            public PNode<tyNode> pnode;

            public tyNode(int max_len)
            {
                buffer = new byte[max_len];
                length = 0;
            }
        }

        public const int TCP_MAX_DATA_LEN = 1024*1024;
        public const int TCP_MAX_DEPTH_NUM = 8;

        //使用eFIFO的效率要比queue高，因为eFIFO是提前把buffer申请出来的，queue进队列还要copy一次
        private eFIFO<tyNode> efifo_rcv = new eFIFO<tyNode>();
        private ePool<tyNode> epool_rcv = new ePool<tyNode>();

        public bool is_active = false;

        int port = 0;
        public bool is_server = true;

        Thread thread_rcv;
        
        public void Enter_MessageQueue(bool _is_server, bool box, string str)
        {
            string role = "";
            if(_is_server == true)
            {
                role = "[TCP Server," + DateTime.Now.ToString() + "]:";
            }
            else
            {
                role = "[TCP Client," + DateTime.Now.ToString() + "]:";
            }            

            if(box == true)
            {
                MessageBox.Show(str, role);
            }
            else
            {
                Console.WriteLine(role + str);
            }
            role = "\r\n" + role + str;
        
            Dbg.queue_message.Enqueue(role);
        }

        public string ShowLocalIP()
        {
            string local_ip = "Local IP:\r\n";
            string name = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            foreach(IPAddress ipa in ipadrlist)
            {
                if(ipa.AddressFamily == AddressFamily.InterNetwork)
                {
                    local_ip += ipa.ToString() + "\r\n";
                }
            }

            return local_ip;
        }

        public int GetRcvNum()
        {
            return efifo_rcv.GetValidNum();
        }

        public byte[] GetRcvBuffer(ref int _recv_length)
        {
            if(efifo_rcv.GetValidNum() == 0)
            {
                _recv_length = 0;
                return null;
            }
            else
            {
                tyNode nnode = efifo_rcv.Output();

                byte[] _recv_buffer = nnode.buffer;
                _recv_length = nnode.length;

                epool_rcv.Put(nnode.pnode);

                return _recv_buffer;
            }
        }

        public void Init()
        {
            efifo_rcv.Init(TCP_MAX_DEPTH_NUM);
            for(int i = 0; i < TCP_MAX_DEPTH_NUM; i++)
            {
                tyNode nnode = new tyNode(TCP_MAX_DATA_LEN);
                PNode<tyNode> pnode = new PNode<tyNode>();

                nnode.pnode = pnode;
                epool_rcv.Add(pnode, nnode);
            }
        }

        public bool ConfigNet(int _port, string ip1, string ip2, string ip3, string ip4)
        {
            bool res;

            port = _port;

            ConfigIP(ip1, ip2, ip3, ip4);
            res = Start();

            return res;
        }

        string str_ip = "";
        public bool ConfigIP(string ip1, string ip2, string ip3, string ip4)
        {
            int val1, val2, val3, val4;
            bool res1, res2, res3, res4;

            res1 = int.TryParse(ip1, out val1);
            res2 = int.TryParse(ip2, out val2);
            res3 = int.TryParse(ip3, out val3);
            res4 = int.TryParse(ip4, out val4);
            if((res1 == true) && (res2 == true) && (res3 == true) && (res4 == true))
            {
                str_ip = ip1 + "." + ip2 + "." + ip3 + "." + ip4;
                return true;
            }
            else
            {
                str_ip = "";
                return false;
            }
        }

        public bool Start()
        {            
            if(is_server == true)
            {
                thread_rcv = new Thread(ThreadEntry_ServerRcv);
            }
            else
            {
                thread_rcv = new Thread(ThreadEntry_ClientRcv);
            }
            
            thread_rcv.IsBackground = true;//设置为后台程序，它的主线程结束，它也一起结束                                       
            thread_rcv.Start();                                             //启动线程

            return true;
        }

        TcpListener Listener = null;                                        //必须放到线程里，否则无法自动重连...
        TcpClient remoteClient = null;                                      //Server用，远端的client, 从Listener中获得
        NetworkStream network_stream_server = null;
        BinaryReader br_server_read_from_client = null;
        BinaryWriter bw_server_write_to_client = null;

        TcpClient remoteServer = null;                                      //Client用，远端的Server，从IP地址中获得
        NetworkStream network_stream_client = null;
        BinaryReader bw_client_read_from_server = null;
        BinaryWriter bw_client_write_to_server = null;
        

        public void Close()
        {
            thread_rcv.Abort();
            Thread.Sleep(1200);

            if(Listener != null)
            {
                Listener.Stop();
                Listener = null;
            }

            if(remoteClient != null)
            {
                bw_server_write_to_client.Close();
                br_server_read_from_client.Close();
                network_stream_server.Close();

                remoteClient.Close();
                remoteClient = null;
            }

            if(remoteServer != null)
            {
                remoteServer.Close();
                remoteServer = null;
            }

            if(network_stream_client != null)
            {
                bw_client_write_to_server.Close();
                bw_client_read_from_server.Close();

                network_stream_client.Close();
                network_stream_client = null;
            }

            is_active = false;
			
            Enter_MessageQueue(is_server, false, "eTCP break!");
        }

        public void SendData(byte[] snd_buff)
        {
            Dbg.Assert(snd_buff.Length > 0, "Send length can not be empty!");
            
            if(is_active == false)
            {
                Enter_MessageQueue(is_server, false, "Client is not connected, return!");
                return;
            }

#if SUPPORT_SHOW_LEN
            Console.WriteLine("Snd:{0}", snd_buff.Length);
#endif

#if SUPPORT_SHOW_DATA
            Func.DumpBuffer(snd_buff, snd_buff.Length);
#endif

            try
            {
                if(is_server == true)
                {
                    bw_server_write_to_client.Write(snd_buff);              //向客户端发送字符串
                }
                else
                {
                    bw_client_write_to_server.Write(snd_buff);              //向服务器发送字符串
                }
            }
            catch(Exception ex)
            {
                Enter_MessageQueue(is_server, false, "Tcp break, send fail! " + ex.Message);
            }
        }

        public void ThreadEntry_ClientRcv()
        {
            Enter_MessageQueue(is_server, false, "ThreadEntry_ClientRcv run");

            while(true)
            {
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(str_ip), port);    //远程服务器端地址；
                remoteServer = new TcpClient();
                
                try
                {
                    remoteServer.Connect(remoteEP);                         //调用connect方法连接远端服务器；  
                }
                catch(Exception ex)
                {
                    remoteServer.Close();
                    remoteServer = null;

                    Enter_MessageQueue(is_server, false, "Can't connect to server:" + str_ip + ", " + ex.Message);
                    Thread.Sleep(500);
                    continue;
                }

                is_active = true;

                network_stream_client = remoteServer.GetStream();
                bw_client_read_from_server = new BinaryReader(network_stream_client);
                bw_client_write_to_server = new BinaryWriter(network_stream_client);

                Enter_MessageQueue(is_server, false, "Connect to server successfully:" + str_ip);                
                Console.WriteLine("I'm using {0}.", remoteServer.Client.LocalEndPoint); //打印自己使用的端地址

                while(true)
                {
                    /********************接收数据部分 Start******************/
                    int rcv_length;

                    PNode<tyNode> pnode = epool_rcv.Get();

                    tyNode nnode = pnode.obj;

                    try
                    {
                        //接受服务器发送过来的消息,注意Client已经把数据处理成字符串了！
                        rcv_length = bw_client_read_from_server.Read(nnode.buffer, 0, TCP_MAX_DATA_LEN);
                    }
                    catch(Exception ex)
                    {
                        epool_rcv.Put(pnode);
                        Enter_MessageQueue(is_server, false, "Server lost, Read fail!" + ex.Message);
                        is_active = false;
                        break;
                    }

#if SUPPORT_SHOW_LEN
                	Console.WriteLine("Client rcv:{0}", rcv_length);
#endif

#if SUPPORT_SHOW_DATA
                    Func.DumpBuffer(efifo_rcv.Peek(), rcv_length);
#endif
                    if(rcv_length == 0)
                    {
                        epool_rcv.Put(pnode);
                        Enter_MessageQueue(is_server, false, "Server lost, Read error!");
                        is_active = false;
                        break;
                    }
                    else
                    {
                        nnode.length = rcv_length;
                        efifo_rcv.Input(nnode);
                    }
                    /********************接收数据部分 End********************/
                }
            }

            //TcpMessage(is_server, false, "ThreadEntry_ClientRcv end");
        }


        public void ThreadEntry_ServerRcv()                                 //线程入口
        {
            Enter_MessageQueue(is_server, false, "ThreadEntry_ServerRcv run");

            while(true)
            {
                while(true)
                {
                    IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(str_ip), port); //本地端地址  
                    Listener = new TcpListener(localEP);                                //建立监听类，并绑定到指定的端地址  

                    try
                    {
                        Listener.Start();                                               //开始监听
                    }
                    catch(Exception ex)
                    {
                        Listener.Stop();
                        Listener = null;

                        Enter_MessageQueue(is_server, false, "Error local IP:" + localEP.ToString() + "setup. " + ex.Message);

                        Thread.Sleep(500);
                        continue;
                    }
                    
                    Enter_MessageQueue(is_server, false, "IP setup ok!");

                    DateTime listener_pending_mark;
                    DateTime listener_pending_timeout;

                    listener_pending_mark = DateTime.Now;

                    while(true)
                    {
                        if(Listener.Pending() == false)
                        {
                            //为了避免每次都被tcpListener.AcceptTcpClient()阻塞线程，添加了此判断，  
                            //no connection requests have arrived
                            //当没有连接请求时，什么也不做，有了请求再执行到tcpListener.AcceptTcpClient() 

                            listener_pending_timeout = DateTime.Now;
                            TimeSpan ts = listener_pending_timeout - listener_pending_mark;
                            int second_pass = ts.Minutes * 60 + ts.Seconds;
                            if(second_pass >= 5)
                            {
                                Listener.Stop();
                                Listener = null;

                                Enter_MessageQueue(is_server, false, "Can't find any client");

                                Thread.Sleep(500);

                                break;
                            }
                        }
                        else
                        {
                            remoteClient = Listener.AcceptTcpClient();      //等待连接（阻塞）

                            network_stream_server = remoteClient.GetStream();
                            br_server_read_from_client = new BinaryReader(network_stream_server);
                            bw_server_write_to_client = new BinaryWriter(network_stream_server);

                            is_active = true;

                            break;
                        }
                    }

                    if(is_active == true)
                    {
                        break;
                    }
                }

                Enter_MessageQueue(is_server, false, "Tcp is active!");

                while(true) //循坏 接收/发送 数据
                {
                    /********************接收数据部分 Start******************/
                    int rcv_length;

                    PNode<tyNode> pnode = epool_rcv.Get();
                    tyNode nnode = pnode.obj;

                    try
                    {
                        //接收客户端发送的数据
                        rcv_length = br_server_read_from_client.Read(nnode.buffer, 0, TCP_MAX_DATA_LEN);
                    }
                    catch(Exception ex)
                    {
                        Enter_MessageQueue(is_server, false, "Client lost, Read fail!" + ex.Message);
                        is_active = false;
                        break;
                    }

#if SUPPORT_SHOW_LEN
                Console.WriteLine("Servre rcv:{0}", rcv_length);
#endif

#if SUPPORT_SHOW_DATA
                Func.DumpBuffer(efifo_rcv.Peek(), rcv_length);
#endif

                    if(rcv_length == 0)
                    {
                        epool_rcv.Put(pnode);
                        Enter_MessageQueue(is_server, false, "Client lost, Read error!");
                        is_active = false;
                        break;
                    }
                    else
                    {
                        nnode.length = rcv_length;
                        efifo_rcv.Input(nnode);
                    }
                    /********************接收数据部分 End********************/
                }
            }

            //TcpMessage(is_server, false, "ThreadEntry_ServerRcv end");
        }
    }
}
