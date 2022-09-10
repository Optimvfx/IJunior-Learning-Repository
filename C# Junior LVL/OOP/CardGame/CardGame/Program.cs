using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            uint deckLenght = 64;

            Player player = new Player();

            Croupier croupier = new Croupier(deckLenght);

            player.PlayGame(croupier);

            Console.ReadKey();
        }
    }

    public class Croupier
    {
        private Deck _deck;

        public Croupier(uint deckLenght)
        {
            _deck = Deck.Create((int)deckLenght, new Random());
        }

        public bool AskForCards(Deck to, uint requiredСardsCount)
        {
            if (requiredСardsCount > _deck.Count)
                return false;

            for (int i = 0; i < requiredСardsCount; i++)
            {
                to.TryTakeAwayCard(_deck);
            }

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
                if(croupier.AskForCards(_deck, (uint)requiredСardsCount))
                {
                    Console.WriteLine("You get new cards frow croupier.");
                }
                else
                {
                    Console.WriteLine("Croupier did not give you a cards.");
                }
            }
            else
            {
                Console.WriteLine("Invalid required cards count input!");
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

        public int Count => _cards.Count;

        public Deck()
        {
            _cards = new Stack<Card>();
        }

        public Deck(IEnumerable<Card> cards)
        {
            _cards = new Stack<Card>();

            foreach (var card in cards)
            {
                _cards.Push(card);
            }
        }

        public static Deck Create(int lenght, Random random)
        {
            var cards = new List<Card>();

            for (int i = 0; i < lenght; i++)
            {
                cards.Add(Card.Create(random));
            }

            return new Deck(cards);
        }

        public bool TryTakeAwayCard(Deck from)
        {
            if (from == this)
            {
                return false;
            }

            if (from.TryTakeCard(out Card takedCard))
            {
                _cards.Push(takedCard);
                return true;
            }

            return false;
        }

        public bool TryTakeCard(out Card card)
        {
            card = Card.StandartCard;

            if (_cards.Count == 0)
                return false;

            card = _cards.Pop();
            return true;
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
        public static readonly Card StandartCard = new Card(CardType.One, CardFraction.Heart);

        public readonly CardType Type;
        public readonly CardFraction Fraction;

        public Card(CardType type, CardFraction fraction)
        {
            Type = type;
            Fraction = fraction;
        }

        public static Card Create(Random random)
        {
            var randomCardType = (CardType)random.Next((int)CardType.One, (int)CardType.Jester);
            var randomCardFraction = (CardFraction)random.Next((int)CardFraction.Heart, (int)CardFraction.Club);

            return new Card(randomCardType, randomCardFraction);
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

