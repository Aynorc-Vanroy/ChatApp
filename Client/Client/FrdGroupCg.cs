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
    public partial class FrdGroupCg : Form
    {
        public string UID { get; set; }
        public bool IsWork { get; set; }
        public BinaryWriter Bw { get; set; }
        public FrdGroupCg()
        {
            InitializeComponent();
        }

        private void FrdGroupCg_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < ListForm.GroupList.Count; i++)
            {
                cbGroup.Items.Add(ListForm.GroupList[i]);
            }
        }

        private void FrdGroupCg_FormClosed(object sender, FormClosedEventArgs e)
        {
            ListForm.RemoveFrdGrpCg();
        }

        private void cbSend_Click(object sender, EventArgs e)
        {
            if (cbGroup.Text == "")
            {
                lbState.Text = "分组不能为空";
                return;
            }
            String sndmsg = "cgfrdgroup#";
            sndmsg += UID;
            sndmsg += "#";
            sndmsg += cbGroup.Text;
            Bw.Write(sndmsg);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
