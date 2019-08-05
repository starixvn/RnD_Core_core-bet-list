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

        [TestCase("betChoice=1705152116,101;referID=")]
        [TestCase("betChoice=1705152116,102;referID=")]
        [TestCase("betChoice=1705152116,103;referID=")]
        [TestCase("betChoice=1705152116,104;referID=")]
        [TestCase("betChoice=1705152116,105;referID=")]
        [TestCase("betChoice=1705152116,106;referID=")]
        public void Render_ChoiceIdIsInHidingOddsList_ShowHyphenAsOdds(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var template = _odds.Render(_ticket, null, _gettingOddsTypeNameFunc);

            // Assert.
            Assert.AreEqual("-", template.odds);
        }

        [TestCase("betChoice=1705152116,101;referID=")]
        [TestCase("betChoice=1705152116,102;referID=")]
        [TestCase("betChoice=1705152116,103;referID=")]
        [TestCase("betChoice=1705152116,104;referID=")]
        [TestCase("betChoice=1705152116,105;referID=")]
        [TestCase("betChoice=1705152116,106;referID=")]
        public void Render_ChoiceIdIsInHidingOddsList_HideOddsTypeName(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var template = _odds.Render(_ticket, null, _gettingOddsTypeNameFunc);

            // Assert.
            Assert.IsEmpty(template.oddsType);
        }

        [Test]
        public void Render_ChoiceIdIsNotInHidingOddsList_ShowTicketOdds()
        {
            // Arrange.
            const decimal odds = 2;

            _ticket.TransDesc = "betChoice=1705152116,100;referID=";
            _ticket.Odds = odds;

            // Act.
            var template = _odds.Render(_ticket, null, _gettingOddsTypeNameFunc);

            // Assert.
            Assert.AreEqual(odds.ToString(CultureInfo.InvariantCulture), template.odds);
        }
    }
}