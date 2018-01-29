namespace Fanex.BetList.VirtualSports.Tests.Builder.OddsBuilder
{
    using Core.Builder.OddsBuilder;
    using Core.Entities;
    using Core.UnitTest.Common;
    using Core.UnitTest.Common.Enums;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Odd test class for bet type 1231.
    /// </summary>
    public class Odds1231Tests
    {
        private Odds1231 _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds1231();
            _ticket = TestData.CreateTicket(BetTypes.VHR_Win);
        }

        [Test]
        public void Render_OddsTypeIsNull_OddsTypeIsZero()
        {
            // Arrange
            _ticket.OddsType = null;

            // Act
            _odds.Render(_ticket, null, Substitute.For<GetCachePropertyById>());

            // Assert
            Assert.AreEqual("<font color='#B50000'>-0.73</font>", _odds.Template.odds);
        }

        [Test]
        public void Render_OddsTypeIsNull_OddsTypeIsZero1()
        {
            // Arrange
            _ticket.OddsType = "1";

            // Act
            _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            // Assert
            Assert.AreEqual("0.27", _odds.Template.odds);
        }

        [Test]
        public void Render_OddsTypeIsNull_OddsTypeIsZero12()
        {
            // Arrange
            _ticket.OddsType = "1";

            // Act
            _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            // Assert
            Assert.AreEqual("1", _odds.Template.oddsType);
        }

        private static string GetCachePropertyByIdStub(object id)
        {
            return id.ToString();
        }
    }
}