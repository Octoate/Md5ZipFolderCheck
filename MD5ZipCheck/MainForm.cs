using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MD5ZipFolderCheck
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void m_BrowseFileButton_Click(object sender, EventArgs e)
        {
            var dialogResult = m_OpenZipFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK && m_OpenZipFileDialog.CheckFileExists)
            {
                m_ZipFilePathTextbox.Text = m_OpenZipFileDialog.FileName;
            }
        }

        private void m_OpenCompareFolderButton_Click(object sender, EventArgs e)
        {
            var dialogResult = m_OpenCompareFolderDialog.ShowDialog();
            if (dialogResult == DialogResult.OK && Directory.Exists(m_OpenCompareFolderDialog.SelectedPath))
            {
                m_CompareDirectoryTextbox.Text = m_OpenCompareFolderDialog.SelectedPath;
            }
        }

        private void m_CheckFileButton_Click(object sender, EventArgs e)
        {
            if (CheckParameters())
            {
                var compareForm = new CompareForm(m_MD5HashTextbox.Text, m_ZipFilePathTextbox.Text, m_CompareDirectoryTextbox.Text);
                compareForm.Show();
            }
        }

        private bool CheckParameters()
        {
            if (!(m_MD5HashTextbox.Text.Replace(" ", "").Replace("-", "").Length == 32))
            {
                MessageBox.Show("Invalid MD5 hash", "Invalid MD5 hash (32 character hex string expected).", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (!File.Exists(m_ZipFilePathTextbox.Text))
            {
                MessageBox.Show("ZIP file not found", string.Format("ZIP file not found: '{0}'", m_ZipFilePathTextbox.Text), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (!Directory.Exists(m_CompareDirectoryTextbox.Text))
            {
                MessageBox.Show("Directory not found", string.Format("Directory not found: '{0}'", m_CompareDirectoryTextbox.Text), MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
    }
}
