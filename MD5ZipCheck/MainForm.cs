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

namespace MD5ZipCheck
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void m_MD5HashLabel_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
            var comparer = new Md5Comparison(m_MD5HashTextbox.Text, m_ZipFilePathTextbox.Text, m_CompareDirectoryTextbox.Text);
            comparer.Compare();
        }
    }
}
