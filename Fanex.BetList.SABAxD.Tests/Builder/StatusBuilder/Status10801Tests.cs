namespace Fanex.BetList.VGaming.Tests.Builder.StatusBuilder
{
    using System;
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status10801Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void Setup()
        {
            _status = new Status10801();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_BetCheckIsEmpty_HideResultLink()
        {
            // Arrange
            _ticket.WinlostDate = DateTime.Today.AddDays(-11);

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_MoreThan10Day_HideResultLink()
        {
            // Arrange
            _ticket.BetCheck = "betcheck";
            _ticket.WinlostDate = DateTime.Today.AddDays(-11);

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_TicketToday_ShowResultLink()
        {
            // Arrange
            _ticket.BetCheck = "betcheck";
            _ticket.WinlostDate = DateTime.Today;
            _ticket.Status = "Won";

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_SettleTicket_ShowResultLink()
        {
            // Arrange
            _ticket.BetCheck = "betcheck";
            _ticket.WinlostDate = DateTime.Today;
            _ticket.Status = "Won";

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_RefundNewTicket_ShowResultLink()
        {
            // Arrange
            _ticket.BetCheck = "betcheck";
            _ticket.WinlostDate = DateTime.Today.AddDays(-1);
            _ticket.Status = "refund";
            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(!string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_RefundOldTicket_HideResultLink()
        {
            // Arrange
            _ticket.BetCheck = "betcheck";
            _ticket.WinlostDate = DateTime.Today.AddDays(-11);
            _ticket.Status = "refund";
            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }
    }
}
