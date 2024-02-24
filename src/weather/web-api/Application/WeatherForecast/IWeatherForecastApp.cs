using Domain.WeatherForecast;

namespace Application.WeatherForecast
{
    public interface IWeatherForecastApp
    {
        public IEnumerable<WeatherForecastDomain> List(int? start, int? offset);
        public WeatherForecastDomain? Get(int id);
        public WeatherForecastDomain? Create(WeatherForecastDomain item);
        public WeatherForecastDomain? Update(WeatherForecastDomain item);
        public WeatherForecastDomain? Delete(int id);
        public IEnumerable<WeatherForecastDomain> Shuffle(int? count);
    }
}
