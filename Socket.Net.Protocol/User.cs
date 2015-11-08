using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Drawing;

namespace Socket.Net.Protocol
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        public Guid id;
        public Color titleColor = Color.Blue;
        public Color msgColor = Color.Black;
        public string name { get; set; }
        public string pwd { get; set; }
        public string role { get; set; }
        public string auth { get; set; }
        public TcpClient client { get; private set; }
        public BinaryReader br { get; private set; }
        public BinaryWriter bw { get; private set; }

        public User()
        {
            this.id = Guid.Empty;
            this.name = "HOST";
            this.pwd = "HOST_PWD";
            this.role = "HOST_ROLE";
            this.auth = "12369";
            this.titleColor = Color.Red;
            this.msgColor = Color.Black;
        }

        public User(TcpClient client)
        {
            this.id = Guid.NewGuid();
            this.client = client;
            this.name = client.Client.RemoteEndPoint.ToString();

            NetworkStream networkStream = client.GetStream();
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
            //连接时获取认证流信息
        }

        /// <summary>
        /// 认证方法
        /// </summary>
        /// <param name="auth_code"></param>
        /// <returns></returns>
        public bool Auth(string auth_code)
        {
            return true;
        }

        /// <summary>
        /// 拼凑认证码
        /// </summary>
        /// <returns></returns>
        public string MakeAuthCode()
        {
            string template = "'id':'{0}','name':'{1}','pwd':'{2}','role':'{3}','auth':'{4}'";
            return "{" + string.Format(template, id.ToString(), name, pwd, role, auth) + "}";
        }

        public void Close()
        {
            br.Close();
            bw.Close();
            client.Close();
        }
    }
}
