using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolicyBased.Authorization;
using Shared;

namespace PolicyBased.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = Policies.AdminPolicy)]
public class WeatherForecastController : ControllerBase
{
    private static readonly IEnumerable<string> Summaries =
        new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries.ElementAt(Random.Shared.Next(Summaries.Count()))
            })
            .ToArray();
    }
}