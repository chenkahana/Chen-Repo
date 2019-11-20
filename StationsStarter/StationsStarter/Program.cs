using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StationsStarter
{
    class Program
    {


        static void Main(string[] args)
        {
            string simHostDir = @"\\192.168.0.100\c\JFTES\IdAllocStandAlone";
            string simHostFile = @"IdAllocServer.exe";

            string simServerDir = @"\\192.168.0.110\c\JFTES\JFTApp\release";
            string simServerFile = @"Bagira.AutoRefreshService.Host.exe";

            string simServerDir2 = @" \\192.168.0.110\c\JFTES\JFTApp\release\DDSService";
            string simServerFile2 = @"JFTApp.DDSService.exe";

            string cgfxbeDir = @"\\192.168.0.120\c\JFTES\JFTApp\release";
            string cgfxbeFile = @"JFTApp.StationManager.exe";

            string mainIOSDir = @" \\192.168.0.150\c\JFTES\JFTApp\release";
            string mainIOSFile = @"JFTApp.StationManager.exe";



            List<string> dirs = new List<string>();
            List<string> files = new List<string>();
            ProcessStartInfo proc = new ProcessStartInfo();


            dirs.Add(simHostDir);
            dirs.Add(simServerDir);
            dirs.Add(simServerDir2);
            dirs.Add(cgfxbeDir);
            dirs.Add(mainIOSDir);

            files.Add(simHostFile);
            files.Add(simServerFile);
            files.Add(simServerFile2);
            files.Add(cgfxbeFile);
            files.Add(mainIOSFile);
            try
            {
                for (int i = 0; i < dirs.Count(); i++)
                {
                    proc.WorkingDirectory = dirs[i];

                    proc.FileName = files[i];

                    proc.Verb = "runas";

                    if (i < 2)
                    {
                        Thread.Sleep(3000);
                    }
                    else if(i+1!=dirs.Count()) 
                    {
                        Thread.Sleep(10000);
                    }

                    Process.Start(proc);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }
}
