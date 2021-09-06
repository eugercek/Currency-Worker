using System.Net;
using System.Xml.Linq;
using CurrencyWorker.Services.Contracts;
using Microsoft.Extensions.Configuration;

namespace CurrencyWorker.Services
{
    public class TcmbFetchData : IFetchData<XElement>
    {
        private readonly string fileName;
        private readonly string url;

        public TcmbFetchData(IConfiguration configuration)
        {
            fileName = configuration["TemporaryFile"];
            url = configuration["ParseURL"];
        }

        public void DownloadData()
        {
            using var client = new WebClient();

            client.DownloadFile(url, fileName);
        }

        XElement IFetchData<XElement>.ReturnData()
        {
            return XElement.Load(fileName);
        }
    }
}