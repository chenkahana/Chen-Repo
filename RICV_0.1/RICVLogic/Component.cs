using System;
using System.Collections.Generic;
using System.Text;

namespace RICV
{
    public class Component
    {
        public string _path;
        public string _name;
        //key= name, value= version
        public Dictionary<string, string> dllsVersion;
        //key=key in config file, value= value of that key in the config file
        public Dictionary<string, string> configChanges;

    }
}
