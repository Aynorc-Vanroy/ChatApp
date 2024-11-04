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
using System.IO;
using System.Windows.Forms;

namespace Client
{
    public partial class ListForm : Form
    {
        public string UID { get; set; }
        public string Username { get; set; }
        public string sign { get;set; }
        public string Users { get; set; }
        public int Port { get; set; }
        public BinaryReader Br { get; set; }
        public BinaryWriter Bw { get; set; }
        private TcpClient tc = null;
        private UdpClient uc = null;
        private Socket socketClient;
        private NetworkStream ns;
        //private BinaryReader Br;
        //private BinaryWriter Bw;
        public string ServerIP;
        public string ServerPort;

        public string IP { get; set; }

        public List<User> UserList = new List<User>();
        //聊天窗口List
        public static List<Talking> TalkList = new List<Talking>();
        public static List<MutualTalk> MutualTalkList = new List<MutualTalk>();
        //请求窗口List
        public static List<Friendreq> FriendReqList = new List<Friendreq>();
        public static List<Addfriend> AddFmList = new List<Addfriend>();
        //好友菜单窗口List
        public static List<FrdGroupCg> FrdGrpCgList = new List<FrdGroupCg>();
        public static List<UserState> UserStateList = new List<UserState>();
        //群组菜单窗口List
        public static List<MuGroupCg> MugRpCgList = new List<MuGroupCg>();
        public static List<Createmu> CrtMuList = new List<Createmu>();
        public static List<MuState> MuStateList = new List<MuState>();
        //未读消息List
        public static List<noread> ReadList = new List<noread>();
        public static List<GroupNoRead> GrpReadList = new List<GroupNoRead>();
        //锁List
        public static List<reqlock> LockList = new List<reqlock>();
        public static List<GroupLock> GrpLockList = new List<GroupLock>();
        //好友请求List
        public static List<User> FrdReqList = new List<User>();
        public static List<User> PutFrdReqList = new List<User>();
        //群组请求List
        public static List<MuReq> MuReqList = new List<MuReq>();
        public static List<MuReq> PutMuReqList = new List<MuReq>();
        //好友分组List
        public static List<String> GroupList = new List<string>();
        public static List<String> MuGroupList = new List<string>();
        

        bool isWork = false;
        public ListForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

        }
        private void list_Load(object sender, EventArgs e)
        {
            this.Text = UID + ":" + Username;
            lbUID.Text = UID;
            lbUserName.Text = Username;
            Start();
            //获取主界面列表
            GetFriends();
            GetGroups();
            GetMuGroups();
            GetMutualChat();
            //获取请求
            GetFrdReq();
            GetPutReq();
            GetMuReq();
            GetPutMuReq();
            //获取未读消息
            LoadNoRead();
            LoadMunOread();
            lvFriends.Columns.Add("ID", 20, HorizontalAlignment.Left);
            lvFriends.Columns.Add("昵称", 40, HorizontalAlignment.Left);
            lvFriends.Columns.Add("分组", 60, HorizontalAlignment.Left);
            lvFriends.Columns.Add("在线状态", 60, HorizontalAlignment.Left);
            lvGroup.Columns.Add("ID", 20, HorizontalAlignment.Left);
            lvGroup.Columns.Add("群聊名称", 60, HorizontalAlignment.Left);
            lvGroup.Columns.Add("分组", 60, HorizontalAlignment.Left);
        }

        private void Start()
        {
            isWork = true;
            //tcp消息监听
            Thread tcpth = new Thread(TcpListen);
            tcpth.IsBackground = true;
            tcpth.Start();
        }

        //监听服务器端发来的消息
        private void TcpListen()
        {
            while (isWork)
            {
                string receiveMsg = null;
                try
                {
                    receiveMsg = Br.ReadString();
                }
                catch (Exception)
                {
                }
                if (receiveMsg != null)
                {
                    string command = string.Empty;
                    string[] splitStrings = receiveMsg.Split('#');
                    command = splitStrings[0];
                    switch (command)
                    {
                        case "friends":
                            string[] frienditem = splitStrings[1].Split('/');
                            LockList.Clear();
                            foreach (string s in frienditem)
                            {
                                if (s != ".ed")
                                {
                                    string[] _index = s.Split(';');
                                    ListViewItem lvitem = new ListViewItem();
                                    lvitem.ImageIndex = 0;
                                    lvitem.Text = _index[0];
                                    lvitem.SubItems.Add(_index[1]);
                                    lvitem.SubItems.Add(_index[2]);
                                    lvitem.SubItems.Add(_index[4]);
                                    lvitem.Tag = _index[3];
                                    lvFriends.Items.Add(lvitem);
                                    reqlock n_lock = new reqlock(_index[0]);
                                    LockList.Add(n_lock);
                                }
                            }
                            break;
                        case "newmsg":
                            foreach (reqlock l in LockList)
                            {
                                if (l.UID == splitStrings[1])
                                {
                                    lock (l.newMsgLock)
                                    {
                                        l.newMsg++;
                                        noread reader = new noread(splitStrings[1], splitStrings[2], splitStrings[3], splitStrings[4]);
                                        ReadList.Add(reader);
                                        ReMind(splitStrings[1]);
                                        Monitor.Pulse(l.newMsgLock);
                                    }
                                }
                            }
                            break;
                        case "friendreq":
                            //格式：friendreq#UID1;username1/UID2;username2/.ed
                            string[] reqitem = splitStrings[1].Split('/');
                            FrdReqList.Clear();
                            foreach (string s in reqitem)
                            {
                                if (s != ".ed")
                                {
                                    string[] frdr = s.Split(';');
                                    User newusr = new User(frdr[0], frdr[1]);
                                    FrdReqList.Add(newusr);
                                }

                            }
                            if (IsHaveFrdRq() != null)
                            {
                                IsHaveFrdRq().ReloadReqItem();
                            }
                            break;
                        case "putreq":
                            //格式：friendreq#UID1;username1/UID2;username2/.ed
                            string[] putreqitem = splitStrings[1].Split('/');
                            PutFrdReqList.Clear();
                            foreach (string s in putreqitem)
                            {
                                if (s != ".ed")
                                {
                                    string[] pfrdr = s.Split(';');
                                    User pnewusr = new User(pfrdr[0], pfrdr[1]);
                                    PutFrdReqList.Add(pnewusr);
                                }

                            }
                            if (IsHaveFrdRq() != null)
                            {
                                IsHaveFrdRq().ReLoadPutReqItem();
                            }
                            break;
                        case "mureq":
                            //格式：mureq#GID1;groupname;UID1;username/GID2;groupname;UID2;username/.ed
                            string[] mureqitem = splitStrings[1].Split('/');
                            MuReqList.Clear();
                            foreach (string s in mureqitem)
                            {
                                if (s != ".ed")
                                {
                                    string[] pfrdr = s.Split(';');
                                    MuReq newmu = new MuReq(pfrdr[2], pfrdr[3], pfrdr[0], pfrdr[1]);
                                    MuReqList.Add(newmu);
                                }

                            }
                            if (IsHaveFrdRq() != null)
                            {
                                IsHaveFrdRq().ReloadMuReqItem();
                            }
                            break;
                        case "putmureq":
                            //格式：putmureq#GID1;groupname1/GID2;groupname2/.ed
                            string[] putmureqitem = splitStrings[1].Split('/');
                            PutMuReqList.Clear();
                            foreach (string s in putmureqitem)
                            {
                                if (s != ".ed")
                                {
                                    string[] pfrdr = s.Split(';');
                                    MuReq newmu = new MuReq("","",pfrdr[0], pfrdr[1]);
                                    PutMuReqList.Add(newmu);
                                }

                            }
                            if (IsHaveFrdRq() != null)
                            {
                                IsHaveFrdRq().ReloadPutMuReqItem();
                            }
                            break;
                        case "groups":
                            string[] groupitem = splitStrings[1].Split(';');
                            foreach (string s in groupitem)
                            {
                                if (s != ".ed")
                                {
                                    GroupList.Add(s);
                                }
                            }
                            break;
                        case "mugroups":
                            string[] muGroupItem = splitStrings[1].Split(';');
                            foreach (string s in muGroupItem)
                            {
                                if (s != ".ed")
                                {
                                    MuGroupList.Add(s);
                                }
                            }
                            break;
                        case "reloadlist":
                            lvFriends.Items.Clear();
                            lvGroup.Items.Clear();
                            GetFriends();
                            GetMutualChat();
                            foreach(MutualTalk mt in MutualTalkList)
                            {
                                mt.GetMember();
                            }
                            GetFrdReq();
                            GetPutReq();
                            GetMuReq();
                            GetPutMuReq();
                            break;
                        case "addreturn":
                            switch (splitStrings[1])
                            {
                                case "msg":
                                    if (IsHaveAddFrd() != null)
                                    {
                                        IsHaveAddFrd().get_msg(splitStrings[2]);
                                    }
                                    break;
                                case "true":
                                    if (IsHaveAddFrd() != null)
                                    {
                                        IsHaveAddFrd().get_true();
                                    }
                                    break;
                                case "mumsg":
                                    if (IsHaveAddFrd() != null)
                                    {
                                        IsHaveAddFrd().GetMumsg(splitStrings[2]);
                                    }
                                    break;
                                case "mutrue":
                                    if (IsHaveAddFrd() != null)
                                    {
                                        IsHaveAddFrd().GetMutrue();
                                    }
                                    break;
                                default:
                                    if (IsHaveAddFrd() != null)
                                    {
                                        IsHaveAddFrd().get_msg(receiveMsg);
                                    }
                                    break;
                            }
                            break;
                        case "addmureturn":
                            switch (splitStrings[1])
                            {
                                case "msg":
                                    if (IsHaveAddFrd() != null)
                                    {
                                        IsHaveAddFrd().get_msg(splitStrings[2]);
                                    }
                                    break;
                                case "true":
                                    if (IsHaveAddFrd() != null)
                                    {
                                        IsHaveAddFrd().get_true();
                                    }
                                    break;
                                default:
                                    if (IsHaveAddFrd() != null)
                                    {
                                        IsHaveAddFrd().get_msg(receiveMsg);
                                    }
                                    break;
                            }
                            break;
                        case "mutualname":
                            //格式:mutualname#GID1;Groupname1;权限;分组/GID2;Groupname;权限;分组/.ed
                            string[] mutualitem = splitStrings[1].Split('/');
                            GrpLockList.Clear();
                            foreach (string s in mutualitem)
                            {
                                if (s != ".ed")
                                {
                                    string[] _index = s.Split(';');
                                    ListViewItem lvItem = new ListViewItem();
                                    lvItem.ImageIndex = 0;
                                    lvItem.Text = _index[0];
                                    lvItem.SubItems.Add(_index[1]);
                                    lvItem.SubItems.Add(_index[3]);
                                    lvItem.Tag = _index[2];
                                    lvItem.SubItems.Add(_index[4]);
                                    lvGroup.Items.Add(lvItem);
                                    GroupLock glock = new GroupLock(_index[0]);
                                    GrpLockList.Add(glock);
                                }
                            }
                            break;
                        case "member":
                            string[] mem = splitStrings[2].Split('/');
                            foreach (string s in mem)
                            {
                                MutualTalk mtk = IsHaveMutualTalk(splitStrings[1]);
                                if (mtk == null)
                                {
                                    break;
                                }
                                else
                                {
                                    if (s != ".ed")
                                    {
                                        string[] _index = s.Split(';');
                                        ListViewItem lvitem = new ListViewItem();
                                        lvitem.ImageIndex = 0;
                                        lvitem.Text = _index[0];
                                        lvitem.SubItems.Add(_index[1]);
                                        lvitem.SubItems.Add(_index[2]);
                                        lvitem.Tag = _index[2];
                                        mtk.AddMemberItem(lvitem);
                                    }
                                }
                            }
                            break;
                        case "munewmsg":
                            foreach (GroupLock l in GrpLockList)
                            {
                                if (l.GID == splitStrings[1])
                                {
                                    lock (l.newmsglock)
                                    {
                                        l.newmsg++;
                                        GroupNoRead reader = new GroupNoRead(splitStrings[1], splitStrings[2], splitStrings[3], splitStrings[4], splitStrings[5]);
                                        GrpReadList.Add(reader);
                                        MuReMind(splitStrings[1]);
                                        Monitor.Pulse(l.newmsglock);
                                    }
                                }
                            }
                            break;
                        case "userstate":
                            Username = splitStrings[1];
                            sign = splitStrings[2];
                            lbUserName.Text = Username;
                            break;
                        /*case "mustate":
                            mustate(splitStrings[1], splitStrings[2], splitStrings[3]);
                            break;*/
                        default:
                            MessageBox.Show(receiveMsg);
                            break;
                    }
                }
            }
        }

        //获取列表
        private void GetFriends()
        {
            try
            {
                Bw.Write("getfriends");
            }
            catch
            {
                MessageBox.Show("获取好友列表失败");
            }
        }
        private void GetGroups()
        {
            try
            {
                Bw.Write("getgroups");
            }
            catch
            {
                MessageBox.Show("获取好友分组失败");
            }
        }
        private void GetMuGroups()
        {
            try
            {
                Bw.Write("getmugroups");
            }
            catch
            {
                MessageBox.Show("获取群组分组失败");
            }
        }
        private void GetMutualChat()
        {
            try
            {
                Bw.Write("getmutualchat");
            }
            catch
            {
                MessageBox.Show("获取群聊信息失败");
            }
        }

        //获取请求
        public void GetFrdReq()
        {
            try
            {
                Bw.Write("getfriendreq");
            }
            catch
            {
                MessageBox.Show("获取好友列表失败");
            }
        }
        public void GetPutReq()
        {
            try
            {
                Bw.Write("getputreq");
            }
            catch
            {
                MessageBox.Show("获取发送的好友列表失败");
            }
        }
        public void GetMuReq()
        {
            try
            {
                Bw.Write("getmureq");
            }
            catch
            {
                MessageBox.Show("获取群组申请列表失败");
            }
        }
        public void GetPutMuReq()
        {
            try
            {
                Bw.Write("getputmureq");
            }
            catch
            {
                MessageBox.Show("获取发送的群组申请列表失败");
            }
        }

        private void LoadNoRead()
        {
            string sndmsg = "getnoread#";
            try
            {
                Bw.Write(sndmsg);
            }
            catch
            {
                MessageBox.Show("获取消息失败");
            }

        }
        private void LoadMunOread()
        {
            string sndmsg = "getmunoread#";
            try
            {
                Bw.Write(sndmsg);
            }
            catch
            {
                MessageBox.Show("获取消息失败");
            }

        }
        private void ReMind(String UID)
        {
            foreach (ListViewItem item in this.lvFriends.Items)
            {
                if (IsHaveTalk(UID) != null)
                {
                    break;
                }
                else
                {
                    if (item.Text == UID)
                    {
                        item.SubItems[0].BackColor = Color.Red;
                    }
                }
            }
        }
        private void MuReMind(String GID)
        {
            foreach (ListViewItem item in this.lvGroup.Items)
            {
                if (IsHaveMutualTalk(GID) != null)
                {
                    break;
                }
                else
                {
                    if (item.Text == GID)
                    {
                        item.SubItems[0].BackColor = Color.Red;
                    }
                }
            }
        }
        //修改属性
        /*private void mustate(string GID,string groupname,string sign)
        {
            foreach (ListViewItem item in this.lv_group.Items)
            {
                if (item.Text == GID)
                {
                    item.SubItems[1].Text = groupname;
                    item.SubItems[4].Text = sign;
                }    
            }
        }
        private void state(string UID,string username,string sign)
        {

        }*/
        //关闭窗口
        private void listFormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                string sendmsg = "logout#" + UID;
                Bw.Write(sendmsg);
                Bw.Flush();
                isWork = false;
                Br.Close();
                Bw.Close();
            }
            catch
            { }
            Application.Exit();
        }

        private void lvFriends_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lvFriends.SelectedItems.Count > 0)
            {
                ListViewItem lvitem = this.lvFriends.SelectedItems[0];
                string toUID = lvitem.Text;
                string toname = lvitem.SubItems[1].Text;
                string toips = lvitem.Tag.ToString();
                Talking t = IsHaveTalk(toUID);
                if (t != null)
                {
                    t.Focus();
                }
                else
                {
                    this.lvFriends.SelectedItems[0].SubItems[0].BackColor = Color.Transparent;
                    Talking talk = new Talking();
                    talk.UID = toUID;
                    talk.UserName = Username;
                    talk.ToName = toname;
                    talk.Br = Br;
                    talk.Bw = Bw;
                    TalkList.Add(talk);
                    talk.Show();
                }
            }
        }
        private Talking IsHaveTalk(string toUID)
        {
            foreach (Talking tk in TalkList)
            {
                if (tk.UID == toUID)
                    return tk;
            }
            return null;
        }
        public static void RemoveTalking(Talking _talk)
        {
            foreach (Talking tk in TalkList)
            {
                if (tk.UID == _talk.UID)
                {
                    TalkList.Remove(_talk);
                    return;
                }
            }
        }

        private void btnFrdReq_Click(object sender, EventArgs e)
        {
            Friendreq f = IsHaveFrdRq();
            if (f != null)
            {
                f.Focus();
            }
            else
            {
                Friendreq frq = new Friendreq();
                frq.iswork = true;
                frq.Bw = Bw;
                FriendReqList.Add(frq);
                frq.Show();
            }

        }
        private Friendreq IsHaveFrdRq()
        {
            foreach (Friendreq frq in FriendReqList)
            {
                if (frq.iswork == true)
                    return frq;
            }
            return null;
        }
        public static void RemoveFriendReq()
        {
            foreach (Friendreq f in FriendReqList)
            {
                if (f.iswork == true)
                {
                    FriendReqList.Remove(f);
                    return;
                }
            }
        }

        private void btnAddFrd__Click(object sender, EventArgs e)
        {
            Addfriend f = IsHaveAddFrd();
            if (f != null)
            {
                f.Focus();
            }
            else
            {
                Addfriend adf = new Addfriend();
                adf.IsWork = true;
                adf.Bw = Bw;
                AddFmList.Add(adf);
                adf.Show();
            }
        }
        private Addfriend IsHaveAddFrd()
        {
            foreach (Addfriend adf in AddFmList)
            {
                if (adf.IsWork == true)
                    return adf;
            }
            return null;
        }
        public static void RemoveAddFrd()
        {
            foreach (Addfriend a in AddFmList)
            {
                if (a.IsWork == true)
                {
                    AddFmList.Remove(a);
                    return;
                }
            }
        }

        private void lvGroup_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.lvGroup.SelectedItems.Count > 0)
            {
                ListViewItem lvitem = this.lvGroup.SelectedItems[0];
                string GID = lvitem.Text;
                string Groupname = lvitem.SubItems[1].Text;
                string auth = lvitem.Tag.ToString();
                MutualTalk mt = IsHaveMutualTalk(GID);
                if (mt != null)
                {
                    mt.Focus();
                }
                else
                {
                    this.lvGroup.SelectedItems[0].SubItems[0].BackColor = Color.Transparent;
                    MutualTalk mtalk = new MutualTalk();
                    mtalk.UID = UID;
                    mtalk.GID = GID;
                    mtalk.GroupName = Groupname;
                    mtalk.UserName = Username;
                    mtalk.Auth = auth;
                    mtalk.Br = Br;
                    mtalk.Bw = Bw;
                    MutualTalkList.Add(mtalk);
                    mtalk.Show();
                }
            }
        }
        private MutualTalk IsHaveMutualTalk(string GID)
        {
            foreach (MutualTalk mtk in MutualTalkList)
            {
                if (mtk.GID == GID)
                    return mtk;
            }
            return null;
        }
        public static void RemoveMutualTalking(MutualTalk _mtalk)
        {
            foreach (MutualTalk mtk in MutualTalkList)
            {
                if (mtk.GID == _mtalk.GID)
                {
                    MutualTalkList.Remove(_mtalk);
                    return;
                }
            }

        }
        //弹出菜单
        private void lvFriends_MouseClick(object sender, MouseEventArgs e)
        {
            lvFriends.MultiSelect = false;
            if (e.Button == MouseButtons.Right)
            {
                string UID = lvFriends.SelectedItems[0].Text;
                Point p = new Point(e.X, e.Y);
                cmFrd.Show(lvFriends, p);
            }
        }
        private void lvGroup_MouseClick(object sender, MouseEventArgs e)
        {
            lvGroup.MultiSelect = false;
            if (e.Button == MouseButtons.Right)
            {
                string GID = lvGroup.SelectedItems[0].Text;
                Point p = new Point(e.X, e.Y);
                if (lvGroup.SelectedItems[0].Tag.ToString() != "0")
                {
                    delMuToolStripMenuItem.Visible = false;
                    exitMuToolStripMenuItem.Visible = true;
                }
                else
                {
                    delMuToolStripMenuItem.Visible = true;
                    exitMuToolStripMenuItem.Visible = false;
                }
                cmGrp.Show(lvGroup, p);
            }
        }
        //删除好友
        private void FrdDelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lvFriends.SelectedItems.Count == 0)
                return;
            ListViewItem lvitm = this.lvFriends.SelectedItems[0];
            DialogResult result = MessageBox.Show("确定删除好友吗？", "提示:",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Bw.Write("delfriends#" + lvitm.SubItems[0].Text);
            }
            else
            {
                return;
            }
            //MessageBox.Show(lvitm.SubItems[1].Text);         
        }
        //修改好友分组
        private void FrdGrpCgToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lvFriends.SelectedItems.Count > 0)
            {
                ListViewItem lvitem = this.lvFriends.SelectedItems[0];
                string UID = lvitem.Text;
                FrdGroupCg f = IsHaveFrdGrpCg();
                if (f != null)
                {
                    f.Focus();
                }
                else
                {
                    FrdGroupCg fgcg = new FrdGroupCg();
                    fgcg.IsWork = true;
                    fgcg.UID = UID;
                    fgcg.Bw = Bw;
                    FrdGrpCgList.Add(fgcg);
                    fgcg.Show();
                }
            }
        }
        private FrdGroupCg IsHaveFrdGrpCg()
        {
            foreach (FrdGroupCg frq in FrdGrpCgList)
            {
                if (frq.IsWork == true)
                    return frq;
            }
            return null;
        }
        public static void RemoveFrdGrpCg()
        {
            foreach (FrdGroupCg frq in FrdGrpCgList)
            {
                if (frq.IsWork == true)
                {
                    FrdGrpCgList.Remove(frq);
                    return;
                }
            }
        }
        //查看用户信息
        private void FrdStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lvFriends.SelectedItems.Count > 0)
            {
                ListViewItem lvitem = this.lvFriends.SelectedItems[0];
                string UID1 = lvitem.Text;
                UserState us = IsHaveUserState(UID1);
                if (us != null)
                {
                    us.Focus();
                }
                else
                {
                    UserState ust = new UserState();
                    ust.UID = UID1;
                    if (UID1 == UID)
                    {
                        ust.isself = true;
                    }
                    else
                    {
                        ust.isself = false;
                    }
                    ust.Bw = Bw;
                    ust.username = lvitem.SubItems[1].Text;
                    ust.sign = lvitem.Tag.ToString();
                    UserStateList.Add(ust);
                    ust.Show();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            UserState us = IsHaveUserState(UID);
            if (us != null)
            {
                us.Focus();
            }
            else
            {
                UserState ust = new UserState();
                ust.UID = UID;
                ust.isself = true;
                ust.Bw = Bw;
                ust.username = Username;
                ust.sign = sign;
                UserStateList.Add(ust);
                ust.Show();
            }
        }

        private UserState IsHaveUserState(string UID)
        {
            foreach (UserState ust in UserStateList)
            {
                if (ust.UID == UID)
                    return ust;
            }
            return null;
        }
        public static void RemoveUserState(string UID)
        {
            foreach (UserState ust in UserStateList)
            {
                if (ust.UID == UID)
                {
                    UserStateList.Remove(ust);
                    return;
                }
            }
        }
        //查看群组信息
        private void muStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lvGroup.SelectedItems.Count > 0)
            {
                ListViewItem lvitem = this.lvGroup.SelectedItems[0];
                string GID1 = lvitem.Text;
                MuState us = IsHaveMuState(GID1);
                if (us != null)
                {
                    us.Focus();
                }
                else
                {
                    MuState ust = new MuState();
                    ust.auth = lvitem.Tag.ToString();
                    ust.GID = GID1;
                    ust.Bw = Bw;
                    ust.groupName = lvitem.SubItems[1].Text;
                    ust.sign = lvitem.SubItems[3].Text;
                    MuStateList.Add(ust);
                    ust.Show();
                }
            }
        }
        private MuState IsHaveMuState(string GID)
        {
            foreach (MuState mst in MuStateList)
            {
                if (mst.GID == GID)
                    return mst;
            }
            return null;
        }
        public static void RemoveMuState(string GID)
        {
            foreach (MuState mst in MuStateList)
            {
                if (mst.GID == GID)
                {
                    MuStateList.Remove(mst);
                    return;
                }
            }
        }
        //创建群组
        private void crtMuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lvGroup.SelectedItems.Count > 0)
            {
                Createmu c = new Createmu();
                c.Bw = Bw;
                c.Show();
            }
        }
        //修改群组分组
        private void cgMuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lvGroup.SelectedItems.Count > 0)
            {
                ListViewItem lvitem = this.lvGroup.SelectedItems[0];
                string GID = lvitem.Text;
                MuGroupCg f = IsHaveMuGrpCg();
                if (f != null)
                {
                    f.Focus();
                }
                else
                {
                    MuGroupCg mgcg = new MuGroupCg();
                    mgcg.iswork = true;
                    mgcg.GID = GID;
                    mgcg.Bw = Bw;
                    MugRpCgList.Add(mgcg);
                    mgcg.Show();
                }
            }
        }
        private MuGroupCg IsHaveMuGrpCg()
        {
            foreach (MuGroupCg frq in MugRpCgList)
            {
                if (frq.iswork == true)
                    return frq;
            }
            return null;
        }
        public static void RemoveMuGrpCg()
        {
            foreach (MuGroupCg frq in MugRpCgList)
            {
                if (frq.iswork == true)
                {
                    MugRpCgList.Remove(frq);
                    return;
                }
            }
        }
        //退出群聊
        private void exitMuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lvGroup.SelectedItems.Count == 0)
                return;
            ListViewItem lvitm = this.lvGroup.SelectedItems[0];
            DialogResult result = MessageBox.Show("确定退出该群吗？", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Bw.Write("exitmu#" + lvitm.SubItems[0].Text);
            }
            else
            {
                return;
            }
        }

        private void delMuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lvGroup.SelectedItems.Count == 0)
                return;
            ListViewItem lvitm = this.lvGroup.SelectedItems[0];
            DialogResult result = MessageBox.Show("确定解散该群吗？", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Bw.Write("delmu#" + lvitm.SubItems[0].Text);
            }
            else
            {
                return;
            }
        }

        
    }

    public class noread
    {

        public string UID { get; set; }
        public string user { get; set; }
        public string time { get; set; }
        public string words { get; set; }
        public noread(string userid, string u, string t, string w)
        {
            UID = userid;
            user = u;
            time = t;
            words = w;
        }


    }
    public class reqlock
    {
        public string UID { get; set; }
        public object newMsgLock { get; set; }
        public int newMsg { get; set; }
        public reqlock(string userId)
        {
            UID = userId;
            newMsg = 0;
            newMsgLock = new object();
        }
    }

    public class GroupNoRead
    {
        public string GID { get; set; }
        public string UID { get; set; }
        public string user { get; set; }
        public string time { get; set; }
        public string words { get; set; }
        public GroupNoRead(string grpId, string userId, string u, string t, string w)
        {
            GID = grpId;
            UID = userId;
            user = u;
            time = t;
            words = w;
        }
    }

    public class GroupLock
    {
        public string GID { get; set; }
        public object newmsglock { get; set; }
        public int newmsg { get; set; }
        public GroupLock(string grpid)
        {
            GID = grpid;
            newmsg = 0;
            newmsglock = new object();
        }

    }

    public class MuReq
    {
        private string uid;
        private string username;
        private string gid;
        private string groupname;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string UID
        {
            get { return uid; }
            set { uid = value; }
        }
        public string Groupname
        {
            get { return groupname; }
            set { groupname = value; }
        }
        public string GID
        {
            get { return gid; }
            set { gid = value; }
        }
        public MuReq(String UID,String Username,String GID,String Groupname)
        {
            uid = UID;
            username = Username;
            gid = GID;
            groupname = Groupname;
        }

    }
}



public class User
{
    private string uid;
    private string username;
    public string Username
    {
        get { return username; }
        set { username = value; }
    }
    public string UID
    {
        get { return uid; }
        set { uid = value; }
    }
    private string password;
    public string Password
    {
        get { return password; }
        set { password = value; }
    }
    private string ip;
    public string IP
    {
        get { return ip; }
        set { ip = value; }
    }
    private int port;
    public int Port
    {
        get { return port; }
        set { port = value; }
    }
    public User(string ID,string Usrname)
    {
        uid = ID;
        username = Usrname;
    }
    //private bool isOnline=false;
    //public bool IsOnline { get; set; }
}