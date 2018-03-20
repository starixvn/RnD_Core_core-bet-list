namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status223Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void SetUp()
        {
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _status = new Status223();
        }

        [Test]
        public void BuildStatusResult_Always_SetBetIdWithHandicap1()
        {
            // Arrange.
            _ticket.Handicap1 = 128;

            // Act.
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            string expectedBetId = $"{_ticket.Handicap1}";

            Assert.AreEqual(expectedBetId, _status.Template.StatusResult.betId);
        }
    }
}