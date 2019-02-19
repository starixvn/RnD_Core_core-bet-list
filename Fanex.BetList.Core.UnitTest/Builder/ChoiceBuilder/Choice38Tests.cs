namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class Choice38Tests
    {
        private const string BetTypeName = "Single Match Parlay";
        private Choice38 choice;
        private ITicket ticket;
        private ITicketHelper ticketHelper;
        private RTFHelper rtfHelper;

        [SetUp]
        public void SetUp()
        {
            choice = new Choice38();
            ticket = new Ticket();
            ticket.BetTypeId = 38;
            ticket.BetCheck = "1111";
            ticket.EventDate = new DateTime(2018, 01, 01, 5, 20, 25);
            ticketHelper = Substitute.For<ITicketHelper>();
            ticketHelper.GetBetTypeNameById(38).Returns(BetTypeName);
            rtfHelper = new RTFHelper().Clone();
        }

        [Test]
        public void BuildBetTeam_Always_Empty()
        {
            // Act
            var template = choice.Render(ticket, ticketHelper, null, false);

            // Assert
            Assert.IsEmpty(template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            // Act
            var template = choice.Render(ticket, ticketHelper, null, false);

            // Assert
            Assert.IsNull(template.Handicap);
        }

        [Test]
        public void BuildBetType_Always_ShowBetTypeAsLink()
        {
            // Act
            var template = choice.Render(ticket, ticketHelper, null, false);

            // Assert
            var expectedBetTypeName = $"<div onclick=\"showSingleMatchParlayDetail('1111')\"><a href=\"javascript:void('');\">{BetTypeName}</a></div>";
            Assert.AreEqual(expectedBetTypeName, template.BetType.betTypeName);
        }

        [Test]
        public void BuildLeague_Always_AppendEventDateAtLast()
        {
            // Act
            var template = choice.Render(ticket, ticketHelper, null, false);

            // Assert
            var expectedEventDate = "<div class=\"event-date\">1/01/2018 05:20 AM</div>";
            Assert.True(template.League.LeagueName.ToString().Contains(expectedEventDate));
        }

        [Test]
        public void RenderRTF_Always_RenderExpectedText()
        {
            // Act
            var template = choice.RenderRTF(ticket, ticketHelper, null, false, rtfHelper);

            // Assert
            var expectedExcelText = "Single Match Parlay- vs -\n1/01/2018 05:20 AM";
            Assert.AreEqual(expectedExcelText, template[0].ToString());
        }
    }
}