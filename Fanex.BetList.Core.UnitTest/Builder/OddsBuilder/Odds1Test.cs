namespace Fanex.BetList.Core.UnitTest.Builder.OddsBuilder
{
    using Common;
    using Constants;
    using Core.Builder.OddsBuilder;
    using Core.Templates;
    using Entities;
    using NUnit.Framework;

    public class Odds1Test
    {
        private Odds1 _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds1();
            _ticket = TestData.CreateTicket(BetTypes.Handicap);
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
        public void OddsTypeIsNull_OddsTypeIsZero1()
        {
            // Arrange
            _ticket.OddsType = "1";

            // Act
            Odds_Template template = _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            // Assert
            var expectedOdds = "0.27";
            Assert.AreEqual(expectedOdds, template.odds);
        }

        [Test]
        public void OddsTypeIsNull_OddsTypeIsZero12()
        {
            // Arrange
            _ticket.OddsType = "1";

            // Act
            Odds_Template template = _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            // Assert
            var expectedOdds = "Dec";
            Assert.AreEqual(expectedOdds, template.oddsType);
        }

        [Test]
        public void RenderOddsValue_Always_GetExpectedOddValue()
        {
            // Arrange
            _ticket.Odds = 100;

            // Act
            var oddsValue = _odds.RenderOddsValue(_ticket, null, GetCachePropertyByIdStub);

            // Assert
            Assert.AreEqual("<div class=\"underdog\">100</div>", oddsValue);
        }

        [TestCase(100, "1", "Dec")]
        [TestCase(-1, "1", "Dec")]
        public void RenderOddsType_Always_GetExpectedOddTypeName(int odds, string oddsType, string expectedOddsType)
        {
            // Arrange
            _ticket.Odds = odds;
            _ticket.OddsType = oddsType;
            _ticket.BetTypeId = 1203;

            // Act
            var oddsTypeName = _odds.RenderOddsType(_ticket, null, GetCachePropertyByIdStub);

            // Assert
            Assert.AreEqual($"<div class=\"oddstype\">{expectedOddsType}</div>", oddsTypeName);
        }

        [Test]
        public void RenderOddsValueRTF_Always_GetOddsValueExcel()
        {
            // Arrange
            _ticket.Odds = -100;
            var rtfHelper = new RTFHelper().Clone();

            // Act
            var oddsValueExcel = _odds.RenderOddsValueRTF(_ticket, null, GetCachePropertyByIdStub, rtfHelper);

            // Assert
            Assert.AreEqual("-100", oddsValueExcel.ToString());
        }

        [Test]
        public void RenderOddsTypeRTF_Always_GetOddsTypeExcel()
        {
            // Arrange
            _ticket.Odds = -100;
            _ticket.OddsType = "1";
            var rtfHelper = new RTFHelper().Clone();

            // Act
            var oddsTypeExcel = _odds.RenderOddsTypeRTF(_ticket, null, GetCachePropertyByIdStub, rtfHelper);

            // Assert
            Assert.AreEqual("Dec", oddsTypeExcel.ToString());
        }

        private static string GetCachePropertyByIdStub(object id) => id.ToString() == "0" ? "" : "Dec";
    }
}