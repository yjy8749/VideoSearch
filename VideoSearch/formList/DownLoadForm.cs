using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VideoSearch
{
    public partial class DownLoadForm : Form
    {
        private DownLoadForm()
        {
            InitializeComponent();
        }
        private static readonly DownLoadForm INTERFACE=new DownLoadForm();
        public static DownLoadForm getInterface()
        {
            return DownLoadForm.INTERFACE;
        }
        public List<Movie> queueList = new List<Movie>();
        public void addQueue(Movie movie)
        {
            this.queueList.Add(movie);
            ListViewItem item = new ListViewItem(movie.name);
            item.SubItems.Add("0.0");
            item.SubItems.Add("0.00 MB/S");
            item.SubItems.Add("正在等待");
            this.scheduleListView.Items.Add(item);
            if (downloadThread == null)
            {
                downloadThread = new Thread(startQueue);
                downloadThread.Start();
            }
        }

        private Thread downloadThread = null;
        private Thread refreshThread = null;
        private bool isDownloading = false;
        private int nowQueueIndex = 0;
        private void startQueue()
        {
            isDownloading = true;
            refreshThread = new Thread(refreshSchedule);
            refreshThread.Start();
            Message msg ;
            for (; nowQueueIndex < this.queueList.Count; nowQueueIndex++)
            {
                this.updateListviewItem(nowQueueIndex, 3, "正在下载");
                msg = this.queueList[nowQueueIndex].download();
                this.updateListviewItem(nowQueueIndex, 3, msg.msg);
            }
            isDownloading = false;
            this.downloadThread = null;
        }
        private void refreshSchedule()
        {
            string sch;
            while (this.isDownloading)
            {
                if (this.nowQueueIndex < this.queueList.Count)
                {
                    sch = this.queueList[nowQueueIndex].getShcedule();
                    this.updateListviewItem(nowQueueIndex, 1, sch.Equals("1") ? "100.00" : sch);
                    this.updateListviewItem(nowQueueIndex, 2, this.queueList[nowQueueIndex].getSpeed() + " M/s");
                    if (sch.Equals("1"))
                    {
                        this.updateListviewItem(nowQueueIndex, 3, "合并缓存");
                    }
                }
                Thread.Sleep(100);
            }
        }
        private delegate void UPDATELISTVIEWITEM(int index,int columns,string value);
        private void updateListviewItem(int index,int columns,string value)
        {
            if (this.scheduleListView.InvokeRequired)
            {
                UPDATELISTVIEWITEM set = new UPDATELISTVIEWITEM(updateListviewItem);
                this.Invoke(set, new object[] {index,columns,value});
            }
            else
            {
                this.scheduleListView.Items[index].SubItems[columns].Text = value;
            }
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            if (this.scheduleListView.SelectedItems.Count > 0)
            {
                if (this.scheduleListView.SelectedItems[0].SubItems[3].Text.IndexOf("完成") > -1)
                {
                    Process.Start(this.queueList[this.scheduleListView.SelectedItems[0].Index].reallyPath());
                }
            }
        }

        private void openDir_Click(object sender, EventArgs e)
        {
            if (this.scheduleListView.SelectedItems.Count > 0)
            {
                if (this.scheduleListView.SelectedItems[0].SubItems[3].Text.IndexOf("完成")>-1)
                {
                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
                    psi.Arguments = "/e,/select," + this.queueList[this.scheduleListView.SelectedItems[0].Index].reallyPath();
                    System.Diagnostics.Process.Start(psi);
                }
            }
        }

        private void stopDownload_Click(object sender, EventArgs e)
        {
            if (this.scheduleListView.SelectedItems.Count > 0)
            {
                Message msg = this.queueList[this.scheduleListView.SelectedItems[0].Index].cancleDownload();
                this.updateListviewItem(this.scheduleListView.SelectedItems[0].Index, 3, msg.msg);
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    Message msg = new HttpThreadFile("shdbz01.mkv", "http://127.0.0.1:8000/生活大爆炸第六季(24集全) 第01集.mkv", Constant.FORCIBLY_DECRYPT_MODEL).startDownload();
        //    MessageBox.Show(msg.msg);
        //}
    }
}
