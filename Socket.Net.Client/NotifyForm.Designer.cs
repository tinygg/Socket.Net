namespace Socket.Net.Client
{
    partial class NotifyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.closeLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.msgBox = new System.Windows.Forms.RichTextBox();
            this.replyLabel = new System.Windows.Forms.Label();
            this.userListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // closeLabel
            // 
            this.closeLabel.AutoSize = true;
            this.closeLabel.BackColor = System.Drawing.Color.Green;
            this.closeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.closeLabel.Location = new System.Drawing.Point(88, 108);
            this.closeLabel.Name = "closeLabel";
            this.closeLabel.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.closeLabel.Size = new System.Drawing.Size(43, 16);
            this.closeLabel.TabIndex = 0;
            this.closeLabel.Text = "Close";
            this.closeLabel.Click += new System.EventHandler(this.closeLabel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(91, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "系统消息";
            // 
            // msgBox
            // 
            this.msgBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.msgBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.msgBox.ForeColor = System.Drawing.SystemColors.Info;
            this.msgBox.Location = new System.Drawing.Point(22, 33);
            this.msgBox.Name = "msgBox";
            this.msgBox.Size = new System.Drawing.Size(215, 60);
            this.msgBox.TabIndex = 2;
            this.msgBox.Text = "这里是消息内容...";
            this.msgBox.TextChanged += new System.EventHandler(this.msgBox_TextChanged);
            // 
            // replyLabel
            // 
            this.replyLabel.AutoSize = true;
            this.replyLabel.BackColor = System.Drawing.Color.OrangeRed;
            this.replyLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.replyLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.replyLabel.Location = new System.Drawing.Point(143, 108);
            this.replyLabel.Name = "replyLabel";
            this.replyLabel.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.replyLabel.Size = new System.Drawing.Size(43, 16);
            this.replyLabel.TabIndex = 3;
            this.replyLabel.Text = "Reply";
            this.replyLabel.Click += new System.EventHandler(this.replyLabel_Click);
            this.replyLabel.MouseHover += new System.EventHandler(this.replyLabel_MouseHover);
            // 
            // userListBox
            // 
            this.userListBox.FormattingEnabled = true;
            this.userListBox.ItemHeight = 12;
            this.userListBox.Items.AddRange(new object[] {
            "所有人"});
            this.userListBox.Location = new System.Drawing.Point(186, 28);
            this.userListBox.Margin = new System.Windows.Forms.Padding(0);
            this.userListBox.Name = "userListBox";
            this.userListBox.Size = new System.Drawing.Size(70, 100);
            this.userListBox.TabIndex = 0;
            this.userListBox.Click += new System.EventHandler(this.userListBox_Click);
            this.userListBox.MouseLeave += new System.EventHandler(this.userListBox_MouseLeave);
            // 
            // NotifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(261, 133);
            this.Controls.Add(this.userListBox);
            this.Controls.Add(this.replyLabel);
            this.Controls.Add(this.msgBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.closeLabel);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NotifyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "消息提示";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.NotifyForm_Activated);
            this.Load += new System.EventHandler(this.NotifyForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label closeLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox msgBox;
        private System.Windows.Forms.Label replyLabel;
        private System.Windows.Forms.ListBox userListBox;



    }
}