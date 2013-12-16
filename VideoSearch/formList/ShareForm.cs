using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace VideoSearch
{
    public partial class ShareForm : Form
    {
        private static readonly ShareForm INTERFACE = new ShareForm();
        private ShareForm()
        {
            InitializeComponent();
            XMLService.initShareConfig();
            this.sourceDirText.Text = WebConstant.SHARE_DIRS_STRING;
            this.allowIpText.Text = WebConstant.ALLOW_IP_TABLE;
            this.shutDownPasswordText.Text = WebConstant.SHUT_DOWN_PASSWORD;
        }
        public static ShareForm getInterface()
        {
            return ShareForm.INTERFACE;
        }

        private void explanationLabel_Click(object sender, EventArgs e)
        {
            Process.Start(Constant.SHARE_SERVICE_EXPLAN_URL);
        }

        private void sourceDirText_Click(object sender, EventArgs e)
        {
            Constant.folderBrowserDialog.ShowDialog();
            if (this.sourceDirText.Text == null || this.sourceDirText.Text.Equals(""))
            {
                this.sourceDirText.Text = Constant.folderBrowserDialog.SelectedPath;
            }
            else
            {
                this.sourceDirText.Text = this.sourceDirText.Text +"|"+ Constant.folderBrowserDialog.SelectedPath;
            }
            isNeedUpdateConfig = true;
        }
        private bool isNeedUpdateConfig = false;
        private void startService_Click(object sender, EventArgs e)
        {
            if (WebConstant.webService == null)
            {
                WebConstant.webService = new WebService();
            }
            Message msg = WebConstant.webService.start();
            this.stateLabel.Text = msg.msg;
            IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
            string myip = null;
            foreach (IPAddress ip in IpEntry.AddressList)
            {
                if (ip.ToString().Length <= 15)
                {
                    myip = ip.ToString();
                    break;
                }
            }
            if (myip != null)
            {
                WebConstant.LOCAL_URL = myip;
                Process.Start("http://" + myip + ":9999");
            }
            this.saveShareConfig();
        }

        private void stopServiceLabel_Click(object sender, EventArgs e)
        {
            this.stateLabel.Text = "正在停止服务………………";
            if (WebConstant.webService != null)
            {
                this.stateLabel.Text = WebConstant.webService.stop();
                WebConstant.webService = null;
            }
        }

        private void allowIpText_TextChanged(object sender, EventArgs e)
        {
            isNeedUpdateConfig = true;
        }
        private void saveShareConfig()
        {
            if (!isNeedUpdateConfig) return;
            Hashtable configTable = new Hashtable();
            configTable.Add("shareDir", this.sourceDirText.Text);
            configTable.Add("allowIp", this.allowIpText.Text);
            configTable.Add("password", this.shutDownPasswordText.Text);
            XMLService.saveShareConfigXMl(configTable);
            WebConstant.SHARE_DIRS = this.sourceDirText.Text.Split('|');
            WebConstant.ALLOW_IP_TABLE = this.allowIpText.Text;
            WebConstant.SHUT_DOWN_PASSWORD = this.shutDownPasswordText.Text;
            isNeedUpdateConfig = false;
        }

        private void shutDownPasswordText_TextChanged(object sender, EventArgs e)
        {
            isNeedUpdateConfig = true;
        }
    }
}
