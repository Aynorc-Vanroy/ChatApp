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
    public partial class MutualReqItem : Form
    {
        public string UID { get; set; }
        public string GID { get; set; }
        public string toName { get; set; }
        public string groupName { get; set; }
        public BinaryWriter Bw { get; set; }
        public MutualReqItem()
        {
            InitializeComponent();
        }

        private void MutualReqItem_FormClosed(object sender, FormClosedEventArgs e)
        {
            Friendreq.RemoveMutualReqItem(GID, UID);
        }

        private void MutualReqItem_Load(object sender, EventArgs e)
        {
            tbUsername.Text = toName;
            tbMutualName.Text = groupName;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            String Com = "acceptmureq#";
            Com += GID;
            Com += "#";
            Com += UID;
            Bw.Write(Com);
            this.Close();
        }

        private void btRefuse_Click(object sender, EventArgs e)
        {
            String Com = "refusemureq#";
            Com += GID;         
            Com += "#";
            Com += UID;
            Bw.Write(Com);
            this.Close();
        }
    }
}
