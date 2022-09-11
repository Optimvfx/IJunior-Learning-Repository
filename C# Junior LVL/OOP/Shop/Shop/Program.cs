using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int playerMoney = 125;

            var product = new Product[] { new Product(new Item("Sword"), 100), new Product(new Item("Posion"), 15), new Product(new Item("HealPosion"), 25), new Product(new Item("Legendary Sword"), 10000) };
            var shop = new Shop(product);

            var buyer = new Buyer(playerMoney);

            shop.StartTraiding(buyer);
        }
    }

    public class Buyer
    {
        private Invectory _invectory;
        private Wallet _wallet;
         
        public Buyer(int money)
        {
            _invectory = new Invectory();
            _wallet = new Wallet(money);
        }

        public bool TrySpendMoney(int moneyToSpend)
        {
            return _wallet.TrySpend(moneyToSpend);  
        }

        public void AddItem(Item item)
        {
            _invectory.Add(item);
        }

        public void SeeInfo()
        {
            Console.WriteLine($"\nMoney {_wallet.Money}.\n");

            foreach(var item in _invectory.GetAllItems())
            {
                Console.WriteLine(item.GetInfo());
            }    
        }
    }

    public class Shop
    {
        private readonly Traider _traider;

        public Shop(IEnumerable<Product> products)
        {
            _traider = new Traider(products);
        }

        public void StartTraiding(Buyer buyer)
        {
            const string SeeBuyerInfoCommand = "SEEBUYERINFO";
            const string SeeAllProductInfoCommand = "SEEALLPRODUCTS";
            const string BuyProductCommand = "BUY";
            const string ExitCommand = "EXIT";

            bool isTraiding = true;

            while (isTraiding)
            {
                Console.WriteLine($"\nPosible commands:" +
                    $"\n{SeeBuyerInfoCommand}" +
                    $"\n{SeeAllProductInfoCommand}" +
                    $"\n{BuyProductCommand}" +
                    $"\n{ExitCommand}\n");

                var userInput = Console.ReadLine().ToUpper();

                switch (userInput)
                {
                    case SeeBuyerInfoCommand:
                        SeeBuyerInfo(buyer);
                        break;
                    case SeeAllProductInfoCommand:
                        SeeAllProduct();
                        break;
                    case BuyProductCommand:
                        BuyProduct(buyer);
                        break;
                    case ExitCommand:
                        isTraiding = false;
                        break;
                    default:
                        Console.WriteLine("Uncnown command!");
                        break;
                }
            }

            Console.ReadKey();
            Console.Clear();
        }

        private void SeeBuyerInfo(Buyer buyer)
        {
            buyer.SeeInfo();
        }

        private void SeeAllProduct()
        {
            int indexOfProduct = 0;

            foreach (var product in _traider.GetAllProducts())
            {
                Console.WriteLine($"{indexOfProduct} : {product.GetInfo()}");
                indexOfProduct++;
            }
        }

        private void BuyProduct(Buyer buyer)
        {
            Console.Write("Index of produduct: ");

            if (int.TryParse(Console.ReadLine(), out int requiredProductIndex) && _traider.TryGetProductPrice(requiredProductIndex, out int price))
            {
                if (buyer.TrySpendMoney(price) && _traider.TryAddMoney(price))
                {
                    Console.WriteLine("Product buyed.");

                    _traider.TryGiveAwayItem(requiredProductIndex, out Item buyedItem);
                    buyer.AddItem(buyedItem);
                }
                else
                {
                    Console.WriteLine("Product buy invalid!");
                }
            }
            else
            {
                Console.WriteLine("Index of product invalid!");
            }
        }

        private class Traider
        {
            private readonly List<Product> _products;
            private readonly Wallet _wallet;

            public Traider(IEnumerable<Product> products)
            {
                _products = products.ToList();
                _wallet = new Wallet();
            }

            public bool TryGiveAwayItem(int index, out Item item)
            {
                item = new Item();

                if (ItemIndexInBounds(index))
                {
                    item = _products[index].Item;
                    _products.RemoveAt(index);

                    return true;
                }

                return false;
            }

            public bool TryGetProductPrice(int index, out int price)
            {
                price = 0;

                if (ItemIndexInBounds(index))
                {
                    price = _products[index].Price;

                    return true;
                }

                return false;
            }

            public bool TryAddMoney(int money)
            {
                return _wallet.TryAddMoney(money);
            }

            public IEnumerable<Product> GetAllProducts()
            {
                return _products;
            }

            private bool ItemIndexInBounds(int indexOfItem)
            {
                return indexOfItem >= 0 && indexOfItem < _products.Count;
            }
        }
    }

    public class Invectory
    {
        private List<Item> _items;

        public Invectory()
        {
            _items = new List<Item>();
        }

        public Invectory(IEnumerable<Item> items)
        {
            _items = items.ToList();
        }

        public void Add(Item item)
        {
            _items.Add(item);
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _items;
        }
    }

    public class Wallet
    {
        public int Money { get; private set; }

        public Wallet()
        {
            Money = 0;
        }

        public Wallet(int money)
        {
            Money = Math.Max(money, 0);
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
