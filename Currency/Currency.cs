using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Money
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

    /// <summary>
    /// Representation of the https://www.tcmb.gov.tr/kurlar/today.xml.
    /// Field's name are same with the respect of the C#'s conventions.
    /// </summary>
    public class Currency : CurrencyBase
    {
        public int CurrencyId { get; set; }
        public override decimal BuyingPrice
        {
            get
            {
                return ForexBuying / Unit;
            }
            set { }
        }
        public override decimal SellingPrice
        {
            get
            {

                return ForexSelling / Unit;
            }
            set { }
        }
        public override string Name
        {
            get
            {
                return Isim;
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

    public class CurrenciesContext : DbContext
    {
        public string DBName { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={DBName}");
        }

    }
}
