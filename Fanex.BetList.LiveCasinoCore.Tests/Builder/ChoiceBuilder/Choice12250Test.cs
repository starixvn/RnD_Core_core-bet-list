namespace Fanex.BetList.LiveCasinoCore.Tests.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice12250Test
    {
        private Choice12250 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice12250();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = new List<ITicketData>();
        }

        [Test]
        public void BuildBetTeam_WhenIsDetailTicketAndTransDescContainsChoiceKey_ShouldSetBetTeamFromResourceDataUsingOFA_BC()
        {
            // Arrange
            const string choiceValue = "1001";
            const string expectedBetTeam = "Banker";
            const string transDesc = "choice=1001*comms=1*tableCode=B001*result=P;S6;H2:B;D10;D5";

            _ticket.BetCheck = "someBetCheck"; // Makes it a detail ticket
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("OFA_BC", choiceValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("OFA_BC", choiceValue);
        }



        [Test]
        public void BuildBetTeam_WhenIsDetailTicketButTransDescDoesNotContainChoiceKey_ShouldSetBetTeamToNull()
        {
            // Arrange
            const string transDesc = "comms=1*tableCode=B001*result=P;S6;H2:B;D10;D5";

            _ticket.BetCheck = "someBetCheck"; // Makes it a detail ticket
            _ticket.TransDesc = transDesc;

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNull(_choice.Template.betTeam);
            _ticketHelper.DidNotReceive().GetResourceData(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void BuildBetTeam_WhenIsDetailTicketAndTransDescContainsChoiceKeyWithDifferentValue_ShouldSetBetTeamFromResourceData()
        {
            // Arrange
            const string choiceValue = "2001";
            const string expectedBetTeam = "Player";
            const string transDesc = "choice=2001*comms=1*tableCode=B001*result=P;S6;H2:B;D10;D5";

            _ticket.BetCheck = "someBetCheck"; // Makes it a detail ticket
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("OFA_BC", choiceValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("OFA_BC", choiceValue);
        }

        [Test]
        public void BuildBetTeam_WhenIsDetailTicketAndTransDescContainsChoiceKeyWithSpecialCharacters_ShouldSetBetTeamFromResourceData()
        {
            // Arrange
            const string choiceValue = "BANKER_PAIR";
            const string expectedBetTeam = "Banker Pair";
            const string transDesc = "choice=BANKER_PAIR*comms=1*tableCode=B001*result=P;S6;H2:B;D10;D5";

            _ticket.BetCheck = "someBetCheck"; // Makes it a detail ticket
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("OFA_BC", choiceValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("OFA_BC", choiceValue);
        }

        [Test]
        public void BuildBetTeam_WhenIsDetailTicketAndTransDescIsEmpty_ShouldSetBetTeamToNull()
        {
            // Arrange
            _ticket.BetCheck = "someBetCheck"; // Makes it a detail ticket
            _ticket.TransDesc = "";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNull(_choice.Template.betTeam);
            _ticketHelper.DidNotReceive().GetResourceData(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void BuildBetTeam_WhenIsDetailTicketAndTransDescIsNull_ShouldSetBetTeamToNull()
        {
            // Arrange
            _ticket.BetCheck = "someBetCheck"; // Makes it a detail ticket
            _ticket.TransDesc = null;

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNull(_choice.Template.betTeam);
            _ticketHelper.DidNotReceive().GetResourceData(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void BuildBetTeam_WhenIsNotDetailTicket_ShouldSetBetTeamToNull()
        {
            // Arrange
            _ticket.BetCheck = null; // Makes it NOT a detail ticket
            _ticket.TransDesc = "choice=1001*comms=1*tableCode=B001*result=P;S6;H2:B;D10;D5";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNull(_choice.Template.betTeam);
            _ticketHelper.DidNotReceive().GetResourceData(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void BuildBetTeam_WhenIsNotDetailTicketAndTransDescContainsChoiceKey_ShouldSetBetTeamToNull()
        {
            // Arrange
            _ticket.BetCheck = ""; // Makes it NOT a detail ticket
            _ticket.TransDesc = "choice=1001*comms=1*tableCode=B001*result=P;S6;H2:B;D10;D5";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNull(_choice.Template.betTeam);
            _ticketHelper.DidNotReceive().GetResourceData(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void BuildBetTeam_WhenIsDetailTicketAndTransDescContainsChoiceKeyWithDifferentSeparator_ShouldSetBetTeamFromResourceData()
        {
            // Arrange
            const string choiceValue = "1001";
            const string expectedBetTeam = "Banker";
            const string transDesc = "choice=1001*comms=1*tableCode=B001";

            _ticket.BetCheck = "someBetCheck"; // Makes it a detail ticket
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("OFA_BC", choiceValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("OFA_BC", choiceValue);
        }

        [Test]
        public void BuildBetTeam_WhenIsDetailTicketAndTransDescContainsChoiceKeyWithMixedSeparators_ShouldSetBetTeamFromResourceData()
        {
            // Arrange
            const string choiceValue = "1001";
            const string expectedBetTeam = "Banker";
            const string transDesc = "choice=1001*comms=1;tableCode=B001*result=P;S6;H2:B;D10;D5";

            _ticket.BetCheck = "someBetCheck"; // Makes it a detail ticket
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("OFA_BC", choiceValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("OFA_BC", choiceValue);
        }

        [Test]
        public void BuildBetTeam_WhenIsDetailTicketAndTransDescContainsChoiceKeyWithCaseInsensitiveKey_ShouldSetBetTeamFromResourceData()
        {
            // Arrange
            const string choiceValue = "1001";
            const string expectedBetTeam = "Banker";
            const string transDesc = "CHOICE=1001*comms=1*tableCode=B001*result=P;S6;H2:B;D10;D5";

            _ticket.BetCheck = "someBetCheck"; // Makes it a detail ticket
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("OFA_BC", choiceValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("OFA_BC", choiceValue);
        }

        [Test]
        public void BuildBetTeam_WhenIsDetailTicketAndTransDescContainsChoiceKeyWithMixedCase_ShouldSetBetTeamFromResourceData()
        {
            // Arrange
            const string choiceValue = "1001";
            const string expectedBetTeam = "Banker";
            const string transDesc = "Choice=1001*Comms=1*TableCode=B001*Result=P;S6;H2:B;D10;D5";

            _ticket.BetCheck = "someBetCheck"; // Makes it a detail ticket
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("OFA_BC", choiceValue).Returns(expectedBetTeam);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
            _ticketHelper.Received(1).GetResourceData("OFA_BC", choiceValue);
        }
    }
}
