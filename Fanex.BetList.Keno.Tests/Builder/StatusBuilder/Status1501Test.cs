namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status1501Test
    {
        [Test]
        public void BuildStatusResult_Always_HideStatusLink()
        {
            // Arrange.
            IStatus status = new Status1501();
            ITicket ticket = Substitute.For<ITicket>();
            ITicketHelper ticketHelper = Substitute.For<ITicketHelper>();

            // Act.
            status.Render(ticket, ticketHelper, null, false);

            // Assert.
            Assert.IsFalse(status.Template.StatusResult.Visible);
        }
    }
}