namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using System;
    using Core.App_GlobalResources;
    using Core.Builder.StatusBuilder;
    using Core.Constants;
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
        [TestCase(BetStatus.Running, "Running")]
        [TestCase(BetStatus.Reject, "Reject")]
        [TestCase(BetStatus.Won, "Won")]
        [TestCase(BetStatus.Lose, "Lose")]
        [TestCase(BetStatus.Draw, "Draw")]
        [TestCase(BetStatus.Waiting, "Waiting")]
        [TestCase(BetStatus.Void, "Void")]
        [TestCase(BetStatus.Refund, "Refund")]
        [TestCase(BetStatus.HalfWon, "Half Won")]
        [TestCase(BetStatus.HalfLose, "Half Lose")]
        public void BuildResult_Status_Result(string status, string expectedResult)
        {
            // Arrange
            _ticket.Status = status;

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedResult, _status.Template.result);
        }

        [TestCase(0, "Won")]
        [TestCase(1, "Delayed Payout")]        
        public void BuildResult_WonTicket_ShowExpectedResult(byte settlementStatus, string expectedResult)
        {
            // Arrange
            _ticket.Status = "Won";
            _ticket.SettlementStatus = settlementStatus;

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedResult, _status.Template.result);
        }

        [TestCase(0, "Lose")]
        [TestCase(1, "Delayed Payout")]
        public void BuildResult_LoseTicket_ShowExpectedResult(byte settlementStatus, string expectedResult)
        {
            // Arrange
            _ticket.Status = "Lose";
            _ticket.SettlementStatus = settlementStatus;

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedResult, _status.Template.result);
        }

        [TestCase(0, "Draw")]
        [TestCase(1, "Delayed Payout")]
        public void BuildResult_DrawTicket_ShowExpectedResult(byte settlementStatus, string expectedResult)
        {
            // Arrange
            _ticket.Status = "Draw";
            _ticket.SettlementStatus = settlementStatus;

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedResult, _status.Template.result);
        }

        [TestCase("", "Cash Out")]
        [TestCase(BetStatus.Running, "Cash Out")]
        [TestCase(BetStatus.Reject, "Cash Out")]
        [TestCase(BetStatus.Won, "Cash Out")]
        [TestCase(BetStatus.Lose, "Cash Out")]
        [TestCase(BetStatus.Draw, "Cash Out")]
        [TestCase(BetStatus.Waiting, "Cash Out")]
        [TestCase(BetStatus.Void, "Void")]
        [TestCase(BetStatus.Refund, "Refund")]
        [TestCase(BetStatus.HalfWon, "Cash Out")]
        [TestCase(BetStatus.HalfLose, "Cash Out")]
        public void BuildResult_IsCashOutAllTicket_Result(string status, string expectedResult)
        {
            // Arrange
            _ticket.OriginalBetType = 0;
            _ticket.Status = status;
            _ticket.Stake = 0;
            _ticket.BetdaqId = 0;
            _ticket.ExtraInfo = "<p25 s=\"999\" a=\"1000\"/>";

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedResult, _status.Template.result);
        }

        [TestCase("", "")]
        [TestCase(BetStatus.Running, "Running")]
        [TestCase(BetStatus.Reject, "Reject")]
        [TestCase(BetStatus.Won, "Won")]
        [TestCase(BetStatus.Lose, "Lose")]
        [TestCase(BetStatus.Draw, "Draw")]
        [TestCase(BetStatus.Waiting, "Waiting")]
        [TestCase(BetStatus.Void, "Void")]
        [TestCase(BetStatus.Refund, "Refund")]
        [TestCase(BetStatus.HalfWon, "Half Won")]
        [TestCase(BetStatus.HalfLose, "Half Lose")]
        public void BuildResult_IsNotCashOutAllTicket_Result(string status, string expectedResult)
        {
            // Arrange
            _ticket.OriginalBetType = -1;
            _ticket.Status = status;
            _ticket.Stake = 0;
            _ticket.BetdaqId = 0;
            _ticket.ExtraInfo = "<p25 s=\"999\" a=\"1000\"/>";

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(expectedResult, _status.Template.result);
        }

        [TestCase("", "")]
        [TestCase(BetStatus.Running, "Running")]
        [TestCase(BetStatus.Reject, "Reject")]
        [TestCase(BetStatus.Won, "Won")]
        [TestCase(BetStatus.Lose, "Lose")]
        [TestCase(BetStatus.Draw, "Draw")]
        [TestCase(BetStatus.Waiting, "Waiting")]
        [TestCase(BetStatus.Void, "Void")]
        [TestCase(BetStatus.Refund, "Refund")]
        [TestCase(BetStatus.HalfWon, "Half Won")]
        [TestCase(BetStatus.HalfLose, "Half Lose")]
        public void BuildResult_IsNotCashOutTicket_Result(string status, string expectedResult)
        {
            // Arrange
            _ticket.OriginalBetType = 1;
            _ticket.Status = status;
            _ticket.Stake = 0;
            _ticket.BetdaqId = 0;
            _ticket.ExtraInfo = "<p25 sss=\"999\" a=\"1000\"/>";

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(expectedResult, _status.Template.result);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(13)]
        [TestCase(15)]
        public void BuildResult_RunningTicketWithPendingSettlementStatus_ShowDelayPayout(byte settlementStatus)
        {
            // Arrange
            _ticket.Status = BetStatus.Running;
            _ticket.SettlementStatus = settlementStatus;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(CoreBetList.DelayPayout, _status.Template.result);
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(12)]
        [TestCase(20)]
        public void BuildResult_RunningTicketWithUnderSettlementStatus_ShowUnderSettlement(byte settlementStatus)
        {
            // Arrange
            _ticket.Status = BetStatus.Running;
            _ticket.SettlementStatus = settlementStatus;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(CoreBetList.UnderSettlement, _status.Template.result);
        }

#pragma warning disable S4144 // Methods should not have identical implementations

        [TestCase(8)]
        [TestCase(10)]
        [TestCase(24)]
        [TestCase(26)]
        public void BuildResult_RunningTicketWithSystemSettlementStatus_ShowUnderSettlement(byte settlementStatus)
        {
            // Arrange
            _ticket.Status = BetStatus.Running;
            _ticket.SettlementStatus = settlementStatus;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(CoreBetList.UnderSettlement, _status.Template.result);
        }

#pragma warning restore S4144 // Methods should not have identical implementations

        [Test]
        public void BuildResult_RunningTicketWithUndecidedSettlementStatus_ShowRunning()
        {
            // Arrange
            _ticket.Status = BetStatus.Running;
            _ticket.SettlementStatus = 2;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(CoreBetList.running, _status.Template.result);
        }

        [Test]
        public void BuildResult_RunningTicketWithUnderSettlementStatusBeingZero_ShowRunning()
        {
            // Arrange
            _ticket.Status = BetStatus.Running;
            _ticket.SettlementStatus = 0;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(CoreBetList.running, _status.Template.result);
        }

        [TestCase("<p1 ab=\"1\" />")]
        public void BuildResult_IsAbnormalBetTicket_ShowAbnormalBet(string extraInfo)
        {
            // Arrange
            _ticket.Status = BetStatus.Void;
            _ticket.ExtraInfo = extraInfo;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(CoreBetList.AbnormalBet, _status.Template.result);
        }

        [TestCase(BetStatus.Void, "<p1 ab=\"2\" />")]
        [TestCase(BetStatus.Void, "<p2 ab=\"2\" />")]
        [TestCase(BetStatus.Void, "<p1 aab=\"2\" />")]
        [TestCase(BetStatus.Void, "")]
        [TestCase(BetStatus.Void, null)]
        public void BuildResult_IsNotAbnormalBetTicket_ShowAsNormalTicket(string status, string extraInfo)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.ExtraInfo = extraInfo;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            StringAssert.AreEqualIgnoringCase(status, _status.Template.result);
        }

        [TestCase(BetStatus.Running)]
        [TestCase(BetStatus.Waiting)]
        [TestCase(BetStatus.Void)]
        [TestCase(BetStatus.Reject)]
        public void BuildStatusResult_UnsettledTicket_HideResultLink(string status)
        {
            // Arrange
            _ticket.Status = status;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.IsFalse(_status.Template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_SportsbookRefundedTicket_ShowResultLink()
        {
            // Arrange
            _ticket.SportTypeId = 1;
            _ticket.Status = BetStatus.Refund;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.IsTrue(_status.Template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_NonSportsbookRefundedTicket_ShowResultLink()
        {
            // Arrange
            _ticket.SportTypeId = 100;
            _ticket.Status = BetStatus.Refund;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.IsFalse(_status.Template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_ParlayRefundedTicket_ShowResultLink()
        {
            // Arrange
            _ticket.BetTypeId = BetTypes.SystemParlay;
            _ticket.Status = BetStatus.Refund;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.IsFalse(_status.Template.StatusResult.Visible);
        }

        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.HalfWon)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.HalfLose)]
        [TestCase(BetStatus.Draw)]
        public void BuildStatusResult_SettledTicket_HideResultLink(string status)
        {
            // Arrange
            _ticket.Status = status;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.IsTrue(_status.Template.StatusResult.Visible);
        }

        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.HalfWon)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.HalfLose)]
        [TestCase(BetStatus.Draw)]
        public void BuildStatusResult_Status_MatchIdIsMatchId(string status)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.MatchId = 100;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(_ticket.MatchId.ToString(), _status.Template.StatusResult.matchId);
        }

        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.HalfWon)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.HalfLose)]
        [TestCase(BetStatus.Draw)]
        public void BuildStatusResult_Status_BetTypeIsBetTypeId(string status)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.BetTypeId = 1;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(_ticket.BetTypeId.ToString(), _status.Template.StatusResult.betType);
        }

        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.HalfWon)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.HalfLose)]
        [TestCase(BetStatus.Draw)]
        public void BuildStatusResult_StatusAndRaceIsEmpty_RaceIsZero(string status)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.Race = string.Empty;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            const string RACE = "0";
            Assert.AreEqual(RACE, _status.Template.StatusResult.race);
        }

        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.HalfWon)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.HalfLose)]
        [TestCase(BetStatus.Draw)]
        public void BuildStatusResult_Status_RaceIsRace(string status)
        {
            // Arrange
            const string race = "10";

            _ticket.Status = status;
            _ticket.Race = race;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(race, _status.Template.StatusResult.race);
        }

        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.HalfWon)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.HalfLose)]
        [TestCase(BetStatus.Draw)]
        public void BuildStatusResult_Status_RefNoIsTransId(string status)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.TransId = 100;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(_ticket.TransId.ToString(), _status.Template.StatusResult.refNo);
        }

        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.HalfWon)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.HalfLose)]
        [TestCase(BetStatus.Draw)]
        public void BuildStatusResult_Status_SportTypeIsSportTypeId(string status)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.SportTypeId = 1;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(_ticket.SportTypeId.ToString(), _status.Template.StatusResult.sportType);
        }

        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.HalfWon)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.HalfLose)]
        [TestCase(BetStatus.Draw)]
        public void BuildStatusResult_Status_LeagueIsLeagueId(string status)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.LeagueId = 1;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(_ticket.LeagueId.ToString(), _status.Template.StatusResult.league);
        }

        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.HalfWon)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.HalfLose)]
        [TestCase(BetStatus.Draw)]
        public void BuildStatusResult_Status_UsernameIsUsername(string status)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.UserName = "username";

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(_ticket.UserName, _status.Template.StatusResult.userName);
        }

        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.HalfWon)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.HalfLose)]
        [TestCase(BetStatus.Draw)]
        public void BuildStatusResult_Status_WinlostDateIsWinlostDate(string status)
        {
            // Arrange
            _ticket.Status = status;
            _ticket.WinlostDate = new DateTime(2012, 12, 24, 12, 12, 12);

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

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
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(_status.Template.ShowIP.Visible);
        }

        [Test]
        public void BuildIP_IsShowVNIPIsFalseAndNotVnIp_BetIpIsIP()
        {
            // Arrange
            _ticket.IP = "not vn ip";

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

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
        [TestCase(-1, "<p25 sss=\"1000\" a=\"1000\"/>", false)]
        [TestCase(0, "<p25 sss=\"1000\" a=\"1000\"/>", false)]
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

        [TestCase(BetTypes.Sold)]
        [TestCase(BetTypes.Bought)]
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

        [TestCase(100)]
        [TestCase(200)]
        public void BuildCashOutHistory_SportTypeIsNotSportsbook_HideCashOutHistory(int sportTypeId)
        {
            // Arrange
            _ticket.SportTypeId = sportTypeId;
            _ticketHelper.HideStatusCashOutHistory.Returns(false);
            ArrangeCashOutTicket(1, string.Empty);

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(_status.Template.CashOutHistory.Visible);
        }

        [TestCase(-1, "<p25 s=\"1000\" a=\"1000\"/><p25 s=\"1000\" a=\"1000\"/>", 99)]
        [TestCase(0, "<p25 s=\"1000\" a=\"1000\"/>", 154)]
        [TestCase(1, "<p25 s=\"1000\" a=\"1000\"/>", 100)]
        [TestCase(1, "", 1)]
        public void BuildCashOutHistory_WhenHaveEnoughCondition_ShowCashOutHistory(long betdaqId, string extraInfo, int sportTypeId)
        {
            // Arrange
            _ticket.SportTypeId = sportTypeId;
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

        [Test]
        public void BuildStatusResult_NewCashOut_HideResultLink()
        {
            // Arrange
            _ticket.BetdaqId = -2;

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.False(_status.Template.StatusResult.Visible);
        }

        [Test]
        public void BuildResult_NewCashOut_ShowCashOut()
        {
            // Arrange
            _ticket.BetdaqId = -2;

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.True(_status.Template.NewCashOut.Visible);
        }

        [Test]
        public void BuildResult_IsNotNewCashOut_HideCashOut()
        {
            // Arrange
            _ticket.BetdaqId = -1;

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.False(_status.Template.NewCashOut.Visible);
        }

        private void ArrangeCashOutTicket(long betdaqId, string extraInfo, decimal originalStake = 0)
        {
            _ticket.BetdaqId = betdaqId;
            _ticket.ExtraInfo = extraInfo;
            _ticket.OriginalStake = originalStake;
        }
    }
}