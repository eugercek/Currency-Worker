using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Extensions;
using Money;

namespace Curr
{
    class Program
    {
        static readonly string URL = "https://www.tcmb.gov.tr/kurlar/today.xml";
        static readonly string fileName = "/tmp/today.xml";

        static void Main(string[] args)
        {
            DownloadXML();
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
        }

        static void DownloadXML()
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(URL, fileName);
            }
        }
    }

}

