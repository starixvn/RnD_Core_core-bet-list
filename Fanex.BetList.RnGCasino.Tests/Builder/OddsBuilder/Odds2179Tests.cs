namespace Fanex.BetList.RnGCasino.Tests.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Odds2179Tests
    {
        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds2179();
            _ticket = Substitute.For<ITicket>();
        }

        [Test]
        public void Render_Always_ShowOddsAsHyphenAndEmptyOddsType()
        {
            // Act
            var odds = _odds.Render(_ticket, null, null);

            // Assert
            Assert.AreEqual("-", odds.odds);
            Assert.AreEqual(string.Empty, odds.oddsType);
        }
    }
}