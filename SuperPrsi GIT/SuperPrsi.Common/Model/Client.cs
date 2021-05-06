using System.Collections.Generic;

namespace PrsiCommon.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Card> OnHand { get; set; }
        public PlayerGameStatus GameStatus { get; set; }
    }
}