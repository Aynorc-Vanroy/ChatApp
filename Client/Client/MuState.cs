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
    public partial class MuState : Form
    {
        public string GID { set; get; }
        public BinaryWriter Bw { get; set; }
        public string groupName { get; set; }
        public string crtname { get; set; }
        public string sign { get; set; }
        public bool iscrt { get; set; }
        public string auth { get; set; }
        public MuState()
        {
            InitializeComponent();
        }

        private void MuState_FormClosed(object sender, FormClosedEventArgs e)
        {
            ListForm.RemoveMuState(GID);
        }

        private void MuState_Load(object sender, EventArgs e)
        {
            lbGID.Text = GID;
            tbGrpName.Text = groupName;
            tbGrpSign.Text = sign;
            if (auth == "2")
            {
                tbGrpSign.ReadOnly = true;
                tbGrpName.ReadOnly = true;
                btnSend.Enabled = false;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Bw.Write("mustate#" + GID + "#" + tbGrpName.Text+"#"+tbGrpSign.Text);
        }
    }
}
