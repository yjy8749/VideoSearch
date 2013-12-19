namespace VideoSearch
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            MainForm.disposeHolders();
            base.Dispose(disposing);
            System.Environment.Exit(System.Environment.ExitCode);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.forwardBtn = new System.Windows.Forms.PictureBox();
            this.backBtn = new System.Windows.Forms.PictureBox();
            this.keyValue = new System.Windows.Forms.TextBox();
            this.goBtn = new System.Windows.Forms.PictureBox();
            this.setBtn = new System.Windows.Forms.PictureBox();
            this.newResourceBtn = new System.Windows.Forms.PictureBox();
            this.allResourceBtn = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.showDownloadFormBtn = new System.Windows.Forms.PictureBox();
            this.showExploreModelBtn = new System.Windows.Forms.PictureBox();
            this.showOnlineHelpBtn = new System.Windows.Forms.PictureBox();
            this.shareServiceBtn = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.runStateLabel = new System.Windows.Forms.Label();
            this.movieCataMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.analyzeMovieCata = new System.Windows.Forms.ToolStripMenuItem();
            this.showInWebView = new System.Windows.Forms.ToolStripMenuItem();
            this.movieMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.downloadAutoModel = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadForciblyModel = new System.Windows.Forms.ToolStripMenuItem();
            this.justDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.copyDownloadUrl = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedOther = new System.Windows.Forms.ToolStripMenuItem();
            this.fighting = new System.Windows.Forms.LinkLabel();
            this.reportBug = new System.Windows.Forms.LinkLabel();
            this.recordList = new VideoSearch.ScheduleListView();
            this.sourceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addressAndInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.forwardBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.goBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.setBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newResourceBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.allResourceBtn)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.showDownloadFormBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.showExploreModelBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.showOnlineHelpBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.shareServiceBtn)).BeginInit();
            this.movieCataMenu.SuspendLayout();
            this.movieMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // forwardBtn
            // 
            this.forwardBtn.BackColor = System.Drawing.Color.Transparent;
            this.forwardBtn.InitialImage = null;
            this.forwardBtn.Location = new System.Drawing.Point(75, 37);
            this.forwardBtn.Name = "forwardBtn";
            this.forwardBtn.Size = new System.Drawing.Size(42, 40);
            this.forwardBtn.TabIndex = 0;
            this.forwardBtn.TabStop = false;
            this.forwardBtn.Click += new System.EventHandler(this.forwardBtn_Click);
            // 
            // backBtn
            // 
            this.backBtn.BackColor = System.Drawing.Color.Transparent;
            this.backBtn.InitialImage = null;
            this.backBtn.Location = new System.Drawing.Point(31, 37);
            this.backBtn.Name = "backBtn";
            this.backBtn.Size = new System.Drawing.Size(43, 40);
            this.backBtn.TabIndex = 1;
            this.backBtn.TabStop = false;
            this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
            // 
            // keyValue
            // 
            this.keyValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.keyValue.Font = new System.Drawing.Font("宋体", 14F);
            this.keyValue.Location = new System.Drawing.Point(157, 48);
            this.keyValue.Name = "keyValue";
            this.keyValue.Size = new System.Drawing.Size(467, 22);
            this.keyValue.TabIndex = 2;
            this.keyValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyValue_KeyDown);
            // 
            // goBtn
            // 
            this.goBtn.BackColor = System.Drawing.Color.Transparent;
            this.goBtn.InitialImage = null;
            this.goBtn.Location = new System.Drawing.Point(633, 37);
            this.goBtn.Name = "goBtn";
            this.goBtn.Size = new System.Drawing.Size(81, 40);
            this.goBtn.TabIndex = 3;
            this.goBtn.TabStop = false;
            this.goBtn.Click += new System.EventHandler(this.goBtn_Click);
            // 
            // setBtn
            // 
            this.setBtn.BackColor = System.Drawing.Color.Transparent;
            this.setBtn.InitialImage = null;
            this.setBtn.Location = new System.Drawing.Point(717, 37);
            this.setBtn.Name = "setBtn";
            this.setBtn.Size = new System.Drawing.Size(44, 40);
            this.setBtn.TabIndex = 4;
            this.setBtn.TabStop = false;
            this.setBtn.Click += new System.EventHandler(this.setBtn_Click);
            // 
            // newResourceBtn
            // 
            this.newResourceBtn.BackColor = System.Drawing.Color.Transparent;
            this.newResourceBtn.InitialImage = null;
            this.newResourceBtn.Location = new System.Drawing.Point(35, 153);
            this.newResourceBtn.Name = "newResourceBtn";
            this.newResourceBtn.Size = new System.Drawing.Size(31, 137);
            this.newResourceBtn.TabIndex = 5;
            this.newResourceBtn.TabStop = false;
            this.newResourceBtn.Click += new System.EventHandler(this.newResourceBtn_Click);
            // 
            // allResourceBtn
            // 
            this.allResourceBtn.BackColor = System.Drawing.Color.Transparent;
            this.allResourceBtn.InitialImage = null;
            this.allResourceBtn.Location = new System.Drawing.Point(35, 310);
            this.allResourceBtn.Name = "allResourceBtn";
            this.allResourceBtn.Size = new System.Drawing.Size(30, 137);
            this.allResourceBtn.TabIndex = 6;
            this.allResourceBtn.TabStop = false;
            this.allResourceBtn.Click += new System.EventHandler(this.allResourceBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.recordList);
            this.panel1.Location = new System.Drawing.Point(66, 134);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(547, 335);
            this.panel1.TabIndex = 8;
            // 
            // showDownloadFormBtn
            // 
            this.showDownloadFormBtn.BackColor = System.Drawing.Color.Transparent;
            this.showDownloadFormBtn.InitialImage = null;
            this.showDownloadFormBtn.Location = new System.Drawing.Point(619, 154);
            this.showDownloadFormBtn.Name = "showDownloadFormBtn";
            this.showDownloadFormBtn.Size = new System.Drawing.Size(142, 66);
            this.showDownloadFormBtn.TabIndex = 9;
            this.showDownloadFormBtn.TabStop = false;
            this.showDownloadFormBtn.Click += new System.EventHandler(this.showDownloadFormBtn_Click);
            // 
            // showExploreModelBtn
            // 
            this.showExploreModelBtn.BackColor = System.Drawing.Color.Transparent;
            this.showExploreModelBtn.InitialImage = null;
            this.showExploreModelBtn.Location = new System.Drawing.Point(619, 226);
            this.showExploreModelBtn.Name = "showExploreModelBtn";
            this.showExploreModelBtn.Size = new System.Drawing.Size(142, 66);
            this.showExploreModelBtn.TabIndex = 10;
            this.showExploreModelBtn.TabStop = false;
            this.showExploreModelBtn.Click += new System.EventHandler(this.showExploreModelBtn_Click);
            // 
            // showOnlineHelpBtn
            // 
            this.showOnlineHelpBtn.BackColor = System.Drawing.Color.Transparent;
            this.showOnlineHelpBtn.InitialImage = null;
            this.showOnlineHelpBtn.Location = new System.Drawing.Point(619, 298);
            this.showOnlineHelpBtn.Name = "showOnlineHelpBtn";
            this.showOnlineHelpBtn.Size = new System.Drawing.Size(142, 66);
            this.showOnlineHelpBtn.TabIndex = 11;
            this.showOnlineHelpBtn.TabStop = false;
            this.showOnlineHelpBtn.Click += new System.EventHandler(this.showOnlineHelpBtn_Click);
            // 
            // shareServiceBtn
            // 
            this.shareServiceBtn.BackColor = System.Drawing.Color.Transparent;
            this.shareServiceBtn.InitialImage = null;
            this.shareServiceBtn.Location = new System.Drawing.Point(619, 370);
            this.shareServiceBtn.Name = "shareServiceBtn";
            this.shareServiceBtn.Size = new System.Drawing.Size(142, 66);
            this.shareServiceBtn.TabIndex = 12;
            this.shareServiceBtn.TabStop = false;
            this.shareServiceBtn.Click += new System.EventHandler(this.showAboutUsBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(66, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "软件状态:";
            // 
            // runStateLabel
            // 
            this.runStateLabel.AutoSize = true;
            this.runStateLabel.BackColor = System.Drawing.Color.Transparent;
            this.runStateLabel.ForeColor = System.Drawing.Color.Black;
            this.runStateLabel.Location = new System.Drawing.Point(123, 115);
            this.runStateLabel.Name = "runStateLabel";
            this.runStateLabel.Size = new System.Drawing.Size(0, 12);
            this.runStateLabel.TabIndex = 16;
            // 
            // movieCataMenu
            // 
            this.movieCataMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analyzeMovieCata,
            this.showInWebView});
            this.movieCataMenu.Name = "movieCataMenu";
            this.movieCataMenu.Size = new System.Drawing.Size(161, 48);
            // 
            // analyzeMovieCata
            // 
            this.analyzeMovieCata.Name = "analyzeMovieCata";
            this.analyzeMovieCata.Size = new System.Drawing.Size(160, 22);
            this.analyzeMovieCata.Text = "解析下载地址";
            this.analyzeMovieCata.Click += new System.EventHandler(this.analyzeMovieCata_Click);
            // 
            // showInWebView
            // 
            this.showInWebView.Name = "showInWebView";
            this.showInWebView.Size = new System.Drawing.Size(160, 22);
            this.showInWebView.Text = "在浏览器中查看";
            this.showInWebView.Click += new System.EventHandler(this.showInWebView_Click);
            // 
            // movieMenu
            // 
            this.movieMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadAutoModel,
            this.downloadForciblyModel,
            this.justDownload,
            this.copyDownloadUrl,
            this.selectedOther});
            this.movieMenu.Name = "movieMenu";
            this.movieMenu.Size = new System.Drawing.Size(213, 114);
            // 
            // downloadAutoModel
            // 
            this.downloadAutoModel.Name = "downloadAutoModel";
            this.downloadAutoModel.Size = new System.Drawing.Size(212, 22);
            this.downloadAutoModel.Text = "下载所选视频(自动解密）";
            this.downloadAutoModel.Click += new System.EventHandler(this.downloadAutoModel_Click);
            // 
            // downloadForciblyModel
            // 
            this.downloadForciblyModel.Name = "downloadForciblyModel";
            this.downloadForciblyModel.Size = new System.Drawing.Size(212, 22);
            this.downloadForciblyModel.Text = "下载所选视频(强制解密)";
            this.downloadForciblyModel.Click += new System.EventHandler(this.downloadForciblyModel_Click);
            // 
            // justDownload
            // 
            this.justDownload.Name = "justDownload";
            this.justDownload.Size = new System.Drawing.Size(212, 22);
            this.justDownload.Text = "下载所选视频(直接下载)";
            this.justDownload.Click += new System.EventHandler(this.justDownload_Click);
            // 
            // copyDownloadUrl
            // 
            this.copyDownloadUrl.Name = "copyDownloadUrl";
            this.copyDownloadUrl.Size = new System.Drawing.Size(212, 22);
            this.copyDownloadUrl.Text = "复制下载地址";
            this.copyDownloadUrl.Click += new System.EventHandler(this.copyDownloadUrl_Click);
            // 
            // selectedOther
            // 
            this.selectedOther.Name = "selectedOther";
            this.selectedOther.Size = new System.Drawing.Size(212, 22);
            this.selectedOther.Text = "全选/反选";
            this.selectedOther.Click += new System.EventHandler(this.selectedOther_Click);
            // 
            // fighting
            // 
            this.fighting.AutoSize = true;
            this.fighting.BackColor = System.Drawing.Color.Transparent;
            this.fighting.ForeColor = System.Drawing.Color.White;
            this.fighting.LinkColor = System.Drawing.Color.White;
            this.fighting.Location = new System.Drawing.Point(631, 448);
            this.fighting.Name = "fighting";
            this.fighting.Size = new System.Drawing.Size(53, 12);
            this.fighting.TabIndex = 17;
            this.fighting.TabStop = true;
            this.fighting.Text = "支持我们";
            this.fighting.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.fighting_LinkClicked);
            // 
            // reportBug
            // 
            this.reportBug.AutoSize = true;
            this.reportBug.BackColor = System.Drawing.Color.Transparent;
            this.reportBug.ForeColor = System.Drawing.Color.White;
            this.reportBug.LinkColor = System.Drawing.Color.White;
            this.reportBug.Location = new System.Drawing.Point(697, 448);
            this.reportBug.Name = "reportBug";
            this.reportBug.Size = new System.Drawing.Size(47, 12);
            this.reportBug.TabIndex = 18;
            this.reportBug.TabStop = true;
            this.reportBug.Text = "BUG反馈";
            this.reportBug.Click += new System.EventHandler(this.reportBug_Click);
            // 
            // recordList
            // 
            this.recordList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.recordList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.recordList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sourceName,
            this.addressAndInfo});
            this.recordList.FullRowSelect = true;
            this.recordList.GridLines = true;
            this.recordList.Location = new System.Drawing.Point(-1, 0);
            this.recordList.Name = "recordList";
            this.recordList.OwnerDraw = true;
            this.recordList.ProgressColor = System.Drawing.Color.Green;
            this.recordList.ProgressColumIndex = -1;
            this.recordList.ProgressTextColor = System.Drawing.Color.Black;
            this.recordList.Size = new System.Drawing.Size(547, 352);
            this.recordList.TabIndex = 2;
            this.recordList.UseCompatibleStateImageBehavior = false;
            this.recordList.View = System.Windows.Forms.View.Details;
            this.recordList.DoubleClick += new System.EventHandler(this.analyzeMovieCata_Click);
            // 
            // sourceName
            // 
            this.sourceName.Text = "资源名称";
            this.sourceName.Width = 101;
            // 
            // addressAndInfo
            // 
            this.addressAndInfo.Text = "下载地址";
            this.addressAndInfo.Width = 446;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 518);
            this.Controls.Add(this.reportBug);
            this.Controls.Add(this.fighting);
            this.Controls.Add(this.runStateLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shareServiceBtn);
            this.Controls.Add(this.showOnlineHelpBtn);
            this.Controls.Add(this.showExploreModelBtn);
            this.Controls.Add(this.showDownloadFormBtn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.allResourceBtn);
            this.Controls.Add(this.newResourceBtn);
            this.Controls.Add(this.setBtn);
            this.Controls.Add(this.goBtn);
            this.Controls.Add(this.keyValue);
            this.Controls.Add(this.backBtn);
            this.Controls.Add(this.forwardBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "校园高清视频网视频下载工具2.1.8";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.forwardBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.goBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.setBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newResourceBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.allResourceBtn)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.showDownloadFormBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.showExploreModelBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.showOnlineHelpBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.shareServiceBtn)).EndInit();
            this.movieCataMenu.ResumeLayout(false);
            this.movieMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox forwardBtn;
        private System.Windows.Forms.PictureBox backBtn;
        private System.Windows.Forms.TextBox keyValue;
        private System.Windows.Forms.PictureBox goBtn;
        private System.Windows.Forms.PictureBox setBtn;
        private System.Windows.Forms.PictureBox newResourceBtn;
        private System.Windows.Forms.PictureBox allResourceBtn;
        private ScheduleListView recordList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox showDownloadFormBtn;
        private System.Windows.Forms.PictureBox showExploreModelBtn;
        private System.Windows.Forms.PictureBox showOnlineHelpBtn;
        private System.Windows.Forms.PictureBox shareServiceBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label runStateLabel;
        private System.Windows.Forms.ColumnHeader sourceName;
        private System.Windows.Forms.ColumnHeader addressAndInfo;
        private System.Windows.Forms.ContextMenuStrip movieCataMenu;
        private System.Windows.Forms.ToolStripMenuItem analyzeMovieCata;
        private System.Windows.Forms.ToolStripMenuItem showInWebView;
        private System.Windows.Forms.ContextMenuStrip movieMenu;
        private System.Windows.Forms.ToolStripMenuItem downloadAutoModel;
        private System.Windows.Forms.ToolStripMenuItem downloadForciblyModel;
        private System.Windows.Forms.ToolStripMenuItem copyDownloadUrl;
        private System.Windows.Forms.ToolStripMenuItem selectedOther;
        private System.Windows.Forms.ToolStripMenuItem justDownload;
        private System.Windows.Forms.LinkLabel fighting;
        private System.Windows.Forms.LinkLabel reportBug;
    }
}

