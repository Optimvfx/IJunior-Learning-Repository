using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoWorkshop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int penaltyForInvalidDetail = 100;
            int priceOfRepair = 100;

            var clients = new List<AutoWorkshop.Client>();

            clients.Add(new AutoWorkshop.Client(300, new BrokenCar(new Detail("Fuil Tank",150))));
            clients.Add(new AutoWorkshop.Client(500, new BrokenCar(new Detail("Fuil Tank", 150))));
            clients.Add(new AutoWorkshop.Client(100, new BrokenCar(new Detail("Fuil Tank", 150))));
            clients.Add(new AutoWorkshop.Client(250, new BrokenCar(new Detail("Fuil Tank", 150))));
            clients.Add(new AutoWorkshop.Client(2000, new BrokenCar(new Detail("Wheel", 1500))));
            clients.Add(new AutoWorkshop.Client(5000, new BrokenCar(new Detail("Wheel", 1500))));
            clients.Add(new AutoWorkshop.Client(1000, new BrokenCar(new Detail("Wheel", 1500))));
            clients.Add(new AutoWorkshop.Client(6000, new BrokenCar(new Detail("Wheel", 1500))));

            var autoWorkshopDetails = new List<Detail>();

            autoWorkshopDetails.Add(new Detail("Fuil Tank", 150));
            autoWorkshopDetails.Add(new Detail("Fuil Tank", 150));
            autoWorkshopDetails.Add(new Detail("Wheel", 1500));
            autoWorkshopDetails.Add(new Detail("Fuil Tank", 150));

            var autoWorkshopTerminal = new AutoWorkshopTerminal(autoWorkshopDetails, penaltyForInvalidDetail, priceOfRepair);
            autoWorkshopTerminal.ServeClients(clients);

            Console.ReadKey();
        }
    }

    public class AutoWorkshopTerminal
    {
        private readonly AutoWorkshop _autoWorkshop;

        public AutoWorkshopTerminal(IEnumerable<Detail> details, int penaltyForInvalidDetail, int priceOfRepair)
        {
            _autoWorkshop = new AutoWorkshop(details, penaltyForInvalidDetail, priceOfRepair);
        }

        public void ServeClients(IEnumerable<AutoWorkshop.Client> clients)
        {
            int servicedBuyerIndex = 0;

            foreach (var client in clients)
            {
                Console.WriteLine($"\nAutoWorkshop money {_autoWorkshop.Wallet.Money}");
                Console.WriteLine($"AutoWorkshop serve {servicedBuyerIndex} client:");
                ServeClient(client);

                servicedBuyerIndex++;
            }
        }

        private void ServeClient(AutoWorkshop.Client client)
        {
            Console.WriteLine("Details in auto workshop:");
            ShowDetails(_autoWorkshop.GetAllDetails());

            Console.WriteLine($"\nClient info: {client.GetInfo()}");

            Console.Write("Index of detail to repair: ");

            if (int.TryParse(Console.ReadLine(), out int detailIndex))
            {
                var repairResult = _autoWorkshop.TryServeClient(client, detailIndex);

                switch (repairResult)
                {
                    case AutoWorkshop.RepairResult.NotEnoughMoney:
                        Console.WriteLine("Client not enough money!");
                        break;
                    case AutoWorkshop.RepairResult.NotBroken:
                        Console.WriteLine("Auto is not broken");
                        break;
                    case AutoWorkshop.RepairResult.InvalidDetail:
                        Console.WriteLine("You fail repair and pay a penalty!");
                        break;
                    case AutoWorkshop.RepairResult.Seccess:
                        Console.WriteLine("Repair secces.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }
        }

        private void ShowDetails(IEnumerable<Detail> details)
        {
            var stringBuilder = new StringBuilder();

            Console.WriteLine($"Details in workshop:");

            int detailIndex = 0;

            foreach (var detail in details)
            {
                Console.WriteLine($"{detailIndex} : {detail.GetInfo()}");

                detailIndex++;
            }
        }
    }

    public class AutoWorkshop
    {
        private readonly List<Detail> _details;

        private readonly Wallet _wallet;

        private readonly int _penaltyForInvalidDetail;
        private readonly int _priceOfRepair;

        public ReadOnlyWallet Wallet => _wallet;

        public AutoWorkshop(IEnumerable<Detail> details, int penaltyForInvalidDetail, int priceOfRepair)
        {
            _details = details.ToList();

            _wallet = new Wallet(0);

            _penaltyForInvalidDetail = Math.Max(penaltyForInvalidDetail, 0);
            _priceOfRepair = Math.Max(priceOfRepair, 0);
        }

        public IEnumerable<Detail> GetAllDetails()
        {
            return _details;
        }

        public RepairResult TryServeClient(Client client, int detailIndex)
        {
            var repairResult = default(RepairResult);

            if (DetailIndexInBounds(detailIndex) == false)
            {
                repairResult = RepairResult.InvalidDetail;
            }
            else
            {
                repairResult = client.TryRepair(_details[detailIndex], _priceOfRepair);
            }

            if (repairResult == RepairResult.InvalidDetail)
            {
                PayPenalty();
            }
            else if (repairResult == RepairResult.Seccess)
            {
                _details.RemoveAt(detailIndex);
                _wallet.TryAddMoney(client.BrokenCar.BrokenDetail.Price + _priceOfRepair);
            }

            return repairResult;
        }

        private bool DetailIndexInBounds(int detailIndex)
        {
            return detailIndex >= 0 && detailIndex < _details.Count;
        }

        private void PayPenalty()
        {
            var moneyPanalty = Math.Min(_penaltyForInvalidDetail, _wallet.Money);

            _wallet.TrySpend(moneyPanalty);
        }

        public class Client
        {
            private readonly BrokenCar _brokenCar;
            private readonly Wallet _wallet;

            public ReadOnlyBrokenCar BrokenCar => _brokenCar;
            public ReadOnlyWallet Wallet => _wallet;

            public Client(int money, BrokenCar brokenCar)
            {
                _wallet = new Wallet(money);

                _brokenCar = brokenCar;
            }

            public RepairResult TryRepair(in Detail detail, int priceOfRepair)
            {
                if(_brokenCar.BrokenDetail.Equals(detail) == false)
                {
                    return RepairResult.InvalidDetail;
                }

                if(_brokenCar.IsBroken == false)
                {
                    return RepairResult.NotBroken;
                }

                if (priceOfRepair < 0 || _wallet.TrySpend(detail.Price + priceOfRepair) == false)
                {
                    return RepairResult.NotEnoughMoney;
                }

                if (_brokenCar.TryRepair(detail))
                {
                    return RepairResult.Seccess;
                }
                else
                {
                    _wallet.TryAddMoney(detail.Price);

                    return RepairResult.InvalidDetail;
                }
            }

            public string GetInfo()
            {
                return $"\nMoney {_wallet.Money}." +
                       $"\nBroken car:" +
                       $"\n{_brokenCar.GetInfo()}";
            }
        }

        public enum RepairResult
        {
            NotEnoughMoney,
            NotBroken,
            InvalidDetail,
            Seccess
        }
    }

    public class BrokenCar : ReadOnlyBrokenCar
    {
        public BrokenCar(Detail brokenDetail) : base(brokenDetail)
        {
        }

        public bool TryRepair(in Detail detailToRepair)
        {
            if (IsBroken == false)
                return false;

            IsBroken = BrokenDetail.Equals(detailToRepair) == false;

            return IsBroken == false;
        }
    }

    public class ReadOnlyBrokenCar
    {
        public readonly Detail BrokenDetail;

        public bool IsBroken { get; protected set; }

        public ReadOnlyBrokenCar(Detail brokenDetail)
        {
            BrokenDetail = brokenDetail;

            IsBroken = true;
        }

        public string GetInfo()
        {
            return $"IsBroken: {IsBroken}" +
                $"\nBroke: {BrokenDetail.GetInfo()}";
        }
    }

    public class Wallet : ReadOnlyWallet
    {
        public Wallet(int money) : base(money)
        {
        }

        public bool TrySpend(int moneyToSpend)
        {
            if (moneyToSpend > 0 && Money >= moneyToSpend)
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
        public readonly string Title;
        public readonly int Price;

        public Detail(string title, int price)
        {
            Title = title;
            Price = Math.Max(price, 0);
        }

        public string GetInfo()
        {
            return $"Title {Title}, Price {Price}.";
        }

        public override bool Equals(object obj)
        {
            if (obj is Detail other)
            {
                return Title == other.Title;
            }

            return false;
        }
    }
}
