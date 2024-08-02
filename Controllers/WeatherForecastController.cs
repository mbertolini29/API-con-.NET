using Microsoft.AspNetCore.Mvc;

namespace API_con_.NET.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    //lista en memoria.
    private static List<WeatherForecast> ListWeatherForecast = new List<WeatherForecast>();

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;

        if(ListWeatherForecast == null || !ListWeatherForecast.Any())
        {    
            ListWeatherForecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToList();            
        }
    }

    [HttpGet(Name = "GetWeatherForecast")]
    [Route("Get/Weatherforecast")]
    [Route("Get/Weatherforecast2")]
    [Route("[action]")]
    public IEnumerable<WeatherForecast> Get()
    {
        //en ambiente de produccion.
        //_logger.LogInformation("Retornando la lista de weatherforecast");
        //en ambiente de desarrollo.
        _logger.LogDebug("Retornando la lista de weatherforecast");
        return ListWeatherForecast;    
    }
    
    [HttpPost]
    public IActionResult Post(WeatherForecast weatherForecast)
    {
        ListWeatherForecast.Add(weatherForecast);

        return Ok();        
    }
    
    [HttpDelete("{index}")]
    public IActionResult Delete(int index)
    {
        ListWeatherForecast.RemoveAt(index);
        
        return Ok(); 
    }
}
