using Fanex.BetList.Core.Builder.OddsBuilder;
using Fanex.BetList.Core.Constants;
using Fanex.BetList.Core.Entities;
using NUnit.Framework;

namespace Fanax.BetList.Allbet.Test.Builder.OddBuilder
{
    public class Odds3409Test
    {
        private const decimal Odds = 0.75M;

        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds3409();
            _ticket = new Ticket
            {
                BetTypeId = WMBetTypes.Baccarat,
                OddsType = "1",
                Odds = Odds
            };
        }


        [Test]
        public void Render_SummaryTicket_HideOdds()
        {
            // Arrange
            _ticket.TransDesc = string.Empty;

            // Act
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert
            Assert.AreEqual("-", template.odds);
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [TestCase("choice=18026*site=IBC*tableCode=301")]
        [TestCase("choice=18027*site=IBC*tableCode=301")]
        [TestCase("choice=18028*site=IBC*tableCode=301")]
        [TestCase("choice=18029*site=IBC*tableCode=301")]
        [TestCase("choice=18030*site=IBC*tableCode=301")]
        [TestCase("choice=18031*site=IBC*tableCode=301")]
        public void Render_BetChoiceHideOdds_HideOdds(string transdesc)
        {
            // Arrange
            _ticket.TransDesc = transdesc;

            // Act
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert
            Assert.AreEqual("-", template.odds);
            Assert.AreEqual(string.Empty, template.oddsType);
        }

        [Test]
        public void Render_BetChoiceShowOdds_ShowOdds()
        {
            // Arrange
            _ticket.TransDesc = "choice=1001*site=IBC*tableCode=301";

            // Act
            var template = _odds.Render(_ticket, null, id => id.ToString());

            // Assert
            Assert.IsTrue(template.odds != "-");
            Assert.IsTrue(template.oddsType != "");
        }
    }
}
