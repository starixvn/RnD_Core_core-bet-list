namespace Fanex.BetList.VirtualGames.Tests.Builder.TransBuilder
{
    using Fanex.BetList.Core.Builder.TransBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.UnitTest.Common;
    using NUnit.Framework;

    public class Trans4601Tests
    {
        [Test]
        public void Render_Always_RefNoIsTicketRefNo()
        {
            // Arrange.
            const string refNo = "1010";

            var trans = new Trans4601();
            var ticket = TestData.CreateTicket(VirtualGamesBetTypes.Baccarat);

            ticket.RefNo = refNo;

            // Act.
            var template = trans.Render(ticket);

            // Assert.
            Assert.AreEqual(refNo, template.TransTime.refNo);
        }
    }
}