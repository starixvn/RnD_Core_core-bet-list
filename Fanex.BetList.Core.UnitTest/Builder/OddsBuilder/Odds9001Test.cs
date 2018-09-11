namespace Fanex.BetList.Core.UnitTest.Builder.OddsBuilder
{
    using Core.Builder.OddsBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Odds9001Test
    {
        private IOdds _odds;
        private ITicket _ticket;
        private GetCachePropertyById _getOddsTypeNameFunc;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds9001();
            _ticket = Substitute.For<ITicket>();
            _getOddsTypeNameFunc = oddsTypeId => oddsTypeId.ToString();
        }

        [Test]
        public void Render_Always_CallPassedFuncGetOddsTypeNameToGetOddsTypeName()
        {
            // Arrange.
            var getOddsTypeNameFunc = Substitute.For<GetCachePropertyById>();

            _ticket.OddsType = "1";

            // Act.
            _odds.Render(_ticket, null, getOddsTypeNameFunc);

            // Assert.
            getOddsTypeNameFunc.Received().Invoke(Arg.Is(_ticket.OddsType));
        }

        [Test]
        public void Render_DecimalOdds_AddOneToOddsValue()
        {
            // Arrange.
            _ticket.OddsType = "1";
            _ticket.Odds = 2;

            // Act.
            var template = _odds.Render(_ticket, null, _getOddsTypeNameFunc);

            // Assert.
            const string expectedDisplayingOdds = "3";

            Assert.AreEqual(expectedDisplayingOdds, template.odds);
        }

        [Test]
        public void Render_USOdds_MultiplyOddsWith100()
        {
            // Arrange.
            _ticket.OddsType = "5";
            _ticket.Odds = 0.2M;

            // Act.
            var template = _odds.Render(_ticket, null, _getOddsTypeNameFunc);

            // Assert.
            const string expectedDisplayingOdds = "20";

            Assert.AreEqual(expectedDisplayingOdds, template.odds);
        }

        [TestCase("2")]
        [TestCase("3")]
        [TestCase("4")]
        public void Render_AnyOtherOddsType_ShowOriginalOdds(string oddsType)
        {
            // Arrange.
            _ticket.OddsType = oddsType;
            _ticket.Odds = 3;

            // Act.
            var template = _odds.Render(_ticket, null, _getOddsTypeNameFunc);

            // Assert.
            const string expectedDisplayingOdds = "3";

            Assert.AreEqual(expectedDisplayingOdds, template.odds);
        }

        [Test]
        public void Render_OddsIsZero_ShowHyphenAsOddsValue()
        {
            // Arrange.
            _ticket.Odds = 0;

            // Act.
            var template = _odds.Render(_ticket, null, _getOddsTypeNameFunc);

            // Assert.
            const string expectedDisplayingOdds = "-";

            Assert.AreEqual(expectedDisplayingOdds, template.odds);
        }

        [Test]
        public void Render_NegativeOdds_ShowOddsValueInRed()
        {
            // Arrange.
            _ticket.Odds = -2.5M;

            // Act.
            var template = _odds.Render(_ticket, null, _getOddsTypeNameFunc);

            // Assert.
            string expectedDisplayingOdds = "<font color='#B50000'>-2.5</font>";

            Assert.AreEqual(expectedDisplayingOdds, template.odds);
        }
    }
}