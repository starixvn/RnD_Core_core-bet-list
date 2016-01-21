namespace Fanex.BetList.Allbet.Test.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status2401Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void Setup()
        {
            _status = new Status2401();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_TrandescDataIsEmpty_HideStatusResultLink()
        {
            _ticket.TransDesc = string.Empty;

            _status.Render(_ticket, _ticketHelper, null, false);

            Assert.IsNullOrEmpty(_status.Template.StatusResult.ToString());
        }

        [Test]
        public void BuildStatusResult_TrandescDataIsNotEmpty_ShowStatusResultLink()
        {
            _ticket.BetId = 1;
            _ticket.BetCheck = "BetCheck";
            _ticket.CustId = 123;
            _ticket.TransDesc = "TranDesc";

            _status.Render(_ticket, _ticketHelper, null, false);

            Assert.AreEqual("1,'BetCheck',123", _status.Template.StatusResult.betId);
        }
    }
}
