namespace Fanex.BetList.Spadegaming.Tests.Builder.ChoiceBuilder
{
    using System;
    using System.Globalization;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice4301Tests
    {
        private const string FakeGameType = "Fishing God";
        private const string FakeGameGroup = "Video Slot";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice4301();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.TransId = 1;
            _ticket.WinlostDate = DateTime.Today;
            _ticket.CustId = 2;
            _ticket.Status = BetStatus.Won;
            _ticket.TransDesc = SpadegamingBetTypes.FishingGod.ToString(CultureInfo.InvariantCulture);

            _ticketHelper.GetResourceData(Arg.Is(Choice4301.GameTypeRefId), Arg.Is(_ticket.TransDesc)).Returns(FakeGameType);
            _ticketHelper.GetResourceData(Arg.Is(Choice4301.GameGroupRefId), Arg.Is(_ticket.TransDesc)).Returns(FakeGameGroup);
        }

        [Test]
        public void BuildBetTeam_Always_CallGetResourceDataOfTicketHelperToGetExpectedBetOption()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is(Choice4301.GameTypeRefId), Arg.Is(_ticket.TransDesc));
        }

        [Test]
        public void BuildBetTeam_Always_BeTeamStarstsWithHtmlContainingExpectedBetOption()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBetOptionHtml = $"<span class=\"handicap\">{FakeGameType}</span>";

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
            var expectedBetOptionHtml = $"<span class=\"handicap\">{FakeGameType}</span>";

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
        public void BuildLeague_Always_CallGetResourceDataOfTicketHelperToGetGameGroup()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is(Choice4301.GameGroupRefId), Arg.Is(_ticket.TransDesc));
        }

        [Test]
        public void BuildLeague_Always_LeagueNameStarstsWithHtmlContainingGameGroup()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            string expectedBetOptionHtml = $"<span class=\"gamegroupname\"> {FakeGameGroup}</span>";

            StringAssert.StartsWith(expectedBetOptionHtml, template.League.LeagueName.ToString());
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
            var expectedHtml = $"<div class=\"detail sg-detail\" onclick=\"showSGDetails({parameterList});\">{detailLink}</div>";

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
            var expectedHtml = $"<div class=\"detail sg-detail\" onclick=\"showSGDetails({parameterList});\">{detailLink}</div>";

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
            var expectedHtml = $"<div class=\"detail sg-detail\" onclick=\"showSGDetails({parameterList});\">{detailLink}</div>";

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
            var expectedHtml = $"<div class=\"detail sg-detail\" onclick=\"showSGDetails({parameterList});\">{detailLink}</div>";

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
            var expectedHtml = $"<div class=\"detail sg-detail\" onclick=\"showSGDetails({parameterList});\">{detailLink}</div>";

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
            var expectedHtml = $"<div class=\"detail sg-detail\" onclick=\"showSGDetails({parameterList});\">{detailLink}</div>";

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

        [Test]
        public void AdjustBetTeamToRTF_TicketBetCheckIsNullOrEmpty_AdjustedBetTeamOnlyContainsGameType()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            // Act.
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            // Assert.
            Assert.AreEqual(FakeGameType, _choice.Template.betTeam);
        }

        [Test]
        public void AdjustBetTeamToRTF_TicketBetCheckIsNotNullOrEmpty_AdjustedBetTeamContainsGameTypeAndBetCheck()
        {
            // Arrange.
            const string betCheck = "Bet Check";

            var rtfHelper = new RTFHelper().Clone();

            _ticket.BetCheck = betCheck;

            // Act.
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            // Assert.
            var expectedBetTeam = $"{FakeGameType} {betCheck}";

            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildMatchInfo_Always_SecondRowIsSportTypeAndGameGroup()
        {
            // Arrange.
            const int sportTypeId = 247;
            const string sportName = "Sport name";

            var rtfHelper = new RTFHelper().Clone();

            _ticket.SportTypeId = sportTypeId;
            _ticketHelper.GetSportNameById(Arg.Is(sportTypeId)).Returns(sportName);

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectation = $"{sportName} {FakeGameGroup}";

            Assert.AreEqual(expectation, linesInActualText[1]);
        }
    }
}