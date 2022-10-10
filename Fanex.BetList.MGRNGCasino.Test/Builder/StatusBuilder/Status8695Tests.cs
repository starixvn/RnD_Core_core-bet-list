namespace Fanex.BetList.MGRNGCasino.Tests.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status8695Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status8695();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_Always_HideResultLink()
        {
            // Arrange.

            // Act.
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.False(template.StatusResult.Visible);
        }
    }
}