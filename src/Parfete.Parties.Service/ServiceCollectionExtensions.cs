using Microsoft.Extensions.DependencyInjection;

namespace Parfete.Parties.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPartiesService(this IServiceCollection services)
        {
            services.AddTransient<ISearchParties, SearchParties>();
            services.AddTransient<IPartiesRepository, PartiesRepository>();
            return services;
        }    
    }    
}