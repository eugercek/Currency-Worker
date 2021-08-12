using System;

namespace Money
{
    public class Currency
    {
        public int Unit;
        public string Isim;
        public string CurrencyName;
        public decimal ForexBuying;
        public decimal ForexSelling;
        public decimal BanknoteBuying;
        public decimal BanknoteSelling;

        public override string ToString()
        {
            const int boxWidth = 3;
            return $"{Isim}\n" +
            $"|{ForexBuying.ToString(),boxWidth}|{ForexSelling,boxWidth}|\n" +
            $"|{BanknoteBuying.ToString(),boxWidth}|{BanknoteSelling,boxWidth}|\n";
        }
    }
}
