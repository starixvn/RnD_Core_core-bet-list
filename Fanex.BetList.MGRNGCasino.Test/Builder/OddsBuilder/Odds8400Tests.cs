namespace Fanex.BetList.MGRNGCasino.Test.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;


    public class Odds8400Tests
    {
        private const decimal Odds = 0.75M;

        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds8400();
            _ticket = Substitute.For<ITicket>();
        }

        [Test]
        public void Render_Alway_SetOddIsHyphen()
        {
            // Arrange.

            // Act.
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert.
            Assert.AreEqual("-", template.odds);
        }

        [Test]
        public void Render_Alway_SetOddTypeIsEmpty()
        {
            // Arrange.

            // Act.
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert.
            Assert.IsEmpty(template.oddsType);
        }
    }
}
