using System.Collections.Generic;
using System.Linq;
using SuperPrsi.Common.Implementation;

namespace PrsiCommon.Model
{
    public class Players
    {
        private Dictionary<int, Client> Clients { get; set; }
        private List<int> Order { get; set; }

        private int acctualPlayer = 0;
        public int PlayersCount { get; set; }

        public Players()
        {
            Clients = new Dictionary<int, Client>();
            Order = new List<int>();
        }

        public void AddPlayer(Client client)
        {
            Clients.Add(client.Id, client);
            Order.Add(client.Id);
            PlayersCount = PlayersCount + 1;
        }

        public void NextPlayer(PlayerGameStatus acctualPlayerStatus,
            PlayerGameStatus nextPlayerStatus)
        {
            var acctualClient = Clients.ElementAt(acctualPlayer);

            if ((acctualClient.Value.GameStatus == PlayerGameStatus.Loosing) || (acctualClient.Value.GameStatus == PlayerGameStatus.Winning))
            {
                return;
            }

            acctualClient.Value.GameStatus = acctualPlayerStatus;

            if (acctualPlayer < PlayersCount - 1)
            {
                acctualPlayer++;
            }
            else
            {
                acctualPlayer = 0;
            }
            var newClient = Clients.ElementAt(acctualPlayer);
            newClient.Value.GameStatus = nextPlayerStatus;
        }

        public void EndGame(int id)
        {
            var acctualClient = Clients.ElementAt(acctualPlayer);
            if (acctualClient.Value.Id == id)
            {
                NextPlayer(PlayerGameStatus.Loosing, PlayerGameStatus.Winning);
            }
            else
            {
                NextPlayer(PlayerGameStatus.Winning, PlayerGameStatus.Loosing);
            }
        }

        public Client GetClientById(int id)
        {
            Clients.TryGetValue(id, out Client client);
            return client;
        }

        public void AddInitialCardsToPlayers(Deck stack, int initialNumberOfCardsInHand)
        {
            foreach (var client in Clients)
            {
                client.Value.OnHand = CardsHelper.GetInitialCards(stack, initialNumberOfCardsInHand);
            }
        }

        public void LetsPlayTheGame()
        {
            foreach (var client in Clients)
            {
                client.Value.GameStatus = PlayerGameStatus.Waiting;
            }
            Clients.First().Value.GameStatus = PlayerGameStatus.Playing;
        }
    }
}