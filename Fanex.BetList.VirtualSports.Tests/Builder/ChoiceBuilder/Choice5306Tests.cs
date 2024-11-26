namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using System;
    using System.Globalization;
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice5306Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice5306();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTypeId = VirtualSports3BetTypes.Handicap;
            _ticket.TransId = 1;
            _ticket.CustId = 1000;
            _ticket.HomeId = 1;
            _ticket.AwayId = 2;

            _ticketHelper.GetBetTypeNameById(Arg.Is(VirtualSports3BetTypes.Handicap), Arg.Any<object>()).Returns("Handicap");
            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns("Team name 1");
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns("Team name 2");

            _ticketHelper.GetResourceData("LVS_MatchCode", "2").Returns("Match Day");
            _ticketHelper.GetResourceData("LVS_MatchCode", "1").Returns("Cup");
            _ticketHelper.GetResourceData("LVS_MatchCode", "1001").Returns("Group A");
            _ticketHelper.GetVirtualSportMatchCodeFormat("{2} 6, {1} 57853", "LVS_MatchCode").Returns("Match Day 6, Cup 57853");
        }

        [TestCase("h", "Team name 1")]        
        [TestCase("a", "Team name 2")]
        public void BuildTeamName_Always_RenderCorrectName(string betTeam, string expectedBetTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetTeam, builtChoice.betTeam);
        }

        [Test]
        public void BuildMatch_TransDescContainsFullMatchDayOrRoundInfo_AppendMatchDayOrRoundInfoRightAfterMatch()
        {
            // Arrange.
            var ExpectedMatchDayOrRoundInfo ="Match Day 6, Cup 57853";
            _ticket.TransDesc = "m=\"{2} 6, {1} 57853\"";

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedHtml = $"<div class=\"match\">{ExpectedMatchDayOrRoundInfo}</div>";

            StringAssert.EndsWith(expectedHtml, builtChoice.Match.ToString());
        }
    }
}