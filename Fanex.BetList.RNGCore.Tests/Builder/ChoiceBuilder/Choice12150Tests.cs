namespace Fanex.BetList.RNGCore.Tests.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice12150Tests
    {
        private Choice12150 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice12150();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = new List<ITicketData>();

            // Setup default ticket properties
            _ticket.TransId = 1;
            _ticket.CustId = 123;
            _ticket.BetTypeId = 12150;
            _ticket.SportTypeId = 1;
            _ticket.WinlostDate = DateTime.Today;
            _ticket.JackpotInfo = string.Empty;
            _ticket.BetCheck = string.Empty;
        }

        [Test]
        public void BuildLeague_Always_SetsLeagueNameWithCorrectGameGroupRefId()
        {
            // Arrange
            _ticketHelper.GetResourceData("SS_RNG_GG", _ticket.BetTypeId.ToString()).Returns("SS RNG Game Group");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("<span class=\"gamegroupname\">", template.League.LeagueName.ToString());
            StringAssert.Contains("SS RNG Game Group", template.League.LeagueName.ToString());
        }

        [Test]
        public void BuildLeague_SummaryTicket_AppendsDetailsLink()
        {
            // Arrange
            _ticket.BetCheck = "";
            _ticketHelper.GetResourceData("SS_RNG_GG", _ticket.BetTypeId.ToString()).Returns("SS RNG Game Group");
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("showRNGBetDetails", template.League.ToString());
            StringAssert.Contains("detail-link", template.League.ToString());
            StringAssert.Contains("rng-detail", template.League.ToString());
        }

        [Test]
        public void BuildLeague_DetailTicketWithJackpotInfo_AppendsJackpotWonDetails()
        {
            // Arrange
            _ticket.BetCheck = "detail|&|displayDetail";
            _ticket.JackpotInfo = "jpw=12345,jp=1";
            _ticketHelper.GetResourceData("SS_RNG_GG", _ticket.BetTypeId.ToString()).Returns("SS RNG Game Group");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("showRNGJackpotWonDetails", template.League.ToString());
            StringAssert.Contains("jackpot-detail", template.League.ToString());
            StringAssert.Contains(CoreBetList.JackpotWonDetails, template.League.ToString());
        }

        [Test]
        public void BuildLeague_DetailTicketWithoutJackpotInfo_DoesNotAppendJackpotDetails()
        {
            // Arrange
            _ticket.BetCheck = "detail|&|displayDetail";
            _ticket.JackpotInfo = string.Empty;
            _ticketHelper.GetResourceData("SS_RNG_GG", _ticket.BetTypeId.ToString()).Returns("SS RNG Game Group");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.DoesNotContain("showRNGJackpotWonDetails", template.League.ToString());
            StringAssert.DoesNotContain("jackpot-detail", template.League.ToString());
        }

        [Test]
        public void BuildDetailsLink_SummaryTicket_ReturnsCorrectMarkup()
        {
            // Arrange
            _ticket.BetCheck = "";
            _ticket.TransId = 999;
            _ticket.CustId = 888;
            _ticket.SportTypeId = 777;
            _ticket.WinlostDate = new DateTime(2023, 6, 10);
            _ticketHelper.GetResourceData("SS_RNG_GG", _ticket.BetTypeId.ToString()).Returns("SS RNG Game Group");
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Basketball");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("showRNGBetDetails('999', '6/10/2023 12:00:00 AM', 888, 0, 777, 'Basketball')", template.League.ToString());
            StringAssert.Contains("rng-detail", template.League.ToString());
            StringAssert.Contains(CoreBetList.details, template.League.ToString());
        }

        [Test]
        public void BuildLeague_WithNullBetCheck_HandlesGracefully()
        {
            // Arrange
            _ticket.BetCheck = null;
            _ticketHelper.GetResourceData("SS_RNG_GG", _ticket.BetTypeId.ToString()).Returns("SS RNG Game Group");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(template.League.LeagueName.ToString());
            StringAssert.Contains("SS RNG Game Group", template.League.LeagueName.ToString());
        }

        [Test]
        public void BuildLeague_WithEmptyBetCheck_HandlesGracefully()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;
            _ticketHelper.GetResourceData("SS_RNG_GG", _ticket.BetTypeId.ToString()).Returns("SS RNG Game Group");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(template.League.LeagueName.ToString());
            StringAssert.Contains("SS RNG Game Group", template.League.LeagueName.ToString());
        }

        [Test]
        public void BuildLeague_WithDifferentBetTypeId_UsesCorrectRefId()
        {
            // Arrange
            _ticket.BetTypeId = 9999;
            _ticketHelper.GetResourceData("SS_RNG_GG", "9999").Returns("Different Game Group");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("Different Game Group", template.League.LeagueName.ToString());
        }

        [Test]
        public void BuildLeague_WithNullResourceData_HandlesGracefully()
        {
            // Arrange
            _ticketHelper.GetResourceData("SS_RNG_GG", _ticket.BetTypeId.ToString()).Returns((string)null);

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(template.League.LeagueName.ToString());
            StringAssert.Contains("<span class=\"gamegroupname\">", template.League.LeagueName.ToString());
        }

        [Test]
        public void BuildLeague_WithEmptyResourceData_HandlesGracefully()
        {
            // Arrange
            _ticketHelper.GetResourceData("SS_RNG_GG", _ticket.BetTypeId.ToString()).Returns(string.Empty);

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(template.League.LeagueName.ToString());
            StringAssert.Contains("<span class=\"gamegroupname\">", template.League.LeagueName.ToString());
        }

        [Test]
        public void BuildLeague_WithInvalidJackpotInfo_DoesNotAppendJackpotDetails()
        {
            // Arrange
            _ticket.BetCheck = "detail|&|displayDetail";
            _ticket.JackpotInfo = "invalid=info,nojpw=123";
            _ticketHelper.GetResourceData("SS_RNG_GG", _ticket.BetTypeId.ToString()).Returns("SS RNG Game Group");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.DoesNotContain("showRNGJackpotWonDetails", template.League.ToString());
            StringAssert.DoesNotContain("jackpot-detail", template.League.ToString());
        }

        [Test]
        public void BuildLeague_WithNullJackpotInfo_DoesNotAppendJackpotDetails()
        {
            // Arrange
            _ticket.BetCheck = "detail|&|displayDetail";
            _ticket.JackpotInfo = null;
            _ticketHelper.GetResourceData("SS_RNG_GG", _ticket.BetTypeId.ToString()).Returns("SS RNG Game Group");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.DoesNotContain("showRNGJackpotWonDetails", template.League.ToString());
            StringAssert.DoesNotContain("jackpot-detail", template.League.ToString());
        }
    }
}
