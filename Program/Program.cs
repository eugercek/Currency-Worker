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
            DownloadXML();
            XDocument doc = XDocument.Load(fileName);

            var res = from node in doc.Descendants("Currency")
                      select new Currency
                      {
                          Unit = Int32.Parse(node.Element("Unit").Value),
                          Isim = node.Element("Isim").Value,
                          CurrencyName = node.Element("CurrencyName").Value,
                          ForexBuying = float.Parse(node.Element("ForexBuying").Value),
                          ForexSelling = float.Parse(node.Element("ForexSelling").Value),
                          BanknoteBuying = float.Parse(node.Element("BanknoteBuying").Value),
                          BanknoteSelling = float.Parse(node.Element("BanknoteSelling").Value)
                      };

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

