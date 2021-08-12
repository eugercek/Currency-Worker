using System;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using Extensions;
using Money;

namespace Curr
{
    class Program
    {
        static readonly string URL = "https://www.tcmb.gov.tr/kurlar/today.xml";
        static readonly string fileName = "today.xml";

        static void Main(string[] args)
        {
            DownloadXML();
            XDocument doc = XDocument.Load(fileName);

            var currencies = (from node in doc.Descendants("Currency")
                              select new Currency
                              {
                                  Unit = node.Element("Unit").Value.ParseOrDefault<int>(),
                                  Isim = node.Element("Isim").Value,
                                  CurrencyName = node.Element("CurrencyName").Value,
                                  ForexBuying = node.Element("ForexBuying").Value.ParseOrDefault<decimal>(),
                                  ForexSelling = node.Element("ForexSelling").Value.ParseOrDefault<decimal>(),
                                  BanknoteBuying = node.Element("BanknoteBuying").Value.ParseOrDefault<decimal>(),
                                  BanknoteSelling = node.Element("BanknoteSelling").Value.ParseOrDefault<decimal>(),
                              }).ToDictionary(c => c.Isim, c => c);

            System.Console.Write("Enter an amount: ");
            decimal amount = System.Console.ReadLine().ParseOrDefault<decimal>();

            System.Console.Write("Enter from currency: ");
            string fromCurrencyy = System.Console.ReadLine();

            System.Console.Write("Enter to currency: ");
            string toCurrency = System.Console.ReadLine();

            System.Console.WriteLine($"{fromCurrencyy} -> {toCurrency}");
            var result = amount * (currencies[fromCurrencyy].BanknoteBuying / currencies[toCurrency].BanknoteBuying);
            System.Console.WriteLine(result);
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

