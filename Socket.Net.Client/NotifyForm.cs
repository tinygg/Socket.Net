using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Socket.Net.Protocol;

namespace Socket.Net.Client
{
    public partial class NotifyForm : Form
    {
        private object host;

        private int currentX;//横坐标      
        private int currentY;//纵坐标      
        private int screenHeight;//屏幕高度      
        private int screenWidth;//屏幕宽度 


        const int AW_ACTIVE = 0x20000; //激活窗口，在使用了AW_HIDE标志后不要使用这个标志   
        const int AW_HIDE = 0x10000;//隐藏窗口   
        const int AW_BLEND = 0x80000;// 使用淡入淡出效果   
        const int AW_SLIDE = 0x40000;//使用滑动类型动画效果，默认为滚动动画类型，当使用AW_CENTER标志时，这个标志就被忽略   
        const int AW_CENTER = 0x0010;//若使用了AW_HIDE标志，则使窗口向内重叠；否则向外扩展   
        const int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志   
        const int AW_HOR_NEGATIVE = 0x0002;//自右向左显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志   
        const int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志   
        const int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志 

        [DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dateTime, int dwFlags);//hwnd窗口句柄.dateTime:动画时长.dwFlags:动画类型组合  

        public NotifyForm()
        {
            InitializeComponent();
        }

        public NotifyForm(string msg, bool auto_Show_Close, object invoker)
        {
            this.host = invoker;
            InitializeComponent();
            this.msgBox.Text = msg;

            if (auto_Show_Close)
            {
                Timer timer = new Timer();
                timer.Interval = 3000; //3秒启动
                timer.Tick += new EventHandler(closeLabel_Click);
                timer.Start();
            }
            this.TopMost = true;
            this.closeLabel.Focus();
            this.ShowDialog();
        }

        public NotifyForm(string msg, bool auto_Show_Close)
        {
            InitializeComponent();
            this.msgBox.Text = msg;

            if (auto_Show_Close)
            {
                Timer timer = new Timer();
                timer.Interval = 2000; //3秒启动
                timer.Tick += new EventHandler(closeLabel_Click);
                timer.Start();
            }
            this.TopMost = true;
            this.closeLabel.Focus();
            this.ShowDialog();
        }

        /// <summary>
        /// 自动关闭
        /// </summary>
        /// <param name="frm"></param>
        public static void AutoClose(NotifyForm frm)
        {
            frm.CloseThisWindow();
        }

        private void CloseThisWindow()
        {
            if (!this.IsDisposed)
            {
                AnimateWindow(this.Handle, 300, AW_HIDE + AW_VER_POSITIVE);
                this.Close();
            }
        }

        private void NotifyForm_Load(object sender, EventArgs e)
        {
            this.userListBox.Visible = false;

            Rectangle rect = Screen.PrimaryScreen.WorkingArea;
            screenHeight = rect.Height;
            screenWidth = rect.Width;
            currentX = screenWidth - this.Width;
            currentY = screenHeight - this.Height;
            this.Location = new System.Drawing.Point(currentX, currentY);

            AnimateWindow(this.Handle, 300, AW_SLIDE | AW_VER_NEGATIVE);
        }

        private void closeLabel_Click(object sender, EventArgs e)
        {
            CloseThisWindow();
        }

        private void NotifyForm_Activated(object sender, EventArgs e)
        {
            this.closeLabel.Focus();
        }

        private void replyLabel_MouseHover(object sender, EventArgs e)
        {
            if (changeCount > 0 && enableSend)
            {

            }
            else
            {
                this.userListBox.Visible = true;
            }
        }

        private void userListBox_MouseLeave(object sender, EventArgs e)
        {
            this.userListBox.Visible = false;
        }

        bool enableSend = false;
        private void userListBox_Click(object sender, EventArgs e)
        {
            enableSend = true;
            //open dialog
            //send msg to selected user
            this.msgBox.Text = "在这里回复...";
        }

        private void replyLabel_Click(object sender, EventArgs e)
        {
            if (enableSend)
            {
                if (host != null && host.GetType() == typeof(NotifyClient))
                {
                    NotifyClient invoker = (NotifyClient)host;
                    invoker.SendMessage(CMD_TYPE.MSG, "ALL", this.msgBox.Text);
                    this.CloseThisWindow();
                }
            }
        }

        int changeCount = 0;
        private void msgBox_TextChanged(object sender, EventArgs e)
        {
            changeCount++;
        }


    }
}
