namespace PrsiCommon.Model
{
    public class Card
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public Suit Suit { get; set; }
    }

    public enum Suit
    {
        Srdce,
        Gule,
        Zaludy, 
        Listy
    }
}