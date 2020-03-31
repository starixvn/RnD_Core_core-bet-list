namespace Fanex.BetList.TableGames.Tests.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status4901Tests
    {
        [Test]
        public void BuildStatusResult_Always_HideResultLink()
        {
            // Arrange.
            IStatus status = new Status4901();
            ITicket ticket = Substitute.For<ITicket>();
            ITicketHelper ticketHelper = Substitute.For<ITicketHelper>();

            // Act.
            status.Render(ticket, ticketHelper, null, false);

            // Assert.
            Assert.IsFalse(status.Template.StatusResult.Visible);
        }
    }
}
