namespace Fanex.BetList.RNGCore.Tests.Builder.StatusBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status12150Tests
    {
        private Status12150 _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;

        [SetUp]
        public void Setup()
        {
            _status = new Status12150();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = new List<ITicketData>();

            // Setup default ticket properties
            _ticket.TransId = 1;
            _ticket.CustId = 123;
            _ticket.Status = BetStatus.Won;
            _ticket.BetTypeId = 12150;
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
            _ticket.BetTeam = "Test Team";
            _ticket.SiteId = "SITE001";
            _ticket.CurrencyId = 20;
        }

        [Test]
        public void ExpireDateResultLink_Always_Returns90()
        {
            // Act & Assert
            Assert.AreEqual(90, _status.ExpireDateResultLink);
        }

        [Test]
        public void BuildStatusResult_WithValidTicket_ShowsStatusResult()
        {
            // Arrange
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(template.StatusResult.betId);
            StringAssert.Contains(_ticket.BetTeam, template.StatusResult.betId);
            StringAssert.Contains(_ticket.CustId.ToString(), template.StatusResult.betId);
            StringAssert.Contains(_ticket.BetCheck, template.StatusResult.betId);
            StringAssert.Contains(_ticket.SiteId, template.StatusResult.betId);
            StringAssert.Contains("Football", template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithNullBetCheck_HidesStatusResult()
        {
            // Arrange
            _ticket.BetCheck = null;

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_WithEmptyBetCheck_HidesStatusResult()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_WithExpiredTicket_HidesStatusResult()
        {
            // Arrange
            _ticket.WinlostDate = DateTime.Today.AddDays(-91); // More than 90 days ago

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_WithRecentTicket_ShowsStatusResult()
        {
            // Arrange
            _ticket.WinlostDate = DateTime.Today.AddDays(-30); // Within 90 days
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Basketball");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(template.StatusResult.betId);
            StringAssert.Contains("Basketball", template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithExactExpireDate_ShowsStatusResult()
        {
            // Arrange
            _ticket.WinlostDate = DateTime.Today.AddDays(-90); // Exactly 90 days ago
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Tennis");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(template.StatusResult.betId);
            StringAssert.Contains("Tennis", template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithNullSportName_HandlesGracefully()
        {
            // Arrange
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns((string)null);

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(template.StatusResult.betId);
            StringAssert.Contains("''", template.StatusResult.betId); // Empty sport name
        }

        [Test]
        public void BuildStatusResult_WithEmptySportName_HandlesGracefully()
        {
            // Arrange
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns(string.Empty);

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(template.StatusResult.betId);
            StringAssert.Contains("''", template.StatusResult.betId); // Empty sport name
        }

        [Test]
        public void BuildStatusResult_WithDifferentBetTeam_ShowsCorrectBetTeam()
        {
            // Arrange
            _ticket.BetTeam = "Another Team";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("Another Team", template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentSiteId_ShowsCorrectSiteId()
        {
            // Arrange
            _ticket.SiteId = "SITE002";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("SITE002", template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentBetCheck_ShowsCorrectBetCheck()
        {
            // Arrange
            _ticket.BetCheck = "custom|&|check";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("custom|&|check", template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithDifferentCustId_ShowsCorrectCustId()
        {
            // Arrange
            _ticket.CustId = 999;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("999", template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithSpecialCharactersInBetTeam_HandlesCorrectly()
        {
            // Arrange
            _ticket.BetTeam = "Team & Co. (Ltd.)";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("Team & Co. (Ltd.)", template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithSpecialCharactersInBetCheck_HandlesCorrectly()
        {
            // Arrange
            _ticket.BetCheck = "detail|&|display|$|123";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("detail|&|display|$|123", template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithLongBetTeam_HandlesCorrectly()
        {
            // Arrange
            _ticket.BetTeam = "Very Long Team Name That Exceeds Normal Length For Testing Purposes";
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            StringAssert.Contains("Very Long Team Name That Exceeds Normal Length For Testing Purposes", template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_WithZeroValues_HandlesCorrectly()
        {
            // Arrange
            _ticket.CustId = 0;
            _ticket.Stake = 0;
            _ticket.Winlost = 0;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Football");

            // Act
            var template = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(template.StatusResult.betId);
            StringAssert.Contains("0", template.StatusResult.betId);
        }
    }
}
