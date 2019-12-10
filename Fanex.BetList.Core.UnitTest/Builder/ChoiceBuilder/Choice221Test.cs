namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Templates;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice221Test
    {
        private const string FastMarketsChoice = "Goal (09:00-09:59)";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice221();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = 221;
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticketHelper
                .GetFastMarketSituation(_ticket.BetTypeId.ToString(), _ticket.BetTeam, _ticket.Handicap1, _ticket.Handicap2)
                .Returns(FastMarketsChoice);
            _ticketHelper.GetBetTypeNameById(_ticket.BetTypeId, Arg.Any<object>()).Returns("Next 1 Minute");
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_TicketOddsSpreadIsNotZero_SetTicketOddsSpreadToHandicap()
        {
            // Arrange.
            _ticket.OddsSpread = 2.5M;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("2.5", template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_TicketOddsSpreadIsNotZero_HandicapIsInNewLine()
        {
            // Arrange.
            _ticket.OddsSpread = 2.5M;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            StringAssert.EndsWith("<br/>", template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_TicketOddsSpreadIsZero_SetHandicapToNull()
        {
            // Arrange.
            _ticket.OddsSpread = 0;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameToFavorite()
        {
            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string Favorite = "favorite";

            Assert.AreEqual(Favorite, template.betTeamClassName);
        }

        [Test]
        public void BuildBetFigure_TicketOddsSpreadIsNotZero_HandicapAndLiveScoreAreAtTheSecondRow()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.OddsSpread = 2.5M;
            _ticket.IsLive = true;
            _ticket.LiveHomeScore = 1;
            _ticket.LiveAwayScore = 0;

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedMatchName = "2.5 [1-0]";

            Assert.AreEqual(expectedMatchName, linesInActualText[1]);
        }

        [Test]
        public void BuildBetFigure_TicketOddsSpreadIsZero_LiveScoreIsAtTheSameRowOfBetTeam()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.OddsSpread = 0;
            _ticket.IsLive = true;
            _ticket.LiveHomeScore = 1;
            _ticket.LiveAwayScore = 0;

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedMatchName = $"{FastMarketsChoice} [1-0]";

            Assert.AreEqual(expectedMatchName, linesInActualText[0]);
        }
    }
}