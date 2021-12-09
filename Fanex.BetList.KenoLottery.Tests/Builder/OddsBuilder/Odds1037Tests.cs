namespace Fanex.BetList.KenoLottery.Tests.Builder.OddsBuilder
{
    using System.Globalization;
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Odds1037Tests
    {
        private IOdds _odds;
        private ITicket _ticket;
        private GetCachePropertyById _gettingOddsTypeNameFunc;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds1037();
            _ticket = Substitute.For<ITicket>();
            _gettingOddsTypeNameFunc = Substitute.For<GetCachePropertyById>();
        }

        [TestCase(BetStatus.Running)]
        public void Render_RunningTicket_ShowHyphenAsOdds(string status)
        {
            // Arrange.
            _ticket.Status = status;

            // Act.
            var template = _odds.Render(_ticket, null, _gettingOddsTypeNameFunc);

            // Assert.
            Assert.AreEqual("-", template.odds);
        }

        [TestCase(null)]
        [TestCase(BetStatus.Won)]
        [TestCase(BetStatus.HalfWon)]
        [TestCase(BetStatus.Draw)]
        [TestCase(BetStatus.Lose)]
        [TestCase(BetStatus.HalfLose)]
        [TestCase(BetStatus.Void)]
        [TestCase(BetStatus.Refund)]
        [TestCase(BetStatus.Waiting)]
        [TestCase(BetStatus.Pending)]
        [TestCase(BetStatus.Reject)]
        public void Render_NotRunningTicket_ShowTicketOdds(string status)
        {
            // Arrange.
            const decimal odds = 2;
            _ticket.Status = status;
            _ticket.Odds = odds;

            // Act.
            var template = _odds.Render(_ticket, null, _gettingOddsTypeNameFunc);

            // Assert.
            Assert.AreEqual(odds.ToString(CultureInfo.InvariantCulture), template.odds);
        }
    }
}