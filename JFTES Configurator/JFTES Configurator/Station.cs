using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFTES_Configurator
{
    public class Station
    {
        private string name;
        private string IP;
        private int ID;
        private bool isPilot;
        private bool hasSME;

        public Station(string name, string iP, int iD, bool isPilot, bool hasSME)
        {
            setName(name);
            setIP(iP);
            setID(iD);
            setIsPilot(isPilot);
            setHasSME(hasSME);
        }
        public Station() { }

        

        public string getName()
        {
            return name;
        }
        public void setName(string value)
        {
            name = value;
        }
        public string getIP()
        {
            return IP;
        }
        public void setIP(string value)
        {
            IP = value;
        }
        public int getID()
        {
            return ID;
        }
        public void setID(int value)
        {
            ID = value;
        }
        public bool getIsPilot()
        {
            return isPilot;
        }
        public void setIsPilot(bool value)
        {
            isPilot = value;
        }
        public bool getHasSME()
        {
            return hasSME;
        }
        public void setHasSME(bool value)
        {
            hasSME = value;
        }

        override
        public String ToString()
        {
            return this.getName() + ", " + this.getIP() + ", " + this.getID() + ", " + this.getIsPilot() + ", " + this.getHasSME();
        }
    }
}
