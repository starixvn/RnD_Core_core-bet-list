namespace Fanex.BetList.RNGCore.Tests.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class ChoiceRNGCoreTests
    {
        private ChoiceRNGCore _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;

        [SetUp]
        public void Setup()
        {
            _choice = new ChoiceRNGCore();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = new List<ITicketData>();

            // Setup default ticket properties
            _ticket.TransId = 1;
            _ticket.CustId = 123;
            _ticket.BetTypeId = 1201;
            _ticket.SportTypeId = 1;
            _ticket.WinlostDate = DateTime.Today;
            _ticket.JackpotInfo = string.Empty;
            _ticket.BetCheck = string.Empty;
        }

        [Test]
        public void BuildLeague_Always_SetsLeagueNameWithGameGroupSpan()
        {
            // Arrange
            _ticketHelper.GetResourceData(Arg.Any<string>(), Arg.Any<string>()).Returns("Game Group Name");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("<span class=\"gamegroupname\">", template.League.LeagueName.ToString());
            StringAssert.Contains("Game Group Name", template.League.LeagueName.ToString());
        }

        [Test]
        public void BuildLeague_SummaryTicket_AppendsDetailsLink()
        {
            // Arrange
            _ticket.BetCheck = "";
            _ticketHelper.GetResourceData(Arg.Any<string>(), Arg.Any<string>()).Returns("Game Group");
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("showRNGBetDetails", template.League.ToString());
            StringAssert.Contains("detail-link", template.League.ToString());
        }

        [Test]
	[Test]
        public void BuildLeague_DetailTicketWithoutJackpotInfo_DoesNotAppendJackpotDetails()
        {
            // Arrange
            _ticket.BetCheck = "detail|&|displayDetail";
            _ticket.JackpotInfo = string.Empty;
            _ticketHelper.GetResourceData(Arg.Any<string>(), Arg.Any<string>()).Returns("Game Group");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.DoesNotContain("showJackpotWonDetails", template.League.ToString());
            StringAssert.DoesNotContain("jackpot-detail", template.League.ToString());
        }

        [Test]
        public void BuildLeague_DetailTicketWithNullJackpotInfo_DoesNotAppendJackpotDetails()
        {
            // Arrange
            _ticket.BetCheck = "detail|&|displayDetail";
            _ticket.JackpotInfo = null;
            _ticketHelper.GetResourceData(Arg.Any<string>(), Arg.Any<string>()).Returns("Game Group");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.DoesNotContain("showJackpotWonDetails", template.League.ToString());
            StringAssert.DoesNotContain("jackpot-detail", template.League.ToString());
        }

        [Test]
        public void BuildLeague_DetailTicketWithInvalidJackpotInfo_DoesNotAppendJackpotDetails()
        {
            // Arrange
            _ticket.BetCheck = "detail|&|displayDetail";
            _ticket.JackpotInfo = "invalid=info";
            _ticketHelper.GetResourceData(Arg.Any<string>(), Arg.Any<string>()).Returns("Game Group");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.DoesNotContain("showJackpotWonDetails", template.League.ToString());
            StringAssert.DoesNotContain("jackpot-detail", template.League.ToString());
        }

        [Test]
            _ticket.BetCheck = "detail|&|displayDetail";
	[Test]
        public void BuildDetailsLink_SummaryTicket_ReturnsCorrectMarkup()
        {
            // Arrange
            _ticket.BetCheck = "";
            _ticket.TransId = 999;
            _ticket.CustId = 888;
            _ticket.SportTypeId = 777;
            _ticket.WinlostDate = new DateTime(2023, 6, 10);
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Basketball");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("showRNGBetDetails('999', '6/10/2023 12:00:00 AM', 888, 0, 777, 'Basketball')", template.League.ToString());
            StringAssert.Contains("rng-detail", template.League.ToString());
            StringAssert.Contains(CoreBetList.details, template.League.ToString());
        }

        [Test]
        public void BuildDetailsLink_WithNullSportName_HandlesGracefully()
        {
            // Arrange
            _ticket.BetCheck = "";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns((string)null);

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("showRNGBetDetails", template.League.ToString());
            StringAssert.Contains(", ''", template.League.ToString()); // Empty sport name
        }

        [Test]
        public void BuildLeague_WithEmptyRefIdForGameGroup_UsesEmptyString()
        {
            // Arrange
            _ticketHelper.GetResourceData(string.Empty, _ticket.BetTypeId.ToString()).Returns("Default Game Group");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("Default Game Group", template.League.LeagueName.ToString());
        }

        [Test]
        public void BuildLeague_WithCustomRefIdForGameGroup_UsesCustomRefId()
        {
            // Arrange
            var customChoice = new CustomChoiceRNGCore();
            _ticketHelper.GetResourceData("CUSTOM_REF", _ticket.BetTypeId.ToString()).Returns("Custom Game Group");

            // Act
            var template = customChoice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("Custom Game Group", template.League.LeagueName.ToString());
        }

        [Test]
        public void BuildLeague_WithNullBetCheck_HandlesGracefully()
        {
            // Arrange
            _ticket.BetCheck = null;
            _ticketHelper.GetResourceData(Arg.Any<string>(), Arg.Any<string>()).Returns("Game Group");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(template.League.LeagueName.ToString());
        }

        [Test]
        public void BuildLeague_WithEmptyBetCheck_HandlesGracefully()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;
            _ticketHelper.GetResourceData(Arg.Any<string>(), Arg.Any<string>()).Returns("Game Group");

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(template.League.LeagueName.ToString());
        }

        private class CustomChoiceRNGCore : ChoiceRNGCore
        {
            protected override string RefIdForGameGroup => "CUSTOM_REF";
        }
    }
} 