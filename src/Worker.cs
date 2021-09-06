using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using CurrencyWorker.Data.Model;
using CurrencyWorker.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CurrencyWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHostApplicationLifetime _life;
        private readonly CurrencyContext _context;

        public Worker(ILogger<Worker> logger,
            IConfiguration configuration,
            IHostApplicationLifetime hostApplicationLifetime,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _configuration = configuration;
            _life = hostApplicationLifetime;
            _context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<CurrencyContext>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _context.Database.GetService<IMigrator>().Migrate();
            string URL = _configuration["ParseURL"];
            string fileName = _configuration["TemporaryFile"];

            using (var client = new WebClient())
            {
                client.DownloadFile(URL, fileName);
            }

            _logger.LogInformation($"Downloaded({fileName}) and wrote file");

            XElement doc = XElement.Load(fileName);

            var currencies = (from node in doc.Descendants("Currency")
                              select new Currency
                              {
                                  Code = node.Attribute("CurrencyCode").Value,
                                  Unit = node.Element("Unit").Value.ParseOrDefault<int>(),
                                  Isim = node.Element("Isim").Value.Trim(),
                                  CurrencyName = node.Element("CurrencyName").Value.Trim(),
                                  ForexBuying = node.Element("ForexBuying").Value.ParseOrDefault<decimal>(),
                                  ForexSelling = node.Element("ForexSelling").Value.ParseOrDefault<decimal>(),
                                  BanknoteBuying = node.Element("BanknoteBuying").Value.ParseOrDefault<decimal>(),
                                  BanknoteSelling = node.Element("BanknoteSelling").Value.ParseOrDefault<decimal>(),
                              }).ToList();

            _context.Currencies.AddRange(currencies);

            _context.SaveChanges();

            _logger.LogInformation($"Saved Database");
            _life.StopApplication();
        }
    }
}
