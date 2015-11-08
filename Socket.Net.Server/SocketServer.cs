using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Socket.Net.Protocol;
using System.Text.RegularExpressions;

namespace Socket.Net.Server
{
    public partial class SocketServer : UserControl
    {
        private int port;
        private string ServerIP;
        private TcpListener serverTCPListener;

        /// <summary>
        /// 是否正常退出所有接收线程
        /// </summary>
        private bool isNormalExit = false;

        /// <summary>
        /// 保存连接的所有用户
        /// </summary>
        private List<User> userList = new List<User>();

        public SocketServer()
        {
            InitializeComponent();
            this.clientListBox.ItemHeight = 15;
            this.clientListBox.DisplayMember = "Name";
        }

        /// <summary>
        /// 主网络线程
        /// </summary>
        Thread threadSocket = null;

        /// <summary>
        /// 心跳定时器
        /// </summary>
        System.Windows.Forms.Timer heartBeatTimer = null;

        /// <summary>
        /// 启动、停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startBtn_Click(object sender, EventArgs e)
        {
            if (this.startBtn.Text == "启动")
            {
                this.startBtn.Text = "停止";
                this.startBtn.BackColor = Color.Red;

                //读取配置，设置监听
                ServerIP = System.Configuration.ConfigurationManager.AppSettings["socketIP"].ToString();
                if (Regex.IsMatch(ServerIP, @"([a-zA-Z0-9]+\.)+[a-zA-Z]+"))
                {
                    ServerIP = Dns.GetHostEntry(ServerIP).AddressList[0].ToString();
                }
                port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["socketPort"].ToString());

                serverTCPListener = new TcpListener(IPAddress.Parse(ServerIP), port);
                serverTCPListener.Start();

                threadSocket = new Thread(new ThreadStart(SocketThread));
                threadSocket.Start();



                heartBeatTimer = new System.Windows.Forms.Timer();
                heartBeatTimer.Interval = 5000;
                heartBeatTimer.Tick += HeartBeatHandler;
                heartBeatTimer.Start();
            }
            else
            {
                this.startBtn.Text = "启动";
                this.startBtn.BackColor = Color.Green;

                //关闭所有用户的流,将会自动移除用户
                foreach (var user in this.userList)
                {
                    user.br.Close();
                    user.bw.Close();
                }
                Close();
            }
        }

        public void Close(bool softClose = true)
        {
            if (serverTCPListener != null)
            {
                serverTCPListener.Stop();
            }

            this.isNormalExit = true;
            if (threadSocket != null && threadSocket.IsAlive)
            {
                threadSocket.Abort();
            }

            if (heartBeatTimer != null && !softClose)
            {
                heartBeatTimer.Stop();
            }
        }

        /// <summary>
        /// 清除回话厅消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearBtn_Click(object sender, EventArgs e)
        {
            this.msgHouse.Text = "";
        }

        /// <summary>
        /// 发送消息按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendBtn_Click(object sender, EventArgs e)
        {

            if (!this.sendAllCheckBox.Checked)
            {
                if (this.clientListBox.SelectedItems.Count > 0)
                {
                    string users_str = string.Empty;
                    foreach (ItemKeyValue item in this.clientListBox.SelectedItems)
                    {
                        User to = (User)item.Value;
                        users_str += to.name + ",";
                        SendToClient(CMD_TYPE.MSG, new User(), to, this.msgEdit.Text);
                    }
                    AddMsgHouseNewMsg(new User(), string.Format("TO:[{0}]{1}", users_str.TrimEnd(','), Environment.NewLine) + this.msgEdit.Text);
                    this.msgEdit.Text = "";
                }
                else
                {
                    MessageBox.Show("请选择至少一个【在线用户】或勾选【所有客户端】!");
                }
            }
            else
            {
                SendToAllClient(CMD_TYPE.MSG, this.msgEdit.Text);
                AddMsgHouseNewMsg(new User(), string.Format("TO:[所有在线]{0}", Environment.NewLine) + this.msgEdit.Text);
                this.msgEdit.Text = "";
            }


        }

        /// <summary>
        /// 心跳检测离线
        /// </summary>
        private void HeartBeatHandler(object sender, EventArgs args)
        {
            if (this.userList.Count > 0)
            {
                List<User> removeList = new List<User>();
                foreach (var user in this.userList)
                {
                    string msg = "----";
                    if (!SendToClient(CMD_TYPE.BEAT, new User(), user, msg))
                    {
                        removeList.Add(user);
                    }
                }

                foreach (var remove in removeList)
                {
                    if (this.userList.Contains(remove))
                    {
                        this.userList.Remove(remove);
                        DelClientListBoxItem(remove);
                    }
                }

                UpdateTotalNum(this.userList.Count);
            }
        }


        /// <summary>
        /// socket服务线程
        /// </summary>
        private void SocketThread()
        {
            TcpClient newClient = null;
            while (true)
            {
                try
                {
                    newClient = serverTCPListener.AcceptTcpClient();
                    newClient.SendTimeout = 500;
                }
                catch (Exception e)
                {
                    break;
                }

                //每接收一个客户端连接，就创建一个对应的线程循环接收该客户端发来的信息；
                User user = new User(newClient);

                Thread threadReceive = new Thread(ReceiveDataThread);
                threadReceive.Start(user);
                userList.Add(user);
                AddClientListBoxItem(user);
                UpdateTotalNum(this.userList.Count);
            }
        }

        /// <summary>
        /// 处理接收的客户端信息
        /// </summary>
        /// <param name="userState">客户端信息</param>
        private void ReceiveDataThread(object userState)
        {
            User user = (User)userState;
            TcpClient client = user.client;
            SocketQueryHandler handler = new SocketQueryHandler();
            string query_template = "发起一次查询。\r\n类别:{0}\r\n关键词:{1}\r\n结果:{2}\r\n";
            while (isNormalExit == false)
            {
                string msg = null;
                try
                {
                    Protocol.CMD_TYPE msg_type = CMD_TYPE.NULL;
                    string auth_code = string.Empty;
                    string to_user = string.Empty;
                    string msg_temp = string.Empty;
                    bool isOK = Protocol.Protocol.ParseProtocolMsg(user.br, out msg_type, out auth_code, out to_user, out msg_temp);
                    if (isOK)
                    {
                        msg = msg_temp;
                        //解析用户信息
                        try
                        {
                            var parser = new System.Web.Script.Serialization.JavaScriptSerializer();
                            User user_tmp = parser.Deserialize(auth_code.Replace('\0', ' '), typeof(User)) as User;
                            if (null != user_tmp)
                            {
                                user.name = user_tmp.name;
                                user.pwd = user_tmp.pwd;
                                user.role = user_tmp.role;
                                user.id = user_tmp.id;
                                user.auth = user.auth;

                                RefreshUserList();
                            }
                        }
                        catch (Exception e)
                        {
                            AddMsgHouseNewMsg(new User(), string.Format("用户信息解析异常:{0}", e.Message));
                        }

                    }
                    else
                    {
                        msg_temp = auth_code;
                        //log 无法解析的信息
                    }

                    if (msg_type != CMD_TYPE.NULL)
                    {
                        if (msg_type == CMD_TYPE.EXIT)
                        {
                            AddMsgHouseNewMsg(user, "离线.");
                            user.bw.Close();
                            user.br.Close();
                        }
                        else if (msg_type == CMD_TYPE.QUERY)
                        {
                            string query_type = string.Empty;
                            string query_kw = string.Empty;
                            string query_rlt = handler.Query(msg, out query_type, out query_kw);
                            AddMsgHouseNewMsg(user, string.Format(query_template, query_type, query_kw, query_rlt));
                            //发送查询结果
                            SendToClient(CMD_TYPE.QUERY, new User(), user, query_rlt);
                        }
                        else
                        {
                            AddMsgHouseNewMsg(user, msg);
                        }

                    }

                }
                catch (Exception)
                {
                    break;
                }
            }
        }


        /// <summary>
        /// 发送给所有用户
        /// </summary>
        /// <param name="from_user"></param>
        /// <param name="message"></param>
        private void SendToAllClient(CMD_TYPE cmd_type, string message, User from_user = null)
        {
            if (from_user == null) { from_user = new User(); }
            for (int i = 0; i < userList.Count; i++)
            {
                SendToClient(cmd_type, from_user, userList[i], message);
            }
        }


        /// <summary>
        /// 发送消息给指定的用户
        /// </summary>
        /// <param name="msg_type">消息类型</param>
        /// <param name="from_user">消息发送者</param>
        /// <param name="to_user">接收者</param>
        /// <param name="message">消息内容</param>
        /// <returns></returns>
        private bool SendToClient(CMD_TYPE msg_type, User from_user, User to_user, string message)
        {
            try
            {
                byte[] msg_bytes = Protocol.Protocol.MakeProtocolMsg(msg_type, from_user.MakeAuthCode(), to_user.MakeAuthCode(), message);
                to_user.bw.Write(msg_bytes);
                to_user.bw.Flush();
                return true;
            }
            catch (ObjectDisposedException e)
            {
                return false;
            }
            catch (Exception e)
            {
                AddMsgHouseNewMsg(new User(), string.Format("服务端给{0}发送消息失败", to_user.name));
                return false;
            }
        }

        #region 跨线程操作界面
        /// <summary>
        /// 消息格式
        /// </summary>
        /// <returns></returns>
        private void AppendMsg(RichTextBox richTextBox, User user, object msg_body)
        {
            if (!richTextBox.IsDisposed)
            {
                richTextBox.SelectionColor = user.titleColor;
                if (richTextBox.Text.Length != 0)
                {
                    richTextBox.AppendText(" " + Environment.NewLine);
                }
                richTextBox.SelectionColor = user.titleColor;
                richTextBox.AppendText(string.Format("{0} {1}{2}", user.name, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), Environment.NewLine));
                richTextBox.SelectionColor = user.msgColor;
                richTextBox.AppendText(string.Format("{0}{1}", msg_body.ToString(), Environment.NewLine));
            }
            return;
        }

        /// <summary>
        /// 更新会话大厅消息
        /// </summary>
        /// <param name="str"></param>
        public void AddMsgHouseNewMsg(User user, object msg)
        {
            if (msgHouse.InvokeRequired)
            {
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                Action<object> actionDelegate = (x) =>
                {
                    AppendMsg(this.msgHouse, user, msg);
                };

                if (!this.msgHouse.IsDisposed)
                {
                    this.msgHouse.Invoke(actionDelegate, msg);
                }

            }
            else
            {
                AppendMsg(this.msgHouse, user, msg);
            }
        }

        /// <summary>
        /// 刷新左侧列表
        /// </summary>
        private void RefreshUserList()
        {
            ItemKeyValue newer = null;
            ItemKeyValue older = null;
            for (int i = 0; i < this.clientListBox.Items.Count; i++)
            {
                ItemKeyValue item = (ItemKeyValue)this.clientListBox.Items[i];
                if (item.Name != ((User)item.Value).name)
                {
                    newer = new ItemKeyValue(((User)item.Value).name, (User)item.Value);
                    older = item;
                    break;
                }
            }

            if (newer != null)
            {
                int index = this.clientListBox.Items.IndexOf(older);
                if (this.clientListBox.InvokeRequired)
                {
                    Action<int> update = x =>
                    {
                        this.clientListBox.Items.RemoveAt(index);
                        this.clientListBox.Items.Insert(index, newer);
                    };
                    this.clientListBox.Invoke(update, index);
                }
                else
                {
                    this.clientListBox.Items.RemoveAt(index);
                    this.clientListBox.Items.Insert(index, newer);
                }

            }
        }

        /// <summary>
        /// 新增客户端
        /// </summary>
        /// <param name="conn"></param>
        public void AddClientListBoxItem(User conn)
        {
            if (clientListBox.InvokeRequired)
            {
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                Action<User> actionDelegate = (x) => { this.clientListBox.Items.Add(new ItemKeyValue(x.name.ToString(), x)); };
                // 或者
                // Action<string> actionDelegate = delegate(string txt) { this.label2.Text = txt; };
                this.msgHouse.Invoke(actionDelegate, conn);
            }
            else
            {
                this.clientListBox.Items.Add(new ItemKeyValue(conn.name.ToString(), conn));
            }
        }

        /// <summary>
        /// 删除客户端
        /// </summary>
        /// <param name="toDeleteUser"></param>
        public void DelClientListBoxItem(User toDeleteUser)
        {
            if (clientListBox.InvokeRequired)
            {
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                Action<User> actionDelegate = (x) =>
                {
                    ItemKeyValue tmp = null;
                    foreach (ItemKeyValue item in this.clientListBox.Items)
                    {
                        if (((User)item.Value).id == x.id)
                        {
                            tmp = item;
                            break;
                        }
                    }

                    if (tmp != null)
                    {
                        this.clientListBox.Items.Remove(tmp);
                    }
                };
                // 或者
                // Action<string> actionDelegate = delegate(string txt) { this.label2.Text = txt; };
                this.msgHouse.Invoke(actionDelegate, toDeleteUser);
            }
            else
            {
                ItemKeyValue tmp = null;
                foreach (ItemKeyValue item in this.clientListBox.Items)
                {
                    if (((User)item.Value).id == toDeleteUser.id)
                    {
                        tmp = item;
                        break;
                    }
                }

                if (tmp != null)
                {
                    this.clientListBox.Items.Remove(tmp);
                }
            }
        }

        /// <summary>
        /// 更新在线人数
        /// </summary>
        /// <param name="count"></param>
        public void UpdateTotalNum(int count)
        {
            string str = count + "个用户在线";
            if (totalLabel.InvokeRequired)
            {
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                Action<string> actionDelegate = (x) => { this.totalLabel.Text = str; };
                // 或者
                // Action<string> actionDelegate = delegate(string txt) { this.label2.Text = txt; };
                this.totalLabel.Invoke(actionDelegate, str);
            }
            else
            {
                this.totalLabel.Text = str;
            }
        }


        #endregion

        //编辑监控项【扩展】
        private void editMonitor_Click(object sender, EventArgs e)
        {

        }

    }

    /// <summary>
    /// ListBox Item
    /// </summary>
    public class ItemKeyValue
    {
        public string Name { set; get; }
        public object Value { get; set; }

        public ItemKeyValue(string name, object val)
        {
            this.Name = name;
            this.Value = val;
        }
    }
}
