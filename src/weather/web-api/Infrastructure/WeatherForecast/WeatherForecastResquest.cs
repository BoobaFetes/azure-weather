using Application.Common;
using Application.WeatherForecast;
using Application.WeatherForecast.Request;
using Domain.WeatherForecast;
using Infrastructure.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.WeatherForecast
{
    public class WeatherForecastResquest : RequestBase<WeatherForecastDomain>, IWeatherForecastRequest
    {
        public static void Initialize()
        {
            var random = new Random();
            InMemories = Enumerable.Range(0, 9)
            .ToDictionary(index => index, index =>
            {
                return new WeatherForecastDomain(index)
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(Random.Shared.Next(1, 10))),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = (SummaryDomain)SummaryRequest.InMemories[Random.Shared.Next(SummaryRequest.InMemories.Count)].Clone(),
                };
            });
            PkValue = InMemories.Values.Count;
        }

        protected override WeatherForecastDomain NewInstance(int id, WeatherForecastDomain item)
        {
            return new WeatherForecastDomain(id)
            {
                Summary = item.Summary == null ? null : (SummaryDomain)item.Summary.Clone(),
                Date = item.Date,
                TemperatureC = item.TemperatureC,
            };
        }
    }
}
