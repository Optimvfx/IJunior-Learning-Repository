using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainAdministrator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RailwayStationTerminal railwayStationTerminal = new RailwayStationTerminal();

            railwayStationTerminal.CreateVoyage();

            Console.ReadKey();
        }
    }

    public class RailwayStationTerminal
    {
        public void CreateVoyage()
        {
            if(TrainRoute.TryCreate(out TrainRoute trainRoute))
            {
                Console.WriteLine("SS");
            }
        }
    }

    public class TrainRoute
    {
        private readonly static TrainRoute _default = new TrainRoute(InhabitedLocality.Default, InhabitedLocality.Default);

        public readonly InhabitedLocality From;
        public readonly InhabitedLocality To;

        public float RouteLength => From.GetDistance(To);

        public TrainRoute(InhabitedLocality from, InhabitedLocality to)
        {
            From = from;
            To = to;
        }

        public static bool TryCreate(out TrainRoute trainRoute)
        {
            trainRoute = _default;

            Console.WriteLine("From:");

            if (InhabitedLocality.TryCreate(out InhabitedLocality from))
            {
                Console.WriteLine("To:");

                if (InhabitedLocality.TryCreate(out InhabitedLocality to))
                {
                    trainRoute = new TrainRoute(from, to);

                    return true;
                }
            }

            return false;
        }
    }

    public class InhabitedLocality
    {
        public readonly static InhabitedLocality Default = new InhabitedLocality("", new Vector2());

        public readonly string Name;    
        public readonly Vector2 Position;

        public InhabitedLocality(string name, Vector2 position)
        {
            Name = name;
            Position = position;
        }

        public static bool TryCreate(out InhabitedLocality inhabitedLocality)
        {
            inhabitedLocality = Default;

            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Postion: ");

            if(Vector2.TryCreate(out Vector2 positon))
            {
                inhabitedLocality = new InhabitedLocality(name, positon);
                return true;
            }

            return false;
        }

        public float GetDistance(InhabitedLocality to)
        {
            return Position.GetDistance(to.Position); 
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

        public static bool TryCreate(out Vector2 vector)
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

        public float GetDistance(Vector2 other)
        {
            return (float)Math.Sqrt(((other.X - X) * (other.X - X)) + ((other.Y - Y) * (other.Y - Y)));
        }
    }
}
