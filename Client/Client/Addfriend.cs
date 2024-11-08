﻿using System;
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
    public partial class Addfriend : Form
    {
        public bool IsWork { get; set; }
        public BinaryWriter Bw { get; set; }
        public Addfriend()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Addfriend_FormClosed(object sender, FormClosedEventArgs e)
        {
            ListForm.RemoveAddFrd();
        }
        private void Addfriend_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < ListForm.GroupList.Count; i++)
            {
                cb_group.Items.Add(ListForm.GroupList[i]);
            }
            for (int i = 0; i < ListForm.MuGroupList.Count; i++)
            {
                cb_mugroup.Items.Add(ListForm.MuGroupList[i]);
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if (tb_toUID.Text == "")
            {
                lb_state.Text = "用户UID不能为空";
                return;
            }
            if(cb_group.Text=="")
            {
                lb_state.Text = "分组不能为空";
                return;
            }
            String sndmsg = "addfriend#";
            sndmsg += tb_toUID.Text;
            sndmsg += "#";
            sndmsg += cb_group.Text;
            Bw.Write(sndmsg);
        }
        public void get_msg(String msg)
        {
            lb_state.Text = msg;
        }

        public void get_true()
        {
            MessageBox.Show("添加成功");
            //Bw.Write("getputreq");
            this.Close();
        }

        private void btn_musend_Click(object sender, EventArgs e)
        {
            if (tb_GID.Text == "")
            {
                lb_mustate.Text = "GID不能为空";
                return;
            }
            if (cb_mugroup.Text == "")
            {
                lb_mustate.Text = "分组不能为空";
                return;
            }
            String sndmsg = "addgroup#";
            sndmsg += tb_GID.Text;
            sndmsg += "#";
            sndmsg += cb_mugroup.Text;
            Bw.Write(sndmsg);
        }
        public void GetMumsg(String msg)
        {
            lb_mustate.Text = msg;
        }

        public void GetMutrue()
        {
            MessageBox.Show("添加成功");
            //Bw.Write("getputreq");
            this.Close();
        }
    }
}
