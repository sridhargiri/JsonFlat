using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text.Json;
using WebApplication1.Model;
using WebApplication1.Abstraction;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FlattenController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IFlatten _flatten;
        private readonly ILogger<FlattenController> _logger;
        public FlattenController(ILogger<FlattenController> logger, IFlatten flatten)
        {
            _logger = logger; _flatten = flatten;
        }
        [HttpPost]
        public IActionResult Post([FromBody] List<RouteClass> input)
        {
            if (input == null || input.Count == 0) return new BadRequestResult();
            var stoplist = _flatten.FlatObject(input);
            return  Ok(stoplist);
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
            }).ToArray();
        }
    }
}
