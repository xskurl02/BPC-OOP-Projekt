using System.Collections.Generic;

namespace PrsiCommon.Model
{
    public class GamePlay
    {
        public GamePlay(Deck allCards)
        {
            Stack = allCards;
            DiscardPile = new Deck();
            Players = new Players();
        }
        public Players Players { get; set; }

        private Deck AllCards { get; set; }
        public Deck Stack { get; set; }
        public Deck DiscardPile { get; set; }
        public int ActiveClient { get; set; }
        public bool GameStarted { get; set; }
        public bool GameFinished { get; set; }
        public bool IsTemporarySuitActive { get; set; }
        public Suit TemporarySuit { get; set; }
        public int PunishedCards { get; set; }
    }

    public enum PlayerGameStatus //hracie statusy 
    {
        WaitingForStart,
        Playing,
        PlayingStopped,
        PlayingPunished,
        Waiting,
        Waiting_For_Winning,
        Playing_For_Loosing,
        Winning,
        Loosing
    }

    public class PlayerStatus
    {
        public PlayerGameStatus Status { get; set; }
        public Card CardOnTopOfDiscardPile { get; set; }
        public int PunishedCards { get; set; }
    }
}