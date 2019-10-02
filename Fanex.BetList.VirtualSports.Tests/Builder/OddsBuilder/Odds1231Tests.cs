namespace Fanex.BetList.VirtualSports.Tests.Builder.OddsBuilder
{
    using Core.Builder.OddsBuilder;
    using Core.Constants;
    using Core.Entities;
    using Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Odds1231Tests
    {
        private Odds1231 _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds1231();
            _ticket = TestData.CreateTicket(InspireVirtualRacingBetTypes.Win);
        }

        [Test]
        public void Render_TicketOddsIsZero_ShowZeroAsOdds()
        {
            // Arrange
            _ticket.OddsType = null;

            // Act
            _odds.Render(_ticket, null, Substitute.For<GetCachePropertyById>());

            // Assert
            Assert.AreEqual("<font color='#B50000'>-0.73</font>", _odds.Template.odds);
        }

        [Test]
        public void Render_Always_DoNotShowOddsTypeName()
        {
            // Arrange
            _ticket.OddsType = "1";

            // Act
            _odds.Render(_ticket, null, id => id.ToString());

            // Assert
            Assert.IsEmpty(_odds.Template.oddsType);
        }
    }
}