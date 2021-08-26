using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Abstraction;
using WebApplication1.Model;

namespace WebApplication1.Implementation
{
    public class Flatten : IFlatten
    {
        public IEnumerable<StopClass> FlatObject(List<RouteClass> input)
        {
            List<StopClass> stoplist = new List<StopClass>();
            foreach (var u in input)
            {
                foreach (var stop in u.stops)
                {
                    foreach (var o in stop.objects)
                    {
                        StopClass cls = new StopClass();
                        cls.routename = u.routename;
                        cls.stopname = stop.stopname;
                        cls.objectname = o.objectname;
                        cls.objecttype = o.objecttype;
                        stoplist.Add(cls);
                    }
                }

            }
            return stoplist;
        }
    }
}
