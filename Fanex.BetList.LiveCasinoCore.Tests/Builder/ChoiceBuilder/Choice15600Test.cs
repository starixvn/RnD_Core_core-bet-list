namespace Fanex.BetList.LiveCasinoCore.Tests.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice15600Test
    {
        private Choice15600 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice15600();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = new List<ITicketData>();

            // Setup default ticket properties
            _ticket.TransId = 1;
            _ticket.BetCheck = "detail|&|displayDetail";
            _ticket.TransDesc = "tablecode=B001*comms=1*result=P";
            _ticket.BetTypeId = 15600;
            _ticket.SportTypeId = 1;
            _ticket.CustId = 123;
            _ticket.MatchCode = "MATCH001";
            _ticket.SiteId = "SITE001";
            _ticket.CurrencyId = 20;
        }

        [Test]
        public void Render_WithValidTransDescAndTableCode_ShouldSetBetTeamFromResourceDataUsingPTL_BB()
        {
            // Arrange
            const string tableCodeValue = "B001";
            const string expectedBetTeam = "Baccarat Table B001";
            const string transDesc = "tablecode=B001*comms=1*result=P";

            _ticket.BetCheck = "someBetCheck"; // Makes it a detail ticket
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("PTL_BB", tableCodeValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("PTL_BB", tableCodeValue);
        }

        [Test]
        public void Render_WithTransDescMissingTableCode_ShouldSetBetTeamToNull()
        {
            // Arrange
            const string transDesc = "comms=1*result=P"; // No tablecode

            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = transDesc;

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNull(_choice.Template.betTeam);
            _ticketHelper.DidNotReceive().GetResourceData(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Render_WithEmptyTransDesc_ShouldSetBetTeamToNull()
        {
            // Arrange
            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = "";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNull(_choice.Template.betTeam);
            _ticketHelper.DidNotReceive().GetResourceData(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Render_WithNullTransDesc_ShouldSetBetTeamToNull()
        {
            // Arrange
            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = null;

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNull(_choice.Template.betTeam);
            _ticketHelper.DidNotReceive().GetResourceData(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Render_WithSummaryTicket_ShouldSetBetTeamToNull()
        {
            // Arrange
            _ticket.BetCheck = ""; // Makes it a summary ticket
            _ticket.TransDesc = "tablecode=B001*comms=1*result=P";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNull(_choice.Template.betTeam);
            _ticketHelper.DidNotReceive().GetResourceData(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Render_WithNullBetCheck_ShouldSetBetTeamToNull()
        {
            // Arrange
            _ticket.BetCheck = null; // Makes it a summary ticket
            _ticket.TransDesc = "tablecode=B001*comms=1*result=P";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNull(_choice.Template.betTeam);
            _ticketHelper.DidNotReceive().GetResourceData(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Render_WithDifferentTableCodeValues_ShouldSetBetTeamCorrectly()
        {
            // Arrange
            const string tableCodeValue = "VIP001";
            const string expectedBetTeam = "VIP Baccarat Table";
            const string transDesc = "tablecode=VIP001*comms=2*result=W";

            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("PTL_BB", tableCodeValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("PTL_BB", tableCodeValue);
        }

        [Test]
        public void Render_WithTableCodeInDifferentPosition_ShouldSetBetTeamCorrectly()
        {
            // Arrange
            const string tableCodeValue = "C002";
            const string expectedBetTeam = "Baccarat Table C002";
            const string transDesc = "comms=1*tablecode=C002*result=P";

            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("PTL_BB", tableCodeValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("PTL_BB", tableCodeValue);
        }

        [Test]
        public void Render_WithTableCodeAtEnd_ShouldSetBetTeamCorrectly()
        {
            // Arrange
            const string tableCodeValue = "D003";
            const string expectedBetTeam = "Baccarat Table D003";
            const string transDesc = "comms=1*result=P*tablecode=D003";

            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("PTL_BB", tableCodeValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("PTL_BB", tableCodeValue);
        }

        [Test]
        public void Render_WithCaseInsensitiveTableCode_ShouldSetBetTeamCorrectly()
        {
            // Arrange
            const string tableCodeValue = "E004";
            const string expectedBetTeam = "Baccarat Table E004";
            const string transDesc = "TABLECODE=E004*comms=1*result=P";

            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("PTL_BB", tableCodeValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("PTL_BB", tableCodeValue);
        }

        [Test]
        public void Render_WithMixedCaseTableCode_ShouldSetBetTeamCorrectly()
        {
            // Arrange
            const string tableCodeValue = "F005";
            const string expectedBetTeam = "Baccarat Table F005";
            const string transDesc = "TableCode=F005*comms=1*result=P";

            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("PTL_BB", tableCodeValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("PTL_BB", tableCodeValue);
        }

        [Test]
        public void Render_WithDifferentSeparators_ShouldSetBetTeamIsNull()
        {
            // Arrange
            const string transDesc = "tablecode=G006;comms=1;result=P";

            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = transDesc;

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNull(_choice.Template.betTeam);
        }

        [Test]
        public void Render_WithEmptyTableCodeValue_ShouldSetBetTeamToNull()
        {
            // Arrange
            const string transDesc = "tablecode=*comms=1*result=P";

            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = transDesc;

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNull(_choice.Template.betTeam);
            _ticketHelper.DidNotReceive().GetResourceData(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Render_WithSpecialCharactersInTableCode_ShouldSetBetTeamCorrectly()
        {
            // Arrange
            const string tableCodeValue = "VIP-001_Table";
            const string expectedBetTeam = "VIP Baccarat Table 001";
            const string transDesc = "tablecode=VIP-001_Table*comms=1*result=P";

            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("PTL_BB", tableCodeValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("PTL_BB", tableCodeValue);
        }

        [Test]
        public void Render_WithLongTableCode_ShouldSetBetTeamCorrectly()
        {
            // Arrange
            const string tableCodeValue = "VERY_LONG_TABLE_CODE_THAT_CONTAINS_MANY_CHARACTERS";
            const string expectedBetTeam = "Long Table Code";
            const string transDesc = "tablecode=VERY_LONG_TABLE_CODE_THAT_CONTAINS_MANY_CHARACTERS*comms=1*result=P";

            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("PTL_BB", tableCodeValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("PTL_BB", tableCodeValue);
        }

        [Test]
        public void Render_WithUnicodeTableCode_ShouldSetBetTeamCorrectly()
        {
            // Arrange
            const string tableCodeValue = "Table中文";
            const string expectedBetTeam = "Chinese Table";
            const string transDesc = "tablecode=Table中文*comms=1*result=P";

            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("PTL_BB", tableCodeValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("PTL_BB", tableCodeValue);
        }

        [Test]
        public void Render_WithEmojiTableCode_ShouldSetBetTeamCorrectly()
        {
            // Arrange
            const string tableCodeValue = "🎯🎲🎮";
            const string expectedBetTeam = "Emoji Table";
            const string transDesc = "tablecode=🎯🎲🎮*comms=1*result=P";

            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("PTL_BB", tableCodeValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("PTL_BB", tableCodeValue);
        }

        [Test]
        public void Render_WithMockedTicketHelper_ShouldSetBetTeamCorrectly()
        {
            // Arrange
            const string tableCodeValue = "H007";
            const string expectedBetTeam = "Mocked Bet Team";
            const string transDesc = "tablecode=H007*comms=1*result=P";

            var mockTicketHelper = Substitute.For<ITicketHelper>();
            mockTicketHelper.GetResourceData("PTL_BB", tableCodeValue).Returns(expectedBetTeam);

            _ticket.BetCheck = "someBetCheck";
            _ticket.TransDesc = transDesc;

            // Act
            _choice.Render(_ticket, mockTicketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            mockTicketHelper.Received(1).GetResourceData("PTL_BB", tableCodeValue);
        }

        [Test]
        public void Render_WithAllPropertiesChanged_ShouldSetBetTeamCorrectly()
        {
            // Arrange
            const string tableCodeValue = "I008";
            const string expectedBetTeam = "Changed Bet Team";
            const string transDesc = "tablecode=I008*comms=2*result=W";

            _ticket.TransId = 999;
            _ticket.BetCheck = "differentBetCheck";
            _ticket.TransDesc = transDesc;
            _ticket.BetTypeId = 9999;
            _ticket.SportTypeId = 5;
            _ticket.CustId = 999;
            _ticket.MatchCode = "DIFFERENT_MATCH";
            _ticket.SiteId = "DIFFERENT_SITE";
            _ticket.CurrencyId = 30;

            _ticketHelper.GetResourceData("PTL_BB", tableCodeValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("PTL_BB", tableCodeValue);
        }
    }
}
