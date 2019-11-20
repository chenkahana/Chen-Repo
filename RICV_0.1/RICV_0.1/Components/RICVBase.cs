using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace RICV
{
    public class RICVBase
    {
        // public static SimDAL _dal;
        // public ErrorLog LoggerError;
        // public Station station
        public string _site;
        //public List<Station> _stations;

        public RICVBase(string site)
        {
            _site = site;
        }

        public bool CreateStationList()
        {
            return false;
        }

        public bool init()
        {
            //initiallizing all components



            CreateConnection();
            return false;
        }

        public void CreateConnection()
        {
           // foreach (Station station in _stations)
           // {
            //    station.SetConnection();
            //}

        }

        public virtual void doJob()
        {

        }

        public Form getView()
        {

        }





    }
}
