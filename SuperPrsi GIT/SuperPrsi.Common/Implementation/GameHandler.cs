using PrsiCommon.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperPrsi.Common.Implementation
{
    public static class GameHandler
    {
        private static int MaxPlayersInGame = 2;
        private static int InitialNumberOfCardsInHand = 4;
        private static GamePlay GamePlay = new GamePlay(Shuffle());

        public static int ConnectPlayerToGame(string name)
        {
            if (GamePlay.Players.PlayersCount < MaxPlayersInGame)
            {
                Random rnd = new Random();
                int userId = rnd.Next(1, int.MaxValue);
                Client client = new Client() { Id = userId, Name = name, GameStatus = PlayerGameStatus.WaitingForStart };
                GamePlay.Players.AddPlayer(client);
                return userId;
            }
            return -1;
        }

        public static bool DiscardCard(int playerId, int cardId, int suit = 0)
        {
            if (GamePlay.GameStarted == false || GamePlay.GameFinished) return false;

            var player = GetPlayerById(playerId);
            if (player == null || player.OnHand.Exists(c => c.Id == cardId) == false) { return false; }

            var currentCard = CardsHelper.AllCards[cardId];
            if (CanCardBeDiscarded(currentCard, GamePlay.DiscardPile.Cards.Peek(), player.GameStatus))
            {
                if (currentCard.Value == 3)
                {
                    GamePlay.IsTemporarySuitActive = true;
                    GamePlay.TemporarySuit = (Suit)suit;
                }
                GamePlay.DiscardPile.Cards.Push(currentCard);
                player.OnHand.RemoveAt(player.OnHand.FindIndex(c => c.Id == cardId));

                if (player.OnHand.Count == 0)
                {
                    NexActivePlayer(PlayerGameStatus.Waiting_For_Winning, PlayerGameStatus.Playing_For_Loosing);
                }
                else if (currentCard.Value == 7)
                {
                    NexActivePlayer(PlayerGameStatus.Waiting, PlayerGameStatus.PlayingPunished);
                    GamePlay.PunishedCards = GamePlay.PunishedCards + 2;
                }
                else if (currentCard.Value == 11)
                {
                    NexActivePlayer(PlayerGameStatus.Waiting, PlayerGameStatus.PlayingStopped);
                }
                else
                {
                    NexActivePlayer(PlayerGameStatus.Waiting, PlayerGameStatus.Playing);
                }
                return true;
            }
            return false;
        }
        public static bool PlayerStopped(int clientId)
        {
            NexActivePlayer(PlayerGameStatus.Waiting, PlayerGameStatus.Playing);
            return true;
        }

        public static bool LoseGame(int clientId)
        {
            NexActivePlayer(PlayerGameStatus.Loosing, PlayerGameStatus.Winning);
            return true;
        }

        public static bool EndGame(bool suddenly, int clientId)
        {
            if (suddenly)
            {
                GamePlay.Players.EndGame(clientId);
            }
            else
            {
                GamePlay = new GamePlay(Shuffle());
            }
            return true;
        }

        public static bool CanCardBeDiscarded(Card currentCard, Card cardOnTop, PlayerGameStatus gameStatus)
        {
            if (gameStatus == PlayerGameStatus.PlayingPunished)
            {
                return (currentCard.Value == 7);
            }

            if (gameStatus == PlayerGameStatus.PlayingStopped)
            {
                return (currentCard.Value == 11);
            }

            bool isSuiteSame = false;
            if (GamePlay.IsTemporarySuitActive)
            {
                GamePlay.IsTemporarySuitActive = false;
                if (GamePlay.TemporarySuit == currentCard.Suit) return true;
                if (currentCard.Value == 3) return true;
            }
            else
            {
                isSuiteSame = currentCard.Suit == cardOnTop.Suit;
            }

            var isValueSame = currentCard.Value == cardOnTop.Value;

            if (isSuiteSame || isValueSame)
            {
                return true;
            }

            var isWeirdGuyThatChangesColor = currentCard.Value == 3;

            if (isWeirdGuyThatChangesColor)
            {
                return true;
            }

            return false;
        }

        public static List<Card> TakeCardFromStack(int playerId, int count)
        {
            if (GamePlay.GameStarted == false || GamePlay.GameFinished) return null;

            List<Card> cards = new List<Card>();
            var player = GetPlayerById(playerId);
            if (player == null) { return null; }

            for (int i = 0; i < count; i++)
            {
                if (GamePlay.Stack.Cards.Count > 0)
                {
                    var card = GamePlay.Stack.Cards.Pop();
                    player.OnHand.Add(card);
                    cards.Add(card);
                }
                else
                {
                    var topCard = GamePlay.DiscardPile.Cards.Pop();
                    CardsHelper.TurnDiscardPileToStack(GamePlay.DiscardPile, GamePlay.Stack);
                    GamePlay.DiscardPile.Cards.Push(topCard);

                    if (GamePlay.Stack.Cards.Count > 0)
                    {
                        var card = GamePlay.Stack.Cards.Pop();//odstaní z vrcha balička
                        player.OnHand.Add(card);
                        cards.Add(card);
                    }
                }
            }
            GamePlay.PunishedCards = 0;
            NexActivePlayer(PlayerGameStatus.Waiting, PlayerGameStatus.Playing);
            return cards;
        }

        public static PlayerStatus GetPlayerStatus(int clientId)
        {
            if (GamePlay.GameStarted == false) return new PlayerStatus() { Status = PlayerGameStatus.WaitingForStart };
            var playerStatus = new PlayerStatus();

            if (GamePlay.IsTemporarySuitActive)
            {
                playerStatus.CardOnTopOfDiscardPile = new Card
                {
                    Value = 0,
                    Suit = GamePlay.TemporarySuit
                };
            }
            else
            {
                playerStatus.CardOnTopOfDiscardPile = GamePlay.DiscardPile.Cards.Peek();
            }

            Client client = GamePlay.Players.GetClientById(clientId);
            playerStatus.Status = client.GameStatus;
            playerStatus.PunishedCards = GamePlay.PunishedCards;
            return playerStatus;
        }

        public static List<Card> GetCardsOnHand(int clientId)
        {
            var player = GetPlayerById(clientId);
            if (player == null) { return null; }

            return player.OnHand;
        }

        private static Client GetPlayerById(int clientId)
        {
            return GamePlay.Players.GetClientById(clientId);
        }

        public static bool ArePlayersReady()
        {
            if (GamePlay.Players.PlayersCount == MaxPlayersInGame)
            {
                GamePlay.Players.LetsPlayTheGame();
                return true;
            }
            return false;
        }

        public static bool BeginGame(int clientId)
        {
            if (GamePlay.GameStarted) { return true; }
            if (ArePlayersReady() == false) { return false; }

            GamePlay.GameStarted = true;
            GamePlay.ActiveClient = clientId;

            GamePlay.Players.AddInitialCardsToPlayers(GamePlay.Stack, InitialNumberOfCardsInHand);
            GamePlay.DiscardPile.Cards.Push(GamePlay.Stack.Cards.Pop());

            return true;
        }

        private static Deck Shuffle()
        {
            var allCards = CardsHelper.AllCards.Values;

            Random rnd = new Random();
            var alLCardsShuffled = allCards.OrderBy((item) => rnd.Next());

            return new Deck() { Cards = new Stack<Card>(alLCardsShuffled.ToList()) };
        }

        private static void NexActivePlayer(PlayerGameStatus acctualPlayerStatus,
            PlayerGameStatus nextPlayerStatus)
        {
            GamePlay.Players.NextPlayer(acctualPlayerStatus, nextPlayerStatus);
        }
    }
}
