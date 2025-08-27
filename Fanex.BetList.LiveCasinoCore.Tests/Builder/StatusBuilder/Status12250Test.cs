namespace Fanex.BetList.LiveCasinoCore.Tests.Builder.StatusBuilder
{
    using System;
    using System.Collections.Generic;
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status12250Test
    {
        private Status12250 _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;

        [SetUp]
        public void Setup()
        {
            _status = new Status12250();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = new List<ITicketData>();

            // Setup default ticket properties
            _ticket.TransId = 1;
            _ticket.CustId = 123;
            _ticket.Status = "Won";
            _ticket.BetTypeId = 12250;
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

        [Test]
        public void ExpireDateResultLink_Always_Returns60()
        {
            // Act & Assert
            Assert.AreEqual(60, _status.ExpireDateResultLink);
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
        public void BuildStatusResult_WithTicketOlderThan60Days_HidesStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.WinlostDate = DateTime.Today.AddDays(-61); // Older than 60 days
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsFalse(template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_WithTicketExactly60DaysOld_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.WinlostDate = DateTime.Today.AddDays(-60); // Exactly 60 days old
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
            Assert.IsNotNull(template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithTicketNewerThan60Days_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.WinlostDate = DateTime.Today.AddDays(-59); // Newer than 60 days
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(template.StatusResult.Visible);
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
        public void BuildStatusResult_WithTicketFromYesterday_ShowsStatusResult()
        {
            // Arrange
            _ticket.BetCheck = "validBetCheck";
            _ticket.WinlostDate = DateTime.Today.AddDays(-1);
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
    }
}
