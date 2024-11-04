using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.IO;

namespace Client
{
    public partial class Talking : Form
    {
        public string UID { get; set; }
        public string UserName { get; set; }
        public string ToName { get; set; }
        public BinaryReader Br { get; set; }
        public BinaryWriter Bw { get; set; }
        
        bool iswork = false;
        public Talking()
        {
            InitializeComponent();
        }
        string getMessage = string.Empty;
        public Talking(string message)
        {
            getMessage = message;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        private void Talking_Load(object sender, EventArgs e)
        {
            this.Text = "和" + ToName + "聊天中";
            StartListen();
            //TcpListen();

        }
        private Thread th=null;
        private void StartListen()
        {
            iswork = true;
            //tcp消息监听
            th = new Thread(GetNoRead);
            th.IsBackground = true;
            th.Start();
        }
        
        private void GetNoRead()
        {
            int locknum=0;
            for (int i = 0; i < ListForm.LockList.Count; i++)
            {
                if (UID == ListForm.LockList[i].UID)
                {
                    locknum = i;
                }
            }
            while (true)
            {
                lock(ListForm.LockList[locknum].newMsgLock)
                {
                    while (ListForm.LockList[locknum].newMsg == 0) { Monitor.Wait(ListForm.LockList[locknum].newMsgLock); }
                    for (int i = 0; i < ListForm.ReadList.Count; i++)
                    {
                        if (UID == ListForm.ReadList[i].UID)
                        {
                            AddMessage(ListForm.ReadList[i].words, true, ListForm.ReadList[i].time);
                            ListForm.ReadList.Remove(ListForm.ReadList[i]);
                            ListForm.LockList[locknum].newMsg--;
                        }
                    }
                    Readed();
                    Monitor.Pulse(ListForm.LockList[locknum].newMsgLock);
                }       
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            string temp = this.tbSendMsg.Text; //保存TextBox文本
            //格式chat#ToName#words
            string sndmsg = "chat#" + UID + "#" + temp;
            try {
                Bw.Write(sndmsg);
            }
            catch
            {
                MessageBox.Show("发送失败");
            }
            AddMessage(temp, false,null);
            this.tbSendMsg.Clear();

        }

        public void AddMessage(string str, bool isuser,string time)
        {
            int startindex = this.rtb_ShowMsg.Text.Length;

            string message = string.Empty;

            if (isuser)
                message = "【" + ToName + "】  " + time + "\n" + str + "\n";
            else
                message = "【" + UserName + "】  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n" + str + "\n";
            this.rtb_ShowMsg.AppendText(message);
            this.rtb_ShowMsg.Select(startindex, message.Length);
            if (isuser)
            {
                this.rtb_ShowMsg.SelectionAlignment = HorizontalAlignment.Left;
            }
            else
            {
                this.rtb_ShowMsg.SelectionAlignment = HorizontalAlignment.Right;
            }
            this.rtb_ShowMsg.Select(this.rtb_ShowMsg.Text.Length, 0);
        }

        private void Readed()
        {
            string sndmsg = "readed#"+UID;
            try
            {
                Bw.Write(sndmsg);
            }
            catch
            {
                MessageBox.Show("发送失败");
            }
        }
        private void Talking_FormClosed(object sender, FormClosedEventArgs e)
        {
            ListForm.RemoveTalking(this);
            th.Abort();
        }

        private void sendPIC_Click(object sender, EventArgs e)
        {
            string sndmsg = "sendPic#" + UID;
            try
            {
                Bw.Write(sndmsg);
            }
            catch
            {
                MessageBox.Show("发送失败");
            }
        }

        private void sendFile_Click(object sender, EventArgs e)
        {
            string sndmsg = "sendFile#" + UID;
            try
            {
                Bw.Write(sndmsg);
            }
            catch
            {
                MessageBox.Show("发送失败");
            }
        }
    }
}

