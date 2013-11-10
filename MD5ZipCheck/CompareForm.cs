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
        public CompareForm(string md5hash, string zipFilePath, string compareFolder)
        {
            InitializeComponent();

            var comparer = new Md5Comparison(md5hash, zipFilePath, compareFolder, new TextBoxStreamWriter(m_CompareMessages));
            comparer.Compare();
        }
    }

    //inner class to redirect the info messages from the compare class
    class TextBoxStreamWriter : TextWriter
    {
        TextBox _output = null;

        public TextBoxStreamWriter(TextBox output)
        {
            _output = output;
        }

        public override void Write(char value)
        {
            base.Write(value);
            _output.AppendText(value.ToString()); // When character data is written, append it to the text box.
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
