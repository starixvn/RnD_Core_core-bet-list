namespace Fanex.BetList.GoldDeluxe.Tests.Builder.TransBuilder
{
    using Core.Builder.TransBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Trans1701Tests
    {
        [Test]
        public void BuildRefNo_Always_SetRefNoIsRefNo()
        {
            // Arrange.
            var trans = new Trans1701();
            var ticket = Substitute.For<ITicket>();

            ticket.RefNo = "100";

            // Act.
            var template = trans.Render(ticket);

            // Assert.
            Assert.AreEqual(ticket.RefNo, template.TransTime.refNo);
        }
    }
}