using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopUpgrade
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var buyers = new List<SmartBuyer>();

            buyers.Add(new SmartBuyer(2000, new Product[]{new Product(new Item("Sword"), 200), 
                new Product(new Item("2LVL Sword"), 1000),
                new Product(new Item("3LVL Sword"), 2000),
                new Product(new Item("4LVL Sword"), 10000)}));

            buyers.Add(new SmartBuyer(100, new Product[]{new Product(new Item("HP Heal Food"), 10),
                new Product(new Item("2LVL HP Heal Food"), 20),
                new Product(new Item("3LVL HP Heal Food"), 25),
                new Product(new Item("4LVL HP Heal Food"), 30)}));

            buyers.Add(new SmartBuyer(200, new Product[]{new Product(new Item("Sword"), 200),
                new Product(new Item("2LVL Sword"), 1000),
                new Product(new Item("3LVL Sword"), 2000),
                new Product(new Item("4LVL Sword"), 10000)}));

            buyers.Add(new SmartBuyer(5, new Product[]{new Product(new Item("HP Heal Food"), 10),
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
        private readonly CashRegister _cashRegister;

        public Shop()
        {
            _cashRegister = new CashRegister();
        }

        public void ServeBuyers(IEnumerable<SmartBuyer> buyers)
        {
            int servicedBuyerIndex = 0;

            foreach(var buyer in buyers)
            {
                Console.WriteLine($"\nShop money {_cashRegister.Wallet.Money}");
                Console.WriteLine($"Shop serve {servicedBuyerIndex} buyer:");
                ServeBuyer(buyer);

                servicedBuyerIndex++;
            }
        }

        private void ServeBuyer(SmartBuyer buyer)
        {
            Console.WriteLine("Buyer:" +
                $"{buyer.GetInfo()}");

            buyer.PayMaximalPosibleProducts(_cashRegister, out IEnumerable<Product> payedProducts);

            Console.WriteLine("Buyer enough money to pay products:");

            foreach(var payedProduct in payedProducts)
            {
                Console.WriteLine(payedProduct.GetInfo());
            }
        }
    }

    public class CashRegister
    {
        private readonly Wallet _wallet;

        public ReadOnlyWallet Wallet => _wallet;

        public CashRegister()
        {
            _wallet = new Wallet(0);
        }

        public bool TryAskPayProducts(Buyer buyer)
        {
            if(buyer.TryPayAllProducts())
            {
                _wallet.TryAddMoney(buyer.Basket.GetTotalPrice());

                return true;
            }

            return false;
        }
    }

    public class SmartBuyer : Buyer
    {
        public SmartBuyer(int money, IEnumerable<Product> productsInBacket) : base(money, productsInBacket)
        {
        }

        public void PayMaximalPosibleProducts(CashRegister cashRegister, out IEnumerable<Product> payedProducts)
        {
            payedProducts = _basket.GetAllProducts();

            while (cashRegister.TryAskPayProducts(this) == false && _basket.ProductsCount > 0)
            {
                _basket.TryRemoveAnyProduct();

                payedProducts = _basket.GetAllProducts();
            }
        }
    }

    public class Buyer
    {
        protected readonly Basket _basket;

        private Wallet _wallet;

        public ReadOnlyBasket Basket => _basket;

        public Buyer(int money, IEnumerable<Product> productsInBacket)
        {
            _wallet = new Wallet(money);
            _basket = new Basket(productsInBacket);
        }

        public bool TryPayAllProducts()
        {
            int spendeMoney = 0;

            if (_basket.GetTotalPrice() <= _wallet.Money)
            {
                foreach (var product in _basket.GetAllProducts())
                {
                    if (_wallet.TrySpend(product.Price) == false)
                    {
                        _wallet.TryAddMoney(spendeMoney);

                        return false;
                    }

                    spendeMoney += product.Price;
                }

                return true;
            }

            return false;
        }

        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"\nMoney {_wallet.Money}.\n");
            stringBuilder.AppendLine($"Products in basket:");

            foreach (var product in _basket.GetAllProducts())
            {
                stringBuilder.AppendLine(product.GetInfo());
            }

            return stringBuilder.ToString();
        }
    }

    public class Basket : ReadOnlyBasket
    {
        private Random _random; 

        public Basket(IEnumerable<Product> products) : base(products)
        {
            _random = new Random();
        }

        public bool TryRemoveAnyProduct()
        {
            if(GetRandomProductIndex(out int randomIndex))
            {
                _products.RemoveAt(randomIndex);

                return true;
            }

            return false;
        }

        private bool GetRandomProductIndex(out int randomIndex)
        {
            randomIndex = 0;

            if (ProductsCount <= 0)
                return false;

            randomIndex = _random.Next(0, ProductsCount);

            return true;
        }
    }

    public class ReadOnlyBasket
    {
        protected readonly List<Product> _products;

        public int ProductsCount => _products.Count;

        public ReadOnlyBasket(IEnumerable<Product> products)
        {
            _products = products.ToList();
        }

        public int GetTotalPrice()
        {
            return _products.Sum(product => product.Price);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }

        public string GetInfo()
        {
            var stringBuilder = new StringBuilder();

            int productIndex = 0;

            foreach(var product in _products)
            {
                stringBuilder.AppendLine($"{productIndex}: {product.GetInfo()}");
            }

            return stringBuilder.ToString();
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

    public struct Product
    {
        public readonly Item Item;
        public readonly int Price;

        public Product(Item item, int price)
        {
            Item = item;
            Price = Math.Max(price, 0);
        }

        public string GetInfo()
        {
            return Item.GetInfo() + " " + Price;
        }
    }

    public struct Item
    {
        public readonly string Name;

        public Item(string name)
        {
            Name = name;
        }

        public string GetInfo()
        {
            return Name;
        }
    }
}
