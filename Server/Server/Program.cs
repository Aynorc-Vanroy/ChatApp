using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MySql.Data.MySqlClient;

namespace Server
{
    class Program
    {
        public static List<User> _userlist = new List<User>();
        private static TcpListener t1; //监听对象
        private static NetworkStream ns; //网络流
        private static string _localAddress = GetLocalAddressIp();
        static void Main(string[] args)
        {
            try
            {
                t1 = new TcpListener(9999);
                t1.Start();
                Console.WriteLine("服务器启动成功...");
                Thread th = new Thread(ListenClientConnect);
                th.IsBackground = true;
                th.Start();
                while (true)
                {
                    string index = Console.ReadLine();
                    if (index == "exit")
                    {
                        Console.WriteLine("开始停止服务，并依次使用户退出!");
                        foreach (User user in _userlist)
                        {
                            user.br.Close();
                            user.bw.Close();
                            user.client.Close();
                        }
                        t1.Stop();
                        break;
                    }
                    else
                    {
                        SendToClient(_userlist, "barchmsg#服务器#" + index);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("服务器启动失败..." + e.Message);
                throw;
            }
        }

        /// <summary>
        /// 监听客户端
        /// </summary>
        private static void ListenClientConnect()
        {
            Console.WriteLine("开始在{0}:{1}监听客户连接", _localAddress, 9999);
            while (true)
            {
                TcpClient newClient = null;
                try
                {
                    //等待用户进入
                    newClient = t1.AcceptTcpClient();
                }
                catch
                {
                    break;
                }
                //每接受一个客户端连接，就创建一个线程循环接收该客户端发来的信息
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ReceiveData);
                Thread threadReceive = new Thread(pts);
                User user = new User(newClient);
                threadReceive.Start(user);
                //userlist.Add(user);
            }
        }

        /// <summary>
        /// 接收信息
        /// </summary>
        /// <param name="obj"></param>
        private static void ReceiveData(object obj)
        {
            User user = (User)obj;
            TcpClient client = user.client;
            //是否正常退出接收线程
            bool normalExit = false;
            //用于控制是否退出循环
            bool exitWhile = false;
            //用于指向特定user
            String toName = null;
            String toUID = null;
            String GID = null;
            while (exitWhile == false)
            {
                string receiveString = null;
                try
                {
                    receiveString = user.br.ReadString();
                }
                catch
                {
                    Console.WriteLine("接受数据失败");
                }
                if (receiveString == null)
                {
                    if (normalExit == false)
                    {
                        if (client.Connected == true)
                        {
                            Console.WriteLine("与{0}]失去联系，已终止接收该用户信息", client.Client.RemoteEndPoint);
                        }
                    }
                    break;
                }
                Console.WriteLine("来自[{0}]：{1}", user.client.Client.RemoteEndPoint, receiveString);
                string[] splitString = receiveString.Split('#');
                string sendString = "";
                
                //SendToClient(userlist,receiveString);
                switch (splitString[0])
                {
                    case "login":
                        //格式：login#jack#12345
                        user.Account = splitString[1];
                        user.Password = splitString[2];
                        if (LoginCheck(user)!="false")
                        {
                            user.UID = LoginCheck(user);
                            user.Username = GetUsername(user.UID);
                            user.sign = GetUsersign(user.UID);
                            bool flag = false;
                            foreach (User u in _userlist)
                            {
                                if (u.Username.Equals(user.Username))
                                    flag = true;
                            }
                            if (!flag)
                            {
                                user.IsLogin = true;
                                _userlist.Add(user);
                                sendString = "user#";
                                sendString += user.UID;
                                sendString += "#";
                                sendString += user.Username;
                                sendString += "#";
                                sendString += user.sign;
                                //SendToClient(userlist, sendString);//发给所有用户

                            }
                            else
                            {
                                SendToClient(user, "already login");
                                break;
                            }
                            Console.WriteLine("[{0}]{1}({2})登录成功\t当前连接用户数{3}", user.UID,user.Username, user.client.Client.RemoteEndPoint, _userlist.Count);

                        }
                        else
                        {
                            SendToClient(user, "login fail");
                            break;
                        }
                        SendToClient(user, sendString);//登陆消息反馈，只发给自己
                        SqlDataReader ol = FriendsReader(user.UID);
                        while (ol.Read())
                        {
                            if (GetUserByUID(ol[0].ToString()) != null)
                            {
                                SendToClient(GetUserByUID(ol[0].ToString()), "reloadlist");
                            }
                        }
                        ol.Close();
                        break;
                    case "signup":
                        //格式:signup#账号#密码#昵称
                        string temp = SignUp(splitString[1], splitString[2],splitString[3]);
                        SendToClient(user, temp);
                        break;
                    case "logout":
                        //格式：logout#jack
                        Console.WriteLine("{0}:{1}[{2}]退出", user.UID,user.Username, user.client.Client.RemoteEndPoint);
                        SqlDataReader ofl = FriendsReader(user.UID);
                        while (ofl.Read())
                        {
                            if (GetUserByUID(ofl[0].ToString()) != null)
                            {
                                SendToClient(GetUserByUID(ofl[0].ToString()), "reloadlist");
                            }
                        }
                        normalExit = true;
                        exitWhile = true;
                        user.IsLogin = false;
                        break;
                    case "getfriends":
                        try
                        {
                            //格式：friends#UID1;username1;group1;isonline/UID2;username2;group2;isonline/.ed
                            SqlDataReader friends = FriendsReader(user.UID);
                            sendString = "friends#";
                            while (friends.Read())
                            {
                                sendString += friends[0].ToString();
                                sendString += ";";
                                sendString += friends[1].ToString();
                                sendString += ";";
                                sendString += friends[2].ToString();
                                sendString += ";";
                                sendString += friends[3].ToString();
                                sendString += ";";

                                //if (isol(friends[0].ToString()) == true)
                                if (GetUserByUID(friends[0].ToString()) != null)
                                {
                                    sendString += "在线";
                                }
                                else
                                {
                                    sendString += "离线";
                                }
                                sendString += "/";
                            }
                            sendString += ".ed";
                            SendToClient(user, sendString);
                        }
                        catch
                        {
                            Console.WriteLine("获取好友列表失败");
                        }

                        break;
                    case "chat":
                        toUID = splitString[1];
                        String chatwords = splitString[2];
                        try
                        {
                            Sndchat(user.UID, toUID, chatwords);
                            Console.WriteLine("{0}向{1}发送\"{2}\"成功。", user.Username, toName, chatwords);
                        }
                        catch
                        {
                            Console.WriteLine("{0}向{1}发送的\"{2}\"失败了。", user.Username, toName, chatwords);
                        }
                        if (GetUserByUID(toUID) != null)
                        {
                            //格式：newmsg#UID#senduser#time#chatword
                            sendString = "newmsg#" + user.UID + "#" + user.Username + "#" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "#" + chatwords;
                            Console.WriteLine(sendString);
                            SendToClient(GetUserByUID(toUID), sendString);
                        }
                        break;
                    case "readed":
                        toUID = splitString[1];
                        Readed(toUID,user.UID);
                        break;
                    case "grpreaded":
                        GID = splitString[1];
                        MuReaded(user.UID,GID);
                        break;
                    case "getnoread": //接受未读消息
                        toUID = splitString[1];
                        try
                        {
                            //格式：newmsg#UID#senduser#time#chatword
                            SqlDataReader noread = GetNoRead(user.UID);
                            while (noread.Read())
                            {
                                sendString = "newmsg#" + noread[0].ToString() + "#" + noread[1].ToString() + "#" + noread[2].ToString() + "#" + noread[3].ToString();
                                SendToClient(user, sendString);
                            }
                            //readed(toUID, user.UID);
                        }
                        catch
                        {
                            Console.WriteLine("{0}获取未读消息失败。", user.Username);
                        }
                        break;
                    case "getmunoread":
                        SqlDataReader munoread = GetMuNoRead(user.UID);
                        while (munoread.Read())
                        {
                            //格式：munewmsg#GID#UID#Username#Sendtime#Sendwords
                            sendString = "munewmsg#" + munoread[0].ToString() + "#" + munoread[1].ToString() +"#"+GetUsername(munoread[1].ToString()) +"#" + munoread[2].ToString() + "#" + munoread[3].ToString();
                            SendToClient(user, sendString);
                        }
                        break;
                    case "addfriend": //发送好友请求
                        toUID = splitString[1];
                        String needGroup = splitString[2];
                        if (toUID == user.UID)
                        {
                            sendString = "addreturn#msg#不能添加自己为好友";
                        }
                        else
                        {
                            if (JugeUserIsExit(toUID) == true)
                            {
                                if (IsHaveReq(user.UID, toUID) == true)
                                {
                                    sendString = "addreturn#msg#已经发送过该请求";
                                }
                                else
                                {
                                    if (IsHaveFrd(user.UID, toUID) == true)
                                    {
                                        sendString = "addreturn#msg#你们已经是好友了";
                                    }
                                    else
                                    {
                                        PutFriendReq(user.UID, toUID, needGroup);
                                        sendString = "addreturn#true";
                                        SendToClient(user, "reloadlist");
                                    }
                                }
                                
                            }
                            else
                            {
                                sendString = "addreturn#msg#用户不存在";
                                Console.WriteLine("{0}查找{1}用户失败。", user.Username, toName);
                            }
                        }
                        SendToClient(user, sendString);
                        if (GetUserByUID(toUID) != null)
                        {
                            SendToClient(GetUserByUID(toUID), "reloadlist");
                        }
                        break;
                    case "addgroup": //发送入群申请
                        GID = splitString[1];
                        String needmuGroup = splitString[2];
                        if (GetMu(GID) == true)
                        {
                            if (IsHaveMuReq(GID,user.UID) == true)
                            {
                                sendString = "addreturn#mumsg#已经发送过该请求";
                            }
                            else
                            {
                                if (IsHaveMember(GID, user.UID) == true)
                                {
                                    sendString= "addreturn#mumsg#你已在该群中";
                                }
                                else
                                {
                                    PutMuReq(GID, user.UID, needmuGroup);
                                    sendString = "addreturn#mutrue";
                                    SendToClient(user, "reloadlist");
                                }
                            }
                        }
                        else
                        {
                            sendString = "addreturn#msg#群组不存在";
                        }
                        SendToClient(user, sendString);
                        if (GetMu(GID) == true)
                        {
                            SqlDataReader auth = GetAuth(GID);
                            while (auth.Read())
                            {
                                if (GetUserByUID(auth[0].ToString()) != null)
                                {
                                    SendToClient(GetUserByUID(auth[0].ToString()), "reloadlist");
                                }
                            }
                            auth.Close();
                        }
                        break;
                    case "cancelfrdreq": //取消好友请求
                        toUID = splitString[1];
                        RemoveFriendReq(user.UID, toUID);
                        SendToClient(user, "reloadlist");
                        if (GetUserByUID(toUID) != null)
                        {
                            SendToClient(GetUserByUID(toUID), "reloadlist");
                        }
                        break;
                    case "cancelmureq": //取消入群请求
                        GID = splitString[1];
                        RemoveMuReq(GID, user.UID);
                        SendToClient(user, "reloadlist");
                        SqlDataReader oauth = GetAuth(GID);
                        while (oauth.Read())
                        {
                            if (GetUserByUID(oauth[0].ToString()) != null)
                            {
                                SendToClient(GetUserByUID(oauth[0].ToString()), "reloadlist");
                            }
                        }
                        oauth.Close();
                        break;
                    case "delfriends": //删除好友
                        toUID = splitString[1];
                        RemoveFriends(user.UID, toUID);
                        SendToClient(user, "reloadlist");
                        if (GetUserByUID(toUID) != null)
                        {
                            SendToClient(GetUserByUID(toUID), "reloadlist");
                        }
                        break;
                    case "exitmu": //退出群聊
                        GID = splitString[1];
                        RemoveMu(user.UID, GID);
                        SendToClient(user, "reloadlist");
                        SqlDataReader olme = GetMember(GID);
                        while (olme.Read())
                        {
                            if (GetUserByUID(olme[0].ToString()) != null && olme[0].ToString() != user.UID)
                            {
                                SendToClient(GetUserByUID(olme[0].ToString()), "reloadlist");
                            }
                        }
                        olme.Close();
                        break;
                    case "delmu": //解散群聊
                        GID = splitString[1];
                        DelMu(GID);
                        SendToClient(_userlist, "reloadlist");
                        break;
                    case "cgfrdgroup":
                        toUID = splitString[1];
                        string exgroup = splitString[2];
                        ChangeFrdGrp(user.UID, toUID, exgroup);
                        SendToClient(user, "reloadlist");
                        break;
                    case "cgmugroup":
                        GID = splitString[1];
                        string exmgroup = splitString[2];
                        ChangeMuGrp(user.UID, GID, exmgroup);
                        SendToClient(user, "reloadlist");
                        break;
                    case "getfriendreq": //获取好友请求列表
                        try
                        {
                            //格式：friendreq#UID1;username1/UID2;username2/.ed
                            sendString = "friendreq#";
                            SqlDataReader frdreq = GetFriendReq(user.UID);
                            while (frdreq.Read())
                            {
                                sendString += frdreq[0].ToString();
                                sendString += ";";
                                sendString += GetUsername(frdreq[0].ToString());
                                sendString += "/";
                            }
                            sendString += ".ed";
                            SendToClient(user, sendString);
                        }
                        catch
                        {
                            Console.WriteLine("{0}查找好友请求失败。", user.Username, toName);
                        }
                        break;
                    case "getputreq": //获取已发送好友请求列表
                        try
                        {
                            //格式：putreq#UID1;username1/UID2;username2/.ed
                            sendString = "putreq#";
                            SqlDataReader putreq = GetPutReq(user.UID);
                            while (putreq.Read())
                            {
                                sendString += putreq[0].ToString();
                                sendString += ";";
                                sendString += GetUsername(putreq[0].ToString());
                                sendString += "/";
                            }
                            sendString += ".ed";
                            SendToClient(user, sendString);
                        }
                        catch
                        {
                            Console.WriteLine("{0}查找已发送的好友请求失败。", user.Username);
                        }
                        break;
                    case "createmutual":
                        string groupname = splitString[1];
                        string groupsign = splitString[2];
                        CreateMu(user.UID, groupname,groupsign);
                        SendToClient(user, "reloadlist");
                        break;
                    case "getmureq": //获取群组申请列表
                        try
                        {
                            //格式：mureq#GID1;groupname;UID1;username/GID2;groupname;UID2;username/.ed
                            sendString = "mureq#";
                            SqlDataReader mureq = GetMuReq(user.UID);
                            while (mureq.Read())
                            {
                                sendString += mureq[0].ToString();
                                sendString += ";";
                                sendString += mureq[1].ToString();
                                sendString += ";";
                                sendString += mureq[2].ToString();
                                sendString += ";";
                                sendString += mureq[3].ToString();
                                sendString += "/";
                            }
                            sendString += ".ed";
                            SendToClient(user, sendString);
                        }
                        catch
                        {
                            Console.WriteLine("{0}查找群组请求失败。", user.Username);
                        }
                        break;
                    case "getputmureq":
                        try
                        {
                            //格式：putmureq#GID1;groupname1/GID2;groupname2/.ed
                            sendString = "putmureq#";
                            SqlDataReader putmureq = GetPutMureq(user.UID);
                            while (putmureq.Read())
                            {
                                sendString += putmureq[0].ToString();
                                sendString += ";";
                                sendString += putmureq[1].ToString();
                                sendString += "/";
                            }
                            sendString += ".ed";
                            SendToClient(user, sendString);
                        }
                        catch
                        {
                            Console.WriteLine("{0}查找已发送的好友请求失败。", user.Username);
                        }
                        break;
                    case "getgroups": //获取分组信息
                        sendString = "groups#";
                        sendString += GetGroups(user.UID);
                        sendString += ".ed";
                        SendToClient(user, sendString);
                        break;
                    case "getmugroups"://获取群分组信息
                        sendString = "mugroups#";
                        sendString += GetMuGroups(user.UID);
                        sendString += ".ed";
                        SendToClient(user, sendString);
                        break;
                    case "acceptreq"://同意好友申请
                        toUID = splitString[1];
                        String groups = splitString[2];
                        AddNewFriends(toUID, user.UID, groups);
                        RemoveFriendReq(toUID, user.UID);
                        RemoveFriendReq(user.UID, toUID);
                        sendString = "reloadlist#";
                        SendToClient(user, sendString);
                        if (GetUserByUID(toUID) != null)
                        {
                            SendToClient(GetUserByUID(toUID), "reloadlist");
                        }
                        break;
                    case "refusereq"://拒绝好友申请
                        toUID = splitString[1];
                        RemoveFriendReq(toUID, user.UID);
                        sendString = "reloadlist#";
                        SendToClient(user, sendString);
                        break;
                    case "acceptmureq": //同意入群申请
                        toUID = splitString[2];
                        GID = splitString[1];
                        JoinMutual(GID,toUID);
                        RemoveMuReq(GID, toUID);
                        sendString = "reloadlist#";
                        SendToClient(user, sendString);
                        SqlDataReader olmems= GetMember(GID);
                        while (olmems.Read())
                        {
                            if (GetUserByUID(olmems[0].ToString()) != null && olmems[0].ToString() != user.UID)
                            {
                                SendToClient(GetUserByUID(olmems[0].ToString()), sendString);
                            }
                        }
                        olmems.Close();
                        break;
                    case "refusemureq"://拒绝入群申请
                        toUID = splitString[2];
                        GID = splitString[1];
                        RemoveMuReq(GID, toUID);
                        sendString = "reloadlist#";
                        SendToClient(user, sendString);
                        SqlDataReader olauth = GetAuth(GID);
                        while (olauth.Read())
                        {
                            if (GetUserByUID(olauth[0].ToString()) != null)
                            {
                                SendToClient(GetUserByUID(olauth[0].ToString()), "reloadlist");
                            }
                        }
                        olauth.Close();
                        break;
                    case "getmutualchat"://获取群聊名称
                        SqlDataReader mutualname = GetMutual(user.UID);
                        //格式:mutualname#GID1;Groupname1;权限;分组;简介/GID2;Groupname;权限;分组;简介/.ed
                        sendString = "mutualname#";
                        while (mutualname.Read())
                        {
                            sendString += mutualname[0].ToString();
                            sendString += ";";
                            sendString += mutualname[1].ToString();
                            sendString += ";";
                            sendString += mutualname[2].ToString();
                            sendString += ";";
                            sendString += mutualname[3].ToString();
                            sendString += ";";
                            sendString += mutualname[4].ToString();
                            sendString += "/";
                        }
                        sendString += ".ed";
                        SendToClient(user, sendString);
                        mutualname.Close();
                        break;
                    case "getmutualmember":
                        GID = splitString[1];
                        SqlDataReader mem = GetMember(GID);
                        //格式:member#GID#UID1;auth1;Username1/UID2;auth2;Username2/.ed
                        sendString = "member#" + GID + "#";
                        while (mem.Read())
                        {
                            sendString += mem[0].ToString();
                            sendString += ";";
                            switch (mem[1].ToString())
                            {
                                case "0":
                                    sendString += "群主";
                                    break;
                                case "1":
                                    sendString += "管理员";
                                    break;
                                case "":
                                    sendString += "群员";
                                    break;
                            }
                            sendString += ";";
                            sendString += mem[2].ToString();
                            sendString += "/";
                        }
                        sendString += ".ed";
                        SendToClient(user, sendString);
                        mem.Close();
                        break;
                    case "muchat":
                        GID = splitString[1];
                        String mutualchatwords = splitString[3];
                        try
                        {
                            SndmuChat(user.UID, GID, mutualchatwords);
                            Console.WriteLine("{0}向{1}发送\"{2}\"成功。", user.Username, GID, mutualchatwords);
                        }
                        catch
                        {
                            Console.WriteLine("{0}向{1}发送的\"{2}\"失败了。", user.Username, GID, mutualchatwords);
                        }
                        SqlDataReader olmem = GetMember(GID);
                        while (olmem.Read())
                        {
                            if (GetUserByUID(olmem[0].ToString())!=null && olmem[0].ToString()!=user.UID)
                            {
                                //格式：munewmsg#GID#UID#Username#Sendtime#Sendwords
                                sendString = "munewmsg#" + GID + "#" + user.UID + "#" +GetUsername(olmem[0].ToString()) +"#"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "#" + mutualchatwords;
                                Console.WriteLine(sendString);
                                SendToClient(GetUserByUID(olmem[0].ToString()), sendString);
                            }

                        }
                        break;
                    case "userstate":
                        string usrname = splitString[1];
                        string usrsign = splitString[2];
                        UserState(user.UID, usrname, usrsign);
                        SendToClient(user, "userstate#"+usrname+"#"+usrsign);
                        SqlDataReader ol1 = FriendsReader(user.UID);
                        while (ol1.Read())
                        {
                            if (GetUserByUID(ol1[0].ToString()) != null)
                            {
                                SendToClient(GetUserByUID(ol1[0].ToString()), "reloadlist");
                            }
                        }
                        break;
                    case "mustate":
                        GID = splitString[1];
                        string muname = splitString[2];
                        string musign = splitString[3];
                        MuState(GID, muname, musign);
                        SqlDataReader omem = GetMember(GID);
                        while (omem.Read())
                        {
                            if (GetUserByUID(omem[0].ToString()) != null )
                            {
                                SendToClient(GetUserByUID(omem[0].ToString()), "reloadlist");
                            }
                        }
                        break;
                    case "rmmember":
                        GID = splitString[1];
                        toUID = splitString[2];
                        RemoveMember(GID, toUID);
                        SqlDataReader o1 = GetMember(GID);
                        while (o1.Read())
                        {
                            if (GetUserByUID(o1[0].ToString()) != null)
                            {
                                SendToClient(GetUserByUID(o1[0].ToString()), "reloadlist");
                            }
                        }
                        break;
                    case "aumember":
                        GID = splitString[1];
                        toUID = splitString[2];
                        AuMember(GID, toUID);
                        SqlDataReader o2 = GetMember(GID);
                        while (o2.Read())
                        {
                            if (GetUserByUID(o2[0].ToString()) != null)
                            {
                                SendToClient(GetUserByUID(o2[0].ToString()), "reloadlist");
                            }
                        }
                        break;
                    case "cgmember":
                        GID = splitString[1];
                        toUID = splitString[2];
                        CgMember(GID, toUID);
                        SqlDataReader o3 = GetMember(GID);
                        while (o3.Read())
                        {
                            if (GetUserByUID(o3[0].ToString()) != null)
                            {
                                SendToClient(GetUserByUID(o3[0].ToString()), "reloadlist");
                            }
                        }
                        break;
                    case "rmauthmember":
                        GID = splitString[1];
                        toUID = splitString[2];
                        RemoveAuMember(GID, toUID);
                        SqlDataReader o4 = GetMember(GID);
                        while (o4.Read())
                        {
                            if (GetUserByUID(o4[0].ToString()) != null)
                            {
                                SendToClient(GetUserByUID(o4[0].ToString()), "reloadlist");
                            }
                        }
                        SendToClient(GetUserByUID(toUID), "reloadlist");
                        break;
                    default:
                        Console.WriteLine("未知指令：{0}", receiveString);
                        break;
                }
            }
            _userlist.Remove(user);
            //SendToClient(userlist, "users#" + GetUsers());//发给其他用户
            client.Close();
            Console.WriteLine("当前连接用户数：{0}", _userlist.Count);
        }

        private static void SendToClient(User user, string str)
        {
            try
            {
                //将字符串写入网络流，此方法会自动附加字符串长度前缀
                user.bw.Write(str);
                user.bw.Flush();
                Console.WriteLine("向[{0}({1})]发送：{2}", user.Username, user.client.Client.RemoteEndPoint, str);
            }
            catch
            {
                Console.WriteLine("向[{0}({1})]发送信息失败", user.Username, user.client.Client.RemoteEndPoint);
            }
        }
        //重载方法
        private static void SendToClient(IEnumerable<User> users, string str)
        {
            foreach (var user in users)
            {
                try
                {
                    //将字符串写入网络流，此方法会自动附加字符串长度前缀
                    user.bw.Write(str);
                    user.bw.Flush();
                }
                catch
                {
                    Console.WriteLine("向[{0}({1})]发送信息失败", user.Username, user.client.Client.RemoteEndPoint);
                }
            }
        }
        
        /// <summary>
        /// 根据UID返回一个在线的User
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        private static User GetUserByUID(String UID)
        {
            User oluser = null;
            foreach (User u in _userlist)
            {
                if (u.UID.Equals(UID))
                {
                    oluser = u;
                    break;
                }
            }
            return oluser;
        }

        /// <summary>
        /// 获取本地的IP地址
        /// </summary>
        /// <returns></returns>
        static string GetLocalAddressIp()
        {
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }

        static string LoginCheck(User user)
        {
            SQL_u get = new SQL_u();
            String UID = "";
            get.Initialize("VANROY\\admin", "Chat");
            try { get.OpenConnection(); }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "false";
            }
            String Com = "SELECT Password,UID FROM user WHERE Account=\"" + user.Account + "\";";
            try
            {
                SqlDataReader read  = Reader(Com);
                if (read.HasRows == false)
                {
                    read.Close();
                    Console.WriteLine("账号不存在");
                    return "false";
                }
                else
                {
                    read.Read();
                    if (read[0].ToString() == user.Password)
                    {
                        UID = read[1].ToString();
                        read.Close();
                        return UID;
                    }
                    else
                    {
                        read.Close();
                        return "false";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("查询数据失败了！" + ex.Message);
                return "false";
            }
        }

        static string SignUp(string account, string password,string username)
        {
            SQL_u get = new SQL_u();
            get.Initialize("VANROY\\admin", "Chat");
            try { get.OpenConnection(); }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "无法连接到主机";
            }
            String Com = "SELECT Account FROM user WHERE Account=\"" + account + "\";";
            SqlDataReader read = Reader(Com);

            if (read.HasRows == false)
            {
                read.Close();
                Com = "INSERT INTO user (Account,Password,Groups,G_groups,Username) VALUES (\"" + account + "\",\"" + password + "\",\"我的好友;家人;同学;同事;\",\"我的群;常用群聊;\",\""+username+"\");";
                try
                {
                    get.GetInsert(Com);
                    get.CloseConnection();
                    return "注册成功";         
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return "无法注册";
                }
            }
            else
            {
                read.Close();
                return "用户名已存在";
            }

        }
        static string GetUsername(String UID)
        {
            String Username = "";
            string com = "SELECT Username FROM user WHERE UID=" + UID + ";";
            SqlDataReader read = Reader(com);
            read.Read();
            Username = read[0].ToString();
            read.Close();
            return Username;
        }
        static string GetUsersign(String UID)
        {
            String Username = "";
            string com = "SELECT Sign FROM user WHERE UID=" + UID + ";";
            SqlDataReader read = Reader(com);
            read.Read();
            Username = read[0].ToString();
            read.Close();
            return Username;
        }
        
        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        private static SqlDataReader FriendsReader(String UID)
        {
            SqlDataReader read = null;
            String Com = "SELECT UID2,Username,friends.groups,Sign FROM user,friends WHERE friends.UID2=user.UID AND friends.UID1=\"" + UID + "\"";
            try
            {
                read = Reader(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("查询数据失败了！" + ex.Message);
            }
            //friend.CloseConnection();
            return read;
        }
        /// <summary>
        /// 发送单人消息
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="toUID"></param>
        /// <param name="chatwords"></param>
        private static void Sndchat(String UID, String toUID, String chatwords)
        {

            String Com = "INSERT INTO chathis (UID1,UID2,Sendtime,Sendwords,isread) VALUES (\"" + UID + "\",\"" + toUID + "\",\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"" + chatwords + "\",\"0\");";
            try
            {
                Up(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("插入数据失败了！" + ex.Message);
            }
        }
        /// <summary>
        /// 发送多人消息
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="GID"></param>
        /// <param name="chatwords"></param>
        private static void SndmuChat(String UID,String GID, String chatwords)
        {
            String Com = "INSERT INTO groupchathis (GID,UID,Sendtime,Sendwords) VALUES (\"" + GID + "\",\"" + UID + "\",\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"" + chatwords + "\");";
            Up(Com);
        }
        /// <summary>
        /// 获取未读信息
        /// </summary>
        /// <param name="toUID"></param>
        /// <returns></returns>
        private static SqlDataReader GetNoRead(string toUID)
        {
            String Com = "SELECT chathis.UID1,user.Username,chathis.Sendtime,chathis.Sendwords FROM chathis,user WHERE chathis.UID1=user.UID AND chathis.isread=0 AND chathis.UID2=" + toUID + ";";
            SqlDataReader read = null;
            try
            {
                read = Reader(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("查找数据失败了！" + ex.Message);
            }
            return read;

        }
        /// <summary>
        /// 将未读消息全部标为已读
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="toUID"></param>
        private static void Readed(string UID, String toUID)
        {
            SqlDataReader read = null;
            String Com = "SELECT * FROM chathis WHERE UID2=\"" + toUID + "\" AND isread=0;";
            read = Reader(Com);
            read.Read();
            if (read.HasRows == true)
            {
                read.Close();
                Com = "UPDATE chathis SET isread=1 WHERE UID2=" + toUID + " AND UID1=\"" + UID + "\" AND isread=0;";
                St(Com);
            }
            else
            {
                read.Close();
            }

        }
        /// <summary>
        /// 将群组未读消息标为已读
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="GID"></param>
        private static void MuReaded(string UID,string GID)
        {
            string Com = "UPDATE groupmembers SET closetime=\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\" WHERE GID="+GID+" AND UID="+UID+";";
            St(Com);
        }
        /// <summary>
        /// 获取群组未读消息
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        private static SqlDataReader GetMuNoRead(string UID)
        {
            String Com = "SELECT closetime,GID FROM groupmembers WHERE UID="+UID+";";
            SqlDataReader read = null;
            read = Reader(Com);
            if (read.HasRows == true)
            {
                read.Read();
                String closetime = read[0].ToString();
                read.Close();
                Com = "SELECT * FROM groupchathis WHERE DATE_FORMAT(Sendtime,'%Y-%m-%d %H:%i')>=DATE_FORMAT('" + closetime + "','%Y-%m-%d %H:%i');";
                read = Reader(Com);
            }       
            return read;
        }
        /// <summary>
        /// 获取指向Username的所有好友请求
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        private static SqlDataReader GetFriendReq(String UID)
        {
            SqlDataReader read = null;
            String Com = "SELECT UID1 FROM frdreq WHERE UID2=\"" + UID + "\";";
            try
            {
                read = Reader(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("查找数据失败了！" + ex.Message);
            }
            return read;
        }
        /// <summary>
        /// 获取Username指向toname的所有好友请求
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="toUID"></param>
        /// <returns></returns>
        private static SqlDataReader GetFriendReq(String UID, String toUID)
        {
            SqlDataReader read = null;
            String Com = "SELECT * FROM frdreq WHERE UID1=\"" + UID + "\" AND UID2=\"" + toUID + "\";";
            try
            {
                read = Reader(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("查找数据失败了！" + ex.Message);
            }
            return read;
        }
        /// <summary>
        /// 获取UID的所有发出的好友请求
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        private static SqlDataReader GetPutReq(String UID)
        {
            SqlDataReader read = null;
            String Com = "SELECT UID2 FROM frdreq WHERE UID1=\"" + UID + "\";";
            try
            {
                read = Reader(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("查找数据失败了！" + ex.Message);
            }
            return read;
        }
        /// <summary>
        /// 获取指向UID的所有群组请求
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        private static SqlDataReader GetMuReq(string UID)
        {
            SqlDataReader read = null;
            String Com = " SELECT groups.GID,groups.Groupname,user.UID,user.Username FROM user,groups,groupreq,groupmembers WHERE groups.GID=groupreq.GID AND user.UID=groupreq.UID AND groupmembers.GID=groups.GID AND groupmembers.UID="+UID+" AND groupmembers.auth<2;";
            try
            {
                read = Reader(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("查找数据失败了！" + ex.Message);
            }
            return read;
        }
        
        private static SqlDataReader GetPutMureq(string UID)
        {
            SqlDataReader read = null;
            String Com = "SELECT groups.GID,groups.Groupname FROM groups,groupreq WHERE groups.GID=groupreq.GID AND groupreq.UID="+UID+";";
            try
            {
                read = Reader(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("查找数据失败了！" + ex.Message);
            }
            return read;
        }
        private static SqlDataReader GetMuReq(string GID,string UID)
        {
            SqlDataReader read = null;
            String Com = "SELECT * FROM groupreq WHERE GID=" + GID + " AND UID=" + UID + ";";
            try
            {
                read = Reader(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("查找数据失败了！" + ex.Message);
            }
            return read;
        }
        /// <summary>
        /// 查看是否存在好友请求
        /// </summary>
        /// <param name="UID1"></param>
        /// <param name="UID2"></param>
        /// <returns></returns>
        private static bool IsHaveReq(String UID1,String UID2)
        {
            String Com = "SELECT * FROM frdreq WHERE UID1=" + UID1 + " AND UID2=" + UID2 + ";";
            SqlDataReader read = null;
            read = Reader(Com);
            if (read.HasRows == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查看是否存在入群请求
        /// </summary>
        /// <param name="GID"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        private static bool IsHaveMuReq(string GID,string UID)
        {
            String Com = "SELECT * FROM groupreq WHERE GID=" + GID + " AND UID=" + UID + ";";
            SqlDataReader read = null;
            read = Reader(Com);
            if (read.HasRows == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查看是否已经是好友
        /// </summary>
        /// <param name="UID1"></param>
        /// <param name="UID2"></param>
        /// <returns></returns>
        private static bool IsHaveFrd(String UID1, String UID2)
        {
            String Com = "SELECT * FROM friends WHERE UID1=" + UID1 + " AND UID2=" + UID2 + ";";
            SqlDataReader read = null;
            read = Reader(Com);
            if (read.HasRows == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查看是否已经入群
        /// </summary>
        /// <param name="GID"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        private static bool IsHaveMember(String GID,String UID)
        {
            String Com = "SELECT * FROM groupmembers WHERE GID=" + GID + " AND UID=" + UID + ";";
            SqlDataReader read = null;
            read = Reader(Com);
            if (read.HasRows == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 发送好友请求
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="toUID"></param>
        /// <param name="group"></param>
        private static void PutFriendReq(String UID, String toUID, string group)
        {
            String Com = "INSERT INTO frdreq (UID1,UID2,expgroup) VALUES (\"" + UID + "\",\"" + toUID + "\",\"" + group + "\");";
            try
            {
                Up(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("插入数据失败了！" + ex.Message);
            }
        }
        /// <summary>
        /// 发送入群请求
        /// </summary>
        /// <param name="GID"></param>
        /// <param name="UID"></param>
        /// <param name="group"></param>
        private static void PutMuReq(string GID,string UID,string group)
        {
            String Com = "INSERT INTO groupreq (GID,UID,expgroup) VALUES (\"" + GID + "\",\"" + UID + "\",\"" + group + "\");";
            try
            {
                Up(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("插入数据失败了！" + ex.Message);
            }
        }
        /// <summary>
        /// 删除好友申请条目
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="toUID"></param>
        private static void RemoveFriendReq(String UID, String toUID)
        {
            String Com = "DELETE FROM frdreq WHERE UID1=\"" + UID + "\" AND UID2=\"" + toUID + "\";";
            try
            {
                Rm(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("删除数据失败了！" + ex.Message);
            }
        }
        /// <summary>
        /// 删除好友条目
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="toUID"></param>
        private static void RemoveFriends(string UID,string toUID)
        {
            String Com = "DELETE FROM friends WHERE UID1=\"" + UID + "\" AND UID2=\"" + toUID + "\";";
            try
            {
                Rm(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("删除数据失败了！" + ex.Message);
            }
            Com = "DELETE FROM friends WHERE UID2=\"" + UID + "\" AND UID1=\"" + toUID + "\";";
            try
            {
                Rm(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("删除数据失败了！" + ex.Message);
            }
        }
        /// <summary>
        /// 删除成员条目
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="GID"></param>
        private static void RemoveMu(string UID,string GID)
        {
            String Com = "DELETE FROM groupmembers WHERE UID=\"" + UID + "\" AND GID=\"" + GID + "\";";
            try
            {
                Rm(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("删除数据失败了！" + ex.Message);
            }
        }
        private static void DelMu(string GID)
        {
            string Com = "DELETE FROM groups WHERE GID="+GID+";";
            try
            {
                Rm(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("删除数据失败了！" + ex.Message);
            }
            Com= "DELETE FROM groupmembers WHERE GID=" + GID + ";";
            try
            {
                Rm(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("删除数据失败了！" + ex.Message);
            }
        }
        /// <summary>
        /// 修改好友分组
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="toUID"></param>
        /// <param name="exgroup"></param>
        private static void ChangeFrdGrp(string UID,string toUID,string exgroup)
        {
            String Com = "UPDATE friends SET groups=\""+exgroup+"\" WHERE UID1="+UID+" AND UID2= "+toUID+"";
            try
            {
                St(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("删除数据失败了！" + ex.Message);
            }
        }
        private static void ChangeMuGrp(string UID, string GID, string exgroup)
        {
            String Com = "UPDATE groupmembers SET G_group=\"" + exgroup + "\" WHERE UID=" + UID + " AND GID= " + GID + "";
            try
            {
                St(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("删除数据失败了！" + ex.Message);
            }
        }
        /// <summary>
        /// 创建群聊
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="groupname"></param>
        /// <param name="groupsign"></param>
        private static void CreateMu(string UID,string groupname,string groupsign)
        {
            String Com = "INSERT INTO groups (Groupname,CreatorUID,Groupsine) VALUES (\"" + groupname + "\","+UID+",\""+groupsign+"\");";
            try
            {
                Up(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("插入数据失败了！" + ex.Message);
            }
            Com = "SELECT GID FROM groups WHERE Groupname=\""+groupname+"\" AND CreatorUID="+UID+";";
            SqlDataReader read = Reader(Com);
            read.Read();
            Com = "INSERT INTO groupmembers (GID,UID,auth,jointime,closetime,G_group) VALUES (" + read[0].ToString() + "," + UID + ",0,\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"我的群\");";
            read.Close();
            try
            {
                Up(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("插入数据失败了！" + ex.Message);
            }
        }
        /// <summary>
        /// 添加一对新好友
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="toUID"></param>
        /// <param name="usergroup"></param>
        private static void AddNewFriends(String UID, String toUID, String usergroup)
        {
            String tonamegroup;
            SqlDataReader frdreqreader = GetFriendReq(UID, toUID);
            frdreqreader.Read();
            tonamegroup = frdreqreader[2].ToString();
            frdreqreader.Close();
            String Com = "INSERT INTO friends (UID1,UID2,groups) VALUES (\"" + UID + "\",\"" + toUID + "\",\"" + tonamegroup + "\");";
            try
            {
                Up(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("插入数据失败了！11111111" + ex.Message);
            }
            Com = "INSERT INTO friends (UID1,UID2,groups) VALUES (\"" + toUID + "\",\"" + UID + "\",\"" + usergroup + "\");";
            try
            {
                Up(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("插入数据失败了！22222222" + ex.Message);
            }
        }
        /// <summary>
        /// 加入群组
        /// </summary>
        /// <param name="GID"></param>
        /// <param name="UID"></param>
        private static void JoinMutual(String GID, String UID)
        {
            String mugroup;
            SqlDataReader grpreqreader = GetMuReq(GID, UID);
            grpreqreader.Read();
            mugroup = grpreqreader[2].ToString();
            grpreqreader.Close();
            String Com = "INSERT INTO groupmembers (GID,UID,auth,jointime,closetime,G_group) VALUES (" + GID + "," + UID + ",2,\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"" + mugroup + "\");";
            Console.WriteLine(Com);
            try
            {
                Up(Com);
            }
            catch(Exception ex)
            {
                Console.WriteLine("插入数据失败了！22222222" + ex.Message);
            }
        }
        /// <summary>
        /// 删除入群申请条目
        /// </summary>
        /// <param name="GID"></param>
        /// <param name="UID"></param>
        private static void RemoveMuReq(string GID,string UID)
        {
            String Com = "DELETE FROM groupreq WHERE GID="+GID+" AND UID="+UID+"";
            try
            {
                Rm(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("删除数据失败了！" + ex.Message);
            }
        }
        /// <summary>
        /// 获取管理员列表
        /// </summary>
        /// <param name="GID"></param>
        /// <returns></returns>
        private static SqlDataReader GetAuth(string GID)
        {
            SqlDataReader read = null;
            String Com = "SELECT UID FROM groupmembers WHERE GID=" + GID + " AND auth<2";
            try
            {
                read = Reader(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("查找数据失败了！" + ex.Message);
            }
            return read;
        }
        /// <summary>
        /// 添加新分组
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="toName"></param>
        /// <param name="group"></param>
        private static void PutNewGroups(String Username, String toName, String group)
        {
            SQL_u isrt = new SQL_u();
            isrt.Initialize("VANROY\\admin", "Chat");
            if (isrt.OpenConnection())
            {
                Console.WriteLine("正在添加");
            }
            String Com = "INSERT INTO groups (Username, Friend, groups) VALUES(\"" + Username + "\", \"" + toName + "\", \"" + group + "\");";
            try
            {
                isrt.GetInsert(Com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("插入数据失败了！" + ex.Message);
            }
            isrt.CloseConnection();
        }
        /// <summary>
        /// 用户是否存在
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        private static bool JugeUserIsExit(String UID)
        {
            SqlDataReader read = null;
            String Com = "SELECT UID FROM user WHERE UID=\"" + UID + "\";";
            read = Reader(Com);
            if (read.HasRows == false)
            {
                read.Close();
                return false;
            }
            else
            {
                read.Close();
                return true;
            }
        }
        /// <summary>
        /// 群组是否存在
        /// </summary>
        /// <param name="GID"></param>
        /// <returns></returns>
        private static bool GetMu(string GID)
        {
            SqlDataReader read = null;
            String Com = "SELECT GID FROM groups WHERE GID=\"" + GID + "\";";
            read = Reader(Com);
            if (read.HasRows == false)
            {
                read.Close();
                return false;
            }
            else
            {
                read.Close();
                return true;
            }
        }
        /// <summary>
        /// 获取用户分组信息
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        private static String GetGroups(String UID)
        {
            String Com = "SELECT Groups FROM user WHERE UID=\"" + UID + "\";";
            SqlDataReader read = Reader(Com);
            read.Read();
            string grp = read[0].ToString();
            return grp;
        }
        private static string GetMuGroups(string UID)
        {
            String Com = "SELECT G_Groups FROM user WHERE UID=\"" + UID + "\";";
            SqlDataReader read = Reader(Com);
            read.Read();
            string grp = read[0].ToString();
            return grp;
        }
        /// <summary>
        /// 获取用户的群组
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        private static SqlDataReader GetMutual(string UID)
        {
            SqlDataReader read = null;
            string com = " SELECT groups.GID,groups.Groupname,auth,G_group,groups.Groupsine FROM groups,groupmembers WHERE UID=" + UID + " AND groups.GID=groupmembers.GID;";
            read = Reader(com);
            return read;
        }
        private static SqlDataReader GetMember(string GID)
        {
            SqlDataReader read = null;
            string com = " SELECT groupmembers.UID,auth,user.Username FROM groupmembers,user WHERE groupmembers.UID=user.UID AND GID=" + GID + ";";
            read = Reader(com);
            return read;
        }
        /// <summary>
        /// 修改信息类
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="username"></param>
        /// <param name="sign"></param>
        private static void UserState(string UID,String username,string sign)
        {
            string Com= "UPDATE user SET Username=\""+username+"\" WHERE UID=" + UID + ";";
            St(Com);
            Com = "UPDATE user SET Sign=\"" + sign + "\" WHERE UID=" + UID + ";";
            St(Com);
        }
        private static void MuState(string GID, String muname, string sign)
        {
            string Com = "UPDATE groups SET Groupname=\"" + muname + "\" WHERE GID=" + GID + ";";
            St(Com);
            Com = "UPDATE groups SET Groupsine=\"" + sign + "\" WHERE GID=" + GID + ";";
            St(Com);
        }
        /// <summary>
        /// 群成员管理类
        /// </summary>
        /// <param name="GID"></param>
        /// <param name="UID"></param>
        private static void RemoveMember(string GID, string UID)
        {
            string Com= "DELETE FROM groupmembers WHERE GID=" + GID + " AND UID=" + UID + ";";
            Rm(Com);
        }
        private static void AuMember(string GID, string UID)
        {
            string Com = "UPDATE groupmembers SET auth=1 WHERE GID=" + GID + " AND UID=" + UID + ";";
            Console.WriteLine(Com);
            St(Com);
        }
        private static void CgMember(string GID, string UID)
        {
            string Com = "UPDATE groupmembers SET auth=2 WHERE GID=" + GID + " AND auth=0;";
            St(Com);
            Com = "UPDATE groupmembers SET auth=0 WHERE GID=" + GID + " AND UID=" + UID + ";";
            St(Com);
        }
        private static void RemoveAuMember(string GID, string UID)
        {
            string Com = "UPDATE groupmembers SET auth=2 WHERE GID=" + GID + " AND UID=" + UID + ";";
            St(Com);
        }
        /// <summary>
        /// 数据库类
        /// </summary>
        /// <param name="com"></param>
        /// <returns></returns>
        private static SqlDataReader Reader(String com)
        {
            SQL_u get = new SQL_u();
            SqlDataReader read = null;
            // 使用 SQL Server 服务器地址、数据库名和 SQL Server 登录凭证
            get.Initialize("VANROY\\admin", "Chat");
            if (get.OpenConnection())
            {
                Console.WriteLine("连接数据库成功");
            }
            else
            {
                Console.WriteLine("连接数据库失败");
            }
            try
            {
                read = get.GetReader(com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("查找数据失败了！" + ex.Message);
            }
            return read;
        }
        
        private static void Up(string com)
        {
            SQL_u isrt = new SQL_u();
            isrt.Initialize("VANROY\\admin", "Chat");
            if (isrt.OpenConnection())
            {
                Console.WriteLine("连接数据库成功");
            }
            else
            {
                Console.WriteLine("连接数据库失败");
            }
            try
            {
                isrt.GetInsert(com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("插入数据失败了！" + ex.Message);
            }
            isrt.CloseConnection();
        }
        
        private static void Rm(string com)
        {
            SQL_u rmv = new SQL_u();
            rmv.Initialize("VANROY\\admin", "Chat");
            if (rmv.OpenConnection())
            {
                Console.WriteLine("连接数据库成功");
            }
            else
            {
                Console.WriteLine("连接数据库失败");
            }
            try
            {
                rmv.GetDel(com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("删除数据失败了！" + ex.Message);
            }
            rmv.CloseConnection();
        }
        
        private static void St(string com)
        {
            SQL_u st = new SQL_u();
            st.Initialize("VANROY\\admin", "Chat");
            if (st.OpenConnection())
            {
                Console.WriteLine("连接数据库成功");
            }
            else
            {
                Console.WriteLine("连接数据库失败");
            }
            try
            {
                st.GetUpdate(com);
            }
            catch (Exception ex)
            {
                Console.WriteLine("修改数据失败了！" + ex.Message);
            }
            st.CloseConnection();
        }
    }
}
