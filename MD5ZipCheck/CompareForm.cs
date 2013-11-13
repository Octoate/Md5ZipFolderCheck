using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MD5ZipFolderCheck
{
    public partial class CompareForm : Form
    {
        public CompareForm(string md5hash, string zipFilePath, string compareFolder)
        {
            InitializeComponent();            

            var comparer = new Md5Comparison(md5hash, zipFilePath, compareFolder, new TextBoxStreamWriter(m_CompareMessages));
            Task.Run(() =>
                { 
                    var result = comparer.Compare();
                    m_StatusTextLabel.BeginInvoke(new Action(() =>
                        {
                            switch(result)
                            {
                                case CompareResult.Ok:
                                    m_StatusTextLabel.Text = "OK";
                                    m_StatusTextLabel.BackColor = Color.LightGreen;
                                    break;
                                case CompareResult.InvalidFileHash:
                                    m_StatusTextLabel.Text = "Invalid file MD5 hash.";
                                    m_StatusTextLabel.BackColor = Color.OrangeRed;
                                    break;
                                case CompareResult.InvalidZipHash:
                                    m_StatusTextLabel.Text = "Invalid ZIP MD5 hash.";
                                    m_StatusTextLabel.BackColor = Color.OrangeRed;
                                    break;
                                case CompareResult.Error:
                                    m_StatusTextLabel.Text = "An internal error occurred.";
                                    m_StatusTextLabel.BackColor = Color.Yellow;
                                    break;
                            }
                            m_OkButton.Enabled = true;
                        }));
                });
        }

        private void m_OkButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }

    //inner class to redirect the info messages from the compare class
    class TextBoxStreamWriter : TextWriter
    {
        private TextBox m_Console;

        public TextBoxStreamWriter(TextBox console)
        {
            this.m_Console = console;
        }

        public override void Write(char value)
        {
            base.Write(value);
            if (m_Console.InvokeRequired)
            {
                m_Console.BeginInvoke((MethodInvoker)delegate
                {
                    m_Console.AppendText(value.ToString());

                    //console.SelectionStart = console.Text.Length;
                    //console.ScrollToCaret();
                });
            }
            else
            {
                m_Console.AppendText(value.ToString());
            }
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
