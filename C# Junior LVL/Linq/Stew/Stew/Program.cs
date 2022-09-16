using System;
using System.Collections.Generic;
using System.Linq;

namespace Stew
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int currentYear = DateTime.Now.Year;

            var stews = new List<Stew>();

            stews.Add(new Stew("Boar Tugovlky", 2000,22));
            stews.Add(new Stew("Boar Tugovlky", 1990, 25));
            stews.Add(new Stew("Boar Tugovlky", 1999, 25));
            stews.Add(new Stew("Boar Tugovlky", 1997, 30));
            stews.Add(new Stew("Chicken Canned", 1977, 50));
            stews.Add(new Stew("Chicken Canned", 1937, 50));
            stews.Add(new Stew("Chicken Canned", 1967, 50));
            stews.Add(new Stew("Chicken Canned", 1995, 50));

            var shelfWithStews = new ShelfWithStews(stews);

            Console.WriteLine("Overdued stews:");

            foreach(var stew in shelfWithStews.GetOverdueStews(currentYear))
            {
                Console.WriteLine(stew.GetInfo());
            }

            Console.ReadKey();
        }
    }

    public class ShelfWithStews
    {
        private readonly IEnumerable<Stew> _stews;

        public ShelfWithStews(IEnumerable<Stew> stews)
        {
            _stews = stews;
        }

        //переменная currentYear не ограничена по причине того что год так же может быть до нашей еры.
        public IEnumerable<Stew> GetOverdueStews(int currentYear)
        {
            return _stews.Where(stew => stew.IsOverdue(currentYear));
        }
    }

    public struct Stew
    {
        public readonly string Title;

        public readonly int ManufacturetInYears;
        public readonly int ShelfLifeInYears;

        public Stew(string title, int manufacturetYear, int shelfLifeYears)
        {
            Title = title;

            ManufacturetInYears = Math.Max(manufacturetYear, 0);
            ShelfLifeInYears = Math.Max(shelfLifeYears, 0);
        }

        //переменная currentYear не ограничена по причине того что год так же может быть до нашей еры.
        public bool IsOverdue(int currentYear)
        {
            return ManufacturetInYears + ShelfLifeInYears < currentYear;
        }

        public string GetInfo()
        {
            return $"{Title} , Manufacturet year {ManufacturetInYears} , shelf life years {ShelfLifeInYears}.";
        }
    }
}
