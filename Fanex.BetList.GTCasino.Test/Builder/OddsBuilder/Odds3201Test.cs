namespace Fanax.BetList.GTCasino.Test.Builder.OddsBuilder
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
            _odds = new Odds3201();
            _ticket = new Ticket
            {
                BetTypeId = GTCasinoBetTypes.Baccarat,
                OddsType = "1",
                Odds = Odds
            };
        }

        public void Render_Always_HideOdds()
        {
            // Arrange.

            // Act.
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert.
            Assert.AreEqual("-", template.odds);
            Assert.AreEqual(string.Empty, template.oddsType);
        }
    }
}