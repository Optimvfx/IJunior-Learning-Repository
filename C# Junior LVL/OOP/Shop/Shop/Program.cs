using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            uint playerMoney = 125;

            var goods = new Good[] {new Good(new Item("Sword","Bladles"), 100),new Good(new Item("Posion","black and toxic"),15), new Good(new Item("HealPosion", "red and sweat"),25), new Good(new Item("Legendary Sword", "Glow red"), 10000)};
            var shop = new Shop(goods);

            var player = new Player(playerMoney);
            player.StartTraiding(shop);
        }
    }

    public class Player
    {
        private Invectory _invectory;
        private Wallet _wallet;
         
        public Player(uint money)
        {
            _invectory = new Invectory();
            _wallet = new Wallet(money);
        }

        public void StartTraiding(Shop shop)
        {
            const string SeePlayerInfoCommand = "SEEPLAYERINFO";
            const string SeeAllGoodsInfoCommand = "SEEALLGOODS";
            const string BuyGoodCommand = "BUY";
            const string ExitCommand = "EXIT";

            bool isTraiding = true;

            while(isTraiding)
            {
                Console.WriteLine($"\nPosible commands:" +
                    $"\n{SeePlayerInfoCommand}" +
                    $"\n{SeeAllGoodsInfoCommand}" +
                    $"\n{BuyGoodCommand}" +
                    $"\n{ExitCommand}\n");

                var userInput = Console.ReadLine().ToUpper();

                switch (userInput)
                {
                    case SeePlayerInfoCommand:
                        SeePlayerInfo();
                        break;
                    case SeeAllGoodsInfoCommand:
                        SeeAllGoods(shop);
                        break;
                    case BuyGoodCommand:
                        BuyGoods(shop);
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

        private void SeePlayerInfo()
        {
            Console.WriteLine($"\nMoney {_wallet.Money}.\n");

            foreach(var item in _invectory.GetAllItems())
            {
                Console.WriteLine(item.GetInfo());
            }    
        }

        private void SeeAllGoods(Shop shop)
        {
            foreach(var good in shop.GetAllGoods())
            {
                Console.WriteLine(good.GetInfo());
            }
        }

        private void BuyGoods(Shop shop)
        {
            Console.WriteLine("Item to buy: ");
            var itemToBuy = Item.CreateItem();

            if (shop.TryBuyGood(itemToBuy, _invectory, _wallet))
            {
                Console.WriteLine("Good buyed.");
            }
            else
            {
                Console.WriteLine("Good buy invalid!");
            }
        }
    }

    public class Shop
    {
        private readonly List<Good> _goods; 
        private readonly Wallet _wallet;

        public Shop(IEnumerable<Good> goods)
        {
            _goods = goods.ToList();
            _wallet = new Wallet();
        }

        public bool TryBuyGood(Item desiredItem, Invectory to, Wallet paymentWallet)
        {
            if (_goods.Any(good => good.Item.EqualsName(desiredItem)))
            {
                var desiredGood = _goods.FirstOrDefault(good => good.Item.EqualsName(desiredItem));

                if (_wallet.TryGetMoney(paymentWallet, desiredGood.Price))
                {
                    to.Add(desiredGood.Item);
                    _goods.Remove(desiredGood);

                    return true;
                }

                return false;
            }

            return false;
        }

        public IEnumerable<Good> GetAllGoods()
        {
            return _goods;
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
        private readonly static uint _defaultMoney = 0;

        public uint Money { get; private set; }

        public Wallet()
        {
            Money = _defaultMoney;
        }

        public Wallet(uint money)
        {
            Money = money;
        }

        public bool TrySpend(uint moneyToSpend)
        {
            if (Money >= moneyToSpend)
            {
                Money -= moneyToSpend;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryGetMoney(Wallet from, uint requiredMoney)
        {
            if(from.TrySpend(requiredMoney))
            {
                Money += requiredMoney;
                return true;
            }

            return false;
        }
    }

    public struct Good
    {
        public readonly Item Item;
        public readonly uint Price;

        public Good(Item item, uint price)
        {
            Item = item;
            Price = price;
        }

        public string GetInfo()
        {
            return Item.GetInfo() + " " + Price;
        }
    }

    public struct Item
    {
        public readonly string Name;
        public readonly string Description;

        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public static Item CreateItem()
        {
            Console.Write("Item name: ");
            var itemName = Console.ReadLine();

            Console.Write("Item description: ");
            var itemDescription = Console.ReadLine();

            return new Item(itemName, itemDescription);
        }

        public bool EqualsName(Item other)
        {
            return Name.ToUpper() == other.Name.ToUpper();
        }

        public string GetInfo()
        {
            return Name + " " + Description;
        }
    }
}
