namespace Fanex.BetList.BBIN.Tests.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Entities;
    using NUnit.Framework;

    [TestFixture]
    public class Odds3300Tests
    {
        [Test]
        public void Render_Always_HideOdds()
        {
            // Arrange.
            IOdds odds = new Odds3300();
            ITicket ticket = new Ticket
            {
                BetTypeId = 3300,
                OddsType = "1",
                Odds = 0.75M
            };

            // Act.
            var template = odds.Render(ticket, null, id => id.ToString());

            // Assert.
            Assert.AreEqual("-", template.odds);
        }
    }
}
