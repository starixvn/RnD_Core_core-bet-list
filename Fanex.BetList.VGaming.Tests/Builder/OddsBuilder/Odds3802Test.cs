namespace Fanex.BetList.VGaming.Tests.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NUnit.Framework;

    public class Odds3802Test
    {
        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds3802();
            _ticket = new Ticket
            {
                BetTypeId = VGamingRange1BetTypes.MinRange,
                OddsType = "1",
                Odds = 0.75M
            };
        }


        [Test]
        public void Render_Always_ShowHyphenIcon()
        {
            // Arrange

            // Act
            var act = _odds.Render(_ticket, null, id => id.ToString());

            // Assert
            Assert.AreEqual("-", act.odds);
            Assert.IsTrue(string.IsNullOrEmpty(act.oddsType));
        }
    }
}
