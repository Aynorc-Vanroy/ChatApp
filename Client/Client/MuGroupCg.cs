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
    public partial class MuGroupCg : Form
    {
        public string GID { get; set; }
        public BinaryWriter Bw { get; set; }
        public bool iswork { set; get; }
        public MuGroupCg()
        {
            InitializeComponent();
        }

        private void mugroupcg_FormClosed(object sender, FormClosedEventArgs e)
        {
            ListForm.RemoveMuGrpCg();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (cbGroup.Text == "")
            {
                lbState.Text = "分组不能为空";
                return;
            }
            String sndmsg = "cgmugroup#";
            sndmsg += GID;
            sndmsg += "#";
            sndmsg += cbGroup.Text;
            Bw.Write(sndmsg);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MuGroupCg_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < ListForm.MuGroupList.Count; i++)
            {
                cbGroup.Items.Add(ListForm.MuGroupList[i]);
            }
        }
    }
}
