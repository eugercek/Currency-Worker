using System;

namespace Money
{
    public abstract class CurrencyBase
    {
        public abstract decimal BuyingPrice { get; }
        public abstract decimal SellingPrice { get; }
        public abstract string Name { get; }
    }

    public class Currency : CurrencyBase
    {
        public int Unit;
        public string Isim;
        public string CurrencyName;
        public decimal ForexBuying;
        public decimal ForexSelling;
        public decimal BanknoteBuying;
        public decimal BanknoteSelling;

        public override decimal BuyingPrice => ForexBuying / Unit;
        public override decimal SellingPrice => ForexSelling / Unit;
        public override string Name => Isim;

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
