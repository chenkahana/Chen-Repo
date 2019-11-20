using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace RICV
{
    public abstract class RICVBase
    {
        // public static SimDAL _dal;
        // public ErrorLog LoggerError;
        // public Station station
        public string _site;
        public Form _viewer;
        public List<Station> _stations;
        public List<Component> _components;

        //key=name of dll, value=dll version
        public Dictionary<string, string> sharedDlls;


        public RICVBase(string site)
        {
            _site = site;
        }

        public bool CreateStationList()
        {
            return false;
        }

        public bool Init()
        {
            // add if statment to evrey line and write logs accordinally 
            if (SetStationList())
            {
                //write log
            };
            if (SetComponentsList())
            {
                //write log
            }
            if (SetSharedDll())
            {
                //write log
            }
            if (CreateConnection())
            {
                //write log
            }
            return false;
        }

        public bool SetStationList()
        {
            //get all station from DB using _site and enter it to StationsList;
            return true;
        }
        public bool SetComponentsList()
        {
            //get all components from DB and enter it to ComponentsList
            return true;
        }

        public bool SetSharedDll()
        {
            //Initialazing the shareddll list with the latest versions
            return true;
        }
        public bool CreateConnection()
        {
            foreach (Station station in _stations)
            {
                station.SetConnection();
            }
            return true;
        }

        public abstract void DoJobForSite();
        public abstract void DoJobForComponent(string componentName);
        public abstract void DoJobForStation(int stationID);

        public virtual Form getView()
        {
            return _viewer;
        }





    }
}
