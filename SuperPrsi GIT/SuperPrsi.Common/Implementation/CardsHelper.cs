using PrsiCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperPrsi.Common.Implementation
{
    internal class CardsHelper
    {
        internal static Dictionary<int, Card> AllCards = GetAllCards();

        internal static int SevenOfHeartsId;

        private static int GetIdOfSeverOfHearts()
        {
            return 7;
        }
        private static Dictionary<int, Card> GetAllCards()
        {
            var allCards = new Dictionary<int, Card>();

            var values = new List<int>() { 2, 3, 4, 7, 8, 9, 10, 11 }; //hodnota 2 - dolek,3 - horek,4 - kun,11 - eso
            IEnumerable<Suit> suits = Enum.GetValues(typeof(Suit)).Cast<Suit>();
            var id = 1;

            foreach (Suit suit in suits)
            {
                foreach (var val in values)
                {
                    allCards.Add(id, new Card()
                    {
                        Id = id,
                        Suit = suit,
                        Value = val
                    });
                    id++;
                }
            }
            SevenOfHeartsId = GetIdOfSeverOfHearts();

            return allCards;
        }

        internal static List<Card> GetInitialCards(Deck stock, int initialNumberOfCardsInHand)
        {
            List<Card> initialCards = new List<Card>();
            for (int i = 0; i < initialNumberOfCardsInHand; i++)
            {
                initialCards.Add(stock.Cards.Pop());
            }
            return initialCards;
        }

        internal static void TurnDiscardPileToStack(Deck discardPile, Deck stack)
        {
            for (int i = discardPile.Cards.Count; i > 0; i--)
            {
                stack.Cards.Push(discardPile.Cards.Pop());
            }
        }
    }
}