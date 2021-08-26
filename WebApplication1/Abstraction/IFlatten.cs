using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Abstraction
{
    public interface IFlatten
    {
        IEnumerable<StopClass> FlatObject(List<RouteClass> input);
    }
}
