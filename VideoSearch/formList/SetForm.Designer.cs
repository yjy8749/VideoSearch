namespace VideoSearch
{
    partial class SetForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetForm));
            this.serviceAddressCombo = new System.Windows.Forms.ComboBox();
            this.sourceCodeAddressBtn = new System.Windows.Forms.PictureBox();
            this.resetSoftwareBtn = new System.Windows.Forms.PictureBox();
            this.setFormSureBtn = new System.Windows.Forms.PictureBox();
            this.aboutSoftWare = new System.Windows.Forms.PictureBox();
            this.defaultDownloadDirTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.sourceCodeAddressBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resetSoftwareBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.setFormSureBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aboutSoftWare)).BeginInit();
            this.SuspendLayout();
            // 
            // serviceAddressCombo
            // 
            this.serviceAddressCombo.FormattingEnabled = true;
            this.serviceAddressCombo.Location = new System.Drawing.Point(133, 110);
            this.serviceAddressCombo.Name = "serviceAddressCombo";
            this.serviceAddressCombo.Size = new System.Drawing.Size(306, 20);
            this.serviceAddressCombo.TabIndex = 0;
            // 
            // sourceCodeAddressBtn
            // 
            this.sourceCodeAddressBtn.BackColor = System.Drawing.Color.Transparent;
            this.sourceCodeAddressBtn.ErrorImage = null;
            this.sourceCodeAddressBtn.InitialImage = null;
            this.sourceCodeAddressBtn.Location = new System.Drawing.Point(24, 206);
            this.sourceCodeAddressBtn.Name = "sourceCodeAddressBtn";
            this.sourceCodeAddressBtn.Size = new System.Drawing.Size(121, 38);
            this.sourceCodeAddressBtn.TabIndex = 1;
            this.sourceCodeAddressBtn.TabStop = false;
            // 
            // resetSoftwareBtn
            // 
            this.resetSoftwareBtn.BackColor = System.Drawing.Color.Transparent;
            this.resetSoftwareBtn.ErrorImage = null;
            this.resetSoftwareBtn.InitialImage = null;
            this.resetSoftwareBtn.Location = new System.Drawing.Point(166, 206);
            this.resetSoftwareBtn.Name = "resetSoftwareBtn";
            this.resetSoftwareBtn.Size = new System.Drawing.Size(121, 38);
            this.resetSoftwareBtn.TabIndex = 2;
            this.resetSoftwareBtn.TabStop = false;
            // 
            // setFormSureBtn
            // 
            this.setFormSureBtn.BackColor = System.Drawing.Color.Transparent;
            this.setFormSureBtn.ErrorImage = null;
            this.setFormSureBtn.InitialImage = null;
            this.setFormSureBtn.Location = new System.Drawing.Point(309, 206);
            this.setFormSureBtn.Name = "setFormSureBtn";
            this.setFormSureBtn.Size = new System.Drawing.Size(121, 38);
            this.setFormSureBtn.TabIndex = 3;
            this.setFormSureBtn.TabStop = false;
            this.setFormSureBtn.Click += new System.EventHandler(this.setFormSureBtn_Click);
            // 
            // aboutSoftWare
            // 
            this.aboutSoftWare.BackColor = System.Drawing.Color.Transparent;
            this.aboutSoftWare.ErrorImage = null;
            this.aboutSoftWare.InitialImage = null;
            this.aboutSoftWare.Location = new System.Drawing.Point(365, 43);
            this.aboutSoftWare.Name = "aboutSoftWare";
            this.aboutSoftWare.Size = new System.Drawing.Size(74, 28);
            this.aboutSoftWare.TabIndex = 5;
            this.aboutSoftWare.TabStop = false;
            this.aboutSoftWare.Click += new System.EventHandler(this.aboutSoftWare_Click);
            // 
            // defaultDownloadDirTextBox
            // 
            this.defaultDownloadDirTextBox.BackColor = System.Drawing.Color.White;
            this.defaultDownloadDirTextBox.Location = new System.Drawing.Point(133, 151);
            this.defaultDownloadDirTextBox.Name = "defaultDownloadDirTextBox";
            this.defaultDownloadDirTextBox.ReadOnly = true;
            this.defaultDownloadDirTextBox.Size = new System.Drawing.Size(306, 21);
            this.defaultDownloadDirTextBox.TabIndex = 6;
            // 
            // SetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(451, 269);
            this.Controls.Add(this.defaultDownloadDirTextBox);
            this.Controls.Add(this.aboutSoftWare);
            this.Controls.Add(this.setFormSureBtn);
            this.Controls.Add(this.resetSoftwareBtn);
            this.Controls.Add(this.sourceCodeAddressBtn);
            this.Controls.Add(this.serviceAddressCombo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SetForm";
            this.Text = "配置选项";
            ((System.ComponentModel.ISupportInitialize)(this.sourceCodeAddressBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resetSoftwareBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.setFormSureBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aboutSoftWare)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox serviceAddressCombo;
        private System.Windows.Forms.PictureBox sourceCodeAddressBtn;
        private System.Windows.Forms.PictureBox resetSoftwareBtn;
        private System.Windows.Forms.PictureBox setFormSureBtn;
        private System.Windows.Forms.PictureBox aboutSoftWare;
        private System.Windows.Forms.TextBox defaultDownloadDirTextBox;
    }
}