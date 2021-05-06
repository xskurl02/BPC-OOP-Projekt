using System.Collections.Generic;

namespace PrsiCommon.Model
{
    public class Deck
    {
        public Deck()
        {
            Cards = new Stack<Card>();
        }

        public Stack<Card> Cards { get; set; }
    }
}