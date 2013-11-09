using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MD5ZipCheck
{
    static class MD5ZipFolderCheck
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                //parse command line
                var cliArguments = new CommandLineArguments();
                if (CommandLine.Parser.Default.ParseArguments(args, cliArguments))
                {
                    var md5Comparison = new Md5Comparison(cliArguments);

                    return (int)md5Comparison.Compare();
                }
                else
                {
                    //invalid command line arguments
                    Console.WriteLine(cliArguments.GetUsage());

                    return 0;
                }
            }
            else
            {
                //start GUI application
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());

                return 0;
            }
        }
    }
}
