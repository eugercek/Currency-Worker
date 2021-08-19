using System;

namespace Money
{
    /// <summary>
    /// All currencies that parsed, needs to have this simple 3 property in order to be usable.
    /// </summary>
    public abstract class CurrencyBase : IComparable
    {
        public abstract decimal BuyingPrice { get; }
        public abstract decimal SellingPrice { get; }
        public abstract string Name { get; }

        public int CompareTo(object other)
        {
            // https://stackoverflow.com/questions/2742276/how-do-i-check-if-a-type-is-a-subtype-or-the-type-of-an-object
            bool isSameOrSubclass(Type potentialBase, Type potentialDescendant)
            {
                return potentialDescendant.IsSubclassOf(potentialBase)
                       || potentialDescendant == potentialBase;
            }

            if (isSameOrSubclass(typeof(CurrencyBase), other.GetType()))
            {
                var res = this.BuyingPrice.CompareTo(((CurrencyBase)other).BuyingPrice);

                if (res == 0)
                {
                    return SellingPrice.CompareTo(((CurrencyBase)other).SellingPrice);
                }
                else
                {
                    return res;
                }
            }
            else
                throw new ArgumentException("Object is not Currency and it's not derived from Currency.");
        }

    }

    /// <summary>
    /// Representation of the https://www.tcmb.gov.tr/kurlar/today.xml.
    /// Field's name are same with the respect of the C#'s conventions.
    /// </summary>
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

        /// <summary>
        /// Returns simple representation of the object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return @$"
            {Isim}
            {ForexBuying}     {ForexSelling}";
        }

        /// <summary>
        /// Returns formatted, pretty string for directly output to command line.
        /// </summary>
        /// <param name="maxWidth"></param>
        public void DrawBoxes(int maxWidth)
        {
            string printOrPass(decimal value) => value == 0 ? "" : value.ToString();

            Console.Write($"{Isim}\n\n" +
            " | " + printOrPass(ForexBuying).PadRight(maxWidth) + "|" + printOrPass(ForexSelling).PadRight(maxWidth) + " |\n" +
            " | " + printOrPass(ForexBuying).PadRight(maxWidth) + "|" + printOrPass(ForexSelling).PadRight(maxWidth) + " |\n\n");
        }
    }
}
