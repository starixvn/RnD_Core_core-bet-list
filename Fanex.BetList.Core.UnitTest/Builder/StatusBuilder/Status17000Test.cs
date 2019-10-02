namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Status17000Test
    {
        [Test]
        public void Render_Always_HideResultLink()
        {
            // Arrange.
            var ticket = TestData.CreateTicket(BetTypes.SabaLuckySpin);
            var ticketHelper = Substitute.For<ITicketHelper>();
            var status = new Status17000();

            // Act.
            var template = status.Render(ticket, ticketHelper, null, false);

            // Assert.
            Assert.IsFalse(template.StatusResult.Visible);
        }
    }
}