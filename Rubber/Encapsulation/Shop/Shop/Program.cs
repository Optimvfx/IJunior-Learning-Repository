using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopSpace
{
    class Program
    {
        static void Main(string[] args)
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);

            //Вывод всех товаров на складе с их остатком

            Cart cart = shop.GetCart();
            cart.Add(iPhone12, 4);
            cart.Add(iPhone11, 3); //при такой ситуации возникает ошибка так, как нет нужного количества товара на складе

            //Вывод всех товаров в корзине

            Console.WriteLine(cart.GetOrder().Paylink);

            cart.Add(iPhone12, 9); //Ошибка, после заказа со склада убираются заказанные товары

            Console.ReadLine();
        }
    }

    public class Cart
    {
        private readonly Shop _shop;
        private readonly Inventory _inventory;

        public Cart(Shop shop)
        {
            _shop = shop;
            _inventory = new Inventory();
        }

        public void Add(Good good, int count)
        {
            if (count < 0)
                throw new ArgumentException();

            if (_shop.TryBuy(good, count))
                _inventory.Add(good, count);
            else
                throw new NullReferenceException();
        }

        public Order GetOrder()
        {
            return new Order();
        }

        public class Order
        {
            public string Paylink => (new Random().NextDouble() * int.MaxValue).ToString();
        }
    }

    public class Shop
    {
        private readonly Warehouse _warehouse;

        public Shop(Warehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public Cart GetCart()
        {
            return new Cart(this);
        }

        public bool TryBuy(Good good, int count)
        {
            if (count < 0)
                throw new ArgumentException();

            return _warehouse.TryTake(good, count);
        }
    }

    public class Warehouse
    {
        private readonly Inventory _inventory;

        public IEnumerable<Inventory.ReadOnlySlot> Shelfs => _inventory.Slots;

        public Warehouse()
        {
            _inventory = new Inventory();
        }

        public Warehouse(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void Delive(Good good, int count)
        {
            if (count < 0)
                throw new ArgumentException();

            _inventory.Add(good, count);
        }

        public bool TryTake(Good good, int count)
        {
            if (count < 0)
                throw new ArgumentException();

            return _inventory.TryRemove(good, count);
        }
    }

    public class Inventory
    {
        private readonly List<Slot> _slots;

        public IEnumerable<ReadOnlySlot> Slots => _slots;

        public Inventory(IEnumerable<ReadOnlySlot> slots)
        {
            _slots = slots.Select(slot => new Slot(slot.Good, slot.Count)).ToList();
        }

        public Inventory()
        {
            _slots = new List<Slot>();
        }

        public void Add(Good good, int count)
        {
            if (count < 0)
                throw new ArgumentException();

            if (_slots.Any(shelf => shelf.Good.Equals(good)))
                _slots.First(shelf => shelf.Good.Equals(good)).Add(count);
            else
                _slots.Add(new Slot(good, count));
        }

        public bool TryRemove(Good good, int count)
        {
            if (count < 0)
                throw new ArgumentException();

            if (_slots.Any(shelf => shelf.Good.Equals(good)))
                return _slots.First(shelf => shelf.Good.Equals(good)).TryTake(count);

            return false;
        }

        public class Slot : ReadOnlySlot
        {
            public Slot(Good good, int count) : base(good, count)
            {
            }

            public bool TryTake(int takingCount)
            {
                if (takingCount < 0)
                    throw new ArgumentException();

                if (takingCount > Count || IsEmpty)
                    return false;

                Count -= takingCount;

                return true;
            }

            public void Add(int addingCount)
            {
                if (addingCount < 0)
                    throw new ArgumentException();

                Count += addingCount;
            }
        }

        public class ReadOnlySlot
        {
            public readonly Good Good;
            public int Count { get; protected set; }

            public bool IsEmpty => Count == 0;

            public ReadOnlySlot(Good good, int count)
            {
                if (count < 0)
                    throw new ArgumentException();

                Good = good;
                Count = count;
            }
        }
    }

    public struct Good
    {
        public readonly string Name;

        public Good(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if(obj is Good other)
            {
                return other.Name == Name;
            }

            return false;
        }
    }
}
