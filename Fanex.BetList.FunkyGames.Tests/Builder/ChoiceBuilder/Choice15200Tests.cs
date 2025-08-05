namespace Fanex.BetList.FunkyGames.Tests.Builder.ChoiceBuilder
{
    using System;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice15200Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private readonly string bettype5401 = "Bettype 5401";

        [SetUp]
        public void Setup()
        {
            _choice = new Choice15200();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.TransId = 1;
            _ticket.WinlostDate = DateTime.Today;
            _ticket.CustId = 2;
            _ticket.Status = BetStatus.Won;
            _ticket.BetTypeId = FunkyGamesBetTypes.SweetBombs;

            _ticketHelper.GetBetTypeNameById(_ticket.BetTypeId).Returns(bettype5401);
        }

        [Test]
        public void BuildBetTeam_TicketBetCheckIsNotNullOrEmpty_BeTeamEndsWithBetCheck()
        {
            // Arrange.
            const string betCheck = "BetCheck";

            _ticket.BetCheck = betCheck;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            StringAssert.EndsWith(betCheck, template.betTeam);
        }
      
      
        [TestCase(null)]
        [TestCase("")]
        public void BuildLeague_TicketBetCheckIsNullOrEmpty_AppendLeagueNameWithDetailsLinkContainingExpectedParameters(string betCheck)
        {
            // Arrange.
            _ticket.BetCheck = betCheck;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"'{_ticket.TransId}', '{_ticket.WinlostDate}', {_ticket.CustId}, 1";
            var expectedHtml = $"<div class=\"detail funkygames-detail\" onclick=\"showFunkyGamesBetDetails({parameterList});\">{detailLink}</div>";

            StringAssert.Contains(expectedHtml, template.League.ToString());
        }
    }
}