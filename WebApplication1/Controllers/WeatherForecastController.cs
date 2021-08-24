using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text.Json;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
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
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public List<StopClass> Post([FromBody] List<RouteClass> input)
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
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
