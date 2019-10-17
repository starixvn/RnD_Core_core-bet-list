namespace Fanex.BetList.ColossusBets.Tests.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status18000Tests
    {
        [Test]
        public void BuildStatusResult_Always_HideStatusLink()
        {
            // Arrange.
            var status = new Status18000();
            var ticket = Substitute.For<ITicket>();
            var ticketHelper = Substitute.For<ITicketHelper>();

            // Act.
            status.Render(ticket, ticketHelper, null, false);

            // Assert.
            Assert.IsFalse(status.Template.StatusResult.Visible);
        }
    }
}