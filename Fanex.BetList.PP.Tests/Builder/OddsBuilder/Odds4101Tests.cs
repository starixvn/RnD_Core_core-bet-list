namespace Fanex.BetList.PP.Tests.Builder.OddsBuilder
{
    using Core.Builder.OddsBuilder;
    using Core.Entities;
    using Core.Templates;
    using NSubstitute;
    using NUnit.Framework;

    public class Odds4101Tests
    {
        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds4101();
            _ticket = Substitute.For<ITicket>();
        }

        [Test]
        public void Render_Always_OddsIsHyphen()
        {
            // Act.
            Odds_Template template = _odds.Render(_ticket, null, Substitute.For<GetCachePropertyById>());

            // Assert.
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_Always_OddsTypeIsNull()
        {
            // Act.
            Odds_Template template = _odds.Render(_ticket, null, Substitute.For<GetCachePropertyById>());

            // Assert.
            Assert.Null(template.oddsType);
        }
    }
}