using System;

namespace Money
{
    public struct Currency
    {
        public int Unit;
        public string Isim;
        public string CurrencyName;
        public float ForexBuying;
        public float ForexSelling;
        public float BanknoteBuying;
        public float BanknoteSelling;

        public override string ToString()
        {
            const int boxWidth = 3;
            return $"{Isim}\n" +
            $"|{ForexBuying.ToString(),boxWidth}|{ForexSelling,boxWidth}|\n" +
            $"|{BanknoteBuying.ToString(),boxWidth}|{BanknoteSelling,boxWidth}|\n";
        }
    }
}
