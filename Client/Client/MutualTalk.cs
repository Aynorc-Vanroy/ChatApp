using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class MutualTalk : Form
    {
        public string GID { get; set; }
        public string UID { get; set; }
        public string GroupName { get; set; }
        public string UserName { get; set; }
        public string Auth { get; set; }
        public BinaryReader Br { get; set; }
        public BinaryWriter Bw { get; set; }

        bool isWork = false;
        public MutualTalk()
        {
            InitializeComponent();
        }
        private void MutualTalk_Load(object sender, EventArgs e)
        {
            this.Text = GID + ":" + GroupName;
            lvMembers.Columns.Add("ID", 25, HorizontalAlignment.Left);
            lvMembers.Columns.Add("权限", 60, HorizontalAlignment.Left);
            lvMembers.Columns.Add("昵称", 60, HorizontalAlignment.Left);
            GetMember();
            StartListen();

        }
        private Thread th = null;
        private void StartListen()
        {
            isWork = true;
            //tcp消息监听
            th = new Thread(GetNoRead);
            th.IsBackground = true;
            th.Start();
        }
        private void GetNoRead()
        {
            int locknum = 0;
            for (int i = 0; i < ListForm.GrpLockList.Count; i++)
            {
                if (GID == ListForm.GrpLockList[i].GID)
                {
                    locknum = i;
                }
            }
            while (true)
            {
                //MessageBox.Show(locknum.ToString());
                lock (ListForm.GrpLockList[locknum].newmsglock)
                {
                    while (ListForm.GrpLockList[locknum].newmsg == 0) { Monitor.Wait(ListForm.GrpLockList[locknum].newmsglock); }
                    for (int i = 0; i < ListForm.GrpReadList.Count; i++)
                    {
                        if (GID == ListForm.GrpReadList[i].GID)
                        {
                            //MessageBox.Show(locknum.ToString());
                            AddMessage(ListForm.GrpReadList[i].user, ListForm.GrpReadList[i].words, true, ListForm.GrpReadList[i].time);
                            ListForm.GrpReadList.Remove(ListForm.GrpReadList[i]);
                            ListForm.GrpLockList[locknum].newmsg--;
                        }
                    }
                    Readed();
                    Monitor.Pulse(ListForm.GrpLockList[locknum].newmsglock);
                }
            }
        }

        public void AddMessage(string username,string str, bool isuser, string time)
        {
            int startindex = this.rtbShowMsg.Text.Length;

            string message = string.Empty;

            if (isuser)
                message = "【" + username + "】  " + time + "\n" + str + "\n";
            else
                message = "【" + UserName + "】  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n" + str + "\n";
            this.rtbShowMsg.AppendText(message);
            this.rtbShowMsg.Select(startindex, message.Length);
            if (isuser)
            {
                this.rtbShowMsg.SelectionAlignment = HorizontalAlignment.Left;
            }
            else
            {
                this.rtbShowMsg.SelectionAlignment = HorizontalAlignment.Right;
            }
            this.rtbShowMsg.Select(this.rtbShowMsg.Text.Length, 0);
        }

        private void Readed()
        {
            string sndmsg = "grpreaded#" + GID;
            try
            {
                Bw.Write(sndmsg);
            }
            catch
            {
                MessageBox.Show("发送失败");
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            string temp = this.tbSendMsg.Text; //保存TextBox文本
            //格式chat#ToName#words
            string sndmsg = "muchat#" + GID + "#" + UID + "#" + temp;
            try
            {
                Bw.Write(sndmsg);
            }
            catch
            {
                MessageBox.Show("发送失败");
            }
            AddMessage("",temp, false, null);
            this.tbSendMsg.Clear();
        }
        public void GetMember()
        {
            lvMembers.Items.Clear();
            string sndmsg = "getmutualmember#" + GID;
            try
            {
                Bw.Write(sndmsg);
            }
            catch
            {
                MessageBox.Show("发送失败");
            }
        }
        public void AddMemberItem(ListViewItem lvitem)
        {
            lvMembers.Items.Add(lvitem);
        }
        private void mutualTalk_FormClosed(object sender, FormClosedEventArgs e)
        {
            ListForm.RemoveMutualTalking(this);
            th.Abort();
        }
        
        private void lvMembers_MouseClick(object sender, MouseEventArgs e)
        {
            lvMembers.MultiSelect = false;
            if (e.Button == MouseButtons.Right)
            {
                string UID = lvMembers.SelectedItems[0].Text;
                Point p = new Point(e.X, e.Y);
                string au = lvMembers.SelectedItems[0].SubItems[1].Text;
                if (Auth=="2")
                {
                    tsAuth.Visible = false;
                    tsCg.Visible = false;
                    tsRm.Visible = false;
                    tsRmauth.Visible = false;
                }
                else
                {
                    if(Auth == "1")
                    {
                        if (au == "群主" || au == "管理员")
                        {
                            tsAuth.Visible = false;
                            tsCg.Visible = false;
                            tsRm.Visible = false;
                            tsRmauth.Visible = false;
                        }
                        else
                        {
                            tsCg.Visible = false;
                            tsAuth.Visible = false;
                            tsRmauth.Visible = false;
                            tsRm.Visible = true;
                        }
                    }
                    else
                    {
                        
                        if (au == "群主")
                        {
                            tsAuth.Visible = false;
                            tsCg.Visible = false;
                            tsRm.Visible = false;
                            tsRmauth.Visible = false;
                        }
                        else {
                            if (au == "管理员")
                            {
                                tsAuth.Visible = false;
                                tsCg.Visible = true;
                                tsRm.Visible = true;
                                tsRmauth.Visible = true;
                            }
                            else
                            {
                                tsAuth.Visible = true;
                                tsRmauth.Visible = false;
                                tsCg.Visible = true;
                                tsRm.Visible = true;
                            }
                        }
                       
                    }
                }
                cmMember.Show(lvMembers, p);
            }
        }
        private void tsRm_Click(object sender, EventArgs e)
        {
            if (this.lvMembers.SelectedItems.Count == 0)
                return;
            ListViewItem lvitm = this.lvMembers.SelectedItems[0];
            DialogResult result = MessageBox.Show("确定移出该群员吗？", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Bw.Write("rmmember#" + GID + "#" + lvitm.SubItems[0].Text);
            }
            else
            {
                return;
            }
        }

        private void tsAuth_Click(object sender, EventArgs e)
        {
            if (this.lvMembers.SelectedItems.Count == 0)
                return;
            ListViewItem lvitm = this.lvMembers.SelectedItems[0];
            DialogResult result = MessageBox.Show("将其设置为管理员吗？", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Bw.Write("aumember#" +GID+"#"+ lvitm.SubItems[0].Text);
            }
            else
            {
                return;
            }
        }

        private void tsCg_Click(object sender, EventArgs e)
        {
            if (this.lvMembers.SelectedItems.Count == 0)
                return;
            ListViewItem lvitm = this.lvMembers.SelectedItems[0];
            DialogResult result = MessageBox.Show("确定转让群主吗？", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Bw.Write("cgmember#" + GID + "#" + lvitm.SubItems[0].Text);
                Auth = "2";
            }
            else
            {
                return;
            }
        }

        private void tsRmAuth_Click(object sender, EventArgs e)
        {
            if (this.lvMembers.SelectedItems.Count == 0)
                return;
            ListViewItem lvitm = this.lvMembers.SelectedItems[0];
            DialogResult result = MessageBox.Show("确定移出该群员吗？", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Bw.Write("rmauthmember#" + GID + "#" + lvitm.SubItems[0].Text);
            }
            else
            {
                return;
            }
        }
    }
}
