using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VideoSearch;

namespace VideoSearch
{
    public sealed partial class MainForm : Form
    {
        
        public static void disposeHolders()
        {
            if (Constant.setForm != null) Constant.setForm.Dispose();
            if (Constant.exploreForm != null) Constant.exploreForm.Dispose();
            if (Constant.downloadForm != null) Constant.downloadForm.Dispose();
            if (Constant.shareForm != null) Constant.shareForm.Dispose();
        }
        private MainForm()
        {
            InitializeComponent();
            XMLService.initConfig();
            Constant.mainForm = this;
        }
        private static readonly MainForm INTERFACE=new MainForm();

        public static MainForm getInterface()
        {
            return MainForm.INTERFACE;
        }

        private CycleList<List<MovieCata>> list = new CycleList<List<MovieCata>>(Constant.RECORD_LIST_SIZE);
        private void backBtn_Click(object sender, EventArgs e)
        {
            if (this.list.getCount() <= 1) return;
            this.showDataInRecordList(this.list.getLast());
        }

        private void forwardBtn_Click(object sender, EventArgs e)
        {
            if (this.list.getCount() <= 1) return;
            this.showDataInRecordList(this.list.getNext());
        }
        
        private void goBtn_Click(object sender, EventArgs e)
        {
            if (keyValue.Text.Length > 12)
            {
                this.setRunState(MsgString.NOW_IS_DOING_WORK.Replace("《%name%》", ""));
            }
            else
            {
                this.setRunState(MsgString.NOW_IS_DOING_WORK.Replace("%name%", keyValue.Text));
            }
            Thread th = new Thread(analyzeKeyValue);
            th.Start((object)keyValue.Text);
        }
        //down enter button in keyValue
        private void keyValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.goBtn_Click(sender, e);
        }
        public void analyzeKeyValue(object keyValue)
        {
            Message msg = AnalyzeService.analyzeKeyValue((string)keyValue);
            if (msg.isSucceed)
            {
                this.list.add(msg.movieCataList);
                this.showDataInRecordList(msg.movieCataList);
                this.setRunState(msg.msg);
                while (true)
                {
                    if (Movie.isAllComplete()) break;
                    Thread.Sleep(1000);
                    this.refreshRecordList();
                }
                this.setRunState(MsgString.ALL_MOVIE_ADDRESS_ANALYZE_COMPELTE);
            }
            else
            {
                this.setRunState(msg.msg);
            }
        }
        private void setBtn_Click(object sender, EventArgs e)
        {
            if (Constant.setForm == null)
            {
                Constant.setForm = SetForm.getInterface();
            }
            Constant.setForm.Show();
            Constant.setForm.Focus();
        }

        private void newResourceBtn_Click(object sender, EventArgs e)
        {
            Message msg = AnalyzeService.newResource();
            if (msg.isSucceed)
            {
                this.list.add(msg.movieCataList);
                this.showDataInRecordList(msg.movieCataList);
                this.setRunState(msg.msg);
            }
            else
            {
                this.setRunState(msg.msg);
            }
        }

        private void allResourceBtn_Click(object sender, EventArgs e)
        {
            Message msg = AnalyzeService.allResource();
            if (msg.isSucceed)
            {
                this.list.add(msg.movieCataList);
                this.showDataInRecordList(msg.movieCataList);
                this.setRunState(msg.msg);
            }
            else
            {
                this.setRunState(msg.msg);
            }
        }

        private void showDownloadFormBtn_Click(object sender, EventArgs e)
        {
            if (Constant.downloadForm == null)
            {
                Constant.downloadForm = DownLoadForm.getInterface();
            }
            Constant.downloadForm.Show();
            Constant.downloadForm.Focus();
        }

        private void showExploreModelBtn_Click(object sender, EventArgs e)
        {
            if (Constant.exploreForm == null)
            {
                Constant.exploreForm = ExploreForm.getInterface();
            }
            Constant.exploreForm.Show();
            Constant.exploreForm.Focus();
        }

        private void showOnlineHelpBtn_Click(object sender, EventArgs e)
        {
            Process.Start(Constant.ONLINE_HELP_URL);
        }

        private void showAboutUsBtn_Click(object sender, EventArgs e)
        {
            if (Constant.shareForm == null)
            {
                Constant.shareForm = ShareForm.getInterface();
            }
            Constant.shareForm.Show();
            Constant.shareForm.Focus();
        }

        //Start depute area
        public delegate void SETRUNSTATE(string str);
        public void setRunState(string str)
        {
            if (this.runStateLabel.InvokeRequired)
            {
                SETRUNSTATE set = new SETRUNSTATE(setRunState);
                this.Invoke(set, new object[] { str });
            }
            else
            {
                this.runStateLabel.Text = str;
            }
        }
        private delegate void SHOWDATAINRECORDLIST(List<MovieCata> dataList);
        private void showDataInRecordList(List<MovieCata> dataList)
        {
            if (this.recordList.InvokeRequired)
            {
                SHOWDATAINRECORDLIST set = new SHOWDATAINRECORDLIST(showDataInRecordList);
                this.Invoke(set, new object[] { dataList });
            }
            else
            {
                if (dataList == null) return;
                recordList.BeginUpdate();
                recordList.Items.Clear();
                ListViewItem item;
                if (dataList.Count == 1 && dataList[0].movieList.Count>0)
                {
                    this.recordList.Columns[1].Text = "下载地址";
                    this.recordList.CheckBoxes = true;
                    this.recordList.ContextMenuStrip = this.movieMenu;
                    foreach (Movie movie in dataList[0].movieList)
                    {
                        item = new ListViewItem(movie.name);
                        item.SubItems.Add(movie.url);
                        recordList.Items.Add(item);
                    }
                }
                else
                {
                    this.recordList.Columns[1].Text = "影片简介";
                    this.recordList.CheckBoxes = false;
                    this.recordList.ContextMenuStrip = this.movieCataMenu;
                    foreach (MovieCata movie in dataList)
                    {
                        item = new ListViewItem(movie.name);
                        item.SubItems.Add(movie.describe);
                        recordList.Items.Add(item);
                    }
                }
                recordList.EndUpdate();
            }
        }
        private delegate void REFRESHRECORDLIST();
        private void refreshRecordList()
        {
            if (this.recordList.InvokeRequired)
            {
                REFRESHRECORDLIST set = new REFRESHRECORDLIST(refreshRecordList);
                this.Invoke(set,new object[] {});
            }
            else
            {
                List<MovieCata> dataList = this.list.getNow();
                recordList.BeginUpdate();
                ListViewItem item;
                int index=0;
                if (dataList.Count == 1)
                {
                    foreach (Movie movie in dataList[0].movieList)
                    {
                        item = this.recordList.Items[index];
                        index++;
                        if (movie.url==null||movie.url.Equals(item.SubItems[1]))
                        {
                            continue;
                        }
                        item.SubItems[1].Text=movie.url;
                    }
                }
                else
                {
                    foreach (MovieCata movie in dataList)
                    {
                        item = this.recordList.Items[index];
                        index++;
                        if(movie.describe.Equals(item.SubItems[1]))
                        {
                            continue;
                        }
                        item.SubItems[1].Text=movie.describe;
                    }
                }
                recordList.EndUpdate();
            }
        }
        
        //End depute area
        private void MainForm_Load(object sender, EventArgs e)
        {
            new Thread(XMLService.checkVersion).Start();
        }
        //right button menue
        private void analyzeMovieCata_Click(object sender, EventArgs e)
        {
            if (this.recordList.ContextMenuStrip == this.movieCataMenu)
            {
                this.setRunState(MsgString.NOW_IS_DOING_WORK.Replace("%name%", this.recordList.SelectedItems[0].Text));
                Thread th = new Thread(anlyzeMovieCata);
                th.Start((object)this.recordList.SelectedItems[0].Index);
            }
        }
        private void anlyzeMovieCata(object o)
        {
            int index = (int)o;
            Message msg = this.list.getNow()[index].analyze();
            if (msg.isSucceed)
            {
                this.list.add(msg.movieCataList);
                this.setRunState(msg.msg);
                this.showDataInRecordList(msg.movieCataList);
                while (true)
                {
                    if (Movie.isAllComplete()) break;
                    Thread.Sleep(1000);
                    this.refreshRecordList();
                }
                this.setRunState(MsgString.ALL_MOVIE_ADDRESS_ANALYZE_COMPELTE);
            }
            else
            {
                this.setRunState(msg.msg);
            }
        }

        private void recordList_DoubleClick(object sender, EventArgs e)
        {
            if(this.recordList.ContextMenuStrip == this.movieCataMenu) this.analyzeMovieCata_Click(sender, e);
        }

        private void selectedOther_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.recordList.Items)
            {
                item.Checked = !item.Checked;
            }
            this.recordList.SelectedItems[0].Checked = true;
        }

        private void copyDownloadUrl_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.recordList.SelectedItems[0].SubItems[1].Text);
            this.setRunState(MsgString.COPY_DOWNLOAD_URL_SUCCESS);
        }

        private void downloadForciblyModel_Click(object sender, EventArgs e)
        {
            this.addDownloadQueue(Constant.FORCIBLY_DECRYPT_MODEL);
        }
        private void downloadAutoModel_Click(object sender, EventArgs e)
        {
            this.addDownloadQueue(Constant.AUTO_DECRYPT_MODEL);
        }
        private void justDownload_Click(object sender, EventArgs e)
        {
            this.addDownloadQueue(Constant.NO_DECRYPT_MODEL);
        }
        private void addDownloadQueue(short decryptModel)
        {
            if (this.recordList.SelectedItems[0].SubItems[1].Text.Equals("正在解析地址")
                || this.recordList.SelectedItems[0].SubItems[1].Text.Equals(MsgString.THIS_MOVIE_NOT_EXIST))
            {
                return;
            }
            if (Constant.folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (Constant.downloadForm == null)
            {
                Constant.downloadForm = DownLoadForm.getInterface();
            }
            Constant.downloadForm.Show();
            Constant.downloadForm.Focus();
            this.setRunState(MsgString.ADD_DOWNLOAD_QUEUE.Replace("%name%", this.recordList.SelectedItems[0].Text));
            Movie movie;
            if (this.recordList.CheckedItems.Count == 0)
            {
                movie = this.list.getNow()[0].movieList[this.recordList.SelectedItems[0].Index];
                movie.decryptModel = decryptModel;
                movie.path = Constant.folderBrowserDialog.SelectedPath;
                Constant.downloadForm.addQueue(movie);
            }
            else
            {
                foreach (ListViewItem item in this.recordList.CheckedItems)
                {
                    movie = this.list.getNow()[0].movieList[item.Index];
                    movie.decryptModel = decryptModel;
                    movie.path = Constant.folderBrowserDialog.SelectedPath;
                    Constant.downloadForm.addQueue(movie);
                }
            }
        }

        private void showInWebView_Click(object sender, EventArgs e)
        {
            this.showExploreModelBtn_Click(sender, e);
            Constant.exploreForm.navigate(Constant.SERVICE_ADDRESS + "bofangye.html?info=" + this.list.getNow()[this.recordList.SelectedItems[0].Index].code);
        }

        public delegate string SELECTPATH();
        public string selectPath()
        {
            if (this.keyValue.InvokeRequired)
            {
                SELECTPATH set = new SELECTPATH(selectPath);
                return (string)this.Invoke(set, new object[] { });
            }
            else
            {
                if (Constant.folderBrowserDialog.ShowDialog() != DialogResult.OK)
                {
                    return "";
                }
                return Constant.folderBrowserDialog.SelectedPath;
            }
        }

        private void fighting_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("如果你愿意继续支持我们的工作\r\n请转账到支付宝账号：ahnujyyang@gmail.com,\r\n谢谢你的支持", "继续加油", messButton);
            if (dr == DialogResult.OK)
            {
                Process.Start("https://shenghuo.alipay.com/send/payment/fill.htm?_tosheet=true&_pdType=afcabecbafgggffdhjch");
            }
        }

        private void reportBug_Click(object sender, EventArgs e)
        {
            Process.Start(Constant.REPORT_BUG_OR_SUGGEST_URL);
        }
    }
}
