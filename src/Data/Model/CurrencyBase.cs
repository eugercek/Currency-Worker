using System;

namespace CurrencyWorker.Data.Model
{
    /// <summary>
    /// All currencies that parsed, needs to have this simple 3 property in order to be usable.
    /// </summary>
    public abstract class CurrencyBase : IComparable
    {
        public abstract decimal BuyingPrice { get; set; }
        public abstract decimal SellingPrice { get; set; }
        public abstract string Name { get; set; }
        public abstract DateTime Date { get; set; }

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
}