using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VideoSearch
{
    public sealed partial class SetForm : Form
    {
        private SetForm()
        {
            InitializeComponent();
        }
        private static readonly SetForm INTERFACE = new SetForm();
        public static SetForm getInterface()
        {
            return SetForm.INTERFACE;
        }

        private void setFormSureBtn_Click(object sender, EventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)this.serviceAddressCombo.SelectedItem;
            string serviceIp = item.Value;
            if (System.Text.RegularExpressions.Regex.IsMatch(serviceIp,Constant.SERVIC_CHECK_REGEX))
            {
                Hashtable configTable = new Hashtable();
                configTable.Add("serviceIp", serviceIp);
                configTable.Add("defaultDir", this.defaultDownloadDirTextBox.Text);
                XMLService.saveConfigXMl(configTable);
                Constant.SERVICE_ADDRESS = serviceIp;
                Constant.DEFAULT_DOWNLOAD_DIR = this.defaultDownloadDirTextBox.Text;
                MessageBox.Show("配置成功");
                this.Hide();
            }else{
                MessageBox.Show(MsgString.SERVICE_IP_NOT_RIGHT);
            }
        }

        private static Hashtable servetList = null;
        private void serviceAddressCombo_Click(object sender, EventArgs e)
        {
            if (servetList == null)
            { 
                servetList = XMLService.initServerList();
                foreach (string key in servetList.Keys)
                {
                    this.serviceAddressCombo.Items.Add(new ComboBoxItem(key,(string)servetList[key]));
                }
                this.serviceAddressCombo.Items.Add(new ComboBoxItem("--更新服务器列表","0"));
                this.serviceAddressCombo.Items.Add(new ComboBoxItem("--报告新的服务器地址", "1"));
                this.serviceAddressCombo.Items.Add(new ComboBoxItem("--没有我的学校", "2"));
            }
        }

        private void serviceAddressCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)this.serviceAddressCombo.SelectedItem;
            switch (item.Value)
            {
                case "0":
                    {
                        servetList = XMLService.updateServerList();
                        this.serviceAddressCombo.Items.Clear();
                        foreach (string key in servetList.Keys)
                        {
                            this.serviceAddressCombo.Items.Add(new ComboBoxItem(key, (string)servetList[key]));
                        }
                        this.serviceAddressCombo.Items.Add(new ComboBoxItem("==更新服务器列表", "0"));
                        this.serviceAddressCombo.Items.Add(new ComboBoxItem("==报告新的服务器地址", "1"));
                        this.serviceAddressCombo.Items.Add(new ComboBoxItem("==没有我的学校", "2"));
                        break;
                    }
                case "1":
                    {
                        System.Diagnostics.Process.Start(Constant.ONLINE_HELP_URL);
                        break;
                    }
                case "2":
                    {
                        System.Diagnostics.Process.Start(Constant.HOW_TO_GET_MY_SCHOOL_SERVER);
                        break;
                    }
            }
        }

        private void defaultDownloadDirTextBox_Click(object sender, EventArgs e)
        {
            Constant.folderBrowserDialog.SelectedPath = Constant.DEFAULT_DOWNLOAD_DIR;
            Constant.folderBrowserDialog.ShowDialog();
            this.defaultDownloadDirTextBox.Text = Constant.folderBrowserDialog.SelectedPath;
            Constant.DEFAULT_DOWNLOAD_DIR = Constant.folderBrowserDialog.SelectedPath;
        }

        private void sourceCodeAddressBtn_Click(object sender, EventArgs e)
        {
            new AboutSoftWareForm().Show();
        }

        private void resetSoftwareBtn_Click(object sender, EventArgs e)
        {
            File.Delete(Constant.CONFIG_FILE_PATH);
            for (int i = 0; i < 8; i++)
            {
                File.Delete(i + ".ahnu");
            }
            DirectoryInfo dir = new DirectoryInfo(System.Environment.CurrentDirectory);
            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Name.IndexOf(Constant.TOTAL_FILE_PATH) > -1)
                {
                    file.Delete();
                }
            }
            File.Delete(Constant.SERVET_LIST_FILE_PATH);
            MessageBox.Show(MsgString.RESET_SOFTWARE_SUCCESS);
        }        
    }
}
