using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int deckLenght = 64;

            Player player = new Player();

            Croupier croupier = new Croupier(deckLenght);

            player.PlayGame(croupier);

            Console.ReadKey();
        }
    }

    public class CollectinsExtentions
    {
        public IEnumerable<T> Reverse<T>(IEnumerable<T> collection)
        {
            var array = collection.ToArray();

            Array.Reverse(array);

            return array;
        }
    }

    public class Croupier
    {
        private Deck _deck;

        public Croupier(int deckLenght)
        {
            _deck = new Deck(Deck.GetOrderedCards(deckLenght));
        }

        public bool TryAskForCards(uint requiredСardsCount, out IEnumerable<Card> getedCards)
        {
            getedCards = new List<Card>();

            var cards = new Stack<Card>();

            if (requiredСardsCount > _deck.CardCount)
                return false;

            for (int i = 0; i < requiredСardsCount; i++)
            {
                if(_deck.TryTakeCard(out Card card) == false)
                {
                    _deck.AddCards(cards);

                    return false;
                }

                cards.Push(card);
            }

            getedCards = cards;

            return true;
        }
    }

    public class Player
    {
        private Deck _deck;

        public Player()
        {
            _deck = new Deck();
        }

        public void PlayGame(Croupier croupier)
        {
            const string AskForCardsCommand = "ASK";
            const string ShowPlayerCardsCommand = "SHOW";
            const string ExitCommand = "EXIT";

            var isPlaying = true;

            while (isPlaying)
            {
                Console.Write($"\nEnter command: " +
                    $"\n{AskForCardsCommand}" +
                    $"\n{ShowPlayerCardsCommand}" +
                    $"\n{ExitCommand}\n");

                var userCommand = Console.ReadLine().ToUpper();

                switch (userCommand)
                {
                    case AskForCardsCommand:
                        AskForCards(croupier);
                        break;
                    case ShowPlayerCardsCommand:
                        ShowCards();
                        break;
                    case ExitCommand:
                        isPlaying = false;
                        break;
                    default:
                        Console.WriteLine("Uncnovn command!");
                        break;
                }
            }
        }

        private void AskForCards(Croupier croupier)
        {
            Console.Write("Required cards count: ");

            if (int.TryParse(Console.ReadLine(), out int requiredСardsCount) && requiredСardsCount > 0)
            {
                if(croupier.TryAskForCards((uint)requiredСardsCount, out IEnumerable<Card> getedCards))
                {
                    Console.WriteLine("You get new cards frow croupier." +
                        "\nGeted cards:");

                    foreach (var card in new CollectinsExtentions().Reverse(getedCards))
                    {
                        Console.WriteLine(card.GetInfo());
                    }

                    _deck.AddCards(getedCards);
                }
                else
                {
                    Console.WriteLine("Croupier did not give you a cards.");
                }
            }
            else
            {
                Console.WriteLine("Invalid required getedCards count input!");
            }
        }

        private void ShowCards()
        {
            Console.WriteLine("\n" + _deck.GetInfo());
        }
    }

    public class Deck
    {
        private Stack<Card> _cards;

        public int CardCount => _cards.Count;

        public Deck()
        {
            _cards = new Stack<Card>();
        }

        public Deck(IEnumerable<Card> cards)
        {
            _cards = new Stack<Card>();

            AddCards(cards);
        }

        public static IEnumerable<Card> GetOrderedCards(int needCardCount)
        {
            var orderedCards = GetOrderedCards().ToArray();

            for(int i = 0; i < needCardCount; i++)
            {
                yield return orderedCards[i % (orderedCards.Length - 1)];
            }
        }

        private static IEnumerable<Card> GetOrderedCards()
        {
            foreach (var fraction in Card.GetAllCardFractions())
            {
                foreach (var type in Card.GetAllCardTypes())
                {
                    yield return new Card(type, fraction);
                }
            }
        }

        public bool TryTakeCard(out Card card)
        {
            card = default(Card);

            if (_cards.Count == 0)
                return false;

            card = _cards.Pop();
            return true;
        }

        public void AddCards(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
            {
                AddCard(card);
            }
        }

        public void AddCard(Card card)
        {
            _cards.Push(card);
        }

        public string GetInfo()
        {
            var stringBuilder = new StringBuilder();

            foreach (var card in _cards)
            {
                stringBuilder.AppendLine(card.GetInfo());
            }

            return stringBuilder.ToString();
        }
    }

    public struct Card
    {
        public readonly CardType Type;
        public readonly CardFraction Fraction;

        public Card(CardType type, CardFraction fraction)
        {
            Type = type;
            Fraction = fraction;
        }

        public static IEnumerable<CardFraction> GetAllCardFractions()
        {
            yield return CardFraction.Heart;
            yield return CardFraction.Spades;
            yield return CardFraction.Diamond;
            yield return CardFraction.Club;
        }

        public static IEnumerable<CardType> GetAllCardTypes()
        {
            yield return CardType.One;
            yield return CardType.Two;
            yield return CardType.Three;
            yield return CardType.Four;
            yield return CardType.Five;
            yield return CardType.Six;
            yield return CardType.Seven;
            yield return CardType.Eight;
            yield return CardType.Nine;
            yield return CardType.Ten;
            yield return CardType.Jack;
            yield return CardType.Lady;
            yield return CardType.King;
            yield return CardType.Jester;
        }

        public string GetInfo()
        {
            return $"{Type} {Fraction}";
        }

        public enum CardType
        {
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Lady,
            King,
            Jester
        }

        public enum CardFraction
        {
            Heart,
            Spades,
            Diamond,
            Club
        }
    }
}

