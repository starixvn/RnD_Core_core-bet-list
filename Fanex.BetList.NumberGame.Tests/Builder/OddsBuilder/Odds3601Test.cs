using Fanex.BetList.Core.Builder.OddsBuilder;
using Fanex.BetList.Core.Constants;
using Fanex.BetList.Core.Entities;
using NUnit.Framework;

namespace Fanax.BetList.NumberGame.Test.Builder.OddBuilder
{
    public class Odds3601Test
    {
        private const decimal Odds = 0.75M;

        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds3601();
            _ticket = new Ticket
            {
                BetTypeId = Happy5LinkedGamesBetTypes.OverUnder,
                OddsType = "1",
                Odds = Odds
            };
        }

        [Test]
        public void Render_Always_HideOdds()
        {
            // Arrange

            // Act
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert
            Assert.AreEqual("-", template.odds);
            Assert.AreEqual(string.Empty, template.oddsType);
        }
    }
}
