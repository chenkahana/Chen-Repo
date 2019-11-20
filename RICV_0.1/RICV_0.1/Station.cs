using System;
using System.Collections.Generic;
using System.Text;

namespace RICV
{
    public enum eStationType
    {
        IOS,
        TRAINEE
    }
    class Station
    {
        public string _name;
        public string _IP;
        public bool _connectionStatus;
        public eStationType _type;
        public List<string> paths;

        public void SetConnection()
        {
            //check connction- if exist true, else- false
        }

    }
}
