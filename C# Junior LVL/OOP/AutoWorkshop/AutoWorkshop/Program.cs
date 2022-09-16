using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoWorkshop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var buyers = new List<SmartBuyer>();

            buyers.Add(new SmartBuyer(2000, new Detail[]{new Product(new Item("Sword"), 200),
                new Product(new Item("2LVL Sword"), 1000),
                new Product(new Item("3LVL Sword"), 2000),
                new Product(new Item("4LVL Sword"), 10000)}));

            buyers.Add(new SmartBuyer(100, new Detail[]{new Product(new Item("HP Heal Food"), 10),
                new Product(new Item("2LVL HP Heal Food"), 20),
                new Product(new Item("3LVL HP Heal Food"), 25),
                new Product(new Item("4LVL HP Heal Food"), 30)}));

            buyers.Add(new SmartBuyer(200, new Detail[]{new Product(new Item("Sword"), 200),
                new Product(new Item("2LVL Sword"), 1000),
                new Product(new Item("3LVL Sword"), 2000),
                new Product(new Item("4LVL Sword"), 10000)}));

            buyers.Add(new SmartBuyer(5, new Detail[]{new Product(new Item("HP Heal Food"), 10),
                new Product(new Item("2LVL HP Heal Food"), 20),
                new Product(new Item("3LVL HP Heal Food"), 25),
                new Product(new Item("4LVL HP Heal Food"), 30)}));


            var shop = new Shop();
            shop.ServeBuyers(buyers);

            Console.ReadKey();
        }
    }

    public class Shop
    {
        private readonly AutoWorkshop _cashRegister;

        public Shop()
        {
            _cashRegister = new AutoWorkshop();
        }

        public void ServeBuyers(IEnumerable<SmartBuyer> buyers)
        {
            int servicedBuyerIndex = 0;

            foreach (var buyer in buyers)
            {
                Console.WriteLine($"\nShop money {_cashRegister.Wallet.Money}");
                Console.WriteLine($"Shop serve {servicedBuyerIndex} buyer:");
                ServeBuyer(buyer);

                servicedBuyerIndex++;
            }
        }

        private void ServeBuyer(SmartBuyer buyer)
        {
            Console.WriteLine("Client:" +
                $"{buyer.GetInfo()}");

            buyer.PayMaximalPosibleProducts(_cashRegister, out IEnumerable<Detail> payedProducts);

            Console.WriteLine("Client enough money to pay products:");

            foreach (var payedProduct in payedProducts)
            {
                Console.WriteLine(payedProduct.GetInfo());
            }
        }
    }

    public class AutoWorkshop
    {
         
        private readonly Wallet _wallet;

        public ReadOnlyWallet Wallet => _wallet;

        public AutoWorkshop()
        {
            _wallet = new Wallet(0);
        }

        public bool TryAskPayProducts(Client buyer)
        {
            if (buyer.TryPayAllProducts())
            {
                _wallet.TryAddMoney(buyer.Basket.GetTotalPrice());

                return true;
            }

            return false;
        }
    }

    public class Client
    {
        private readonly BrokenCar _brokenCar;
        private readonly Wallet _wallet;

        public Client(int money, BrokenCar brokenCar)
        {
            _wallet = new Wallet(money);

            _brokenCar = brokenCar;
        }

        public string GetInfo()
        {
           return $"\nMoney {_wallet.Money}." + 
                  $"\nBroken car:" +
                  $"\n{_brokenCar.GetInfo()}";
        }
    }

    public class BrokenCar
    {
        public readonly Detail _brokenDetail;

        public bool IsBroken { get; private set; }

        public BrokenCar(Detail brokenDetail)
        {
            _brokenDetail = brokenDetail;

            IsBroken = true;
        }

        public bool TryRepair(in Detail detailToRepair)
        {
            IsBroken = detailToRepair.Equals(_brokenDetail) == false;

            return IsBroken;
        }

        public string GetInfo()
        {
            return $"Broke: {_brokenDetail.GetInfo()}";
        }
    }

    public class Wallet : ReadOnlyWallet
    {
        public Wallet(int money) : base(money)
        {
        }

        public bool TrySpend(int moneyToSpend)
        {
            if (moneyToSpend >= 0 && Money >= moneyToSpend)
            {
                Money -= moneyToSpend;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryAddMoney(int addingMoney)
        {
            if (addingMoney < 0)
                return false;

            Money += addingMoney;
            return true;
        }
    }

    public class ReadOnlyWallet
    {
        public int Money { get; protected set; }

        public ReadOnlyWallet(int money)
        {
            Money = Math.Max(money, 0);
        }
    }

    public struct Detail
    {
        public readonly int Id;

        public readonly string Title;
        public readonly int Price;

        public Detail(int id, string title, int price)
        {
            Id = id;

            Title = title;
            Price = Math.Max(price, 0);
        }

        public string GetInfo()
        {
            return $"ID: {Id} , Title {Title}, Price {Price}.";
        }
    }
}
