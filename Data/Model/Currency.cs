using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyWorker.Data.Model
{

    /// <summary>
    /// Representation of the https://www.tcmb.gov.tr/kurlar/today.xml.
    /// Field's name are same with the respect of the C#'s conventions.
    /// </summary>
    public class Currency : CurrencyBase
    {
        public int CurrencyId { get; set; }

        public string Code { get; set; }

        public override decimal BuyingPrice
        {
            get
            {
                if (Unit == 0)
                {
                    return ForexBuying;
                }
                else
                {
                    return ForexBuying / Unit;
                }
            }
            set { }
        }
        public override decimal SellingPrice
        {
            get
            {
                if (Unit == 0)
                {
                    return ForexSelling;
                }
                else
                {
                    return ForexSelling / Unit;
                }
            }
            set { }
        }
        public override string Name
        {
            get
            {
                return CurrencyName;
            }
            set
            {

            }
        }

        public override DateTime Date
        {
            get
            {
                return DateTime.Today;
            }
            set
            {

            }

        }


        [NotMapped]
        public int Unit;
        public string Isim;
        public string CurrencyName;
        public decimal ForexBuying;
        public decimal ForexSelling;
        public decimal BanknoteBuying;
        public decimal BanknoteSelling;
    }
}
