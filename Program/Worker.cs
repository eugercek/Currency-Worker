using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Money;

namespace Program
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string URL = "https://www.tcmb.gov.tr/kurlar/today.xml";
            string fileName = "/tmp/today.xml";
            // string dbName = "./Currencies.db";
            string dbName = "/home/umut/src/Currencies.db";

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
