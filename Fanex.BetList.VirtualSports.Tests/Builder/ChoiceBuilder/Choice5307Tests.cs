namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice5307Tests : MatchCodeTests
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();

            Choice = new Choice5307();
            Ticket.BetTypeId = VirtualSports3BetTypes.OverUnder;

            TicketHelper.GetBetTypeNameById(Arg.Is(VirtualSports3BetTypes.OverUnder), Arg.Any<object>()).Returns("Over/Under");
        }

        [TestCase("o")]
        [TestCase("O")]
        public void BuildTeamName_BetTeamIsO_RenderedBetTeamIsOver(string betTeam)
        {
            // Arrange.
            Ticket.BetTeam = betTeam;

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.over, builtChoice.betTeam);
        }

        [TestCase("o")]
        [TestCase("O")]
        public void BuildTeamName_BetTeamIsO_BetTeamClassNameIsFavorite(string betTeam)
        {
            // Arrange.
            Ticket.BetTeam = betTeam;

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            Assert.AreEqual("favorite", builtChoice.betTeamClassName);
        }

        [TestCase("u")]
        [TestCase("U")]
        public void BuildTeamName_BetTeamIsU_RenderedBetTeamIsUnder(string betTeam)
        {
            // Arrange.
            Ticket.BetTeam = betTeam;

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.under, builtChoice.betTeam);
        }

        [TestCase("u")]
        [TestCase("U")]
        public void BuildTeamName_BetTeamIsU_BetTeamClassNameIsUnderdog(string betTeam)
        {
            // Arrange.
            Ticket.BetTeam = betTeam;

            // Act.
            var builtChoice = Choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            Assert.AreEqual("underdog", builtChoice.betTeamClassName);
        }

        [Test]
        public void BuildMatch_TransDescContainsFullMatchDayOrRoundInfo_AppendMatchDayOrRoundInfoRightAfterMatch()
        {
            // Arrange.
            var ExpectedMatchDayOrRoundInfo ="Match Day 6, Cup 57853";
            Ticket.TransDesc = "m=\"{2} 6, {1} 57853\"";

            // Act.
            var builtChoice = _choice.Render(Ticket, TicketHelper, null, false);

            // Assert.
            var expectedHtml = $"<div class=\"match\">{ExpectedMatchDayOrRoundInfo}</div>";

            StringAssert.EndsWith(expectedHtml, builtChoice.Match.ToString());
        }
    }
}