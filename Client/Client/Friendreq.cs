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
    public partial class Friendreq : Form
    {
        public bool iswork { get; set; }
        public BinaryWriter Bw { get; set; }
        public static List<FriendReqItem> frdformList = new List<FriendReqItem>();
        public static List<MutualReqItem> muformList = new List<MutualReqItem>();
        public Friendreq()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            //好友申请
            lvFrdReq.Columns.Add("请求ID", 60, HorizontalAlignment.Left);
            lvFrdReq.Columns.Add("昵称", 60, HorizontalAlignment.Left);
            lvFrdReq.Columns.Add("请求状态", 60, HorizontalAlignment.Left);
            //发送的好友申请
            lvFrdPutReq.Columns.Add("请求ID", 60, HorizontalAlignment.Left);
            lvFrdPutReq.Columns.Add("昵称", 60, HorizontalAlignment.Left);
            lvFrdPutReq.Columns.Add("请求状态", 60, HorizontalAlignment.Left);
            //群组申请
            lvMuReq.Columns.Add("UID", 45, HorizontalAlignment.Left);
            lvMuReq.Columns.Add("昵称", 60, HorizontalAlignment.Left);
            lvMuReq.Columns.Add("GID", 45, HorizontalAlignment.Left);
            lvMuReq.Columns.Add("群名", 60, HorizontalAlignment.Left);
            lvMuReq.Columns.Add("请求状态", 60, HorizontalAlignment.Left);
            //发送的群组申请
            lvPutMuReq.Columns.Add("GID", 45, HorizontalAlignment.Left);
            lvPutMuReq.Columns.Add("群名", 60, HorizontalAlignment.Left);
            lvPutMuReq.Columns.Add("请求状态", 60, HorizontalAlignment.Left);
        }
        private void Friendreq_Load(object sender, EventArgs e)
        {
            AddReqItem();
            AddPutReqItem();
            AddMuReqItem();
            AddPutMuReqItem();
        }

        public void AddReqItem()
        {
            for (int i = 0; i < ListForm.FrdReqList.Count; i++)
            {
                
                ListViewItem lvitem = new ListViewItem();
                lvitem.ImageIndex = 0;
                lvitem.Text = ListForm.FrdReqList[i].UID;
                lvitem.SubItems.Add(ListForm.FrdReqList[i].Username);
                lvitem.SubItems.Add("未处理");
                lvitem.Tag = 0;
                lvFrdReq.Items.Add(lvitem);
            }
        }
        public void AddPutReqItem()
        {
            for (int i = 0; i < ListForm.PutFrdReqList.Count; i++)
            {

                ListViewItem lvitem = new ListViewItem();
                lvitem.ImageIndex = 0;
                lvitem.Text = ListForm.PutFrdReqList[i].UID;
                lvitem.SubItems.Add(ListForm.PutFrdReqList[i].Username);
                lvitem.SubItems.Add("未处理");
                lvitem.Tag = 0;
                lvFrdPutReq.Items.Add(lvitem);
            }
        }
        public void AddMuReqItem()
        {
            for (int i = 0; i < ListForm.MuReqList.Count; i++)
            {

                ListViewItem lvitem = new ListViewItem();
                lvitem.ImageIndex = 0;
                lvitem.Text = ListForm.MuReqList[i].UID;
                lvitem.SubItems.Add(ListForm.MuReqList[i].Username);
                lvitem.SubItems.Add(ListForm.MuReqList[i].GID);
                lvitem.SubItems.Add(ListForm.MuReqList[i].Groupname);
                lvitem.SubItems.Add("未处理");
                lvitem.Tag = 0;
                lvMuReq.Items.Add(lvitem);
            }
        }
        public void AddPutMuReqItem()
        {
            for (int i = 0; i < ListForm.PutMuReqList.Count; i++)
            {

                ListViewItem lvitem = new ListViewItem();
                lvitem.ImageIndex = 0;
                lvitem.Text = ListForm.PutMuReqList[i].GID;
                lvitem.SubItems.Add(ListForm.PutMuReqList[i].Groupname);
                lvitem.SubItems.Add("未处理");
                lvitem.Tag = 0;
                lvPutMuReq.Items.Add(lvitem);
            }
        }
        public void ReloadReqItem()
        {
            //MessageBox.Show("trytoclear");
            lvFrdReq.Items.Clear();
            AddReqItem();
        }
        public void ReLoadPutReqItem()
        {
            //MessageBox.Show("trytoclear");
            lvFrdPutReq.Items.Clear();
            AddPutReqItem();
        }
        public void ReloadMuReqItem()
        {
            //MessageBox.Show("trytoclear");
            lvMuReq.Items.Clear();
            AddMuReqItem();
        }
        public void ReloadPutMuReqItem()
        {
            //MessageBox.Show("trytoclear");
            lvPutMuReq.Items.Clear();
            AddPutMuReqItem();
        }
        private void lvFrdReq_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lvFrdReq.SelectedItems.Count > 0)
            {
                ListViewItem lvitem = this.lvFrdReq.SelectedItems[0];
                string toUID = lvitem.Text;
                //string toips = lvitem.Tag.ToString();
                FriendReqItem f = IsHaveFriendReqItem(toUID);
                if (f != null)
                {
                    f.Focus();
                }
                else
                {
                    FriendReqItem frditm = new FriendReqItem();
                    frditm.toUID = toUID;
                    frditm.toName = lvitem.SubItems[1].Text;
                    frditm.Bw = Bw;
                    frdformList.Add(frditm);
                    frditm.Show();
                }
            }
        }
        private void lvMuReq_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lvMuReq.SelectedItems.Count > 0)
            {
                ListViewItem lvitem = this.lvMuReq.SelectedItems[0];
                string UID = lvitem.Text;
                string GID = lvitem.SubItems[2].Text;
                //string toips = lvitem.Tag.ToString();
                MutualReqItem f = IsHaveMuReqItem(GID,UID);
                if (f != null)
                {
                    f.Focus();
                }
                else
                {
                    MutualReqItem muitm = new MutualReqItem();
                    muitm.UID = UID;
                    muitm.GID = GID;
                    muitm.Bw = Bw;
                    muitm.toName = lvitem.SubItems[1].Text;
                    muitm.groupName = lvitem.SubItems[3].Text;
                    muformList.Add(muitm);
                    muitm.Show();
                }
            }
        }
        private void FriendReq_FormClosed(object sender, FormClosedEventArgs e)
        {
            ListForm.RemoveFriendReq();
        }

        private FriendReqItem IsHaveFriendReqItem(String toUID)
        {
            foreach (FriendReqItem frqitm in frdformList)
            {
                if (frqitm.toUID == toUID)
                    return frqitm;
            }
            return null;
        }
        private MutualReqItem IsHaveMuReqItem(String GID,String UID)
        {
            foreach (MutualReqItem muitm in muformList)
            {
                if (muitm.UID == UID && muitm.GID==GID)
                    return muitm;
            }
            return null;
        }
        public static void RemoveFrdReqItem(String toUID)
        {
            foreach(FriendReqItem frq in frdformList)
            {
                if (frq.toUID == toUID)
                {
                    frdformList.Remove(frq);
                    return;
                }
            }
        }
        public static void RemoveMutualReqItem(String GID,String UID)
        {
            foreach (MutualReqItem muitm in muformList)
            {
                if (muitm.UID == UID && muitm.GID == GID)
                {
                    muformList.Remove(muitm);
                    return;
                }
            }
        }

        private void FriendReq_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*for (int i = 0; i < frdformList.Count; i++)
            {

            }*/
        }

        private void lvFrdPutReq_MouseClick(object sender, MouseEventArgs e)
        {
            lvFrdPutReq.MultiSelect = false;
            //鼠标右键  
            if (e.Button == MouseButtons.Right)
            {
                //filesList.ContextMenuStrip = contextMenuStrip1;  
                //选中列表中数据才显示 空白处不显示  
                string UID = lvFrdPutReq.SelectedItems[0].Text;   
                Point p = new Point(e.X, e.Y);
                contextMenuStrip1.Show(lvFrdPutReq, p);
            }
        }
        private void lvPutMuReq_MouseClick(object sender, MouseEventArgs e)
        {
            lvPutMuReq.MultiSelect = false;
            if (e.Button == MouseButtons.Right)
            {
                string GID = lvPutMuReq.SelectedItems[0].Text;
                Point p = new Point(e.X, e.Y);
                contextMenuStrip2.Show(lvPutMuReq, p);
            }
        }
        private void CancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lvFrdPutReq.SelectedItems.Count == 0)
                return;
            ListViewItem lvitm = this.lvFrdPutReq.SelectedItems[0];
            //MessageBox.Show(lvitm.SubItems[1].Text);
            Bw.Write("cancelfrdreq#" + lvitm.SubItems[0].Text);
        }

        private void CancelMuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lvPutMuReq.SelectedItems.Count == 0)
                return;
            ListViewItem lvitm = this.lvPutMuReq.SelectedItems[0];
            Bw.Write("cancelmureq#" + lvitm.SubItems[0].Text);
        }
    }
}
