namespace Fanex.BetList.AGCasino.Tests.Builder.StatusBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status1801Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status1801();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_TransDescIsEmpty_RemoveStatusLink()
        {
            _ticket.TransDesc = string.Empty;

            _status.Render(_ticket, _ticketHelper, null, false);

            Assert.IsNullOrEmpty(_status.Template.StatusResult.ToString());
        }

        [Test]
        public void BuildStatusResult_TransDescIsNotEmpty_ReturnResultLink()
        {
            _ticket.BetId = 1;
            _ticket.BetCheck = "BetCheck";
            _ticket.CustId = 111;
            _ticket.TransDesc = "TransDesc";

            _status.Render(_ticket, _ticketHelper, null, false);

            Assert.AreEqual("1,'BetCheck',111", _status.Template.StatusResult.betId);
        }
    }
}
