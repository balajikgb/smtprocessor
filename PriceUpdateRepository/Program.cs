using PriceUpdateRepository.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace PriceUpdateRepository
{
    class Program
    {
        private readonly IConfigurationRoot _configuration;

        public Program()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json")
                .Build();
        }
        static void Main(string[] args)
        {

            new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Program>()
                .Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var currentAssembly = "DataLayer.Migrations";
            var connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            //var currentAssembly1 = "DataLayer.Migrations";
            //var connectionString1 =
            //    _configuration.GetConnectionString("DefaultConnection1");

            services.AddDbContext<Context>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(
                    connectionString,
                    builder => builder.MigrationsAssembly(currentAssembly));
            });
            //services.AddDbContext<ExternalDbContext>(optionsBuilder =>
            //{
            //    optionsBuilder.UseSqlServer(
            //        connectionString1,
            //        builder => builder.MigrationsAssembly(currentAssembly1));
            //});
        }
    }
}
