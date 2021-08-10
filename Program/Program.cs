using System;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using Money;

namespace Curr
{
    class Program
    {

        static readonly string URL = "https://www.tcmb.gov.tr/kurlar/today.xml";
        static readonly string fileName = "today.xml";

        static void Main(string[] args)
        {
            float floatParser(string str) => string.IsNullOrEmpty(str) ? 0 : float.Parse(str);
            DownloadXML();
            XDocument doc = XDocument.Load(fileName);

            var res = (from node in doc.Descendants("Currency")
                       select new Currency
                       {
                           Unit = Int32.Parse(node.Element("Unit").Value),
                           Isim = node.Element("Isim").Value,
                           CurrencyName = node.Element("CurrencyName").Value,
                           ForexBuying = floatParser(node.Element("ForexBuying").Value),
                           ForexSelling = floatParser(node.Element("ForexSelling").Value),
                           BanknoteBuying = floatParser(node.Element("BanknoteBuying").Value),
                           BanknoteSelling = floatParser(node.Element("BanknoteSelling").Value)
                       }).ToArray();

            foreach (var obj in res)
            {
                System.Console.WriteLine(obj.ToString());
            }
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

