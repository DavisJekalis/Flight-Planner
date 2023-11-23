using FlightPlanner.Core.Interfaces;
using FlightPlanner.Validation;

namespace FlightPlanner.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidate, FlightValuesValidator>();
            services.AddTransient<IValidate, AirportValuesValidator>();
            services.AddTransient<IValidate, FlightDateValidator>();
            services.AddTransient<IValidate, SameAirportValidator>();
        }
    }
}
