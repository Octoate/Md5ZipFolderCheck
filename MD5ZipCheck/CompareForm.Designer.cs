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
            this.m_OkButton = new System.Windows.Forms.Button();
            this.m_StatusLabel = new System.Windows.Forms.Label();
            this.m_StatusTextLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // m_CompareMessages
            // 
            this.m_CompareMessages.Location = new System.Drawing.Point(13, 13);
            this.m_CompareMessages.Multiline = true;
            this.m_CompareMessages.Name = "m_CompareMessages";
            this.m_CompareMessages.ReadOnly = true;
            this.m_CompareMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_CompareMessages.Size = new System.Drawing.Size(716, 460);
            this.m_CompareMessages.TabIndex = 0;
            // 
            // m_OkButton
            // 
            this.m_OkButton.Enabled = false;
            this.m_OkButton.Location = new System.Drawing.Point(654, 484);
            this.m_OkButton.Name = "m_OkButton";
            this.m_OkButton.Size = new System.Drawing.Size(75, 23);
            this.m_OkButton.TabIndex = 1;
            this.m_OkButton.Text = "OK";
            this.m_OkButton.UseVisualStyleBackColor = true;
            this.m_OkButton.Click += new System.EventHandler(this.m_OkButton_Click);
            // 
            // m_StatusLabel
            // 
            this.m_StatusLabel.AutoSize = true;
            this.m_StatusLabel.Location = new System.Drawing.Point(13, 487);
            this.m_StatusLabel.Name = "m_StatusLabel";
            this.m_StatusLabel.Size = new System.Drawing.Size(52, 17);
            this.m_StatusLabel.TabIndex = 2;
            this.m_StatusLabel.Text = "Status:";
            // 
            // m_StatusTextLabel
            // 
            this.m_StatusTextLabel.AutoSize = true;
            this.m_StatusTextLabel.BackColor = System.Drawing.SystemColors.Control;
            this.m_StatusTextLabel.Location = new System.Drawing.Point(72, 489);
            this.m_StatusTextLabel.MinimumSize = new System.Drawing.Size(300, 0);
            this.m_StatusTextLabel.Name = "m_StatusTextLabel";
            this.m_StatusTextLabel.Size = new System.Drawing.Size(300, 17);
            this.m_StatusTextLabel.TabIndex = 3;
            this.m_StatusTextLabel.Text = "Pending";
            // 
            // CompareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 516);
            this.Controls.Add(this.m_StatusTextLabel);
            this.Controls.Add(this.m_StatusLabel);
            this.Controls.Add(this.m_OkButton);
            this.Controls.Add(this.m_CompareMessages);
            this.Name = "CompareForm";
            this.Text = "Compare MD5 / Files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_CompareMessages;
        private System.Windows.Forms.Button m_OkButton;
        private System.Windows.Forms.Label m_StatusLabel;
        private System.Windows.Forms.Label m_StatusTextLabel;
    }
}