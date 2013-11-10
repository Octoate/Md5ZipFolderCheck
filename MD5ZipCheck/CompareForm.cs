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
    public partial class CompareForm : Form
    {
        public delegate void AppendTextToTextbox(string text);
        public AppendTextToTextbox myDelegate;

        public CompareForm(string md5hash, string zipFilePath, string compareFolder)
        {
            InitializeComponent();            

            myDelegate = new AppendTextToTextbox(AppendTextToTextboxMethod);
            var comparer = new Md5Comparison(md5hash, zipFilePath, compareFolder, new TextBoxStreamWriter(m_CompareMessages));
            comparer.Compare();
        }

        public void AppendTextToTextboxMethod(string text)
        {
            m_CompareMessages.AppendText(text);
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
