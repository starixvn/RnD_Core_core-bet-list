namespace Fanax.BetList.Allbet.Test.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NUnit.Framework;

    public class Odds2401Test
    {
        private const decimal Odds = 0.75M;

        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds2401();
            _ticket = new Ticket
            {
                BetTypeId = AllbetBetTypes.Baccarat,
                OddsType = "1",
                Odds = Odds
            };
        }

        [TestCase("choice=1100", "-")]
        [TestCase("choice=1001", "0.75")]
        [TestCase("choice=1002", "0.75")]
        [TestCase("choice=1003", "0.75")]
        [TestCase("choice=1006", "0.75")]
        [TestCase("choice=1007", "0.75")]
        [TestCase("choice=1004", "0.75")]
        [TestCase("choice=1005", "0.75")]
        public void Render_ChoiceIsDoubleBet_HideOdds(string transDesc, string expectedOdds)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert.
            Assert.AreEqual(expectedOdds, template.odds);
        }
    }
}