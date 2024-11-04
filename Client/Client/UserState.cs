using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class UserState : Form
    {
        public string UID { set; get; }
        public BinaryWriter Bw { get; set; }
        public string username { get; set; }
        public string sign { get; set; }
        public bool isself { get; set; }
        public UserState()
        {
            InitializeComponent();
        }

        private void UserState_FormClosed(object sender, FormClosedEventArgs e)
        {
            ListForm.RemoveUserState(UID);
        }

        private void UserState_Load(object sender, EventArgs e)
        {
            lb_UID.Text = UID;
            tbSign.Text = sign;
            tbUsername.Text = username;
            if (isself != true)
            {
                tbSign.ReadOnly = true;
                tbUsername.ReadOnly = true;
                btnChange.Enabled = false;
            }

        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            Bw.Write("userstate#" + tbUsername.Text + "#" + tbSign.Text);
        }
    }
}
