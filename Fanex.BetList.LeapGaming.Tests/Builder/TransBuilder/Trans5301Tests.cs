namespace Fanex.BetList.LeapGaming.Tests.Builder.TransBuilder
{
    using Core.Builder.TransBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Trans5301Tests
    {
        [Test]
        public void BuildRefNo_Always_SetRefNoIsRefNo()
        {
            // Arrange.
            var trans = new Trans5301();
            var ticket = Substitute.For<ITicket>();

            ticket.RefNo = "100";

            // Act.
            var template = trans.Render(ticket);

            // Assert.
            Assert.AreEqual(ticket.RefNo, template.TransTime.refNo);
        }
    }
}