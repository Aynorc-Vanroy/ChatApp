﻿namespace Client
{
    partial class Talking
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
            this.rtb_ShowMsg = new System.Windows.Forms.RichTextBox();
            this.tbSendMsg = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.sendPIC = new System.Windows.Forms.Button();
            this.sendFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtb_ShowMsg
            // 
            this.rtb_ShowMsg.Location = new System.Drawing.Point(12, 12);
            this.rtb_ShowMsg.Name = "rtb_ShowMsg";
            this.rtb_ShowMsg.ReadOnly = true;
            this.rtb_ShowMsg.Size = new System.Drawing.Size(525, 296);
            this.rtb_ShowMsg.TabIndex = 0;
            this.rtb_ShowMsg.Text = "";
            // 
            // tb_SendMsg
            // 
            this.tbSendMsg.Location = new System.Drawing.Point(12, 327);
            this.tbSendMsg.Multiline = true;
            this.tbSendMsg.Name = "tbSendMsg";
            this.tbSendMsg.Size = new System.Drawing.Size(525, 97);
            this.tbSendMsg.TabIndex = 1;
            // 
            // btn_Send
            // 
            this.btnSend.Location = new System.Drawing.Point(341, 437);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btn_Close
            // 
            this.btnClose.Location = new System.Drawing.Point(448, 437);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // sendPIC
            // 
            this.sendPIC.Location = new System.Drawing.Point(13, 437);
            this.sendPIC.Name = "sendPIC";
            this.sendPIC.Size = new System.Drawing.Size(75, 23);
            this.sendPIC.TabIndex = 4;
            this.sendPIC.Text = "发送图片";
            this.sendPIC.UseVisualStyleBackColor = true;
            this.sendPIC.Click += new System.EventHandler(this.sendPIC_Click);
            // 
            // sendFile
            // 
            this.sendFile.Location = new System.Drawing.Point(94, 437);
            this.sendFile.Name = "sendFile";
            this.sendFile.Size = new System.Drawing.Size(75, 23);
            this.sendFile.TabIndex = 5;
            this.sendFile.Text = "发送文件";
            this.sendFile.UseVisualStyleBackColor = true;
            this.sendFile.Click += new System.EventHandler(this.sendFile_Click);
            // 
            // Talking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 472);
            this.Controls.Add(this.sendFile);
            this.Controls.Add(this.sendPIC);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbSendMsg);
            this.Controls.Add(this.rtb_ShowMsg);
            this.Name = "Talking";
            this.Text = "Talking";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Talking_FormClosed);
            this.Load += new System.EventHandler(this.Talking_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb_ShowMsg;
        private System.Windows.Forms.TextBox tbSendMsg;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button sendPIC;
        private System.Windows.Forms.Button sendFile;
    }
}