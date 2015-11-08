namespace Socket.Net.Server
{
    partial class SocketServer
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.clientListBox = new System.Windows.Forms.ListBox();
            this.msgEdit = new System.Windows.Forms.RichTextBox();
            this.msgHouse = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.totalLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sendAllCheckBox = new System.Windows.Forms.CheckBox();
            this.clearBtn = new System.Windows.Forms.Button();
            this.startBtn = new System.Windows.Forms.Button();
            this.bufferCheckBox = new System.Windows.Forms.CheckBox();
            this.autoSendCheckBox = new System.Windows.Forms.CheckBox();
            this.editMonitor = new System.Windows.Forms.Button();
            this.sendBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // clientListBox
            // 
            this.clientListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clientListBox.FormattingEnabled = true;
            this.clientListBox.ItemHeight = 12;
            this.clientListBox.Location = new System.Drawing.Point(3, 28);
            this.clientListBox.Name = "clientListBox";
            this.clientListBox.Size = new System.Drawing.Size(132, 304);
            this.clientListBox.TabIndex = 0;
            // 
            // msgEdit
            // 
            this.msgEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.msgEdit.Location = new System.Drawing.Point(3, 189);
            this.msgEdit.Name = "msgEdit";
            this.msgEdit.Size = new System.Drawing.Size(268, 62);
            this.msgEdit.TabIndex = 1;
            this.msgEdit.Text = "";
            // 
            // msgHouse
            // 
            this.msgHouse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.msgHouse.HideSelection = false;
            this.msgHouse.Location = new System.Drawing.Point(3, 28);
            this.msgHouse.Name = "msgHouse";
            this.msgHouse.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedHorizontal;
            this.msgHouse.Size = new System.Drawing.Size(268, 135);
            this.msgHouse.TabIndex = 2;
            this.msgHouse.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.totalLabel);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.clientListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.sendAllCheckBox);
            this.splitContainer1.Panel2.Controls.Add(this.clearBtn);
            this.splitContainer1.Panel2.Controls.Add(this.startBtn);
            this.splitContainer1.Panel2.Controls.Add(this.bufferCheckBox);
            this.splitContainer1.Panel2.Controls.Add(this.autoSendCheckBox);
            this.splitContainer1.Panel2.Controls.Add(this.editMonitor);
            this.splitContainer1.Panel2.Controls.Add(this.sendBtn);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.msgHouse);
            this.splitContainer1.Panel2.Controls.Add(this.msgEdit);
            this.splitContainer1.Size = new System.Drawing.Size(416, 333);
            this.splitContainer1.SplitterDistance = 138;
            this.splitContainer1.TabIndex = 3;
            // 
            // totalLabel
            // 
            this.totalLabel.AutoSize = true;
            this.totalLabel.ForeColor = System.Drawing.Color.Blue;
            this.totalLabel.Location = new System.Drawing.Point(77, 10);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(11, 12);
            this.totalLabel.TabIndex = 2;
            this.totalLabel.Text = "N";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "客户端列表";
            // 
            // sendAllCheckBox
            // 
            this.sendAllCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendAllCheckBox.AutoSize = true;
            this.sendAllCheckBox.Checked = true;
            this.sendAllCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sendAllCheckBox.Location = new System.Drawing.Point(105, 261);
            this.sendAllCheckBox.Name = "sendAllCheckBox";
            this.sendAllCheckBox.Size = new System.Drawing.Size(84, 16);
            this.sendAllCheckBox.TabIndex = 10;
            this.sendAllCheckBox.Text = "所有客户端";
            this.sendAllCheckBox.UseVisualStyleBackColor = true;
            // 
            // clearBtn
            // 
            this.clearBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearBtn.Location = new System.Drawing.Point(214, 164);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(54, 23);
            this.clearBtn.TabIndex = 9;
            this.clearBtn.Text = "清除";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // startBtn
            // 
            this.startBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.startBtn.BackColor = System.Drawing.Color.Green;
            this.startBtn.Location = new System.Drawing.Point(214, 285);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(54, 38);
            this.startBtn.TabIndex = 8;
            this.startBtn.Text = "启动";
            this.startBtn.UseVisualStyleBackColor = false;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // bufferCheckBox
            // 
            this.bufferCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bufferCheckBox.AutoSize = true;
            this.bufferCheckBox.Location = new System.Drawing.Point(88, 285);
            this.bufferCheckBox.Name = "bufferCheckBox";
            this.bufferCheckBox.Size = new System.Drawing.Size(72, 16);
            this.bufferCheckBox.TabIndex = 7;
            this.bufferCheckBox.Text = "消息缓存";
            this.bufferCheckBox.UseVisualStyleBackColor = true;
            // 
            // autoSendCheckBox
            // 
            this.autoSendCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.autoSendCheckBox.AutoSize = true;
            this.autoSendCheckBox.Location = new System.Drawing.Point(88, 307);
            this.autoSendCheckBox.Name = "autoSendCheckBox";
            this.autoSendCheckBox.Size = new System.Drawing.Size(120, 16);
            this.autoSendCheckBox.TabIndex = 6;
            this.autoSendCheckBox.Text = "符合条件自动发送";
            this.autoSendCheckBox.UseVisualStyleBackColor = true;
            // 
            // editMonitor
            // 
            this.editMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editMonitor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.editMonitor.Location = new System.Drawing.Point(7, 285);
            this.editMonitor.Name = "editMonitor";
            this.editMonitor.Size = new System.Drawing.Size(75, 38);
            this.editMonitor.TabIndex = 5;
            this.editMonitor.Text = "编辑监控项";
            this.editMonitor.UseVisualStyleBackColor = false;
            this.editMonitor.Click += new System.EventHandler(this.editMonitor_Click);
            // 
            // sendBtn
            // 
            this.sendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendBtn.BackColor = System.Drawing.Color.Goldenrod;
            this.sendBtn.Location = new System.Drawing.Point(214, 257);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(54, 20);
            this.sendBtn.TabIndex = 4;
            this.sendBtn.Text = "发送";
            this.sendBtn.UseVisualStyleBackColor = false;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "手动发送";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "消息记录";
            // 
            // SocketServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.splitContainer1);
            this.Name = "SocketServer";
            this.Size = new System.Drawing.Size(416, 333);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox clientListBox;
        private System.Windows.Forms.RichTextBox msgEdit;
        private System.Windows.Forms.RichTextBox msgHouse;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Button editMonitor;
        private System.Windows.Forms.CheckBox autoSendCheckBox;
        private System.Windows.Forms.CheckBox bufferCheckBox;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Label totalLabel;
        private System.Windows.Forms.CheckBox sendAllCheckBox;
    }
}
