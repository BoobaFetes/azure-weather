using Application.Common;
using Application.WeatherForecast.Request;
using Domain.WeatherForecast;
using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.WeatherForecast
{
    public class SummaryRequest : RequestBase<SummaryDomain>, ISummaryRequest
    {
        public static void Initialize()
        {
            InMemories = new Dictionary<int, SummaryDomain>
            {
                {0 , new SummaryDomain(1) { Name = "Freezing" } },
                {1 , new SummaryDomain(2) { Name = "Bracing" } },
                {2 , new SummaryDomain(3) { Name = "Chilly" } },
                {3 , new SummaryDomain(4) { Name = "Cool" } },
                {4 , new SummaryDomain(5) { Name = "Mild" } },
                {5 , new SummaryDomain(6) { Name = "Warm" } },
                {6 , new SummaryDomain(7) { Name = "Balmy" } },
                {7 , new SummaryDomain(8) { Name =  "Hot" } },
                {8 , new SummaryDomain(9) { Name = "Sweltering" } },
                {9 , new SummaryDomain(10) { Name = "Scorching" } }
            };
            PkValue = InMemories.Values.Count;
        }

        protected override SummaryDomain NewInstance(int id, SummaryDomain item)
        {
            return new SummaryDomain(id)
            {
                Name = item.Name,
            };
        }

        public SummaryDomain? GetByName(string name)
        {
            return InMemories.Values.FirstOrDefault(i => i.Name == name);
        }
    }
}
