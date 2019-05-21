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
            ticket = new Ticket
            {
                BetTypeId = 38,
                BetCheck = "1111",
                TransId = 222,
                CustId = 333
            };
            ticketHelper = Substitute.For<ITicketHelper>();
            ticketHelper.Index = 1;
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
            ticket.Status = "won";
            var template = choice.Render(ticket, ticketHelper, null, false);

            // Assert
            var expectedBetTypeName =
                $"<div class=\"combinationLink\">" +
                $"<div onclick=\"showSingleMatchParlayDetail('222', '333', 'won', 1)\"><a href=\"javascript:void('');\">{BetTypeName}</a></div></div>" +
                $"<div id=\"divEvent_2221\" style=\"display: none\" ></div>";
            Assert.AreEqual(expectedBetTypeName, template.BetType.betTypeName);
        }

        [Test]
        public void RenderRTF_BetType_RenderExpectedText()
        {
            // Act
            var template = choice.RenderRTF(ticket, ticketHelper, null, false, rtfHelper);

            // Assert
            var expectedExcelText = "Single Match Parlay- vs -";
            Assert.AreEqual(expectedExcelText, template[0].ToString());
        }
    }
}