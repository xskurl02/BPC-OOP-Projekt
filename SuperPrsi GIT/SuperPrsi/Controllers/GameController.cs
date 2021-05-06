using PrsiCommon.Model;
using SuperPrsi.Common.Implementation;
using System.Collections.Generic;
using System.Web.Http;

namespace SuperPrsi.Controllers
{
    [RoutePrefix("api/Game")]
    public class GameController : ApiController
    {
        [HttpPost]
        public int ConnectToGame(string userName)
        {
            return GameHandler.ConnectPlayerToGame(userName);
        }

        [HttpGet]
        public bool IsReadyToPlay()
        {
            return GameHandler.ArePlayersReady();
        }

        [HttpPost]
        [Route("BeginGame/{clientId}")]
        public bool BeginGame(int clientId)
        {
            return GameHandler.BeginGame(clientId);
        }

        [HttpPost]
        [Route("GetCardsOnHand/{clientId}")]
        public IEnumerable<Card> GetCardsOnHand(int clientId)
        {
            return GameHandler.GetCardsOnHand(clientId);
        }


        [HttpPost]
        [Route("GetPlayerStatus/{clientId}")]
        public PlayerStatus GetPlayerStatus(int clientId)
        {
            return GameHandler.GetPlayerStatus(clientId);
        }

        [HttpPost]
        [Route("TakeCardFromStack/{clientId}-{count}")]
        public List<Card> TakeCardFromStack(int clientId, int count)
        {
            return GameHandler.TakeCardFromStack(clientId, count);
        }

        [HttpPost]
        [Route("DiscardCard/{clientId}-{cardId}")]
        public bool DiscardCard(int clientId, int cardId)
        {
            return GameHandler.DiscardCard(clientId, cardId);
        }
        [HttpPost]
        [Route("DiscardCard/{clientId}-{cardId}-{suite}")]
        public bool DiscardCard(int clientId, int cardId, int suite)
        {
            return GameHandler.DiscardCard(clientId, cardId, suite);
        }

        [HttpPost]
        [Route("PlayerStopped/{clientId}")]
        public bool PlayerStopped(int clientId)
        {
            return GameHandler.PlayerStopped(clientId);
        }

        [HttpPost]
        [Route("LoseGame/{clientId}")]
        public bool LoseGame(int clientId)
        {
            return GameHandler.LoseGame(clientId);
        }

        [HttpPost]
        [Route("EndGame/{suddenly}-{clientId}")]
        public bool EndGame(bool suddenly, int clientId)
        {
            return GameHandler.EndGame(suddenly, clientId);
        }
    }
}
