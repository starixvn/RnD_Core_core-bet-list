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
    public class StatusJackpotTests
    {
        private StatusJackpot _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;

        [SetUp]
        public void Setup()
        {
            _status = new StatusJackpot();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = new List<ITicketData>();

            // Setup default ticket properties
            _ticket.TransId = 1;
            _ticket.CustId = 123;
            _ticket.Status = BetStatus.Won;
            _ticket.BetTypeId = 1201;
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
        }

        [Test]
        public void BuildStatusResult_Always_HidesStatusResult()
        {
            // Act
            _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_WithSettledTicket_HidesStatusResult()
        {
            // Arrange
            _ticket.Status = BetStatus.Won;

            // Act
            _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_WithRunningTicket_HidesStatusResult()
        {
            // Arrange
            _ticket.Status = BetStatus.Running;

            // Act
            _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_WithWaitingTicket_HidesStatusResult()
        {
            // Arrange
            _ticket.Status = BetStatus.Waiting;

            // Act
            _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_WithLostTicket_HidesStatusResult()
        {
            // Arrange
            _ticket.Status = BetStatus.Lose;

            // Act
            _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_WithVoidTicket_HidesStatusResult()
        {
            // Arrange
            _ticket.Status = BetStatus.Void;

            // Act
            _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_WithRefundTicket_HidesStatusResult()
        {
            // Arrange
            _ticket.Status = BetStatus.Refund;

            // Act
            _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_WithHalfWonTicket_HidesStatusResult()
        {
            // Arrange
            _ticket.Status = BetStatus.HalfWon;

            // Act
            _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_WithHalfLoseTicket_HidesStatusResult()
        {
            // Arrange
            _ticket.Status = BetStatus.HalfLose;

            // Act
            _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_WithDrawTicket_HidesStatusResult()
        {
            // Arrange
            _ticket.Status = BetStatus.Draw;

            // Act
            _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_WithRejectTicket_HidesStatusResult()
        {
            // Arrange
            _ticket.Status = BetStatus.Reject;

            // Act
            _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_WithEmptyStatus_HidesStatusResult()
        {
            // Arrange
            _ticket.Status = string.Empty;

            // Act
            _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildResult_WithEmptyStatus_SetsResultToEmpty()
        {
            // Arrange
            _ticket.Status = string.Empty;

            // Act
            _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(string.Empty, _status.Template.result);
        }

        [Test]
        public void BuildIP_WithVNIPAndIsShowVNIPFalse_HidesIP()
        {
            // Arrange
            _ticket.IP = "192.168.1.1";
            _ticketHelper.IsVnIp = (ip) => true;

            // Act
            _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.ShowIP.ToString()));
        }

        [Test]
        public void BuildIP_WithVNIPAndIsShowVNIPTrue_ShowsIP()
        {
            // Arrange
            _ticket.IP = "192.168.1.1";
            _ticketHelper.IsVnIp = (ip) => true;

            // Act
            _status.Render(_ticket, _ticketHelper, _ticketData, true);

            // Assert
            Assert.AreEqual("192.168.1.1", _status.Template.ShowIP.betIp);
        }

        [Test]
        public void BuildIP_WithNonVNIP_ShowsIP()
        {
            // Arrange
            _ticket.IP = "8.8.8.8";
            _ticketHelper.IsVnIp = (ip) => false;

            // Act
            _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual("8.8.8.8", _status.Template.ShowIP.betIp);
        }

        [Test]
        public void BuildIP_WithNullIsVnIp_ShowsIP()
        {
            // Arrange
            _ticket.IP = "192.168.1.1";
            _ticketHelper.IsVnIp = null;

            // Act
            _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual("192.168.1.1", _status.Template.ShowIP.betIp);
        }

        [Test]
        public void Render_ReturnsStatusTemplate()
        {
            // Act
            var result = _status.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_status.Template, result);
        }       
    }
} 