namespace Fanex.BetList.KenoLottery.Tests.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status1031Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status1031();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_Always_RefNoInTheLinkIsTicketRefNo()
        {
            // Arrange.
            _ticket.TransId = 12345;
            _ticket.RefNo = "34567";

            // Act.
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual("34567", _status.Template.StatusResult.refNo);
        }

        [Test]
        public void BuildStatusResult_Always_BetIdInTheLinkIsTheCombinationOfBetIdAndCustId()
        {
            // Arrange.
            _ticket.BetId = 12345;
            _ticket.CustId = 34567;

            // Act.
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual("12345, 34567", _status.Template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusLink_TicketBetIdIsNull_BetIdInTheLinkContainsZeroAsBetId()
        {
            // Arrange.
            _ticket.BetId = null;
            _ticket.CustId = 34567;

            // Act.
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual("0, 34567", _status.Template.StatusResult.betId);
        }
    }
}