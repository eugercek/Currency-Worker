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
        public decimal RealBuying => ForexBuying / Unit;
        public decimal RealSelling => ForexSelling / Unit;

        public override string ToString()
        {
            return @$"
            {Isim}
            {ForexBuying}     {ForexSelling}";
        }

        public void DrawBoxes(int maxWidth)
        {
            string printOrPass(decimal value) => value == 0 ? "" : value.ToString();

            Console.Write($"{Isim}\n\n" +
            " | " + printOrPass(ForexBuying).PadRight(maxWidth) + "|" + printOrPass(ForexSelling).PadRight(maxWidth) + " |\n" +
            " | " + printOrPass(ForexBuying).PadRight(maxWidth) + "|" + printOrPass(ForexSelling).PadRight(maxWidth) + " |\n\n");
        }
    }
}
