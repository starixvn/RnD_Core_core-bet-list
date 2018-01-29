namespace Fanex.BetList.CashOut.Tests.Builder.OddsBuilder
{
    using Core.Builder.OddsBuilder;
    using Core.Entities;
    using Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Odds911Tests
    {
        private ITicket _ticket;
        private IOdds _odds;

        [SetUp]
        public void Setup()
        {
            _ticket = TestData.CreateTicket(911);
            _ticket.BetTypeId = 911;
            _odds = new Odds911();
        }

        [Test]
        public void Render_Always_ReturnOriginalBetTypeOdds()
        {
            // Arrange
            _ticket.OriginalBetType = 1;
            _ticket.Odds = 100;
            _ticket.OddsType = "1";

            // Act
            var template = _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            // Assert
            _ticket.BetTypeId = 1;
            var odds = new Odds1();
            var expectedTempate = odds.Render(_ticket, null, GetCachePropertyByIdStub);
            Assert.AreEqual(expectedTempate.ToString(), template.ToString());
        }

        [Test]
        public void RenderRTF_Always_ReturnOriginalBetTypeOdds()
        {
            // Arrange
            _ticket.OriginalBetType = 1;
            _ticket.Odds = 100;
            _ticket.OddsType = "1";

            // Act
            var template = _odds.RenderRTF(_ticket, null, GetCachePropertyByIdStub, new RTFHelper().Clone());

            // Assert
            _ticket.BetTypeId = 1;
            var odds = new Odds1();
            var expectedTempate = odds.RenderRTF(_ticket, null, GetCachePropertyByIdStub, new RTFHelper().Clone());
            Assert.AreEqual(expectedTempate.String, template.String);
        }

        private static string GetCachePropertyByIdStub(object id)
        {
            return id.ToString();
        }
    }
}