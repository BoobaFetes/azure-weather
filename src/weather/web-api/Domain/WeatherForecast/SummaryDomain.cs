using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.WeatherForecast
{
    public class SummaryDomain : IHaveId, IHaveName
    {
        public int? Id { get; private set; }

        public string Name { get; set; } = "";


        public SummaryDomain(int? id = null)
        {
            Id = id;
        }

        public object Clone()
        {
            return new SummaryDomain(Id)
            {
                Name = Name,
            };
        }
    }
}
