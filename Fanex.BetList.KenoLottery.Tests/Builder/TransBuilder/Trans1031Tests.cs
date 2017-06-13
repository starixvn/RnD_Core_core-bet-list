namespace Fanex.BetList.KenoLottery.Tests.Builder.TransBuilder
{
    using Core.Builder.TransBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Trans1031Tests
    {
        [Test]
        public void Render_Always_RefNoIsTicketRefNo()
        {
            // Arrange.
            var trans = new Trans1031();
            var ticket = Substitute.For<ITicket>();

            ticket.RefNo = "100";

            // Act.
            var template = trans.Render(ticket);

            // Assert.
            Assert.AreEqual(ticket.RefNo, template.TransTime.refNo);
        }
    }
}