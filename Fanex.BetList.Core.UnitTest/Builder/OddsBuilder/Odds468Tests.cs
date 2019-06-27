namespace Fanex.BetList.Core.UnitTest.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using System.Globalization;
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using Fanex.BetList.Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    public class Odds468Tests
    {
        private IOdds _odds;
        private ITicket _ticket;
        private List<ITicketData> _ticketData;
        private GetCachePropertyById _getOddsTypeNameFunc;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds468();
            _ticket = TestData.CreateTicket(BetTypes.HalfTimeFullTimeExactTotalGoals);
            _ticketData = Substitute.For<List<ITicketData>>();
            _getOddsTypeNameFunc = Substitute.For<GetCachePropertyById>();
        }

        [Test]
        public void Render_Always_HideOddsTypeName()
        {
            // Act.
            var oddsTemplate = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert.
            Assert.IsEmpty(oddsTemplate.oddsType);
        }

        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.Draw)]
        public void Render_SettledTicketAndOddsIsNegative_ShowOddsValueInRed(string status)
        {
            // Arrange
            _ticket.Status = status;

            // Act.
            var oddsTemplate = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert.
            var expectedOddsValueFormat = string.Format(ConvertByBetType.Minus, _ticket.Odds);

            Assert.AreEqual(expectedOddsValueFormat, oddsTemplate.odds);
        }

        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.Draw)]
        public void Render_SettledTicketAnOddsIsPositive_ShowOddsValueInBlack(string status)
        {
            // Arrange
            const decimal odds = 5.25M;

            _ticket.Status = status;
            _ticket.Odds = odds;

            // Act.
            var oddsTemplate = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert.
            Assert.AreEqual(odds.ToString(CultureInfo.InvariantCulture), oddsTemplate.odds);
        }

        [TestCase(BetStatus.Running)]
        [TestCase(BetStatus.Waiting)]
        [TestCase(BetStatus.Refund)]
        [TestCase(BetStatus.Void)]
        [TestCase(BetStatus.Reject)]
        public void Render_UnsettledTicket_ShowHyphenAsOddsValue(string status)
        {
            // Arrange
            _ticket.Status = status;

            // Act.
            var oddsTemplate = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert.
            Assert.AreEqual("-", oddsTemplate.odds);
        }
    }
}