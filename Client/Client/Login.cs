using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Client
{
    public partial class Login : Form
    {
        private TcpClient tc;
        //声明网络流
        private NetworkStream ns;
        private BinaryReader br;
        private BinaryWriter bw;
        private String IP;
        private String port;
        public static List<SignUp> SignFormList = new List<SignUp>();
        public Login()
        {
            InitializeComponent();
            IP= "127.0.0.1";
            //对应服务器 t1 = new TcpListener(9999);
            port= "9999";
            Text = "登录";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string account = tbUsername.Text.Trim();
            string password = tbPassword.Text.Trim();

            StringBuilder sb = new StringBuilder();
            sb.Append("login#");
            sb.Append(account + "#" + password);

            try
            {
                lbShowMsg.Text = "正在连接到主机";
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
                    lbShowMsg.Text = "服务器无响应";
                }
                if (info == null)
                {
                    lbShowMsg.Text = "登陆失败";
                }
                else
                {
                    string[] splitString = info.Split('#');
                    switch (splitString[0])
                    {
                        case "login fail":
                            lbShowMsg.Text = "用户名或密码错误";
                            break;
                        case "already login":
                            lbShowMsg.Text = "请不要重复登陆";
                            break;
                        case "user":
                            //格式：user#UID#UserName
                            //MessageBox.Show("登陆成功");
                            lbShowMsg.Text = "登陆成功";
                            ListForm main = new ListForm();
                            main.UID = splitString[1];
                            main.Username = splitString[2];
                            main.sign = splitString[3];
                            //main.Users = info;
                            main.ServerIP = IP;
                            main.ServerPort = port;
                            main.Br = br;
                            main.Bw = bw;
                            Hide();
                            main.Show();
                            break;
                        default:
                            MessageBox.Show(info);
                            break;
                    }
                }
            }
            catch
            {
                lbShowMsg.Text = "无法连接到主机";
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SignUp sign = new SignUp();
            sign.ShowDialog();
        }
    }
}
