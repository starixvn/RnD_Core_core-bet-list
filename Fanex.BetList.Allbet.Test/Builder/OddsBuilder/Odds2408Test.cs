namespace Fanax.BetList.Allbet.Test.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NUnit.Framework;
    using System.Globalization;

    public class Odds2408Test
    {
        private const decimal Odds = 0.75M;

        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds2408();
            _ticket = new Ticket
            {
                BetTypeId = AllbetBetTypes.PokDeng,
                OddsType = "1",
                Odds = Odds
            };
        }

        [TestCase("choice=5001")]
        [TestCase("Choice=5002*TableCode=C103")]
        [TestCase("CHOICE=5003*TableCode=C103*site=7333333")]
        [TestCase("cHoice=5004*TableCode=C103*site=7333333*comms=100")]
        [TestCase("choice=5005*TableCode=C103*site=7333333*comms=100*enterType=106")]
        public void Render_ChoiceIsSpecialBet_HideOdds(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert.
            Assert.AreEqual("-", template.odds);
        }

        [TestCase("choice=5001")]
        [TestCase("Choice=5002*TableCode=C103")]
        [TestCase("CHOICE=5003*TableCode=C103*site=7333333")]
        [TestCase("cHoice=5004*TableCode=C103*site=7333333*comms=100")]
        [TestCase("choice=5005*TableCode=C103*site=7333333*comms=100*enterType=106")]
        public void Render_ChoiceIsSpecialBet_HideOddsType(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert.
            Assert.IsEmpty(template.oddsType);
        }

        [TestCase("choice=5011")]
        [TestCase("Choice=5012*TableCode=C103")]
        [TestCase("CHOICE=5013*TableCode=C103*site=7333333")]
        [TestCase("cHoice=5014*TableCode=C103*site=7333333*comms=100")]
        [TestCase("choice=5015*TableCode=C103*site=7333333*comms=100*enterType=106")]
        public void Render_ChoiceIsNotSpecialBet_ShowOdds(string transDesc)
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