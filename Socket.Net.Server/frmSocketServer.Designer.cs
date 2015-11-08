namespace Socket.Net.Server
{
    partial class frmSocketServer
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSocketServer));
            this.socketServer1 = new Socket.Net.Server.SocketServer();
            this.SuspendLayout();
            // 
            // socketServer1
            // 
            this.socketServer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.socketServer1.Location = new System.Drawing.Point(1, 1);
            this.socketServer1.Name = "socketServer1";
            this.socketServer1.Size = new System.Drawing.Size(674, 467);
            this.socketServer1.TabIndex = 0;
            // 
            // frmSocketServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 469);
            this.Controls.Add(this.socketServer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSocketServer";
            this.Text = "服务端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private SocketServer socketServer1;
    }
}

