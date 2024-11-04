namespace Client
{
    partial class Createmu
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
            this.tbMuname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbMusign = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbState = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "群名:";
            // 
            // tb_muname
            // 
            this.tbMuname.Location = new System.Drawing.Point(72, 33);
            this.tbMuname.Name = "tbMuname";
            this.tbMuname.Size = new System.Drawing.Size(138, 21);
            this.tbMuname.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "群简介:";
            // 
            // tb_musign
            // 
            this.tbMusign.Location = new System.Drawing.Point(61, 98);
            this.tbMusign.Multiline = true;
            this.tbMusign.Name = "tbMusign";
            this.tbMusign.Size = new System.Drawing.Size(149, 69);
            this.tbMusign.TabIndex = 3;
            // 
            // bt_send
            // 
            this.btnSend.Location = new System.Drawing.Point(45, 200);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "提交";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // bt_cancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(155, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lb_state
            // 
            this.lbState.AutoSize = true;
            this.lbState.Location = new System.Drawing.Point(189, 181);
            this.lbState.Name = "lbState";
            this.lbState.Size = new System.Drawing.Size(0, 12);
            this.lbState.TabIndex = 6;
            // 
            // Createmu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lbState);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbMusign);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbMuname);
            this.Controls.Add(this.label1);
            this.Name = "Createmu";
            this.Text = "Createmu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMuname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbMusign;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbState;
    }
}