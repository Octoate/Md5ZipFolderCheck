namespace MD5ZipCheck
{
    partial class CompareForm
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
            this.m_CompareMessages = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_CompareMessages
            // 
            this.m_CompareMessages.Location = new System.Drawing.Point(13, 13);
            this.m_CompareMessages.Multiline = true;
            this.m_CompareMessages.Name = "m_CompareMessages";
            this.m_CompareMessages.ReadOnly = true;
            this.m_CompareMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_CompareMessages.Size = new System.Drawing.Size(716, 439);
            this.m_CompareMessages.TabIndex = 0;
            // 
            // CompareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 548);
            this.Controls.Add(this.m_CompareMessages);
            this.Name = "CompareForm";
            this.Text = "Compare MD5 / Files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_CompareMessages;
    }
}