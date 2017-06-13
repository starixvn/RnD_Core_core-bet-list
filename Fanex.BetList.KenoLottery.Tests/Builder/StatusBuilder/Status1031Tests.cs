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
        public void BuildStatusLink_BetIdIsNull_ParamBetIdInTheLinkIsZero()
        {
            // Arrange.
            _ticket.BetId = null;

            // Act.
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual("0", _status.Template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_BetIdIsAnyNotNullValue_ParamBetIdInTheLinkIsBetId()
        {
            // Arrange.
            _ticket.BetId = 12345;

            // Act.
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual("12345", _status.Template.StatusResult.betId);
        }
    }
}