namespace Client
{
    partial class Friendreq
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
            this.lvFrdReq = new System.Windows.Forms.ListView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lvFrdPutReq = new System.Windows.Forms.ListView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lvMuReq = new System.Windows.Forms.ListView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lvPutMuReq = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CancelMuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv_frdreq
            // 
            this.lvFrdReq.Location = new System.Drawing.Point(4, 3);
            this.lvFrdReq.Name = "lvFrdReq";
            this.lvFrdReq.Size = new System.Drawing.Size(229, 322);
            this.lvFrdReq.TabIndex = 0;
            this.lvFrdReq.UseCompatibleStateImageBehavior = false;
            this.lvFrdReq.View = System.Windows.Forms.View.Details;
            this.lvFrdReq.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvFrdReq_MouseDoubleClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(246, 375);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lvFrdReq);
            this.tabPage1.Location = new System.Drawing.Point(4, 40);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(238, 331);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "收到的好友请求";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lvFrdPutReq);
            this.tabPage2.Location = new System.Drawing.Point(4, 40);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(238, 331);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "发出的好友请求";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lv_frdputreq
            // 
            this.lvFrdPutReq.Location = new System.Drawing.Point(0, 0);
            this.lvFrdPutReq.Name = "lvFrdPutReq";
            this.lvFrdPutReq.Size = new System.Drawing.Size(235, 328);
            this.lvFrdPutReq.TabIndex = 0;
            this.lvFrdPutReq.UseCompatibleStateImageBehavior = false;
            this.lvFrdPutReq.View = System.Windows.Forms.View.Details;
            this.lvFrdPutReq.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvFrdPutReq_MouseClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lvMuReq);
            this.tabPage3.Location = new System.Drawing.Point(4, 40);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(238, 331);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "入群申请";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lv_mureq
            // 
            this.lvMuReq.Location = new System.Drawing.Point(4, 4);
            this.lvMuReq.Name = "lvMuReq";
            this.lvMuReq.Size = new System.Drawing.Size(231, 324);
            this.lvMuReq.TabIndex = 0;
            this.lvMuReq.UseCompatibleStateImageBehavior = false;
            this.lvMuReq.View = System.Windows.Forms.View.Details;
            this.lvMuReq.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvMuReq_MouseDoubleClick);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lvPutMuReq);
            this.tabPage4.Location = new System.Drawing.Point(4, 40);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(238, 331);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "发出的入群申请";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lv_putmureq
            // 
            this.lvPutMuReq.Location = new System.Drawing.Point(4, 4);
            this.lvPutMuReq.Name = "lv_putmureq";
            this.lvPutMuReq.Size = new System.Drawing.Size(231, 324);
            this.lvPutMuReq.TabIndex = 0;
            this.lvPutMuReq.UseCompatibleStateImageBehavior = false;
            this.lvPutMuReq.View = System.Windows.Forms.View.Details;
            this.lvPutMuReq.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvPutMuReq_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CancelToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            // 
            // CancelToolStripMenuItem
            // 
            this.CancelToolStripMenuItem.Name = "CancelToolStripMenuItem";
            this.CancelToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.CancelToolStripMenuItem.Text = "取消请求";
            this.CancelToolStripMenuItem.Click += new System.EventHandler(this.CancelToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CancelMuToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(125, 26);
            // 
            // CancelmuToolStripMenuItem
            // 
            this.CancelMuToolStripMenuItem.Name = "CancelMuToolStripMenuItem";
            this.CancelMuToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.CancelMuToolStripMenuItem.Text = "取消请求";
            this.CancelMuToolStripMenuItem.Click += new System.EventHandler(this.CancelMuToolStripMenuItem_Click);
            // 
            // Friendreq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 408);
            this.Controls.Add(this.tabControl1);
            this.Name = "Friendreq";
            this.Text = "Friendreq";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FriendReq_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FriendReq_FormClosed);
            this.Load += new System.EventHandler(this.Friendreq_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvFrdReq;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView lvFrdPutReq;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CancelToolStripMenuItem;
        private System.Windows.Forms.ListView lvMuReq;
        private System.Windows.Forms.ListView lvPutMuReq;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem CancelMuToolStripMenuItem;
    }
}