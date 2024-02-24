using AppContainer.Controllers.WeatherForecast.DTO;
using AppContainer.Controllers.WeatherForecast.Parameter;
using Application.WeatherForecast;
using Microsoft.AspNetCore.Mvc;

namespace AppContainer.Controllers.WeatherForecast
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> logger;
        private readonly IWeatherForecastApp weatherForecastApp;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastApp weatherForecastApp)
        {
            this.logger = logger;
            this.weatherForecastApp = weatherForecastApp;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecastDTO>> List([FromQuery] int? start, [FromQuery] int? offset)
        {
            try
            {
                var result = weatherForecastApp.List(start, offset);
                logger.Log(LogLevel.Information, $"WeatherForecast.List(start={start}, offset={offset})", result.Count());
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Information, $"WeatherForecast.List(start={start}, offset={offset})", ex);
                return this.Problem(ex.Message, null, 500, "unexpected error", "usecase error");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<WeatherForecastDTO?> Get(int id)
        {
            try
            {
                var result = weatherForecastApp.Get(id);
                logger.Log(LogLevel.Information, $"WeatherForecast.Get(id={id})", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Information, $"WeatherForecast.Get(id={id})", ex);
                return this.Problem(ex.Message, null, 500, "unexpected error", "usecase error");
            }
        }

        [HttpPost]
        public ActionResult<WeatherForecastDTO?> Create(WeatherForecastDTO item)
        {
            try
            {
                var result = weatherForecastApp.Create(item);
                logger.Log(LogLevel.Information, "WeatherForecast.Create", result);
                if (result == null) { return Ok(null); }
                return Created(new Uri($"{Request.PathBase}/${result.Id}"), result);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Information, "WeatherForecast.Create", ex);
                return this.Problem(ex.Message, null, 500, "unexpected error", "usecase error");
            }
        }

        [HttpPut]
        public ActionResult<WeatherForecastDTO?> Update(WeatherForecastDTO item)
        {
            try
            {
                var result = weatherForecastApp.Update(item);
                logger.Log(LogLevel.Information, "WeatherForecast.Update", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Information, "WeatherForecast.Update", ex);
                return this.Problem(ex.Message, null, 500, "unexpected error", "usecase error");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<WeatherForecastDTO?> Delete(int id)
        {
            try
            {
                var result = weatherForecastApp.Delete(id);
                logger.Log(LogLevel.Information, $"WeatherForecast.Delete(id={id})", result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Information, $"WeatherForecast.Delete(id={id})", ex);
                return this.Problem(ex.Message, null, 500, "unexpected error", "usecase error");
            }
        }

        [HttpGet("shuffle/{count}", Name = "ShuffleWeatherForecast")]
        public ActionResult<IEnumerable<WeatherForecastDTO>> Shuffle(int count)
        {
            if (count > 0)
            {
                var result = weatherForecastApp.Shuffle(count);
                logger.Log(LogLevel.Information, $"WeatherForecast.Shuffle(count={count})", result);
                return this.Ok(result);
            }

            return this.NotFound();
        }
    }
}
