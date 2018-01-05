namespace Fanex.BetList.Bullfighting.Tests.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status1004Tests
    {
        [Test]
        public void BuildStatusResult_Always_RefNoPassedToViewResultIsTicketRefNo()
        {
            // Arrange.
            IStatus status = new Status1004();
            ITicket ticket = Substitute.For<ITicket>();
            ITicketHelper ticketHelper = Substitute.For<ITicketHelper>();

            ticket.RefNo = "123";

            // Act.
            var template = status.Render(ticket, ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(ticket.RefNo, template.StatusResult.refNo);
        }
    }
}