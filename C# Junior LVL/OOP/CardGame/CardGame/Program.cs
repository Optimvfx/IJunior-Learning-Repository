using System;
using System.Collections.Generic;

namespace CardGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }

        public class Player
        {
            private Deck _playerDeck;

            public Player()
            {
                _playerDeck = new Deck();
            }

            public void GetCards(Deck fromAssign)
            {
                while(_playerDeck.TryAssignCard(fromAssign, out Card assignedCard))
                {
                    Console.WriteLine(assignedCard.GetCardInfo());          
                }
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

            public static Deck GenerateRandomDeck(int lenght)
            {
                var carsd = new List<Card>();

                var random = new Random();

                for(int i = 0; i < lenght; i++)
                {
                    carsd.Add(Card.GetRandomCard(new Random()));
                }

                return new Deck(carsd);
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

            public static Card GetRandomCard(Random random)
            {
                var randomCardType = (CardType)random.Next((int)CardType.One, (int)CardType.Jester);
                var randomCardFraction = (CardFraction)random.Next((int)CardFraction.Heart, (int)CardFraction.Club);
                return new Card(randomCardType, randomCardFraction);
            }

            public string GetCardInfo()
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
