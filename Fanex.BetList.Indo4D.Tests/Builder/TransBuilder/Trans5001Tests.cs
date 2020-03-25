namespace Fanex.BetList.VirtualGames.Tests.Builder.TransBuilder
{
    using Fanex.BetList.Core.Builder.TransBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.UnitTest.Common;
    using NUnit.Framework;

    public class Trans5001Tests
    {
        [Test]
        public void Render_Always_RefNoIsTicketRefNo()
        {
            // Arrange.
            const string refNo = "881010";

            var trans = new Trans5001();
            var ticket = TestData.CreateTicket(Indo4DBetTypes.FourD);

            ticket.RefNo = refNo;

            // Act.
            var template = trans.Render(ticket);

            // Assert.
            Assert.AreEqual(refNo, template.TransTime.refNo);
        }
    }
}
