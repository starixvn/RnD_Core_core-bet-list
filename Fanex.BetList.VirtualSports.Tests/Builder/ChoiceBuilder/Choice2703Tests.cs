namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using System;
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2703Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2703();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = 2703;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("o")]
        [TestCase("O")]
        public void BuildTeamName_BetTeamIsO_RenderedBetTeamIsOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.over, builtChoice.betTeam);
        }

        [TestCase("o")]
        [TestCase("O")]
        public void BuildTeamName_BetTeamIsO_BetTeamClassNameIsFavorite(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("favorite", builtChoice.betTeamClassName);
        }

        [TestCase("u")]
        [TestCase("U")]
        public void BuildTeamName_BetTeamIsU_RenderedBetTeamIsUnder(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.under, builtChoice.betTeam);
        }

        [TestCase("u")]
        [TestCase("U")]
        public void BuildTeamName_BetTeamIsU_BetTeamClassNameIsUnderdog(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("underdog", builtChoice.betTeamClassName);
        }

        [Test]
        public void BuildMatch_Always_AppendMatchDayOrRoundInfoRightAfterMatch()
        {
            // Arrange.
            _ticket.TransDesc = "Round 16-1, Nation Cup 6666";

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            string expectedHtmlOfRoundInfo = "<div class=\"match\">Round 16-1, Nation Cup 6666</div>";
            Assert.IsTrue(builtChoice.Match.ToString().EndsWith(expectedHtmlOfRoundInfo, StringComparison.InvariantCulture));
        }

        [Test]
        public void BuildBetTeamsInfo_Always_MatchDayOrRoundInfoIsAtTheThirdRow()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.TransDesc = "Round 16-1, Nation Cup 6666";
            _ticketHelper.GetBetTypeNameById(Arg.Is(2703), Arg.Any<object>()).Returns("FT O/U");

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            var expectedRoundInfo = "Round 16-1, Nation Cup 6666";

            Assert.AreEqual(linesInActualText[2], expectedRoundInfo);
        }
    }
}