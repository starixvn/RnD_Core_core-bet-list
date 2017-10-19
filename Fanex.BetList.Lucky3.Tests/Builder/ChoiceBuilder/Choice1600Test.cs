namespace Fanex.BetList.Lucky3.Tests.Builder.ChoiceBuilder
{
    using System;
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using Core.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice1600Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1600();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void Render_Always_ScoreMapIconIsNull()
        {
            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(actualResult.ScoreMapIcon);
        }

        [Test]
        public void Render_Always_ScoreIsNull()
        {
            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(actualResult.Score);
        }

        [Test]
        public void BuildTicketStatus_StatusIdIsDraw_ShowDetailsLink()
        {
            // Arrange.
            int drawStatusId = 111;

            _ticket.StatusId = drawStatusId;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectHtml = BuildExpectedDetailsLink(_ticket.TransId, _ticket.CustId);

            Assert.True(actualResult.League.ToString().EndsWith(expectHtml, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void BuildTicketStatus_StatusIsDraw_ShowDetailsLink()
        {
            // Arrange.
            string drawStatus = "draw";

            _ticket.Status = drawStatus;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectHtml = BuildExpectedDetailsLink(_ticket.TransId, _ticket.CustId);

            Assert.True(actualResult.League.ToString().EndsWith(expectHtml, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void BuildTicketStatus_StatusIdIsWon_ShowDetailsLink()
        {
            // Arrange.
            int drawStatusId = 112;

            _ticket.StatusId = drawStatusId;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectHtml = BuildExpectedDetailsLink(_ticket.TransId, _ticket.CustId);

            Assert.True(actualResult.League.ToString().EndsWith(expectHtml, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void BuildTicketStatus_StatusIsWon_ShowDetailsLink()
        {
            // Arrange.
            string drawStatus = "won";

            _ticket.Status = drawStatus;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectHtml = BuildExpectedDetailsLink(_ticket.TransId, _ticket.CustId);

            Assert.True(actualResult.League.ToString().EndsWith(expectHtml, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void BuildTicketStatus_StatusIdIsLose_ShowDetailsLink()
        {
            // Arrange.
            int drawStatusId = 113;

            _ticket.StatusId = drawStatusId;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectHtml = BuildExpectedDetailsLink(_ticket.TransId, _ticket.CustId);

            Assert.True(actualResult.League.ToString().EndsWith(expectHtml, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void BuildTicketStatus_StatusIsLose_ShowDetailsLink()
        {
            // Arrange.
            string drawStatus = "lose";

            _ticket.Status = drawStatus;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectHtml = BuildExpectedDetailsLink(_ticket.TransId, _ticket.CustId);

            Assert.True(actualResult.League.ToString().EndsWith(expectHtml, StringComparison.OrdinalIgnoreCase));
        }

        [TestCase(BetStatusId.Running)]
        [TestCase(BetStatusId.Waiting)]
        [TestCase(BetStatusId.Refund)]
        [TestCase(BetStatusId.Void)]
        [TestCase(BetStatusId.Reject)]
        public void BuildTicketStatus_StatusIdIsNotDrawOrWonOrLose_HideDetailsLink(string statusId)
        {
            // Arrange.
            _ticket.StatusId = Convert.ToInt32(statusId);

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectHtml = BuildExpectedDetailsLink(_ticket.TransId, _ticket.CustId);

            Assert.False(actualResult.League.ToString().EndsWith(expectHtml, StringComparison.OrdinalIgnoreCase));
        }

        [TestCase("running")]
        [TestCase("waiting")]
        [TestCase("refund")]
        [TestCase("void")]
        [TestCase("reject")]
        public void BuildTicketStatus_StatusIsNotDrawOrWonOrLose_HideDetailsLink(string status)
        {
            // Arrange.
            _ticket.Status = status;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectHtml = BuildExpectedDetailsLink(_ticket.TransId, _ticket.CustId);

            Assert.False(actualResult.League.ToString().EndsWith(expectHtml, StringComparison.OrdinalIgnoreCase));
        }

        private string BuildExpectedDetailsLink(long transId, long custId)
        {
            string[] detailsFunctionDivCompositions =
                {
                    $"<div class=\"detail lucky3-detail\" onclick=\"showLucky3Details({transId},{custId});\">",
                    $"<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void('');\">{CoreBetList.details}</a>",
                    "</div>"
                };

            return string.Join(null, detailsFunctionDivCompositions);
        }

        [Test]
        public void BuildLeague_Always_CallGetLeagueNameByIdToGetLeagueName()
        {
            // Arrange.
            string leagueName = "League name";

            _ticketHelper.GetLeagueNameById(Arg.Any<int>()).Returns(leagueName);

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(leagueName, actualResult.League.LeagueName.leagueName);
        }

        [Test]
        public void BuildSport_Always_CallGetSportNameByIdToGetSportName()
        {
            // Arrange.
            string sportName = "Sport name";

            _ticketHelper.GetSportNameById(Arg.Any<int>()).Returns(sportName);

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(sportName, actualResult.League.sportTypeName);
        }

        [Test]
        public void BuildMatch_Always_MatchIsCombinationOfNoAndMatchCode()
        {
            // Arrange.
            var matchCode = "1234aa";

            _ticket.MatchCode = matchCode;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            var expectedMatch = string.Format(Lucky3Labels.Number, matchCode);

            Assert.AreEqual(expectedMatch, actualResult.Match.ToString());
        }

        [Test]
        public void BuildBetType_Always_CallGetBetTypeNameByIdToGetBetTypeName()
        {
            // Arrange.
            string betTypeName = "Bet type name";

            _ticketHelper.GetBetTypeNameById(Arg.Any<int>(), Arg.Any<object>()).Returns(betTypeName);

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(betTypeName, actualResult.BetType.betTypeName);
        }

        [Test]
        public void BuildTicketStatus_StatusIdIsNull_TicketStatusIsEmpty()
        {
            // Arrange.
            _ticket.StatusId = null;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsEmpty(actualResult.ticketStatus);
        }

        [Test]
        public void BuildTicketStatus_StatusIdIsVoid_TicketStatusIsVoid()
        {
            // Arrange.
            int voidStatusId = 102;

            _ticket.StatusId = voidStatusId;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string voidStauts = "void";

            Assert.AreEqual(voidStauts, actualResult.ticketStatus);
        }

        [Test]
        public void BuildTicketStatus_StatusIsNull_TicketStatusIsEmpty()
        {
            // Arrange.
            _ticket.Status = null;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsEmpty(actualResult.ticketStatus);
        }

        [Test]
        public void BuildTicketStatus_StatusIsVoid_TicketStatusIsVoid()
        {
            // Arrange.
            string voidStatus = "void";

            _ticket.Status = voidStatus;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(voidStatus, actualResult.ticketStatus);
        }

        [Test]
        public void BuildTicketStatus_StatusIdIsRefund_TicketStatusIsRefund()
        {
            // Arrange.
            int refundStatusId = 103;

            _ticket.StatusId = refundStatusId;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string refundStatus = "refund";

            Assert.AreEqual(refundStatus, actualResult.ticketStatus);
        }

        [Test]
        public void BuildTicketStatus_StatusIsRefund_TicketStatusIsRefund()
        {
            // Arrange.
            string refundStatus = "refund";
            _ticket.Status = refundStatus;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(refundStatus, actualResult.ticketStatus);
        }

        [Test]
        public void BuildTicketStatus_StatusIdIsReject_TicketStatusIsReject()
        {
            // Arrange.
            int rejectStatusId = 101;

            _ticket.StatusId = rejectStatusId;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string rejectStatus = "reject";

            Assert.AreEqual(rejectStatus, actualResult.ticketStatus);
        }

        [Test]
        public void BuildTicketStatus_StatusIsReject_TicketStatusIsReject()
        {
            // Arrange.
            string rejectStatus = "reject";

            _ticket.Status = rejectStatus;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(rejectStatus, actualResult.ticketStatus);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsLucky3()
        {
            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("lucky3", actualResult.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamIsTransDesc()
        {
            // Arrange.
            _ticket.TransDesc = "01, 02, 03";

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(_ticket.TransDesc, actualResult.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetIdIsOne_HandicapIsSingleFormOfBettingCombinations()
        {
            // Arrange.
            _ticket.BetId = 1;
            _ticket.Currency = "AUD";
            _ticket.Handicap1 = 1.5M;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBettingCombinations = "1 Combination x AUD 1.50";

            Assert.AreEqual(expectedBettingCombinations, actualResult.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetIdIsGreaterThanOne_HandicapIsPluralFormOfBettingCombinationOfOne()
        {
            // Arrange.
            _ticket.BetId = 2;
            _ticket.Currency = "USD";
            _ticket.Handicap1 = 1M;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBettingCombinations = "2 Combinations x USD 1.00";

            Assert.AreEqual(expectedBettingCombinations, actualResult.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_FixHandicap1ToTwoDecimalPlaces()
        {
            // Arrange.
            _ticket.BetId = 2;
            _ticket.Currency = "AUD";
            _ticket.Handicap1 = 1.5123M;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBettingCombinations = $"2 Combinations x AUD 1.51";

            Assert.AreEqual(expectedBettingCombinations, actualResult.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_OddsIsOne_OddsIsOneBet()
        {
            // Arrange.
            _ticket.Odds = 1;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedHtml = $"<div class=\"num-of-bets\">1 Bet</div>";

            Assert.AreEqual(expectedHtml, actualResult.Handicap.Odds.ToString());
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_OddsIsGreaterThanOne_OddsIsPluralFormOfNumberOfBets()
        {
            // Arrange.
            _ticket.Odds = 2;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedHtml = $"<div class=\"num-of-bets\">2 Bets</div>";

            Assert.AreEqual(expectedHtml, actualResult.Handicap.Odds.ToString());
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_TruncateTrailingZerosInTheDecimalPartOfOdds()
        {
            // Arrange.
            _ticket.Odds = 2.0000M;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedHtml = $"<div class=\"num-of-bets\">2 Bets</div>";

            Assert.AreEqual(expectedHtml, actualResult.Handicap.Odds.ToString());
        }
    }
}