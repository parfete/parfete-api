using Microsoft.Extensions.DependencyInjection;

namespace Parfete.Users.Service
{
    public static class Configuration
    {
        public static IServiceCollection AddUserService(this IServiceCollection services)
        {
            InitializeTestDatabase();
            services.AddDbContext<UserContext>();
            return services;
        }

        private static void InitializeTestDatabase()
        {
            var dbcontext = new UserContext();
            // To uncomment on database change
            //dbcontext.Database.EnsureDeleted();
            //dbcontext.Database.EnsureCreated();
        }
    }
}