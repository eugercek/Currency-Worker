using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using CurrencyWorker.Data.Model;
using CurrencyWorker.Services;
using CurrencyWorker.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Systemd;

namespace CurrencyWorker
{
    public class CurrencyWorker
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSystemd()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<CurrencyContext>();
                    services.AddHostedService<Worker>();
                    services.AddScoped<IFetchData<XElement>, TcmbFetchData>();
                });
    }
}
