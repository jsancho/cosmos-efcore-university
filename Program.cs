using System.IO;
using System.Threading.Tasks;

using CosmosEfCore.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CosmosEfCore
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            await serviceProvider.GetService<University>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddUserSecrets<Program>()
                .Build();

            var services = new ServiceCollection();
            services.AddLogging(builder => builder.AddDebug().AddConsole());

            services.Configure<CosmosSettings>(config.GetSection("CosmosSettings"));

            var serviceProvider = services.BuildServiceProvider();
            var cosmos = serviceProvider.GetService<IOptions<CosmosSettings>>().Value;

            services.AddDbContextPool<UniversityContext>(options =>
            {
                options.UseCosmos(cosmos.ServiceEndpoint, cosmos.Secret, cosmos.DatabaseName);
                options.EnableDetailedErrors(true);
            });

            //var cosmosConfig = Configuration.GetSection("CosmosDb");
            //services.AddDbContext<UniversityContext>(options =>
            //{
            //    options.UseCosmos(cosmosConfig["Endpoint"], cosmosConfig["Secret"], cosmosConfig["DatabaseName"]);
            //    options.EnableDetailedErrors(true);
            //}, ServiceLifetime.Transient);

            services.AddTransient<University>();

            return services;
        }
    }
}
