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
    public partial class Createmu : Form
    {
        public BinaryWriter Bw { set; get; }
        public Createmu()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (tbMuname.Text == "")
            {
                lbState.Text = "群名不能为空";
                return;
            }
            Bw.Write("createmutual#" + tbMuname.Text + "#" + tbMusign.Text);
            this.Close();
        }
    }
}
