using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;
using Socket.Net.Protocol;
using System.Text.RegularExpressions;

namespace Socket.Net.Client
{

    public class NotifyClient
    {
        private string address;
        private int port;

        private bool isExit = false;
        private TcpClient client;
        private BinaryReader br;
        private BinaryWriter bw;
        private byte[] protocol_header;
        private string from_user = string.Empty;
        private string to_user = string.Empty;
        private string authString = string.Format("[新用户{0}]", new Random().Next(1000));

        System.Windows.Forms.Timer timer = null;
        /// <summary>
        /// 新建socket客户端,并且启动
        /// </summary>
        /// <param name="authString">用户信息/授权信息</param>
        public NotifyClient(string authString)
        {
            this.authString = authString;

            address = ConfigurationManager.AppSettings["socketAddress"].ToString();
            if (Regex.IsMatch(address, @"([a-zA-Z0-9]+\.)+[a-zA-Z]+"))
            {
                address = Dns.GetHostEntry(address).AddressList[0].ToString();
            }
            port = Convert.ToInt32(ConfigurationManager.AppSettings["socketPort"].ToString());

            //定时检测服务端是否开启
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 2000;
            timer.Tick += AutoStart;
            timer.Start();
        }

        bool isIn = false;

        /// <summary>
        /// 自动开启
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void AutoStart(object sender, EventArgs args)
        {
            if ((socketThread == null || !socketThread.IsAlive) && !isIn)
            {
                isIn = true;
                StartSocketThread();
            }
        }

        Thread socketThread = null;
        int failCount = 0;
        private void StartSocketThread()
        {
            socketThread = new Thread(SocketClientThread);
            socketThread.Start(this.authString);
        }

        /// <summary>
        /// 强制关闭
        /// </summary>
        public void Stop()
        {
            if (timer != null)
            {
                timer.Stop();
            }
            isExit = true;
            failCount = 0;
            if (socketThread != null && socketThread.IsAlive)
            {
                socketThread.Abort();
                socketThread = null;
            }
        }

        /// <summary>
        /// 客户端线程
        /// </summary>
        private void SocketClientThread(object auth_code)
        {
            //启动的时候添加当前用户信息
            from_user = auth_code.ToString();
            try
            {
                //此处为方便演示，实际使用时要将Dns.GetHostName()改为服务器域名
                //IPAddress ipAd = IPAddress.Parse("182.150.193.7");
                client = new TcpClient();
                client.Connect(IPAddress.Parse(address), port);
                new NotifyForm("已连接服务器.", true);
            }
            catch (Exception ex)
            {
                failCount++;
                //第一次失败则提醒.【后面连接上,重置计数器】
                if (failCount == 1)
                {
                    new NotifyForm("连接服务器失败！请联系管理员.", true);
                }
                isIn = false;
                socketThread.Abort();
                return;
            }

            failCount = 0;
            //获取网络流
            NetworkStream networkStream = client.GetStream();
            //将网络流作为二进制读写对象
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
            SendMessage(CMD_TYPE.HELLO, "HOST", "请求连接.");
            Thread threadReceive = new Thread(new ThreadStart(ReceiveDataThread));
            threadReceive.IsBackground = true;
            threadReceive.Start();
        }

        /// <summary>
        /// 处理服务器信息
        /// </summary>
        private void ReceiveDataThread()
        {
            string msg = null;
            while (isExit == false)
            {
                Protocol.CMD_TYPE msg_type = CMD_TYPE.NULL;
                string auth_code = string.Empty;
                string to_user = string.Empty;
                string msg_temp = string.Empty;

                try
                {
                    bool isOK = Protocol.Protocol.ParseProtocolMsg(br, out msg_type, out auth_code, out to_user, out msg_temp);
                    if (isOK)
                    {
                        msg = msg_temp;
                    }
                    else
                    {
                        msg = string.Empty;
                    }
                }
                catch (Exception e)
                {
                    if (isExit == false)
                    {
                        new NotifyForm("与服务器失去连接", true);
                    }
                    break;
                }

                if (!string.IsNullOrEmpty(msg) && msg_type != CMD_TYPE.BEAT && msg_type != CMD_TYPE.NULL && msg_type != CMD_TYPE.EXIT)
                {
                    if(CMD_TYPE.QUERY == msg_type)
                    {
                        //怎么暴露给别人调用结果，回调？
                    }
                    else
                    {
                        new NotifyForm(msg, false, this);
                    }                    
                }

            }
        }

        /// <summary>
        /// 向服务端发送消息
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(CMD_TYPE cmd_type, string to_user, string message)
        {
            try
            {
                //添加协议头
                protocol_header = Protocol.Protocol.MakeProtocolMsg(cmd_type, from_user, to_user, message);
                bw.Write(protocol_header);

                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                //bw.Write(message);
                bw.Flush();
            }
            catch
            {
                new NotifyForm("发送失败", true);
            }
        }

    }


}
