namespace Fanex.BetList.FGG.Test.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NUnit.Framework;

    [TestFixture]
    public class Odds8202Tests
    {
        private const decimal Odds = 0.75M;

        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds8202();
            _ticket = new Ticket
            {
                BetTypeId = FGGBetTypes.Baccarat,
                OddsType = "1",
                Odds = Odds
            };
        }
        
        [Test]
        public void Render_HideOdds()
        {
            // Act.
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert.
            Assert.AreEqual("-", template.odds);
        }
    }
}
