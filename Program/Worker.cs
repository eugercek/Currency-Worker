using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        readonly string fileName = "/tmp/today.xml";

        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            string URL = "https://www.tcmb.gov.tr/kurlar/today.xml";
            using (var client = new WebClient())
            {
                client.DownloadFile(URL, fileName);
            }
            _logger.LogInformation("Downloaded and wrote today.xml file");

            return Task.CompletedTask;

        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            File.Copy(fileName, "/tmp/old.xml");
            File.Delete(fileName);
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

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
                DBName = "Currencies.db",
            };
            db.Database.EnsureCreated();

            db.Currencies.AddRange(currencies);

            db.SaveChanges();

            _logger.LogInformation("Saved Databse");
        }
    }
}
