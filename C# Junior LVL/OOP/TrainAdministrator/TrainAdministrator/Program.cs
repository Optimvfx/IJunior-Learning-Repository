using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainAdministrator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RailwayStationTerminal railwayStationTerminal = new RailwayStationTerminal(100);

            railwayStationTerminal.Activate();

            Console.ReadKey();
        }
    }

    public static class CreateByUserInput
    {
        public static void Create(int requiredСapacity, out TrainPlan train)
        {
            var carriages = new Stack<Carriage>();

            while (Carriage.GetCarriagesCapacitysSum(carriages) < requiredСapacity)
            {
                int leftFill = requiredСapacity - Carriage.GetCarriagesCapacitysSum(carriages);

                Console.WriteLine($"Left to fill {leftFill}." +
                    $"\nNext train carriage:");

                if (TryCreate(out Carriage createdCarriage))
                {
                    carriages.Push(createdCarriage);
                }
                else
                {
                    Console.WriteLine("Carriage create is invalid!");
                }
            }

            train = new TrainPlan(carriages);
        }

        public static bool TryCreate(out TrainRoute trainRoute)
        {
            trainRoute = new TrainRoute();

            Console.WriteLine("From:");

            if (TryCreate(out InhabitedLocality from))
            {
                Console.WriteLine("To:");

                if (TryCreate(out InhabitedLocality to))
                {
                    trainRoute = new TrainRoute(from, to);

                    return true;
                }
            }

            return false;
        }

        private static bool TryCreate(out Carriage carriage)
        {
            carriage = new Carriage();

            Console.Write("Capacity: ");

            if (int.TryParse(Console.ReadLine(), out int capacity) && capacity >= Carriage.MinimalCapacity)
            {
                carriage = new Carriage(capacity);

                return true;
            }

            return false;
        }

        private static bool TryCreate(out InhabitedLocality inhabitedLocality)
        {
            inhabitedLocality = new InhabitedLocality();

            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.WriteLine("Postion: ");

            if (TryCreate(out Vector2 positon))
            {
                inhabitedLocality = new InhabitedLocality(name, positon);
                return true;
            }

            return false;
        }

        private static bool TryCreate(out Vector2 vector)
        {
            vector = new Vector2();

            Console.Write("X: ");

            if (float.TryParse(Console.ReadLine(), out float x))
            {
                Console.Write("Y: ");

                if (float.TryParse(Console.ReadLine(), out float y))
                {
                    vector = new Vector2(x, y);

                    return true;
                }
            }

            return false;
        }
    }

    public class RailwayStationTerminal
    {
        private readonly static int _minimalPassengersCount = 0;

        private readonly int _maximalPassengersCount;

        private readonly List<RailwayVoyage> _sendedRailwayVoyages;

        public RailwayStationTerminal(int maximalPassengersCount)
        {
            _maximalPassengersCount = Math.Max(maximalPassengersCount, _minimalPassengersCount);
            
            _sendedRailwayVoyages = new List<RailwayVoyage>();
        }

        public void Activate()
        {
            bool isOpen = true;

            while (isOpen)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                ShowAllSendedRailWayVoyages();

                Console.ForegroundColor = ConsoleColor.Cyan;

                SendTrain();

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowAllSendedRailWayVoyages()
        {
            if (_sendedRailwayVoyages.Count == 0)
            {
                Console.WriteLine("No  railway voyages sended!");
            }
            else
            {
                Console.WriteLine("Sended railway voyages:");

                for (int i = 0; i < _sendedRailwayVoyages.Count; i++)
                {
                    Console.WriteLine($"{i}:" +
                        $"\n{_sendedRailwayVoyages[i].GetInfo()}");
                }
            }
        }

        private void SendTrain()
        {
            var route = CreateTrainRoute();

            Console.WriteLine();

            Console.WriteLine("To sell ticets press Any Key.");
            SellTickets(out int selledTicetsCount);
            Console.WriteLine($"You selled {selledTicetsCount} ticets.");

            Console.WriteLine();

            var plan = CreateTrainPlane(selledTicetsCount);

            AddRailWayVoyage(route, plan);

            Console.WriteLine("Railway voyage create success.");
        }

        private TrainRoute CreateTrainRoute()
        {
            Console.WriteLine("Train route:");
            TrainRoute trainRoute;

            while (CreateByUserInput.TryCreate(out trainRoute) == false)
            {
                Console.WriteLine("Create invalid, to retry press any key!");
                Console.ReadKey(true);
                Console.WriteLine("Train route:");
            }

            return trainRoute;
        }

        private void SellTickets(out int selledTicetsCount)
        {
            selledTicetsCount = GetRandomSelledTicketsCount(new Random());
        }

        private int GetRandomSelledTicketsCount(Random random)
        {
            return random.Next(_minimalPassengersCount, _maximalPassengersCount);
        }

        private TrainPlan CreateTrainPlane(int selledTicetsCount)
        {
            TrainPlan trainPlan;
            CreateByUserInput.Create(selledTicetsCount, out trainPlan);
            return trainPlan;
        }

        private void AddRailWayVoyage(TrainRoute route, TrainPlan plan)
        {
            _sendedRailwayVoyages.Add(new RailwayVoyage(route, plan));
        }
    }

    public class RailwayVoyage
    {
        public readonly TrainRoute TrainRoute;
        public readonly TrainPlan TrainPlan;

        public RailwayVoyage(TrainRoute trainRoute, TrainPlan trainPlan)
        {
            TrainRoute = trainRoute;
            TrainPlan = trainPlan;
        }

        public string GetInfo()
        {
            return $"Route: " +
                $"\n{TrainRoute.GetInfo()}" +
                $"\n\nTrain: " +
                $"\n{TrainPlan.GetInfo()}";
        }
    }

    public class TrainPlan
    {
        private readonly List<Carriage> _carriages;

        private int Capacity => Carriage.GetCarriagesCapacitysSum(_carriages);
        private int Length => _carriages.Count;

        public TrainPlan(IEnumerable<Carriage> carriages)
        {
            _carriages = carriages.ToList();
        }   

        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Length: {Length}");
            stringBuilder.AppendLine($"Capactiy: {Capacity}");
            stringBuilder.AppendLine($"Carriages:");
           
            foreach(Carriage carriage in _carriages)
            {
                stringBuilder.AppendLine(carriage.GetInfo());
            }

            return stringBuilder.ToString();
        }
    }

    public struct Carriage
    {
        public readonly static int MinimalCapacity = 1;

        public readonly int Capacity;

        public Carriage(int capactiy)
        {
            Capacity = Math.Max(capactiy, MinimalCapacity);
        }

        public static int GetCarriagesCapacitysSum(IEnumerable<Carriage> carriages)
        {
            return carriages.Sum(carriage => carriage.Capacity);
        }

        public string GetInfo()
        {
            return $"Carriage cappactiy: {Capacity}";
        }
    }

    public struct TrainRoute
    {
        public readonly InhabitedLocality From;
        public readonly InhabitedLocality To;

        public float RouteLength => From.GetDistance(To);

        public TrainRoute(InhabitedLocality from, InhabitedLocality to)
        {
            From = from;
            To = to;
        }

        public string GetInfo()
        {
            return $"From:" +
                $"\n{From.GetInfo()}" +
                $"\nTo:" +
                $"\n{To.GetInfo()}" +
                $"\nDistance: {GetDistance()}";
        }

        private float GetDistance()
        {
            return From.GetDistance(To);
        }
    }

    public struct InhabitedLocality
    {
        public readonly string Name;    
        public readonly Vector2 Position;

        public InhabitedLocality(string name, Vector2 position)
        {
            Name = name;
            Position = position;
        }

        public float GetDistance(InhabitedLocality to)
        {
            return Position.GetDistance(to.Position); 
        }

        public string GetInfo()
        {
            return $"Name: {Name}" +
                $"\nPosition: {Position.GetInfo()}";
        }
    }

    public struct Vector2
    {
        public readonly float X;
        public readonly float Y;

        public Vector2(float x,float y)
        {
            X = x;
            Y = y;
        }

        public float GetDistance(Vector2 other)
        {
            return (float)Math.Sqrt(((other.X - X) * (other.X - X)) + ((other.Y - Y) * (other.Y - Y)));
        }

        public string GetInfo()
        {
            return $"X: {X}, Y{Y}";
        }
    }
}
