namespace Fanex.BetList.Core.UnitTest.Builder.OddsBuilder
{
    using Core.Builder.OddsBuilder;
    using Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using Fanex.BetList.Core.UnitTest.Common.Enums;
    using Fanex.BetList.Core.UnitTest.Stubs;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Odd test class for bet type 1231.
    /// </summary>
    public class Odds1231Test
    {
        private Odds1231 _odds;
        private ITicket _ticket;
        private ITicketHelper _ticketHelperStub;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds1231();
            _ticket = TestData.CreateTicket(BetTypes.VHR_Win);
            _ticketHelperStub = new TicketHelperStub();
        }

        [Test]
        public void OddsTypeIsNull_OddsTypeIsZero()
        {
            // Arrange
            _ticket.OddsType = null;

            // Act
            _odds.Render(_ticket, null, Substitute.For<GetCachePropertyById>());

            // Assert
            Assert.AreEqual("<font color='#B50000'>-0.73</font>", _odds.Template.odds);
        }

        [Test]
        public void OddsTypeIsNull_OddsTypeIsZero1()
        {
            // Arrange
            _ticket.OddsType = "1";

            // Act
            _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            // Assert
            Assert.AreEqual("0.27", _odds.Template.odds);
        }

        [Test]
        public void OddsTypeIsNull_OddsTypeIsZero12()
        {
            // Arrange
            _ticket.OddsType = "1";

            // Act
            _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            // Assert
            Assert.AreEqual("1", _odds.Template.oddsType);
        }

        private string GetCachePropertyByIdStub(object id)
        {
            return id.ToString();
        }
    }
}