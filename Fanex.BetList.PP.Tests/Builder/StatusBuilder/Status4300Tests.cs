namespace Fanex.BetList.PP.Tests.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status4300Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status4300();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.Draw)]
        public void BuildStatusResult_StatusIsWon_HideResultLink(string status)
        {
            // Arrange.
            _ticket.Status = status;

            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.False(template.StatusResult.Visible);
        }
    }
}