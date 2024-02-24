using Application.WeatherForecast.Request;
using Domain.WeatherForecast;
using System.Collections;
using System.Collections.Generic;

namespace Application.WeatherForecast
{
    public class WeatherForecastApp : IWeatherForecastApp
    {
        private readonly IWeatherForecastRequest weatherForecastRequest;

        public WeatherForecastApp(IWeatherForecastRequest weatherForecastRequest)
        {
            this.weatherForecastRequest = weatherForecastRequest;
        }

        public IEnumerable<WeatherForecastDomain> List(int? start, int? offset)
        {
            return weatherForecastRequest.List(start, offset).ToArray();
        }

        public IEnumerable<WeatherForecastDomain> Shuffle(int? count)
        {
            int? _count = count;
            var list = weatherForecastRequest.List().ToArray();
            if (_count.HasValue && _count.Value > list.Length)
            {
                _count = null;
            }
            var result = Enumerable.Range(1, _count ?? 5).Select(index => list[index]);
            return result.ToArray();
        }

        public WeatherForecastDomain? Get(int id)
        {
            return weatherForecastRequest.Get(id);
        }

        public WeatherForecastDomain? Create(WeatherForecastDomain item)
        {
            if (item.Id != null)
            {
                throw new InvalidOperationException("ShouldNotBeSet/id");
            }

            return weatherForecastRequest.Set(item);
        }

        public WeatherForecastDomain? Update(WeatherForecastDomain item)
        {
            if (item.Id == null)
            {
                throw new InvalidOperationException("ShouldBeSet/id");
            }

            return weatherForecastRequest.Set(item);
        }

        public WeatherForecastDomain? Delete(int id)
        {
            return weatherForecastRequest.Delete(id);
        }
    }
}
