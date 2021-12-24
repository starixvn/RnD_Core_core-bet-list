namespace Fanex.BetList.BBIN.Tests.Builder.StatusBuilder
{
    using System;
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status3300Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void Setup()
        {
            _status = new Status3300();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_TrandescDataIsEmpty_HideStatusResultLink()
        {
            // Arrange.
            _ticket.TransDesc = string.Empty;

            // Act.
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_TrandescDataIsNotEmpty_ShowStatusResultLink()
        {
            // Arrange.
            _ticket.WinlostDate = DateTime.Today;
            _ticket.TransDesc = "gamename=gameid3rd";
            _ticket.CurrencyId = 72;
            _ticket.CustId = 123;
            _ticket.BetCheck = "transid3rd";
            _ticket.SiteId = "siteid";
            // Act.
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("72, 'gameid3rd', 123, 'transid3rd', 'siteid'", _status.Template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_Expired30Day_HideStatusResultLink()
        {
            // Arrange.
            _ticket.WinlostDate = DateTime.Today.AddDays(-30);
            _ticket.TransDesc = "gamename=gameid3rd";
            _ticket.CurrencyId = 72;
            _ticket.CustId = 123;
            _ticket.BetCheck = "transid3rd";
            _ticket.SiteId = "siteid";
            // Act.
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }
    }
}
