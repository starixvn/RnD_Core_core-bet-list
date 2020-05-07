namespace Fanex.BetList.VirtualGames.Tests.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NUnit.Framework;

    public class Odds4601Tests
    {
        private const decimal Odds = 0.75M;

        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds4601();
            _ticket = new Ticket
            {
                BetTypeId =VirtualGamesBetTypes.Baccarat,
                OddsType = "1",
                Odds = Odds
            };
        }

        [Test]
        public void Render_DrawTicket_HideOdds()
        {
            // Arrange
            _ticket.TransDesc = string.Empty;
            _ticket.Status = BetStatus.Draw;

            // Act
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert
            Assert.AreEqual("-", template.odds);
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [TestCase("running")]
        [TestCase("waiting")]
        [TestCase("refund")]
        [TestCase("void")]
        [TestCase("lose")]
        [TestCase("reject")]
        [TestCase("half won")]
        [TestCase("half lose")]
        [TestCase("pending")]
        public void Render_NotDrawTicket_ShowOdds(string statusTicket)
        {
            // Arrange
            _ticket.Status = statusTicket;
            _ticket.Odds = 0.85m;
            // Act
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert
            Assert.AreEqual("0.85", template.odds);
        }
    }
}
