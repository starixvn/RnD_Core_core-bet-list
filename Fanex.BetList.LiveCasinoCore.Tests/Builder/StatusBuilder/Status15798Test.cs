namespace Fanex.BetList.LiveCasinoCore.Tests.Builder.StatusBuilder
{
    using System;
    using System.Collections.Generic;
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status15798Test
    {
        private Status15798 _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;

        [SetUp]
        public void Setup()
        {
            _status = new Status15798();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = new List<ITicketData>();

            // Setup default ticket properties
            _ticket.TransId = 1;
            _ticket.CustId = 123;
            _ticket.Status = "Won";
            _ticket.BetTypeId = 15798;
            _ticket.MatchId = 100;
            _ticket.SportTypeId = 1;
            _ticket.LeagueId = 10;
            _ticket.UserName = "testuser";
            _ticket.WinlostDate = DateTime.Today;
            _ticket.Race = "1";
            _ticket.IP = "192.168.1.1";
            _ticket.Currency = "USD";
            _ticket.Stake = 100;
            _ticket.Winlost = 50;
            _ticket.ActualStake = 100;
            _ticket.BetdaqId = 0;
            _ticket.RefNo = "REF123";
            _ticket.ExtraInfo = string.Empty;
            _ticket.SettlementStatus = 0;
            _ticket.BetCheck = "detail|&|displayDetail";
            _ticket.MatchCode = "MATCH001";
            _ticket.SiteId = "SITE001";
            _ticket.CurrencyId = 20;
        }

        //[Test]
        //public void ExpireDateResultLink_Always_Returns0()
        //{
        //    // Act & Assert
        //    Assert.AreEqual(0, _status.ExpireDateResultLink);
        //}

        [Test]
        public void BuildStatusResult_WithValidTicketAndBetCheck_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
            Assert.IsTrue(template.StatusResult.betId.Contains("20"));
            Assert.IsTrue(template.StatusResult.betId.Contains("MATCH001"));
            Assert.IsTrue(template.StatusResult.betId.Contains("123"));
            Assert.IsTrue(template.StatusResult.betId.Contains("validBetCheck"));
            Assert.IsTrue(template.StatusResult.betId.Contains("SITE001"));
            Assert.IsTrue(template.StatusResult.betId.Contains("Football"));
        }

        [Test]
        public void BuildStatusResult_WithEmptyBetCheck_HidesStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "";

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsFalse(template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_WithNullBetCheck_HidesStatusResult()
        {
            // Arrange
            _ticket.BetCheck = null;

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsFalse(template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_WithTicketOlderThan0Days_HideStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.WinlostDate = DateTime.Today.AddDays(-1); // Older than 0 days
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsFalse(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithTicketExactly0DaysOld_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.WinlostDate = DateTime.Today; // Exactly 0 days old
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithTicketFromYesterday_HideStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.WinlostDate = DateTime.Today.AddDays(-1);

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsFalse(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithTicketFromLastWeek_HidesStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.WinlostDate = DateTime.Today.AddDays(-7);

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsFalse(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithTicketFromLastMonth_HideStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.WinlostDate = DateTime.Today.AddDays(-30);

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsFalse(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithTicketFromLastYear_HideStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.WinlostDate = DateTime.Today.AddDays(-365);

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsFalse(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithTicketFromToday_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.WinlostDate = DateTime.Today;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentSportType_ShowsStatusResultWithCorrectSportName()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.SportTypeId = 2; // Different sport
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Basketball");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
            Assert.IsTrue(template.StatusResult.betId.Contains("Basketball"));
        }

        [Test]
        public void BuildStatusResult_WithDifferentCurrencyId_ShowsStatusResultWithCorrectCurrencyId()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.CurrencyId = 30; // Different currency
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
            Assert.IsTrue(template.StatusResult.betId.Contains("30"));
        }

        [Test]
        public void BuildStatusResult_WithDifferentMatchCode_ShowsStatusResultWithCorrectMatchCode()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.MatchCode = "DIFFERENT_MATCH";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
            Assert.IsTrue(template.StatusResult.betId.Contains("DIFFERENT_MATCH"));
        }

        [Test]
        public void BuildStatusResult_WithDifferentCustId_ShowsStatusResultWithCorrectCustId()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.CustId = 999; // Different customer ID
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
            Assert.IsTrue(template.StatusResult.betId.Contains("999"));
        }

        [Test]
        public void BuildStatusResult_WithDifferentSiteId_ShowsStatusResultWithCorrectSiteId()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.SiteId = "DIFFERENT_SITE";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
            Assert.IsTrue(template.StatusResult.betId.Contains("DIFFERENT_SITE"));
        }

        [Test]
        public void BuildStatusResult_WithDifferentBetCheck_ShowsStatusResultWithCorrectBetCheck()
        {
            // Arrange
            _ticket.BetCheck = "DIFFERENT_BET_CHECK";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
            Assert.IsTrue(template.StatusResult.betId.Contains("DIFFERENT_BET_CHECK"));
        }

        [Test]
        public void BuildStatusResult_WithNullSportName_ShowsStatusResultWithEmptySportName()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns((string)null);

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
            Assert.IsTrue(template.StatusResult.betId.Contains("''")); // Empty sport name
        }

        [Test]
        public void BuildStatusResult_WithEmptySportName_ShowsStatusResultWithEmptySportName()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
            Assert.IsTrue(template.StatusResult.betId.Contains("''")); // Empty sport name
        }

        [Test]
        public void BuildStatusResult_WithWhitespaceSportName_ShowsStatusResultWithWhitespaceSportName()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("   ");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
            Assert.IsTrue(template.StatusResult.betId.Contains("'   '")); // Whitespace sport name
        }

        [Test]
        public void BuildStatusResult_WithSpecialCharactersInBetCheck_ShowsStatusResultWithEscapedBetCheck()
        {
            // Arrange
            _ticket.BetCheck = "bet_check_with_special_chars!@#$%^&*()";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
            Assert.IsTrue(template.StatusResult.betId.Contains("bet_check_with_special_chars!@#$%^&*()"));
        }

        [Test]
        public void BuildStatusResult_WithLongBetCheck_ShowsStatusResultWithFullBetCheck()
        {
            // Arrange
            _ticket.BetCheck = "very_long_bet_check_string_that_contains_many_characters_to_test_the_behavior_of_the_system_with_extremely_long_input_values";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
            Assert.IsTrue(template.StatusResult.betId.Contains("very_long_bet_check_string_that_contains_many_characters_to_test_the_behavior_of_the_system_with_extremely_long_input_values"));
        }

        [Test]
        public void BuildStatusResult_WithDifferentBetTypeId_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.BetTypeId = 9999; // Different bet type
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithZeroBetTypeId_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.BetTypeId = 0;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithNegativeBetTypeId_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.BetTypeId = -1;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentTransId_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.TransId = 999999;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithZeroTransId_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.TransId = 0;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithNegativeTransId_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.TransId = -1;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithExtremeValues_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.Stake = decimal.MaxValue;
            _ticket.Winlost = decimal.MinValue;
            _ticket.TransId = long.MaxValue;
            _ticket.BetTypeId = int.MaxValue;
            _ticket.CurrencyId = int.MaxValue;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithMinimalValues_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.Stake = decimal.MinValue;
            _ticket.Winlost = decimal.MinValue;
            _ticket.TransId = long.MinValue;
            _ticket.BetTypeId = int.MinValue;
            _ticket.CurrencyId = int.MinValue;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithAllPropertiesChanged_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.TransId = 999;
            _ticket.Stake = 500;
            _ticket.Winlost = 450;
            _ticket.Currency = "JPY";
            _ticket.BetTypeId = 8888;
            _ticket.MatchCode = "DIFFERENT_MATCH";
            _ticket.CustId = 777;
            _ticket.SiteId = "DIFFERENT_SITE";
            _ticket.CurrencyId = 30;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Basketball");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
            Assert.IsTrue(template.StatusResult.betId.Contains("30"));
            Assert.IsTrue(template.StatusResult.betId.Contains("DIFFERENT_MATCH"));
            Assert.IsTrue(template.StatusResult.betId.Contains("777"));
            Assert.IsTrue(template.StatusResult.betId.Contains("validBetCheck"));
            Assert.IsTrue(template.StatusResult.betId.Contains("DIFFERENT_SITE"));
            Assert.IsTrue(template.StatusResult.betId.Contains("Basketball"));
        }

        [Test]
        public void BuildStatusResult_WithMockedTicketHelper_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            var mockTicketHelper = Substitute.For<ITicketHelper>();
            mockTicketHelper.GetSportNameById(_ticket.SportTypeId).Returns("MockedSport");

            // Act
            var template = _status.Render(_ticket, mockTicketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
            Assert.IsTrue(template.StatusResult.betId.Contains("MockedSport"));
        }

        [Test]
        public void BuildStatusResult_WithDifferentStatus_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.Status = "Lose";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentMatchId_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.MatchId = 999;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentLeagueId_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.LeagueId = 999;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentUserName_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.UserName = "differentuser";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentRace_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.Race = "2";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentIP_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.IP = "10.0.0.1";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentCurrency_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.Currency = "EUR";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentStake_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.Stake = 500;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentWinlost_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.Winlost = 200;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentActualStake_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.ActualStake = 450;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentBetdaqId_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.BetdaqId = 1;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentRefNo_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.RefNo = "DIFFERENT_REF";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentExtraInfo_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.ExtraInfo = "<p25 s=\"100\" a=\"100\"/>";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentSettlementStatus_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.SettlementStatus = 1;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }
    }
}