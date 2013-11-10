namespace MD5ZipCheck
{
    partial class MainForm
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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_MD5HashLabel = new System.Windows.Forms.Label();
            this.m_MD5HashTextbox = new System.Windows.Forms.TextBox();
            this.m_ZipFileLabel = new System.Windows.Forms.Label();
            this.m_OpenZipFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_ZipFilePathTextbox = new System.Windows.Forms.TextBox();
            this.m_BrowseFileButton = new System.Windows.Forms.Button();
            this.m_CompareDirectoryLabel = new System.Windows.Forms.Label();
            this.m_OpenCompareFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.m_OpenCompareFolderButton = new System.Windows.Forms.Button();
            this.m_CompareDirectoryTextbox = new System.Windows.Forms.TextBox();
            this.m_CheckFileButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_MD5HashLabel
            // 
            this.m_MD5HashLabel.AutoSize = true;
            this.m_MD5HashLabel.Location = new System.Drawing.Point(12, 9);
            this.m_MD5HashLabel.Name = "m_MD5HashLabel";
            this.m_MD5HashLabel.Size = new System.Drawing.Size(181, 17);
            this.m_MD5HashLabel.TabIndex = 0;
            this.m_MD5HashLabel.Text = "MD5 Hash (Hex, 64 Chars):";
            this.m_MD5HashLabel.Click += new System.EventHandler(this.m_MD5HashLabel_Click);
            // 
            // m_MD5HashTextbox
            // 
            this.m_MD5HashTextbox.Location = new System.Drawing.Point(15, 29);
            this.m_MD5HashTextbox.MaxLength = 255;
            this.m_MD5HashTextbox.Name = "m_MD5HashTextbox";
            this.m_MD5HashTextbox.Size = new System.Drawing.Size(374, 22);
            this.m_MD5HashTextbox.TabIndex = 1;
            this.m_MD5HashTextbox.Text = "800452A0 D9BA7876 9C63D42F E04FCD74";
            // 
            // m_ZipFileLabel
            // 
            this.m_ZipFileLabel.AutoSize = true;
            this.m_ZipFileLabel.Location = new System.Drawing.Point(12, 54);
            this.m_ZipFileLabel.Name = "m_ZipFileLabel";
            this.m_ZipFileLabel.Size = new System.Drawing.Size(59, 17);
            this.m_ZipFileLabel.TabIndex = 2;
            this.m_ZipFileLabel.Text = "ZIP File:";
            // 
            // m_OpenZipFileDialog
            // 
            this.m_OpenZipFileDialog.FileName = "*.zip";
            this.m_OpenZipFileDialog.Filter = "ZIP files|*.zip|All files|*.*";
            // 
            // m_ZipFilePathTextbox
            // 
            this.m_ZipFilePathTextbox.Location = new System.Drawing.Point(15, 74);
            this.m_ZipFilePathTextbox.Name = "m_ZipFilePathTextbox";
            this.m_ZipFilePathTextbox.Size = new System.Drawing.Size(334, 22);
            this.m_ZipFilePathTextbox.TabIndex = 3;
            this.m_ZipFilePathTextbox.Text = "D:\\tmp\\KeePass-2.24.zip";
            // 
            // m_BrowseFileButton
            // 
            this.m_BrowseFileButton.Location = new System.Drawing.Point(355, 74);
            this.m_BrowseFileButton.Name = "m_BrowseFileButton";
            this.m_BrowseFileButton.Size = new System.Drawing.Size(34, 22);
            this.m_BrowseFileButton.TabIndex = 4;
            this.m_BrowseFileButton.Text = "...";
            this.m_BrowseFileButton.UseVisualStyleBackColor = true;
            this.m_BrowseFileButton.Click += new System.EventHandler(this.m_BrowseFileButton_Click);
            // 
            // m_CompareDirectoryLabel
            // 
            this.m_CompareDirectoryLabel.AutoSize = true;
            this.m_CompareDirectoryLabel.Location = new System.Drawing.Point(12, 128);
            this.m_CompareDirectoryLabel.Name = "m_CompareDirectoryLabel";
            this.m_CompareDirectoryLabel.Size = new System.Drawing.Size(130, 17);
            this.m_CompareDirectoryLabel.TabIndex = 5;
            this.m_CompareDirectoryLabel.Text = "Compare Directory:";
            // 
            // m_OpenCompareFolderButton
            // 
            this.m_OpenCompareFolderButton.Location = new System.Drawing.Point(355, 156);
            this.m_OpenCompareFolderButton.Name = "m_OpenCompareFolderButton";
            this.m_OpenCompareFolderButton.Size = new System.Drawing.Size(34, 22);
            this.m_OpenCompareFolderButton.TabIndex = 7;
            this.m_OpenCompareFolderButton.Text = "...";
            this.m_OpenCompareFolderButton.UseVisualStyleBackColor = true;
            this.m_OpenCompareFolderButton.Click += new System.EventHandler(this.m_OpenCompareFolderButton_Click);
            // 
            // m_CompareDirectoryTextbox
            // 
            this.m_CompareDirectoryTextbox.Location = new System.Drawing.Point(14, 156);
            this.m_CompareDirectoryTextbox.Name = "m_CompareDirectoryTextbox";
            this.m_CompareDirectoryTextbox.Size = new System.Drawing.Size(335, 22);
            this.m_CompareDirectoryTextbox.TabIndex = 6;
            this.m_CompareDirectoryTextbox.Text = "D:\\tmp\\KeePass";
            // 
            // m_CheckFileButton
            // 
            this.m_CheckFileButton.Location = new System.Drawing.Point(274, 207);
            this.m_CheckFileButton.Name = "m_CheckFileButton";
            this.m_CheckFileButton.Size = new System.Drawing.Size(115, 32);
            this.m_CheckFileButton.TabIndex = 8;
            this.m_CheckFileButton.Text = "Check Files";
            this.m_CheckFileButton.UseVisualStyleBackColor = true;
            this.m_CheckFileButton.Click += new System.EventHandler(this.m_CheckFileButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 251);
            this.Controls.Add(this.m_CheckFileButton);
            this.Controls.Add(this.m_OpenCompareFolderButton);
            this.Controls.Add(this.m_CompareDirectoryTextbox);
            this.Controls.Add(this.m_CompareDirectoryLabel);
            this.Controls.Add(this.m_BrowseFileButton);
            this.Controls.Add(this.m_ZipFilePathTextbox);
            this.Controls.Add(this.m_ZipFileLabel);
            this.Controls.Add(this.m_MD5HashTextbox);
            this.Controls.Add(this.m_MD5HashLabel);
            this.Name = "MainForm";
            this.Text = "MD5 Zip Folder Check";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_MD5HashLabel;
        private System.Windows.Forms.TextBox m_MD5HashTextbox;
        private System.Windows.Forms.Label m_ZipFileLabel;
        private System.Windows.Forms.OpenFileDialog m_OpenZipFileDialog;
        private System.Windows.Forms.TextBox m_ZipFilePathTextbox;
        private System.Windows.Forms.Button m_BrowseFileButton;
        private System.Windows.Forms.Label m_CompareDirectoryLabel;
        private System.Windows.Forms.FolderBrowserDialog m_OpenCompareFolderDialog;
        private System.Windows.Forms.Button m_OpenCompareFolderButton;
        private System.Windows.Forms.TextBox m_CompareDirectoryTextbox;
        private System.Windows.Forms.Button m_CheckFileButton;
    }
}

