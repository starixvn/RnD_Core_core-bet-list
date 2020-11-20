using Fanex.BetList.Core.Builder.OddsBuilder;
using Fanex.BetList.Core.Constants;
using Fanex.BetList.Core.Entities;
using NUnit.Framework;

namespace Fanax.BetList.LeapGaming.Test.Builder.OddBuilder
{
    public class Odds5301Test
    {
        private const decimal Odds = 0.75M;

        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds5301();
            _ticket = new Ticket
            {
                BetTypeId = LeapGamingBetTypes.VirtualFootball,
                OddsType = "1",
                Odds = Odds
            };
        }

        [Test]
        public void Render_MultipleBet_HideOdds()
        {
            // Arrange
            _ticket.ExtraInfo = "<p49 mt= \"GT03, 4\" />";

            // Act
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert
            Assert.AreEqual("-", template.odds);
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [Test]
        public void Render_SingleBet_ShowOdds()
        {
            // Arrange
            _ticket.BetCheck = "GT14, 1";

            // Act
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert
            Assert.IsTrue(template.odds != "-");
            Assert.IsTrue(template.oddsType != "");
        }
    }
}
