using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projeto_BD1.Data;
using Projeto_BD1.Data.Contracts;
using Projeto_BD1.DbAccess;
using Projeto_BD1.DbAccess.Contracts;

namespace Tests
{
    public class Program
    {
        public Program()
        {
            var serviceCollection = new ServiceCollection();

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            // Duplicate here any configuration sources you use.
            configurationBuilder.AddJsonFile("AppSettings.json");
            IConfiguration configuration = configurationBuilder.Build();


            serviceCollection.AddSingleton<IConfiguration>(provider => configuration);


            serviceCollection.AddSingleton<ISqlDataAccess, SqlDataAccess>();
            serviceCollection.AddScoped<IDatabaseMethods, DatabaseMethods>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }


    }
}
