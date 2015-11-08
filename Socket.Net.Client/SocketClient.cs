using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Socket.Net.Client
{
    public partial class SocketClient : Form
    {
        public SocketClient()
        {
            InitializeComponent();
        }

        NotifyClient client = null;
        private void button1_Click(object sender, EventArgs e)
        {
            if (connBtn.Text == "开始连接")
            {

                var user = new Protocol.User();
                user.name = (this.nameBox.Text.Trim().Length > 0 ? this.nameBox.Text.Trim() : "Client_" + new Random().Next(9999));
                user.pwd = ">>密码<<";
                user.role = "管理员";
                user.auth = "xxxxxxxxx";
                user.id = Guid.NewGuid();
                client = new NotifyClient(user.MakeAuthCode());

                this.connBtn.Text = "关闭连接";
            }
            else
            {
                if (this.client!=null)
                {
                    this.client.Stop();
                    this.connBtn.Text = "开始连接";
                }
            }
            
        }

        /// <summary>
        /// 输入文本，立即查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void queryBox_TextChanged(object sender, EventArgs e)
        {
            client.SendMessage(Protocol.CMD_TYPE.QUERY, "HOST", string.Format("3@1@4@1@5@9hint3@1@4@1@5@9{0}", this.queryBox.Text.Trim()));
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            client.SendMessage(Protocol.CMD_TYPE.QUERY, "HOST", string.Format("3@1@4@1@5@9any3@1@4@1@5@9{0}", this.queryBox.Text.Trim()));
        }


    }
}
