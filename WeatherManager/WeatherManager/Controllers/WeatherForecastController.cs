using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WeatherManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ValuesHolder _holder;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ValuesHolder holder)
        {
            _logger = logger;
            _holder = holder;
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

        [HttpPost("create")]
        public IActionResult Create([FromQuery] int input)
        {
            var rng = new Random();
            _holder.Add(new WeatherForecast(DateTime.Now, input, Summaries[rng.Next(Summaries.Length)]));
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult Read()
        {
            return Ok(_holder.Get());
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime dateTimeToUpdate, [FromQuery] int newValue)
        {
            for (int i = 0; i < _holder.GetValuesCount(); i++)
            {
                if (_holder.Get()[i].Date == dateTimeToUpdate)
                {
                    _holder.Get()[i].TemperatureC= newValue;
                    break;
                }
            }
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime dateTimeToDelete)
        {
            _holder.DeleteValue(dateTimeToDelete);
            return Ok();
        }

    }
}
