namespace Fanex.BetList.Core.UnitTest.Builder.OddsBuilder
{
    using Common;
    using Constants;
    using Core.Builder.OddsBuilder;
    using Core.Templates;
    using Entities;
    using NUnit.Framework;

    public class Odds38Test
    {
        private Odds38 _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds38();
            _ticket = TestData.CreateTicket(BetTypes.SingleMatchParlay);
        }

        [Test]
        public void OddsTypeIsNull_OddsTypeIsZero()
        {
            // Arrange
            _ticket.OddsType = null;

            // Act
            Odds_Template template = _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            // Assert
            var expectedOdds = "<font color='#B50000'>-0.73</font>";
            Assert.AreEqual(expectedOdds, template.odds);
        }

        [Test]
        public void OddsTypeIsNull_Always_OddsTypeIsHide()
        {
            // Arrange
            _ticket.OddsType = "1";

            // Act
            Odds_Template template = _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            // Assert
            Assert.IsEmpty(template.oddsType);
        }

        private static string GetCachePropertyByIdStub(object id) => id.ToString() == "0" ? "" : "Dec";
    }
}