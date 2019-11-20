using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Specialized;

namespace RelocatingAppConfigTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // open config
            Configuration config = ConfigurationManager.OpenExeConfiguration(@"C:\JFTES\RelocatingAppConfigTest.exe.config");
            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Full);

            var appSettings =config.GetSection("appSettings");

           // string test = appSettings.CurrentConfiguration.ToString();
            //print(test);
                
        }

        public static void print(string test)
        {
            Console.WriteLine(test);
            Console.ReadLine();
        }
    }
}
