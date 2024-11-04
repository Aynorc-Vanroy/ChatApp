namespace Client
{
    partial class MuState
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbGrpName = new System.Windows.Forms.TextBox();
            this.lbGID = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbGrpSign = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "GID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "群名称:";
            // 
            // tb_grpname
            // 
            this.tbGrpName.Location = new System.Drawing.Point(91, 61);
            this.tbGrpName.Name = "tbGrpName";
            this.tbGrpName.Size = new System.Drawing.Size(132, 21);
            this.tbGrpName.TabIndex = 4;
            // 
            // lb_GID
            // 
            this.lbGID.AutoSize = true;
            this.lbGID.Location = new System.Drawing.Point(89, 34);
            this.lbGID.Name = "lbGID";
            this.lbGID.Size = new System.Drawing.Size(0, 12);
            this.lbGID.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "群简介:";
            // 
            // tb_grpsign
            // 
            this.tbGrpSign.Location = new System.Drawing.Point(51, 121);
            this.tbGrpSign.Multiline = true;
            this.tbGrpSign.Name = "tbGrpSign";
            this.tbGrpSign.Size = new System.Drawing.Size(172, 77);
            this.tbGrpSign.TabIndex = 8;
            // 
            // bt_send
            // 
            this.btnSend.Location = new System.Drawing.Point(148, 211);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 9;
            this.btnSend.Text = "修改";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // mustate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 255);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbGrpSign);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbGID);
            this.Controls.Add(this.tbGrpName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "mustate";
            this.Text = "mustate";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MuState_FormClosed);
            this.Load += new System.EventHandler(this.MuState_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbGrpName;
        private System.Windows.Forms.Label lbGID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbGrpSign;
        private System.Windows.Forms.Button btnSend;
    }
}