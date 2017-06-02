namespace Fanex.BetList.VirtualSports.Tests.Builder.OddsBuilder
{
    using Core.Builder.OddsBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Odds2799Tests
    {
        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds2799();
            _ticket = Substitute.For<ITicket>();
        }

        [Test]
        public void Render_Always_OddsIsHyphen()
        {
            // Act
            var builtOdds = _odds.Render(_ticket, null, Substitute.For<GetCachePropertyById>());

            // Assert
            Assert.AreEqual("-", builtOdds.odds);
        }

        [Test]
        public void Render_Always_OddsTypeIsEmpty()
        {
            // Act
            var builtOdds = _odds.Render(_ticket, null, Substitute.For<GetCachePropertyById>());

            // Assert
            Assert.AreEqual(string.Empty, builtOdds.oddsType);
        }
    }
}