using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class SignUp : Form
    {
        private TcpClient tc;
        //声明网络流
        private NetworkStream ns;
        private BinaryReader br;
        private BinaryWriter bw;
        private String IP;
        private String port;
        public SignUp()
        {
            InitializeComponent();
            IP = "127.0.0.1";
            port = "9999";
            this.Text = "注册";
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (tbAccount.Text == "")
            {
                lbStatus.Text = "账号不能为空";
                return;
            }
            if (tbPasswd.Text == "")
            {
                lbStatus.Text = "密码不能为空";
                return;
            }
            if (tbPasswd.Text != tbPasswdrpt.Text)
            {
                lbStatus.Text = "两次密码不一致";
                return;
            }
            if (tbUsername.Text == "")
            {
                lbStatus.Text = "昵称不能为空";
                return;
            }
            string account = tbAccount.Text.Trim();
            string username = tbUsername.Text.Trim();
            string password = tbPasswd.Text.Trim();

            StringBuilder sb = new StringBuilder();
            sb.Append("signup#");
            sb.Append(account + "#" + password + "#" + username);

            try
            {
                lbStatus.Text = "正在连接到主机";
                tc = new TcpClient(IP, int.Parse(port));
                //实例化网络流对象
                ns = tc.GetStream();
                br = new BinaryReader(ns);
                bw = new BinaryWriter(ns);
                bw.Write(sb.ToString());
                bw.Flush();
                string info = null;
                try
                {
                    info = br.ReadString();
                }
                catch (Exception)
                {
                    lbStatus.Text = "服务器无响应";
                }
                if (info == null)
                {
                    lbStatus.Text = "注册失败";
                }
                else
                {
                    lbStatus.Text = info;

                }
            }
            catch
            {
                lbStatus.Text = "无法连接到主机";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
