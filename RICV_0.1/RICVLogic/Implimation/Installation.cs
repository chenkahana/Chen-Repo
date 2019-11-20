using System;
using System.Collections.Generic;
using System.Text;

namespace RICV
{
    class Installation: RICVBase
    {
        public Installation(string site) : base(site)
        {


        }


        public override void DoJobForSite()
        {
            //Installing all components in site's stations by using _site from base

            throw new NotImplementedException();
        }

        public override void DoJobForComponent(string componentName)
        {

            //Installing a components in all site's stations

            throw new NotImplementedException();
        }

        public override void DoJobForStation(int stationID)
        {

            //Installing all components in a specific station

            throw new NotImplementedException();
        }
    }
}
