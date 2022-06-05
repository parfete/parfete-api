using Microsoft.Extensions.DependencyInjection;

namespace Parfete.Parties
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddParties(this IServiceCollection services)
        {
            services.AddTransient<IGetParties, GetParties>();
            return services;
        }    
    }    
}