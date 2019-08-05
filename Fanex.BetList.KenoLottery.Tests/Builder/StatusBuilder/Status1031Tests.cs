namespace Fanex.BetList.KenoLottery.Tests.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status1031Tests
    {
        [Test]
        public void BuildStatusResult_Always_HideResultLink()
        {
            // Arrange.
            IStatus status = new Status1031();
            ITicket ticket = Substitute.For<ITicket>();
            ITicketHelper ticketHelper = Substitute.For<ITicketHelper>();

            // Act.
            status.Render(ticket, ticketHelper, null, false);

            // Assert.
            Assert.IsFalse(status.Template.StatusResult.Visible);
        }
    }
}