namespace Fanex.BetList.VirtualSports.Tests.Builder.OddsBuilder
{
    using Core.Builder.OddsBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Odds2799Tests
    {
        private IOdds _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds2799();
            _ticket = Substitute.For<ITicket>();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(101)]
        [TestCase(102)]
        [TestCase(103)]
        [TestCase(113)]
        public void Render_StatusIdIsNotWonOrDraw_OddsIsHyphen(int statusId)
        {
            // Arrange.
            _ticket.StatusId = statusId;

            // Act.
            var builtOdds = _odds.Render(_ticket, null, Substitute.For<GetCachePropertyById>());

            // Assert.
            Assert.AreEqual("-", builtOdds.odds);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(101)]
        [TestCase(102)]
        [TestCase(103)]
        [TestCase(113)]
        public void Render_StatusIdIsNotWonOrDraw_OddsTypeIsEmpty(int statusId)
        {
            // Arrange.
            _ticket.StatusId = statusId;

            // Act.
            var builtOdds = _odds.Render(_ticket, null, Substitute.For<GetCachePropertyById>());

            // Assert.
            Assert.AreEqual(string.Empty, builtOdds.oddsType);
        }

        [TestCase("running")]
        [TestCase("waiting")]
        [TestCase("reject")]
        [TestCase("void")]
        [TestCase("refund")]
        [TestCase("lose")]
        public void Render_StatusIsNotWonOrDraw_OddsIsHyphen(string status)
        {
            // Arrange.
            _ticket.Status = status;

            // Act.
            var builtOdds = _odds.Render(_ticket, null, Substitute.For<GetCachePropertyById>());

            // Assert.
            Assert.AreEqual("-", builtOdds.odds);
        }

        [TestCase("running")]
        [TestCase("waiting")]
        [TestCase("reject")]
        [TestCase("void")]
        [TestCase("refund")]
        [TestCase("lose")]
        public void Render_StatusIsNotWonOrDraw_OddsTypeIsEmpty(string status)
        {
            // Arrange.
            _ticket.Status = status;

            // Act.
            var builtOdds = _odds.Render(_ticket, null, Substitute.For<GetCachePropertyById>());

            // Assert.
            Assert.AreEqual(string.Empty, builtOdds.oddsType);
        }

        [Test]
        public void Render_StatusIdIsWon_OddsIsShownWithFourDecimalDigits()
        {
            // Arrange.
            _ticket.StatusId = 112;
            _ticket.MasterOdds = 1;

            // Act.
            var builtOdds = _odds.Render(_ticket, null, Substitute.For<GetCachePropertyById>());

            // Assert.
            Assert.AreEqual("1.0000", builtOdds.odds);
        }

        [Test]
        public void Render_StatusIsWonOrDraw_OddsIsShownWithFourDecimalDigits()
        {
            // Arrange.
            _ticket.Status = "won";
            _ticket.MasterOdds = 1;

            // Act.
            var builtOdds = _odds.Render(_ticket, null, Substitute.For<GetCachePropertyById>());

            // Assert.
            Assert.AreEqual("1.0000", builtOdds.odds);
        }
    }
}