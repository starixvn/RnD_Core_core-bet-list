namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using System;
    using Core.Builder.StatusBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status1Test
    {
        private Status1 _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status1();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("", "")]
        [TestCase("running", "Running")]
        [TestCase("reject", "Reject")]
        [TestCase("won", "Won")]
        [TestCase("lose", "Lose")]
        [TestCase("draw", "Draw")]
        [TestCase("waiting", "Waiting")]
        [TestCase("void", "Void")]
        [TestCase("refund", "Refund")]
        public void BuildResult_Status_Result(string status, string expectedResult)
        {
            // Arrange
            _ticket.Status = status;

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(expectedResult, _status.Template.result);
        }

        [TestCase("", "Cash Out")]
        [TestCase("running", "Cash Out")]
        [TestCase("reject", "Cash Out")]
        [TestCase("won", "Cash Out")]
        [TestCase("lose", "Cash Out")]
        [TestCase("draw", "Cash Out")]
        [TestCase("waiting", "Cash Out")]
        [TestCase("void", "Void")]
        [TestCase("refund", "Refund")]
        public void BuildResult_IsCashOutAllTicket_Result(string status, string expectedResult)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.Stake = 0;
            _ticket.BetdaqId = 0;
            _ticket.ExtraInfo = "<p25 />";

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(expectedResult, _status.Template.result);
        }

        [TestCase("running")]
        [TestCase("waiting")]
        [TestCase("void")]
        [TestCase("reject")]
        [TestCase("refund")]
        public void BuildStatusResult_Status_StatusResultVisibleIsFalse(string status)
        {
            // Arrange
            _ticket.Status = status;

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsFalse(_status.Template.StatusResult.Visible);
        }

        [TestCase("")]
        [TestCase("won")]
        [TestCase("lose")]
        [TestCase("draw")]
        public void BuildStatusResult_Status_MatchIdIsMatchId(string status)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.MatchId = 100;

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_ticket.MatchId.ToString(), _status.Template.StatusResult.matchId);
        }

        [TestCase("")]
        [TestCase("won")]
        [TestCase("lose")]
        [TestCase("draw")]
        public void BuildStatusResult_Status_BetTypeIsBetTypeId(string status)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.BetTypeId = 1;

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_ticket.BetTypeId.ToString(), _status.Template.StatusResult.betType);
        }

        [TestCase("")]
        [TestCase("won")]
        [TestCase("lose")]
        [TestCase("draw")]
        public void BuildStatusResult_StatusAndRaceIsEmpty_RaceIsZero(string status)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.Race = string.Empty;

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string RACE = "0";
            Assert.AreEqual(RACE, _status.Template.StatusResult.race);
        }

        [TestCase("")]
        [TestCase("won")]
        [TestCase("lose")]
        [TestCase("draw")]
        public void BuildStatusResult_Status_RaceIsRace(string status)
        {
            // Arrange
            const string RACE = "10";
            _ticket.Status = status;
            _ticket.Race = RACE;

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(RACE, _status.Template.StatusResult.race);
        }

        [TestCase("")]
        [TestCase("won")]
        [TestCase("lose")]
        [TestCase("draw")]
        public void BuildStatusResult_Status_RefNoIsTransId(string status)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.TransId = 100;

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_ticket.TransId.ToString(), _status.Template.StatusResult.refNo);
        }

        [TestCase("")]
        [TestCase("won")]
        [TestCase("lose")]
        [TestCase("draw")]
        public void BuildStatusResult_Status_SportTypeIsSportTypeId(string status)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.SportTypeId = 1;

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_ticket.SportTypeId.ToString(), _status.Template.StatusResult.sportType);
        }

        [TestCase("")]
        [TestCase("won")]
        [TestCase("lose")]
        [TestCase("draw")]
        public void BuildStatusResult_Status_LeagueIsLeagueId(string status)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.LeagueId = 1;

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_ticket.LeagueId.ToString(), _status.Template.StatusResult.league);
        }

        [TestCase("")]
        [TestCase("won")]
        [TestCase("lose")]
        [TestCase("draw")]
        public void BuildStatusResult_Status_UsernameIsUsername(string status)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.UserName = "username";

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_ticket.UserName, _status.Template.StatusResult.userName);
        }

        [TestCase("")]
        [TestCase("won")]
        [TestCase("lose")]
        [TestCase("draw")]
        public void BuildStatusResult_Status_WinlostDateIsWinlostDate(string status)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.WinlostDate = new DateTime(2012, 12, 24, 12, 12, 12);

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string WINLOST_DATE = "12/24/2012 12:12:12 PM";
            Assert.AreEqual(WINLOST_DATE, _status.Template.StatusResult.winlostDate);
        }

        [Test]
        public void BuildIP_IsShowVNIPIsFalseAndIsVnIp_ShowIPBlockIsVisible()
        {
            // Arrange
            _ticket.IP = "vn-ip";
            _ticketHelper.IsVnIp("vn-ip").Returns(true);

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsFalse(_status.Template.ShowIP.Visible);
        }

        [Test]
        public void BuildIP_IsShowVNIPIsFalseAndNotVnIp_BetIpIsIP()
        {
            // Arrange
            _ticket.IP = "not vn ip";

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_ticket.IP, _status.Template.ShowIP.betIp);
        }

        [Test]
        public void BuildIP_IsShowVNIPIsTrue_BetIpIsIP()
        {
            // Arrange
            _ticket.IP = "sample ip";

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(_ticket.IP, _status.Template.ShowIP.betIp);
        }

        [TestCase(-1, "<p25 s=\"1000\" a=\"1000\"/>", true)]
        [TestCase(-1, "", true)]
        [TestCase(0, "", true)]
        [TestCase(0, "<p25 s=\"1000\" a=\"1000\"/>", true)]
        [TestCase(-1, "", false)]
        [TestCase(0, "", false)]
        public void BuildCashOutHistory_WhenHaveNotEnoughCondition_HideCashOutHistory(long betdaqId, string extraInfo, bool hideHistory)
        {
            // Arrange
            _ticketHelper.HideStatusCashOutHistory.Returns(hideHistory);
            ArrangeCashOutTicket(betdaqId, extraInfo);

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(_status.Template.CashOutHistory.Visible);
        }

        [TestCase(901)]
        [TestCase(902)]
        public void BuildCashOutHistory_BetTypeIsBetTradeAndBetdaqIdIsOne_HideCashOutHistory(int betTypeId)
        {
            // Arrange
            _ticket.BetTypeId = betTypeId;
            _ticketHelper.HideStatusCashOutHistory.Returns(false);
            ArrangeCashOutTicket(1, string.Empty);

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(_status.Template.CashOutHistory.Visible);
        }

        [TestCase(-1, "<p25 s=\"1000\" a=\"1000\"/>")]
        [TestCase(0, "<p25 s=\"1000\" a=\"1000\"/>")]
        [TestCase(1, "<p25 s=\"1000\" a=\"1000\"/>")]
        [TestCase(1, "")]
        public void BuildCashOutHistory_WhenHaveEnoughCondition_ShowCashOutHistory(long betdaqId, string extraInfo)
        {
            // Arrange
            ArrangeCashOutTicket(betdaqId, extraInfo);
            _ticketHelper.HideStatusCashOutHistory.Returns(false);

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(_status.Template.CashOutHistory.Visible);
        }

        [Test]
        public void BuildCashOutHistory_WhenShowHistory_TransIdIsTransId()
        {
            // Arrange
            ArrangeCashOutTicket(0, "<p25 s=\"1000\" a=\"1000\"/>");
            _ticketHelper.HideStatusCashOutHistory.Returns(false);
            _ticket.TransId = 100;

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(_ticket.TransId.ToString(), _status.Template.CashOutHistory.transId);
        }

        [Test]
        public void BuildCashOutHistory_WhenShowHistory_RefNoIsRefNo()
        {
            // Arrange
            ArrangeCashOutTicket(0, "<p25 s=\"1000\" a=\"1000\"/>");
            _ticketHelper.HideStatusCashOutHistory.Returns(false);
            _ticket.RefNo = "100";

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(_ticket.RefNo, _status.Template.CashOutHistory.refNo);
        }

        [Test]
        public void BuildCashOutHistory_WhenShowHistory_BetTypeIsBetType()
        {
            // Arrange
            ArrangeCashOutTicket(0, "<p25 s=\"1000\" a=\"1000\"/>");
            _ticketHelper.HideStatusCashOutHistory.Returns(false);
            _ticket.BetTypeId = 1;

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(_ticket.BetTypeId.ToString(), _status.Template.CashOutHistory.betType);
        }

        [Test]
        public void BuildCashOutHistory_WhenShowHistory_BetdaqIdIsBetdaqId()
        {
            // Arrange
            ArrangeCashOutTicket(0, "<p25 s=\"1000\" a=\"1000\"/>");
            _ticketHelper.HideStatusCashOutHistory.Returns(false);
            _ticket.BetdaqId = 1;

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(_ticket.BetdaqId.ToString(), _status.Template.CashOutHistory.betdaqId);
        }

        private void ArrangeCashOutTicket(long betdaqId, string extraInfo, decimal originalStake = 0)
        {
            _ticket.BetdaqId = betdaqId;
            _ticket.ExtraInfo = extraInfo;
            _ticket.OriginalStake = originalStake;
        }
    }
}