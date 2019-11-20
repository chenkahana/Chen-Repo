using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WatchDog
{
    class Program
    {

        static void Main(string[] args)
        {
            int counter = 0, MaxCounter;
            string PathToProces, RunningMode;
            List<string> watchedProcesses= new List<string>();
            bool DebuggingMode;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.DebuggingMode.ToString()))
            {
                Properties.Settings.Default.DebuggingMode = false;
            }
            DebuggingMode= Properties.Settings.Default.DebuggingMode;
            RunningMode = DebuggingMode ? "Opened" : "Minimized";
            if (!string.IsNullOrEmpty(Properties.Settings.Default.MaxCounter.ToString()))
            {
                PathToProces = Properties.Settings.Default.PathToProcesses;
            }
            else
            {
                PathToProces = ".\\";
            }

                
            if (!string.IsNullOrEmpty(Properties.Settings.Default.MaxCounter.ToString()))
            {
                MaxCounter = Properties.Settings.Default.MaxCounter; //Amount of times the process will try to reset itself
            }
            else
            {
                MaxCounter = 7;
            }
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Processes"].ToString()))
            {
                string[] processes= ConfigurationManager.AppSettings["Processes"].Split(',');
                foreach(string proc in processes)
                {
                    watchedProcesses.Add(proc);
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("No Processes Were Added in Config File");
            }
            ProcessStartInfo process = new ProcessStartInfo(PathToProces+watchedProcesses[0]+".exe");
            process.WindowStyle = DebuggingMode ? ProcessWindowStyle.Normal : ProcessWindowStyle.Minimized;
            Console.WriteLine($"Hello! This app will force run the requsted processes  {MaxCounter} times. The processes wil run in {RunningMode} mode");
            if (!string.IsNullOrEmpty(Properties.Settings.Default.FirstSleep.ToString()))
            {
                Thread.Sleep(Properties.Settings.Default.FirstSleep);
            }
            else
            {
                Thread.Sleep(10000);
            }


            while (true)
            {
                var currentProcesses = Process.GetProcesses().Select(x => x.ProcessName);

                    if (!currentProcesses.Contains(watchedProcesses[0]))
                    {
                    counter++;
                    Console.WriteLine($"Process \'{watchedProcesses[0]}\' crashed! Restarting now");
                        try
                        {
                            if (counter == MaxCounter)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Process \'{watchedProcesses[0]}\' has crashed more then {MaxCounter} times, please check system.");
                                Console.ForegroundColor = ConsoleColor.White;
                                break ;
                            }
                            Process.Start(process);
                             
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine($"Could not restart downed process \'{watchedProcesses[0]}\', {e.Message}");
                        }
                    }
                if (!string.IsNullOrEmpty(Properties.Settings.Default.SleepBetween.ToString()))
                {
                    Thread.Sleep(Properties.Settings.Default.FirstSleep);
                }
                else
                {
                    Thread.Sleep(3000);
                }
            }
            
            Console.ReadLine();
        }


    }
}
