namespace Client
{
    partial class ListForm
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
            this.lvFriends = new System.Windows.Forms.ListView();
            this.btnFrdReq = new System.Windows.Forms.Button();
            this.btnAddFrd = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lvGroup = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.lb1 = new System.Windows.Forms.Label();
            this.lb2 = new System.Windows.Forms.Label();
            this.lbUID = new System.Windows.Forms.Label();
            this.lbUserName = new System.Windows.Forms.Label();
            this.cmFrd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.frdDelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frdGrpCgToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frdstateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmGrp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.crtMuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cgMuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delMuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.muStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.cmFrd.SuspendLayout();
            this.cmGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv_friends
            // 
            this.lvFriends.HideSelection = false;
            this.lvFriends.Location = new System.Drawing.Point(0, 0);
            this.lvFriends.Name = "lvFriends";
            this.lvFriends.Size = new System.Drawing.Size(231, 520);
            this.lvFriends.TabIndex = 0;
            this.lvFriends.UseCompatibleStateImageBehavior = false;
            this.lvFriends.View = System.Windows.Forms.View.Details;
            this.lvFriends.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvFriends_MouseClick);
            this.lvFriends.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvFriends_MouseDoubleClick);
            // 
            // frdreq_btn
            // 
            this.btnFrdReq.Location = new System.Drawing.Point(155, 606);
            this.btnFrdReq.Name = "btnFrdReq";
            this.btnFrdReq.Size = new System.Drawing.Size(75, 23);
            this.btnFrdReq.TabIndex = 1;
            this.btnFrdReq.Text = "好友请求";
            this.btnFrdReq.UseVisualStyleBackColor = true;
            this.btnFrdReq.Click += new System.EventHandler(this.btnFrdReq_Click);
            // 
            // addfrd_btn
            // 
            this.btnAddFrd.Location = new System.Drawing.Point(155, 635);
            this.btnAddFrd.Name = "btnAddFrd";
            this.btnAddFrd.Size = new System.Drawing.Size(75, 23);
            this.btnAddFrd.TabIndex = 2;
            this.btnAddFrd.Text = "添加好友";
            this.btnAddFrd.UseVisualStyleBackColor = true;
            this.btnAddFrd.Click += new System.EventHandler(this.btnAddFrd__Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 51);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(222, 549);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lvFriends);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(214, 523);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "好友";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lvGroup);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(214, 523);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "群";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lv_group
            // 
            this.lvGroup.HideSelection = false;
            this.lvGroup.Location = new System.Drawing.Point(0, 0);
            this.lvGroup.Name = "lvGroup";
            this.lvGroup.Size = new System.Drawing.Size(214, 520);
            this.lvGroup.TabIndex = 0;
            this.lvGroup.UseCompatibleStateImageBehavior = false;
            this.lvGroup.View = System.Windows.Forms.View.Details;
            this.lvGroup.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvGroup_MouseClick);
            this.lvGroup.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvGroup_MouseDoubleClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(155, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "个人资料";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Location = new System.Drawing.Point(16, 13);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(35, 12);
            this.lb1.TabIndex = 5;
            this.lb1.Text = "UID：";
            // 
            // lb2
            // 
            this.lb2.AutoSize = true;
            this.lb2.Location = new System.Drawing.Point(16, 33);
            this.lb2.Name = "lb2";
            this.lb2.Size = new System.Drawing.Size(41, 12);
            this.lb2.TabIndex = 6;
            this.lb2.Text = "昵称：";
            // 
            // lb_UID
            // 
            this.lbUID.AutoSize = true;
            this.lbUID.Location = new System.Drawing.Point(64, 13);
            this.lbUID.Name = "lbUID";
            this.lbUID.Size = new System.Drawing.Size(41, 12);
            this.lbUID.TabIndex = 7;
            this.lbUID.Text = "label1";
            // 
            // lb_Username
            // 
            this.lbUserName.AutoSize = true;
            this.lbUserName.Location = new System.Drawing.Point(64, 33);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(41, 12);
            this.lbUserName.TabIndex = 8;
            this.lbUserName.Text = "label2";
            // 
            // cm_frd
            // 
            this.cmFrd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frdDelToolStripMenuItem,
            this.frdGrpCgToolStripMenuItem,
            this.frdstateToolStripMenuItem});
            this.cmFrd.Name = "cmFrd";
            this.cmFrd.Size = new System.Drawing.Size(125, 70);
            // 
            // frddelToolStripMenuItem
            // 
            this.frdDelToolStripMenuItem.Name = "frdDelToolStripMenuItem";
            this.frdDelToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.frdDelToolStripMenuItem.Text = "删除好友";
            this.frdDelToolStripMenuItem.Click += new System.EventHandler(this.FrdDelToolStripMenuItem_Click);
            // 
            // frdgrpcgToolStripMenuItem
            // 
            this.frdGrpCgToolStripMenuItem.Name = "frdGrpCgToolStripMenuItem";
            this.frdGrpCgToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.frdGrpCgToolStripMenuItem.Text = "修改分组";
            this.frdGrpCgToolStripMenuItem.Click += new System.EventHandler(this.FrdGrpCgToolStripMenuItem_Click);
            // 
            // frdstateToolStripMenuItem
            // 
            this.frdstateToolStripMenuItem.Name = "frdstateToolStripMenuItem";
            this.frdstateToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.frdstateToolStripMenuItem.Text = "查看资料";
            this.frdstateToolStripMenuItem.Click += new System.EventHandler(this.FrdStateToolStripMenuItem_Click);
            // 
            // cm_grp
            // 
            this.cmGrp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.crtMuToolStripMenuItem,
            this.cgMuToolStripMenuItem,
            this.exitMuToolStripMenuItem,
            this.delMuToolStripMenuItem,
            this.muStateToolStripMenuItem});
            this.cmGrp.Name = "cmGrp";
            this.cmGrp.Size = new System.Drawing.Size(125, 114);
            // 
            // crtmuToolStripMenuItem
            // 
            this.crtMuToolStripMenuItem.Name = "crtMuToolStripMenuItem";
            this.crtMuToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.crtMuToolStripMenuItem.Text = "创建群组";
            this.crtMuToolStripMenuItem.Click += new System.EventHandler(this.crtMuToolStripMenuItem_Click);
            // 
            // cgmuToolStripMenuItem
            // 
            this.cgMuToolStripMenuItem.Name = "cgMuToolStripMenuItem";
            this.cgMuToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.cgMuToolStripMenuItem.Text = "修改分组";
            this.cgMuToolStripMenuItem.Click += new System.EventHandler(this.cgMuToolStripMenuItem_Click);
            // 
            // exitmuToolStripMenuItem
            // 
            this.exitMuToolStripMenuItem.Name = "exitMuToolStripMenuItem";
            this.exitMuToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.exitMuToolStripMenuItem.Text = "退出群聊";
            this.exitMuToolStripMenuItem.Click += new System.EventHandler(this.exitMuToolStripMenuItem_Click);
            // 
            // delmuToolStripMenuItem
            // 
            this.delMuToolStripMenuItem.Name = "delMuToolStripMenuItem";
            this.delMuToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.delMuToolStripMenuItem.Text = "解散群聊";
            this.delMuToolStripMenuItem.Click += new System.EventHandler(this.delMuToolStripMenuItem_Click);
            // 
            // mustateToolStripMenuItem
            // 
            this.muStateToolStripMenuItem.Name = "muStateToolStripMenuItem";
            this.muStateToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.muStateToolStripMenuItem.Text = "查看资料";
            this.muStateToolStripMenuItem.Click += new System.EventHandler(this.muStateToolStripMenuItem_Click);
            // 
            // list
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 670);
            this.Controls.Add(this.lbUserName);
            this.Controls.Add(this.lbUID);
            this.Controls.Add(this.lb2);
            this.Controls.Add(this.lb1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnAddFrd);
            this.Controls.Add(this.btnFrdReq);
            this.Name = "ListForm";
            this.Text = "list";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.listFormClosed);
            this.Load += new System.EventHandler(this.list_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.cmFrd.ResumeLayout(false);
            this.cmGrp.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvFriends;
        private System.Windows.Forms.Button btnFrdReq;
        private System.Windows.Forms.Button btnAddFrd;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView lvGroup;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.Label lb2;
        private System.Windows.Forms.Label lbUID;
        private System.Windows.Forms.Label lbUserName;
        private System.Windows.Forms.ContextMenuStrip cmFrd;
        private System.Windows.Forms.ToolStripMenuItem frdDelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frdGrpCgToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frdstateToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmGrp;
        private System.Windows.Forms.ToolStripMenuItem crtMuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cgMuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delMuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem muStateToolStripMenuItem;
    }
}