namespace Client
{
    partial class MutualTalk
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
            this.components = new System.ComponentModel.Container();
            this.rtbShowMsg = new System.Windows.Forms.RichTextBox();
            this.tbSendMsg = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lvMembers = new System.Windows.Forms.ListView();
            this.cmMember = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsRm = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAuth = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsRmauth = new System.Windows.Forms.ToolStripMenuItem();
            this.cmMember.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtb_ShowMsg
            // 
            this.rtbShowMsg.Location = new System.Drawing.Point(12, 12);
            this.rtbShowMsg.Name = "rtbShowMsg";
            this.rtbShowMsg.ReadOnly = true;
            this.rtbShowMsg.Size = new System.Drawing.Size(466, 416);
            this.rtbShowMsg.TabIndex = 0;
            this.rtbShowMsg.Text = "";
            // 
            // tb_SendMsg
            // 
            this.tbSendMsg.Location = new System.Drawing.Point(13, 458);
            this.tbSendMsg.Multiline = true;
            this.tbSendMsg.Name = "tbSendMsg";
            this.tbSendMsg.Size = new System.Drawing.Size(465, 95);
            this.tbSendMsg.TabIndex = 1;
            // 
            // btn_send
            // 
            this.btnSend.Location = new System.Drawing.Point(277, 569);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btn_close
            // 
            this.btnClose.Location = new System.Drawing.Point(391, 569);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lv_members
            // 
            this.lvMembers.Location = new System.Drawing.Point(498, 12);
            this.lvMembers.Name = "lvMembers";
            this.lvMembers.Size = new System.Drawing.Size(134, 541);
            this.lvMembers.TabIndex = 4;
            this.lvMembers.UseCompatibleStateImageBehavior = false;
            this.lvMembers.View = System.Windows.Forms.View.Details;
            this.lvMembers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvMembers_MouseClick);
            // 
            // cm_member
            // 
            this.cmMember.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRm,
            this.tsAuth,
            this.tsCg,
            this.tsRmauth});
            this.cmMember.Name = "cmMember";
            this.cmMember.Size = new System.Drawing.Size(153, 114);
            // 
            // ts_rm
            // 
            this.tsRm.Name = "tsRm";
            this.tsRm.Size = new System.Drawing.Size(152, 22);
            this.tsRm.Text = "移出群聊";
            this.tsRm.Click += new System.EventHandler(this.tsRm_Click);
            // 
            // ts_auth
            // 
            this.tsAuth.Name = "tsAuth";
            this.tsAuth.Size = new System.Drawing.Size(152, 22);
            this.tsAuth.Text = "设置为管理员";
            this.tsAuth.Click += new System.EventHandler(this.tsAuth_Click);
            // 
            // ts_cg
            // 
            this.tsCg.Name = "tsCg";
            this.tsCg.Size = new System.Drawing.Size(152, 22);
            this.tsCg.Text = "转让群主";
            this.tsCg.Click += new System.EventHandler(this.tsCg_Click);
            // 
            // ts_rmauth
            // 
            this.tsRmauth.Name = "tsRmauth";
            this.tsRmauth.Size = new System.Drawing.Size(152, 22);
            this.tsRmauth.Text = "取消管理员";
            this.tsRmauth.Click += new System.EventHandler(this.tsRmAuth_Click);
            // 
            // mutualTalk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 604);
            this.Controls.Add(this.lvMembers);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbSendMsg);
            this.Controls.Add(this.rtbShowMsg);
            this.Name = "MutualTalk";
            this.Text = "mutualTalk";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mutualTalk_FormClosed);
            this.Load += new System.EventHandler(this.MutualTalk_Load);
            this.cmMember.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbShowMsg;
        private System.Windows.Forms.TextBox tbSendMsg;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListView lvMembers;
        private System.Windows.Forms.ContextMenuStrip cmMember;
        private System.Windows.Forms.ToolStripMenuItem tsRm;
        private System.Windows.Forms.ToolStripMenuItem tsAuth;
        private System.Windows.Forms.ToolStripMenuItem tsCg;
        private System.Windows.Forms.ToolStripMenuItem tsRmauth;
    }
}