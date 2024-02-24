using Domain.Common;

namespace Domain.WeatherForecast
{
    public class WeatherForecastDomain : IHaveId
    {
        public int? Id { get; private set; }

        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public SummaryDomain? Summary { get; set; }

        public WeatherForecastDomain(int? id = null)
        {
            this.Id = id;
        }

        public object Clone()
        {
            return new WeatherForecastDomain(Id)
            {
                Date = Date,
                Summary = Summary == null ? null : (SummaryDomain)Summary.Clone(),
                TemperatureC = TemperatureC,
            };
        }
    }
}
