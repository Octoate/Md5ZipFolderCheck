using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MD5ZipFolderCheck
{
    static class MD5ZipFolderCheck
    {
        // defines for commandline output
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            // redirect console output to parent process;
            // must be before any calls to Console.WriteLine()
            AttachConsole(ATTACH_PARENT_PROCESS);

            //check if the GUI shall be invoked
            if (args != null && args.Length > 0)
            {
                //parse command line
                var cliArguments = new CommandLineArguments();
                if (CommandLine.Parser.Default.ParseArguments(args, cliArguments))
                {
                    int returnCode = 0;
                    try
                    {
                        var md5Comparison = new Md5Comparison(cliArguments);

                        returnCode = (int)md5Comparison.Compare();
                    }
                    catch(Exception e)
                    {
                        //argument exceptions (invalid MD5 hash, ZIP file not found, directory not found)
                        Console.WriteLine(cliArguments.GetUsage());
                        Console.WriteLine("\n{0}", e.Message);
                        returnCode = (int)CompareResult.Error;
                    }

                    return returnCode;
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

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Unhandled Exception: " + ((Exception)e.ExceptionObject).Message);
            Console.ReadLine();

            Environment.Exit((int)CompareResult.Error);
        }
    }
}
