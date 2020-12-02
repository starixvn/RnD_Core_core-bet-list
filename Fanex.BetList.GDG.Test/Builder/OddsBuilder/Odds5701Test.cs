namespace Fanax.BetList.GDG.Test.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NUnit.Framework;

    public class Odds5701Test
    {
        private const decimal Odds = 0.75M;

        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds5701();
            _ticket = new Ticket
            {
                BetTypeId = GDGBetTypes.Baccarat,
                OddsType = "1",
                Odds = Odds
            };
        }

        [Test]
        public void Render_Alway_SetOddIsHyphen()
        {
            // Arrange.

            // Act.
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert.
            Assert.AreEqual("-", template.odds);
        }        
    }
}