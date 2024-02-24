using System.Runtime.CompilerServices;
using Application.WeatherForecast;
using Application.WeatherForecast.Request;
using Infrastructure.WeatherForecast;

namespace AppContainer
{
    public static class IOC
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            // bind usecases
            services.AddScoped<IWeatherForecastApp, WeatherForecastApp>();

            // bind requests
            services.AddSingleton<IWeatherForecastRequest, WeatherForecastResquest>();
            services.AddSingleton<ISummaryRequest, SummaryRequest>();

            SummaryRequest.Initialize();
            WeatherForecastResquest.Initialize();

            return services;
        }
    }
}
