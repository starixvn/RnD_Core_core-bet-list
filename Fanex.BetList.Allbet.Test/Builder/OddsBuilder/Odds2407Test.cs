namespace Fanax.BetList.Allbet.Test.Builder.OddsBuilder
{
    using System.Globalization;
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NUnit.Framework;

    public class Odds2407Test
    {
        private const decimal Odds = 0.75M;

        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds2407();
            _ticket = new Ticket
            {
                BetTypeId = AllbetBetTypes.BullBull,
                OddsType = "1",
                Odds = Odds
            };
        }

        [TestCase("choice=8011")]
        [TestCase("Choice=8012*TableCode=C103")]
        [TestCase("CHOICE=8013*TableCode=C103*site=7333333")]
        [TestCase("cHoice=8111*TableCode=C103*site=7333333*comms=100")]
        [TestCase("choice=8012*TableCode=C103*site=7333333*comms=100*enterType=106")]
        [TestCase("CHOICE=8013*TableCode=C103*site=7333333")]
        public void Render_ChoiceIsDoubleBet_HideOdds(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert.
            Assert.AreEqual("-", template.odds);
        }

        [TestCase("choice=8011")]
        [TestCase("Choice=8012*TableCode=C103")]
        [TestCase("CHOICE=8013*TableCode=C103*site=7333333")]
        [TestCase("cHoice=8111*TableCode=C103*site=7333333*comms=100")]
        [TestCase("choice=8012*TableCode=C103*site=7333333*comms=100*enterType=106")]
        [TestCase("CHOICE=8013*TableCode=C103*site=7333333")]
        public void Render_ChoiceIsDoubleBet_HideOddsType(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert.
            Assert.IsEmpty(template.oddsType);
        }

        [TestCase("choice=8001")]
        [TestCase("Choice=8002*TableCode=C103")]
        [TestCase("CHOICE=8003*TableCode=C103*site=7333333")]
        [TestCase("cHoice=8101*TableCode=C103*site=7333333*comms=100")]
        [TestCase("choice=8002*TableCode=C103*site=7333333*comms=100*enterType=106")]
        [TestCase("CHOICE=8003*TableCode=C103*site=7333333")]
        public void Render_ChoiceIsNotDoubleBet_ShowOdds(string transDesc)
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