namespace Fanax.BetList.Allbet.Test.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NUnit.Framework;
    using System.Globalization;

    public class Odds2404Test
    {
        private const decimal Odds = 0.75M;

        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds2404();
            _ticket = new Ticket
            {
                BetTypeId = AllbetBetTypes.SicboHiLo,
                OddsType = "1",
                Odds = Odds
            };
        }

        [TestCase("Choice=3048*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3049*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3050*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3051*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3052*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3053*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3203*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3204*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3205*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3206*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3207*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3208*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3234*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3235*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3236*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3237*comms=100*site=733333*tableCode=C001*enterType=106")]
        public void Render_ChoiceIsSpecialBet_HideOdds(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert.
            Assert.AreEqual("-", template.odds);
        }

        [TestCase("Choice=3200*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3201*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3202*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3209*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3210*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3211*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3212*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3213*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3214*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3215*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3216*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3217*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3218*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3219*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3220*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3221*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3222*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3223*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3224*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3225*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3226*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3227*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3228*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3229*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3230*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3231*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3232*comms=100*site=733333*tableCode=C001*enterType=106")]
        [TestCase("Choice=3233*comms=100*site=733333*tableCode=C001*enterType=106")]
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