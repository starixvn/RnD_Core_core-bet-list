// --------------------------------------------------------------------------
// <copyright file="Choice133Test.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2014.6.10">Created</change>
// </history>

namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.UnitTest.Common;
    using Fanex.BetList.Core.UnitTest.Common.Enums;
    using Fanex.BetList.Core.UnitTest.Stubs;
    using NUnit.Framework;

    [TestFixture]
    public class Choice133Test
    {
        private ITicket _ticket;
        private IChoice _choice;
        private ITicketHelper _ticketHelperStub;

        [SetUp]
        public void SetUp()
        {
            _ticket = TestData.CreateTicket(BetTypes.HomeToWinBothHalves);
            _choice = new Choice133();
            _ticketHelperStub = new TicketHelperStub();
        }

        [Test]
        public void BuildChoice_BetTeamOfTicketContainsY_ShowBetTeamIsLableYes()
        {
            // Arrange
            _ticket.BetTeam = "y";

            // Act
            Choice_Template template = _choice.Render(_ticket, _ticketHelperStub, null, false);

            // Assert
            string expected = "Yes";
            Assert.AreEqual(expected, template.betTeam);
        }

        [Test]
        public void BuildChoice_BetTeamOfTicketContainsN_ShowBetTeamIsNO()
        {
            // Arrange
            _ticket.BetTeam = "n";

            // Act
            Choice_Template template = _choice.Render(_ticket, _ticketHelperStub, null, false);

            // Assert
            string expected = "No";
            Assert.AreEqual(expected, template.betTeam);
        }

        [Test]
        public void BuildChoice_BetTeamOfTicketIsEmpty_ShowBetTeamIsNo()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;

            // Act
            Choice_Template template = _choice.Render(_ticket, _ticketHelperStub, null, false);

            // Assert
            string expected = "No";
            Assert.AreEqual(expected, template.betTeam);
        }

        [Test]
        public void BuildChoice_Ticket_VisibleHandicap()
        {
            // Act
            Choice_Template template = _choice.Render(_ticket, _ticketHelperStub, null, false);

            // Assert
            Assert.IsNull(template.Handicap);
        }

        [Test]
        public void BuildChoice_Ticket_VisibleOddMixParlay()
        {
            // Act
            Choice_Template template = _choice.Render(_ticket, _ticketHelperStub, null, false);

            // Assert
            Assert.IsNull(template.OddMixParlay);
        }

        [Test]
        public void BuildChoice_Ticket_VisibleScore()
        {
            // Act
            Choice_Template template = _choice.Render(_ticket, _ticketHelperStub, null, false);

            // Assert
            Assert.IsNull(template.Score);
        }
    }
}
