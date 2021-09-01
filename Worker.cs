using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using CurrencyWorker.Data.Model;
using CurrencyWorker.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CurrencyWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string URL = _configuration["ParseURL"];
            string fileName = _configuration["TemporaryFile"];
            string dbName = _configuration.GetConnectionString("SQLite");

            using (var client = new WebClient())
            {
                client.DownloadFile(URL, fileName);
            }

            _logger.LogInformation($"Downloaded({fileName}) and wrote file({dbName})");
            var doc = XElement.Load(fileName);

            var currencies = (from node in doc.Descendants("Currency")
                              select new Currency
                              {
                                  Unit = node.Element("Unit").Value.ParseOrDefault<int>(),
                                  Isim = node.Element("Isim").Value.Trim(),
                                  CurrencyName = node.Element("CurrencyName").Value.Trim(),
                                  ForexBuying = node.Element("ForexBuying").Value.ParseOrDefault<decimal>(),
                                  ForexSelling = node.Element("ForexSelling").Value.ParseOrDefault<decimal>(),
                                  BanknoteBuying = node.Element("BanknoteBuying").Value.ParseOrDefault<decimal>(),
                                  BanknoteSelling = node.Element("BanknoteSelling").Value.ParseOrDefault<decimal>(),
                              }).ToList();

            var db = new CurrenciesContext
            {
                DBName = dbName,
            };

            db.Database.EnsureCreated();

            db.Currencies.AddRange(currencies);

            db.SaveChanges();

            _logger.LogInformation($"Saved Databse({dbName})");
        }
    }
}
