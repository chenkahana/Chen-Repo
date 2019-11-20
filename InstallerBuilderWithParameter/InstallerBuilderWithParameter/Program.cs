using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallerBuilderWithParameter
{
    class Program
    {

        public static void startBuild()
        {
            string folder = @"D:\Program Files (x86)\InstallShield\2019\System";
            string ismFolder = @"D:\InstallShield 2019 Projects\JFTES.ism";
            string argument = @"/c " + ConfigurationSettings.AppSettings["literal"];


            //   /c ISCmdBld.exe -p "D:\InstallShield 2019 Projects\JFTES.ism" -c COMP -e y -d TEST_FROM_COMMAND="GOOD"


            var startInfo = new ProcessStartInfo("cmd")
            {
                WorkingDirectory = folder,
                Arguments = argument,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            Process process = new Process();
            process.StartInfo = startInfo;
            Console.WriteLine("Argiments recived");
            process.Start();
            Console.Write("Build Started...");
            process.WaitForExit();

            if (process.ExitCode < 0 || process.ExitCode > 0)
                throw new Exception(process.StandardOutput.ReadToEnd());
        }
        static void Main(string[] args)
        {
            bool passed = true;
            while (passed)
            {
                try
                {
                    Console.WriteLine("Starting the build");
                    startBuild();
                    passed = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Try closing the installer folder and press Enter");

                    Console.ReadLine();

                }
            }
            Console.WriteLine("Done");
        }
    }
}
