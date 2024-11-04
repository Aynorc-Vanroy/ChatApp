namespace Client
{
    partial class FrdGroupCg
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
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSend = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbState = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cb_group
            // 
            this.cbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroup.FormattingEnabled = true;
            this.cbGroup.Location = new System.Drawing.Point(95, 34);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(107, 20);
            this.cbGroup.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "修改分组至：";
            // 
            // cb_send
            // 
            this.cbSend.Location = new System.Drawing.Point(33, 74);
            this.cbSend.Name = "cbSend";
            this.cbSend.Size = new System.Drawing.Size(75, 23);
            this.cbSend.TabIndex = 2;
            this.cbSend.Text = "提交";
            this.cbSend.UseVisualStyleBackColor = true;
            this.cbSend.Click += new System.EventHandler(this.cbSend_Click);
            // 
            // bt_cancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(114, 74);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lb_state
            // 
            this.lbState.AutoSize = true;
            this.lbState.Location = new System.Drawing.Point(95, 16);
            this.lbState.Name = "lbState";
            this.lbState.Size = new System.Drawing.Size(0, 12);
            this.lbState.TabIndex = 4;
            // 
            // frdgroupcg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 121);
            this.Controls.Add(this.lbState);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbSend);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbGroup);
            this.Name = "FrdGroupCg";
            this.Text = "frdgroupcg";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrdGroupCg_FormClosed);
            this.Load += new System.EventHandler(this.FrdGroupCg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cbSend;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbState;
    }
}