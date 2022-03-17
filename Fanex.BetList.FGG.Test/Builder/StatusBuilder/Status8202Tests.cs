namespace Fanex.BetList.FGG.Test.Builder.StatusBuilder
{
    using System;
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status8202Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void Setup()
        {
            _status = new Status8202();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_TrandescDataIsNotEmpty_ShowStatusResultLink()
        {
            ////Arrange
            _ticket.BetId = 1;
            _ticket.CustId = 123;
            _ticket.CurrencyId = 1;
            _ticket.BetCheck = "betcheck";
            _ticket.BetTeam = "team";
            _ticket.EventDate = DateTime.Today;
            _ticket.SiteId = "site";
            _ticket.MatchCode = "MatchCode";

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual("1, 'team', 123, 'betcheck', 'site'", _status.Template.StatusResult.betId);
        }
    }
}
