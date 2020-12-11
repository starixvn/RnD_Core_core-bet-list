namespace Fanax.BetList.BG.Test.Builder.OddsBuilder
{
    using System.Globalization;
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NUnit.Framework;

    public class Odds3508Test
    {
        private const decimal Odds = 0.75M;

        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds3508();
            _ticket = new Ticket
            {
                BetTypeId = BGBetTypes.Baccarat,
                OddsType = "1",
                Odds = Odds
            };
        }

        [TestCase("choice=3489660930_2_1", "-")]
        [TestCase("choice=3489660936_2_2", "-")]
        [TestCase("choice=3489660960_2_3", "-")]
        public void Render_ChoiceIsSpecialChoice_HideOdds(string transDesc, string expectedOdds)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert.
            Assert.AreEqual(expectedOdds, template.odds);
        }

        [TestCase("choice=8001")]
        [TestCase("Choice=8002*TableCode=C103")]
        [TestCase("CHOICE=8003*TableCode=C103*site=7333333")]
        [TestCase("cHoice=8101*TableCode=C103*site=7333333*comms=100")]
        [TestCase("choice=8002*TableCode=C103*site=7333333*comms=100*enterType=106")]
        [TestCase("CHOICE=8003*TableCode=C103*site=7333333")]
        public void Render_ChoiceIsNotNormalChoice_ShowOdds(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert.
            var expectedDisplayedOdds = Odds.ToString(CultureInfo.InvariantCulture);

            Assert.AreEqual(expectedDisplayedOdds, template.odds);
        }
    }
}