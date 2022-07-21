namespace Fanex.BetList.MaxGame.Tests.Builder.ChoiceBuilder
{
    using System;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice5401Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private readonly string bettype5401 = "Bettype 5401";

        [SetUp]
        public void Setup()
        {
            _choice = new Choice5401();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.TransId = 1;
            _ticket.WinlostDate = DateTime.Today;
            _ticket.CustId = 2;
            _ticket.Status = BetStatus.Won;
            _ticket.BetTypeId = MaxGameBetTypes.DiceWar;

            _ticketHelper.GetBetTypeNameById(_ticket.BetTypeId).Returns(bettype5401);
        }

        [Test]
        public void BuildBetTeam_Always_BeTeamStarstsWithHtmlContainingExpectedBetOption()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBetOptionHtml = $"<span class=\"handicap\">{bettype5401}</span>";

            StringAssert.StartsWith(expectedBetOptionHtml, template.betTeam);
        }

        [TestCase(null)]
        [TestCase("")]
        public void BuildBetTeam_TicketBetCheckIsNullOrEmpty_BeTeamOnlyContainsExpectedBetOption(string betCheck)
        {
            // Arrange.
            _ticket.BetCheck = betCheck;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBetOptionHtml = $"<span class=\"handicap\">{bettype5401}</span>";

            Assert.AreEqual(expectedBetOptionHtml, template.betTeam);
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

        [Test]
        public void BuildLeague_Always_SetLeaugeNameIsNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.League.LeagueName);
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
            var expectedHtml = $"<div class=\"detail maxgame-detail\" onclick=\"showMaxGameBetDetails({parameterList});\">{detailLink}</div>";

            StringAssert.Contains(expectedHtml, template.League.ToString());
        }

        [Test]
        public void GetTransactionType_WinlostDateIs2DaysAgo_TransactionTypeInDetailsLinkIsZero()
        {
            // Arrange.
            _ticket.WinlostDate = DateTime.Today.AddDays(-2);

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectedTransactionType = "0";

            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"'{_ticket.TransId}', '{_ticket.WinlostDate}', {_ticket.CustId}, {expectedTransactionType}";
            var expectedHtml = $"<div class=\"detail maxgame-detail\" onclick=\"showMaxGameBetDetails({parameterList});\">{detailLink}</div>";

            StringAssert.Contains(expectedHtml, template.League.ToString());
        }

        [TestCase("won")]
        [TestCase("WON")]
        [TestCase("lose")]
        [TestCase("LOSE")]
        [TestCase("draw")]
        [TestCase("DRAW")]
        [TestCase("refund")]
        [TestCase("REFUND")]
        public void GetTransactionType_WinlostDateIsTodayAndStatusIsWonOrLoseOrDrawOrRefund_TransactionTypeInDetailsLinkIsOne(string status)
        {
            // Arrange.
            _ticket.Status = status;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectedTransactionType = "1";

            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"'{_ticket.TransId}', '{_ticket.WinlostDate}', {_ticket.CustId}, {expectedTransactionType}";
            var expectedHtml = $"<div class=\"detail maxgame-detail\" onclick=\"showMaxGameBetDetails({parameterList});\">{detailLink}</div>";

            StringAssert.Contains(expectedHtml, template.League.ToString());
        }

        [TestCase("reject")]
        [TestCase("REJECT")]
        [TestCase("void")]
        [TestCase("VOID")]
        public void GetTransactionType_WinlostDateIsTodayAndStatusIsRejectOrVoid_TransactionTypeInDetailsLinkIsTwo(string status)
        {
            // Arrange.
            _ticket.Status = status;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectedTransactionType = "2";

            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"'{_ticket.TransId}', '{_ticket.WinlostDate}', {_ticket.CustId}, {expectedTransactionType}";
            var expectedHtml = $"<div class=\"detail maxgame-detail\" onclick=\"showMaxGameBetDetails({parameterList});\">{detailLink}</div>";

            StringAssert.Contains(expectedHtml, template.League.ToString());
        }

        [TestCase("running")]
        [TestCase("RUNNING")]
        [TestCase("waiting")]
        [TestCase("WAITING")]
        public void GetTransactionType_WinlostDateIsTodayAndStatusIsRunningOrWaiting_TransactionTypeInDetailsLinkIsThree(string status)
        {
            // Arrange.
            _ticket.Status = status;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectedTransactionType = "3";

            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"'{_ticket.TransId}', '{_ticket.WinlostDate}', {_ticket.CustId}, {expectedTransactionType}";
            var expectedHtml = $"<div class=\"detail maxgame-detail\" onclick=\"showMaxGameBetDetails({parameterList});\">{detailLink}</div>";

            StringAssert.Contains(expectedHtml, template.League.ToString());
        }

        [Test]
        public void GetTransactionType_WinlostDateIsTodayAndStatusIsNotInAcceptedStatusList_TransactionTypeInDetailsLinkIsZero()
        {
            // Arrange.
            const string unacceptableStatus = "impossible status";

            _ticket.Status = unacceptableStatus;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectedTransactionType = "0";

            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parameterList = $"'{_ticket.TransId}', '{_ticket.WinlostDate}', {_ticket.CustId}, {expectedTransactionType}";
            var expectedHtml = $"<div class=\"detail maxgame-detail\" onclick=\"showMaxGameBetDetails({parameterList});\">{detailLink}</div>";

            StringAssert.Contains(expectedHtml, template.League.ToString());
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapToNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.Null(template.Handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameToUnderdog()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectedCssClassName = "underdog";

            Assert.AreEqual(expectedCssClassName, template.betTeamClassName);
        }

        [Test]
        public void BuildBetType_Always_BetTypeIsSetToNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.Null(template.BetType);
        }

        [Test]
        public void BuildMatch_Always_BetTypeIsSetToNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.Null(template.Match);
        }

        [Test]
        public void BuildScore_Always_BetTypeIsSetToNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.Null(template.Score);
        }
    }
}