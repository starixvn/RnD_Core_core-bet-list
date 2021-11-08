namespace Fanex.BetList.RnGCasino.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice2179Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2179();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("jp=1,jpw=1233213123")]
        [TestCase("JP=1,JPW=1233213123")]
        public void BuildLeague_TicketIsDetailedTicketAndHasJackpotInfo_ShowProgressiveJackpotWonDetails(string jackpotInfo)
        {
            // Arrange
            _ticket.BetCheck = "234234234";
            _ticket.ExtraInfo = jackpotInfo;
            _ticket.CustId = 1;
            _ticket.BetTypeId = 2198;

            // Act
            var choice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedDetailLink = $"<div class=\"detail rng-casino-detail\" onclick=\"showProgressiveJackpotWonDetails('1233213123', '1', '2198');\">" +
                 $"<a class=\"detail-link\" href=\"javascript:void('');\">Progressive Jackpot Won Details</a></div>";
            Assert.True(choice.League.ToString().Contains(expectedDetailLink));
        }

        [Test]
        public void BuildLeague_TicketIsNotDetailed_NotShowProgressiveJackpotWonDetails()
        {
            // Arrange
            _ticket.ExtraInfo = "jp=1,jpw=1233213123";
            _ticket.CustId = 1;
            _ticket.BetTypeId = 2198;

            // Act
            var choice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedDetailLink = $"<div class=\"detail rng-casino-detail\" onclick=\"showProgressiveJackpotWonDetails('1233213123', '1', '2198');\">" +
                 $"<a class=\"detail-link\" href=\"javascript:void('');\">Progressive Jackpot Won Details</a></div>";
            Assert.False(choice.League.ToString().Contains(expectedDetailLink));
        }

        [TestCase("jpw=1233213123")]
        [TestCase("jp=1,jpe=1233213123")]
        public void BuildLeague_TicketIsDetailedTicketButHasNoJackpotInfo_ShowProgressiveJackpotWonDetailsWithNotExactParam(string jackpotInfo)
        {
            // Arrange
            _ticket.BetCheck = "234234234";
            _ticket.ExtraInfo = jackpotInfo;
            _ticket.CustId = 1;
            _ticket.BetTypeId = 2198;

            // Act
            var choice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedDetailLink = $"<div class=\"detail rng-casino-detail\" onclick=\"showProgressiveJackpotWonDetails('1233213123', '1', '2198');\">" +
                 $"<a class=\"detail-link\" href=\"javascript:void('');\">Progressive Jackpot Won Details</a></div>";
            Assert.False(choice.League.ToString().Contains(expectedDetailLink));
        }
    }
}