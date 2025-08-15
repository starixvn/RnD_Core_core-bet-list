namespace Fanex.BetList.Core.UnitTest.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Odds29Test
    {
        private Odds29 _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds29();
            _ticket = Substitute.For<ITicket>();
        }

        [Test]
        public void Render_ParlayBoost_OddsIsStrikethrough()
        {
            // Arange
            _ticket.StatusId = 1;
            _ticket.OddsSpread = 2;
            _ticket.BetTeam = "1";            

            // Action
            var template = _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            // Assert
            Assert.IsTrue(template.oddsClassName == " strikethrough");
        }

        //[TestCase(3.2, "3.2")]
        [TestCase(0, "-")]
        //[TestCase(-1.2, "<font class='minus-value'>-1.2</font>")]
        //[TestCase(3.23456, "3.2346")]
        public void Render_FromOdds_OddsIsOdds(decimal odds, string expectedOdds)
        {
            ////Arrange
            _ticket.Odds = odds;
            _ticket.BetTeam = "1";

            ////Act
            var template = _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            ////Assert
            var actualOdds = template.odds;
            Assert.AreEqual(expectedOdds, actualOdds);
        }

        [Test]
        public void Render_NormalParlay_OddsTypeIsEmpty()
        {
            ////Act
            var template = _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            ////Assert
            var oddsType = template.oddsType;
            Assert.AreEqual(string.Empty, oddsType);
        }

        [Test]
        public void Render_TicketBoostIsRunning_BoostRateGetFromExtraInfo()
        {
            ////Arrange
            _ticket.StatusId = 1;
            _ticket.OddsSpread = 2;
            _ticket.BetTeam = "1";
            _ticket.ExtraInfo = "<p1 bbp=\"0.1\" />";
            var boostRateHtml = "<span class='boostrate'><span class='icon-boost'></span>+10% Boost</span>";

            ///Action
            var template = _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            ///Assert
            Assert.IsTrue(template.oddsType.ToString().Contains(boostRateHtml));
        }

        //[Test]
        //public void Render_TicketBoostIsWon_BoostRateGetFromTransDesc()
        //{
        //    ////Arrange
        //    _ticket.StatusId = 112;
        //    _ticket.OddsSpread = 2;
        //    _ticket.BetTeam = "1";
        //    _ticket.ExtraInfo = "<p1 bbp=\"0.1\" />";
        //    _ticket.TransDesc = "0.2";
        //    var boostRateHtml = "<span class='boostrate'><span class='icon-boost'></span>+20% Boost</span>";

        //    ///Action
        //    var template = _odds.Render(_ticket, null, GetCachePropertyByIdStub);

        //    ///Assert
        //    Assert.IsTrue(template.oddsType.ToString().Contains(boostRateHtml));
        //}

        //[Test]
        //public void Render_TicketBoostIsRunning_CalculateBoostOddsOnCore()
        //{
        //    ////Arrange
        //    _ticket.StatusId = 1;
        //    _ticket.Odds = 2;
        //    _ticket.MasterOdds = 0.1m;
        //    _ticket.OddsSpread = 2;
        //    _ticket.BetTeam = "1";            
        //    var boostOddsHtml = "<span class='underdog'>2.1</span><br/>";

        //    ///Action
        //    var template = _odds.Render(_ticket, null, GetCachePropertyByIdStub);

        //    ///Assert
        //    Assert.IsTrue(template.oddsType.ToString().Contains(boostOddsHtml));
        //}

        //[Test]
        //public void Render_TicketBoostIsWon_BoostOddsGetFromMasterOdds()
        //{
        //    ////Arrange
        //    _ticket.StatusId = 112;
        //    _ticket.OddsSpread = 2;
        //    _ticket.MasterOdds = 1.23443m;
        //    _ticket.BetTeam = "1";
        //    var boostOddsHtml = "<span class='underdog'>1.2344</span><br/>";

        //    ///Action
        //    var template = _odds.Render(_ticket, null, GetCachePropertyByIdStub);

        //    ///Assert
        //    Assert.IsTrue(template.oddsType.ToString().Contains(boostOddsHtml));
        //}

        //[Test]
        //public void RenderOddsValue_NormalParlay_ReturnOdds()
        //{
        //    ////Arrange
        //    _ticket.OddsSpread = 1;
        //    _ticket.Odds = 12.3345667m;
        //    _ticket.BetTeam = "1";

        //    ///Action
        //    var oddsValue = _odds.RenderOddsValue(_ticket, null, GetCachePropertyByIdStub);

        //    ///Assert
        //    Assert.AreEqual($"<div class=\"underdog\">12.3346</div>", oddsValue);
        //}

        [Test]
        public void RenderOddsValue_SystemParlay_ReturnDashOdds()
        {
            ////Arrange
            _ticket.OddsSpread = 1;
            _ticket.Odds = 12.3345667m;
            _ticket.BetTeam = "0";
            
            ///Action
            var oddsValue = _odds.RenderOddsValue(_ticket, null, GetCachePropertyByIdStub);

            ///Assert
            Assert.AreEqual($"<div class=\"underdog\">-</div>", oddsValue);
        }
        [Test]
        public void RenderOddsValue_BoostParlay_ReturnOddsWithClassName()
        {
            ////Arrange
            _ticket.BetTeam = "1";
            _ticket.StatusId = 1;
            _ticket.OddsSpread = 2;
            _ticket.Odds = 12.3345667m;

            ///Action
            var oddsValue = _odds.RenderOddsValue(_ticket, null, GetCachePropertyByIdStub);

            ///Assert
            Assert.AreEqual($"<div class=\"underdog strikethrough\">12.3346</div>", oddsValue);
        }

        //[Test]
        //public void RenderOddsType_BoostParlay_ReturnOddsType()
        //{
        //    ////Arrange
        //    _ticket.StatusId = 1;
        //    _ticket.BetTeam = "1";
        //    _ticket.Odds = 2;
        //    _ticket.OddsSpread = 2;
        //    _ticket.ExtraInfo = "<p1 bbp=\"0.1\" />";
        //    var expectedOddsType = "<div class=\"oddstype\"><span class='underdog'>2.1</span><br/>";
        //    expectedOddsType += "<span class='boostrate'><span class='icon-boost'></span>+10% Boost</span></div>";

        //    ///Action
        //    var oddsType = _odds.RenderOddsType(_ticket, null, GetCachePropertyByIdStub);

        //    ///Assert
        //    Assert.AreEqual(expectedOddsType, oddsType);
        //}

        private static string GetCachePropertyByIdStub(object id) => id.ToString() == "0" ? "" : "Dec";
    }
}