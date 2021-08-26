using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class StopClass
    {
        public string routename { get; set; }
        public string stopname { get; set; }
        public string objecttype { get; set; }
        public string objectname { get; set; }
    }
    public class ObjectClass
    {
        public string objecttype { get; set; }
        public string objectname { get; set; }
    }
    public class StopArrayClass
    {
        public List<ObjectClass> objects { get; set; }
        public string stopname { get; set; }
    }
    public class RouteClass
    {
        public string routename { get; set; }
        public List<StopArrayClass> stops { get; set; }
    }
}
