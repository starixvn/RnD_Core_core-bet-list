namespace Fanex.BetList.TableGames.Tests.Builder.TransBuilder
{
    using Fanex.BetList.Core.Builder.TransBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Trans4901Tests
    {
        [Test]
        public void Render_Always_RefNoIsTicketRefNo()
        {
            // Arrange.
            var trans = new Trans4901();
            var ticket = Substitute.For<ITicket>();

            ticket.RefNo = "100";

            // Act.
            var template = trans.Render(ticket);

            // Assert.
            Assert.AreEqual(ticket.RefNo, template.TransTime.refNo);
        }
    }
}
