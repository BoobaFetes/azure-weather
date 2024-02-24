using Application.Common;
using Domain.WeatherForecast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WeatherForecast.Request
{
    public interface IWeatherForecastRequest : IExternalRequest<WeatherForecastDomain>
    {
    }
}
