using Fanex.BetList.Core.Builder.OddsBuilder;
using Fanex.BetList.Core.Constants;
using Fanex.BetList.Core.Entities;
using NUnit.Framework;

namespace Fanax.BetList.Allbet.Test.Builder.OddBuilder
{
    public class OddsLCTest
    {
        private const decimal Odds = 0.75M;

        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new OddsLC();
            _ticket = new Ticket
            {
                BetTypeId = 99999,
                OddsType = "1",
                Odds = Odds
            };
        }


        [Test]
        public void Render_SummaryTicket_HideOdds()
        {
            // Arrange
            _ticket.TransDesc = string.Empty;

            // Act
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert
            Assert.AreEqual("-", template.odds);
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [Test]
        public void Render_DetailTicketOddsZero_HideOdds()
        {
            // Arrange
            _ticket.TransDesc = "choice=1001*site=IBC*tableCode=301";
            _ticket.Odds = 0;

            // Act
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert
            Assert.AreEqual("-", template.odds);
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [Test]
        public void Render_HasOdds_ShowOdds()
        {
            // Arrange
            _ticket.TransDesc = "choice=1001*site=IBC*tableCode=301";

            // Act
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert
            Assert.IsTrue(template.odds != "-");
            Assert.IsTrue(template.oddsType != "");
        }
    }
}
