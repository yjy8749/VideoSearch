namespace VideoSearch
{
    partial class DownLoadForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            //base.Dispose(disposing);
            base.Hide();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownLoadForm));
            this.downloadMenue = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFile = new System.Windows.Forms.ToolStripMenuItem();
            this.openDir = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleListView = new VideoSearch.ScheduleListView();
            this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.scheduleColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.speedColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stateColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.downloadMenue.SuspendLayout();
            this.SuspendLayout();
            // 
            // downloadMenue
            // 
            this.downloadMenue.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFile,
            this.openDir});
            this.downloadMenue.Name = "downloadMenue";
            this.downloadMenue.Size = new System.Drawing.Size(153, 70);
            // 
            // openFile
            // 
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(152, 22);
            this.openFile.Text = "打开文件";
            this.openFile.Click += new System.EventHandler(this.openFile_Click);
            // 
            // openDir
            // 
            this.openDir.Name = "openDir";
            this.openDir.Size = new System.Drawing.Size(152, 22);
            this.openDir.Text = "打开文件目录";
            this.openDir.Click += new System.EventHandler(this.openDir_Click);
            // 
            // scheduleListView
            // 
            this.scheduleListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.scheduleColumn,
            this.speedColumn,
            this.stateColumn});
            this.scheduleListView.ContextMenuStrip = this.downloadMenue;
            this.scheduleListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scheduleListView.FullRowSelect = true;
            this.scheduleListView.GridLines = true;
            this.scheduleListView.Location = new System.Drawing.Point(0, 0);
            this.scheduleListView.Name = "scheduleListView";
            this.scheduleListView.OwnerDraw = true;
            this.scheduleListView.ProgressColor = System.Drawing.Color.DarkGreen;
            this.scheduleListView.ProgressColumIndex = 1;
            this.scheduleListView.ProgressTextColor = System.Drawing.Color.Black;
            this.scheduleListView.Size = new System.Drawing.Size(534, 291);
            this.scheduleListView.TabIndex = 0;
            this.scheduleListView.UseCompatibleStateImageBehavior = false;
            this.scheduleListView.View = System.Windows.Forms.View.Details;
            // 
            // nameColumn
            // 
            this.nameColumn.Text = "任务名称";
            this.nameColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nameColumn.Width = 120;
            // 
            // scheduleColumn
            // 
            this.scheduleColumn.Text = "下载进度";
            this.scheduleColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.scheduleColumn.Width = 260;
            // 
            // speedColumn
            // 
            this.speedColumn.Text = "速度";
            this.speedColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.speedColumn.Width = 75;
            // 
            // stateColumn
            // 
            this.stateColumn.Text = "任务状态";
            this.stateColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.stateColumn.Width = 80;
            // 
            // DownLoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 291);
            this.Controls.Add(this.scheduleListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DownLoadForm";
            this.Text = "下载管理";
            this.downloadMenue.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ScheduleListView scheduleListView;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.ColumnHeader scheduleColumn;
        private System.Windows.Forms.ColumnHeader speedColumn;
        private System.Windows.Forms.ColumnHeader stateColumn;
        private System.Windows.Forms.ContextMenuStrip downloadMenue;
        private System.Windows.Forms.ToolStripMenuItem openFile;
        private System.Windows.Forms.ToolStripMenuItem openDir;

    }
}