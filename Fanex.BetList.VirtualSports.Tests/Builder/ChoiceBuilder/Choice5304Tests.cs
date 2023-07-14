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
    using Core.App_GlobalResources;

    [TestFixture]
    public class Choice5304Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice5304();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTypeId = VirtualSports3BetTypes.FulltimeOneXTwo;
            _ticket.TransId = 1;
            _ticket.CustId = 1000;
            _ticket.HomeId = 1;
            _ticket.AwayId = 2;

            _ticketHelper.GetBetTypeNameById(Arg.Is(5304), Arg.Any<object>()).Returns("1H Over/Under");
            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns("Team name 1");
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns("Team name 2");

            _ticketHelper.GetResourceData("LVS_MatchCode", "2").Returns("Match Day");
            _ticketHelper.GetResourceData("LVS_MatchCode", "1").Returns("Cup");
            _ticketHelper.GetResourceData("LVS_MatchCode", "1001").Returns("Group A");
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
    }
}