using System;

namespace RICV
{
    public class Restore : RICVBase
    {
        public Restore(string site) : base(site)
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
