using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();

            Deck fromAssign = Deck.GenerateRandom(20);

            player.AssignCards(fromAssign, 10);

            player.AssignCards(fromAssign);

            Console.WriteLine(player.GetInfo());

            Console.ReadKey();
        }

        public class Player
        {
            private Deck _deck;

            public Player()
            {
                _deck = new Deck();
            }

            public void AssignCards(Deck fromAssign, uint cardsToAssign)
            {
                while(_deck.TryAssignCard(fromAssign, out Card assignedCard) && cardsToAssign > 0)
                {
                    cardsToAssign--;

                    Console.WriteLine("\nYou take a new card:" + assignedCard.GetInfo());
                    Console.WriteLine($"Cards to assign left {cardsToAssign}");
                    Console.WriteLine("Prass any key to get next card.");

                    Console.ReadKey();
                }
            }

            public void AssignCards(Deck fromAssign)
            {
                const string ExitCommand = "YES";

                bool contimeGetCards = true;

                while (_deck.TryAssignCard(fromAssign, out Card assignedCard) && contimeGetCards)
                {
                    Console.WriteLine("\nYou take a new card:" + assignedCard.GetInfo());
                    Console.WriteLine("Stop geting cards? YES NO");

                    var userInput = Console.ReadLine().ToUpper();

                    switch (userInput)
                    {
                        case ExitCommand:
                            contimeGetCards = false;
                            break;
                    }
                }
            }

            public string GetInfo()
            {
                return "\n" + _deck.GetInfo();
            }
        }

        public class Deck
        {
            private Stack<Card> _cards;

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

            public static Deck GenerateRandom(int lenght)
            {
                var cards = new List<Card>();

                var random = new Random();

                for(int i = 0; i < lenght; i++)
                {
                    cards.Add(Card.GetRandom(random));
                }

                return new Deck(cards);
            }

            public bool TryAssignCard(Deck fromAssign, out Card assignedCard)
            {
                assignedCard = Card.StandartCard;

                if (fromAssign == this)
                {
                    return false;
                }

                if(fromAssign.TryTakeCard(out assignedCard))
                {
                    _cards.Push(assignedCard);
                    return true;
                }

                return false;
            }

            public bool TryTakeCard(out Card card)
            {
                card = Card.StandartCard;

                if(_cards.Count == 0)
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

            public static Card GetRandom(Random random)
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
}
