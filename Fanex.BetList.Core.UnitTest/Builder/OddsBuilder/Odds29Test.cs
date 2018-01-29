namespace Fanex.BetList.Core.UnitTest.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Odds29Test
    {
        private Odds29 _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds29();
            _ticket = Substitute.For<ITicket>();
        }

        [TestCase(3.2)]
        [TestCase(0)]
        [TestCase(-1.2)]
        public void Render_FromOdds_OddsIsDashSymbol(decimal odds)
        {
            ////Arrange
            _ticket.Odds = odds;

            ////Act
            var template = _odds.Render(_ticket, null, null);

            ////Assert
            var actualOdds = template.odds;
            Assert.AreEqual("-", actualOdds);
        }

        [Test]
        public void Render_Always_OddsTypeIsEmpty()
        {
            ////Act
            var template = _odds.Render(_ticket, null, null);

            ////Assert
            var oddsType = template.oddsType;
            Assert.AreEqual(string.Empty, oddsType);
        }
    }
}