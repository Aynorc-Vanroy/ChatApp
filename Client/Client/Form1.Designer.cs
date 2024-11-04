namespace Client
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSend = new System.Windows.Forms.Button();
            this.tbSendMsg = new System.Windows.Forms.TextBox();
            this.rtbShowMsg = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btn_Send
            // 
            this.btnSend.Location = new System.Drawing.Point(197, 219);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // tb_SendMsg
            // 
            this.tbSendMsg.Location = new System.Drawing.Point(91, 219);
            this.tbSendMsg.Name = "tbSendMsg";
            this.tbSendMsg.Size = new System.Drawing.Size(100, 21);
            this.tbSendMsg.TabIndex = 1;
            // 
            // rtb_ShowMsg
            // 
            this.rtbShowMsg.Location = new System.Drawing.Point(91, 12);
            this.rtbShowMsg.Name = "rtbShowMsg";
            this.rtbShowMsg.Size = new System.Drawing.Size(181, 201);
            this.rtbShowMsg.TabIndex = 2;
            this.rtbShowMsg.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.rtbShowMsg);
            this.Controls.Add(this.tbSendMsg);
            this.Controls.Add(this.btnSend);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbSendMsg;
        private System.Windows.Forms.RichTextBox rtbShowMsg;
    }
}

