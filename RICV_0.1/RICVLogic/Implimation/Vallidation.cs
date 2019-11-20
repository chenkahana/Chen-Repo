using System;
using System.Collections.Generic;
using System.Text;

namespace RICV
{
    class Vallidation: RICVBase
    {
        public Vallidation(string site) : base(site)
        {

        }

        public override void DoJobForComponent(string componentName)
        {
            throw new NotImplementedException();
        }

        public override void DoJobForSite()
        {
            throw new NotImplementedException();
        }

        public override void DoJobForStation(int stationID)
        {
            throw new NotImplementedException();
        }
    }
}
