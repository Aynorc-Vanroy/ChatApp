namespace Client
{
    partial class MutualReqItem
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
            this.lbUsername = new System.Windows.Forms.Label();
            this.lbMutualname = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbMutualName = new System.Windows.Forms.TextBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnRefuse = new System.Windows.Forms.Button();
            this.btnUser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lb_username
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.Location = new System.Drawing.Point(33, 55);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(53, 12);
            this.lbUsername.TabIndex = 0;
            this.lbUsername.Text = "用户ID：";
            // 
            // lb_mutualname
            // 
            this.lbMutualname.AutoSize = true;
            this.lbMutualname.Location = new System.Drawing.Point(33, 130);
            this.lbMutualname.Name = "lbMutualname";
            this.lbMutualname.Size = new System.Drawing.Size(65, 12);
            this.lbMutualname.TabIndex = 1;
            this.lbMutualname.Text = "群组名称：";
            // 
            // tb_username
            // 
            this.tbUsername.Location = new System.Drawing.Point(106, 52);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.ReadOnly = true;
            this.tbUsername.Size = new System.Drawing.Size(100, 21);
            this.tbUsername.TabIndex = 2;
            // 
            // tb_mutualname
            // 
            this.tbMutualName.Location = new System.Drawing.Point(106, 127);
            this.tbMutualName.Name = "tbMutualName";
            this.tbMutualName.ReadOnly = true;
            this.tbMutualName.Size = new System.Drawing.Size(100, 21);
            this.tbMutualName.TabIndex = 3;
            // 
            // bt_accept
            // 
            this.btnAccept.Location = new System.Drawing.Point(53, 182);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 4;
            this.btnAccept.Text = "同意";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // bt_refuse
            // 
            this.btnRefuse.Location = new System.Drawing.Point(162, 182);
            this.btnRefuse.Name = "btnRefuse";
            this.btnRefuse.Size = new System.Drawing.Size(75, 23);
            this.btnRefuse.TabIndex = 5;
            this.btnRefuse.Text = "拒绝";
            this.btnRefuse.UseVisualStyleBackColor = true;
            this.btnRefuse.Click += new System.EventHandler(this.btRefuse_Click);
            // 
            // bt_user
            // 
            this.btnUser.Location = new System.Drawing.Point(197, 88);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(75, 23);
            this.btnUser.TabIndex = 6;
            this.btnUser.Text = "查看信息";
            this.btnUser.UseVisualStyleBackColor = true;
            // 
            // Mutualreqitem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnUser);
            this.Controls.Add(this.btnRefuse);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.tbMutualName);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.lbMutualname);
            this.Controls.Add(this.lbUsername);
            this.Name = "MutualReqItem";
            this.Text = "Mutualreqitem";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MutualReqItem_FormClosed);
            this.Load += new System.EventHandler(this.MutualReqItem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.Label lbMutualname;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbMutualName;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnRefuse;
        private System.Windows.Forms.Button btnUser;
    }
}