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
        static readonly string fileName = "today.xml";

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
                              }).ToDictionary(c => c.Isim, c => c);

            var table = CreateTable();
            LoadTable(table, currencies.Select(d => d.Value).ToList());

        }

        static void DownloadXML()
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(URL, fileName);
            }
        }


        static void printCurrencies(Dictionary<string, Currency> currencies)
        {
            int maxWidth =
                currencies
                .Values
                .OrderByDescending(c => c.BanknoteBuying)
                .First().BanknoteBuying
                .ToString()
                .Length;

            foreach (var c in currencies.Values)
                c.DrawBoxes(maxWidth);
        }

        static DataTable CreateTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Unit", typeof(string));
            table.Columns.Add("İsim", typeof(string));
            table.Columns.Add("CurrencyName", typeof(string));
            table.Columns.Add("ForexBuying", typeof(decimal));
            table.Columns.Add("ForexSelling", typeof(decimal));
            table.Columns.Add("BanknoteBuying", typeof(decimal));
            table.Columns.Add("BanknoteSelling", typeof(decimal));
            return table;
        }

        static void LoadTable(DataTable table, List<Currency> currencies)
        {
            foreach (var c in currencies)
            {
                table.Rows.Add(
                    c.Unit,
                    c.Isim,
                    c.CurrencyName,
                    c.ForexBuying,
                    c.ForexSelling,
                    c.BanknoteBuying,
                    c.BanknoteSelling);
            }
        }

        static void PrintDataTable(DataTable table)
        {
            foreach (DataRow dataRow in table.Rows)
            {
                foreach (var item in dataRow.ItemArray)
                {
                    Console.Write($"{item}\t\t");
                }
                System.Console.WriteLine("");
            }
        }
    }
}

