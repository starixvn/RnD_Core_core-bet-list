namespace Fanex.BetList.Core.UnitTest.Builder.OddsBuilder
{
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    public class Odds501Tests
    {
        private IOdds _odds;
        private ITicket _ticket;
        private List<ITicketData> _ticketData;
        private GetCachePropertyById _getOddsTypeNameFunc;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds501();
            _ticket = TestData.CreateTicket(BetTypes.CricketMatchWinner);
            _ticketData = Substitute.For<List<ITicketData>>();
            _getOddsTypeNameFunc = (oddType) => "DEC";
        }

        [Test]
        public void Render_TransDateGreaterThanCutOffTime_HideOddsTypeName()
        {
            // Arrange
            _ticket.TransDate = new DateTime(2021, 4, 9, 0, 0, 0);

            // Act.
            var oddsTemplate = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert.
            Assert.IsEmpty(oddsTemplate.oddsType);
        }

        [Test]
        public void Render_TransDateLessThanCutOffTime_ShowOddsTypeName()
        {
            // Arrange
            _ticket.TransDate = new DateTime(2021, 4, 7, 0, 0, 0);

            // Act.
            var oddsTemplate = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert.
            Assert.AreEqual("DEC", oddsTemplate.oddsType);
        }
    }
}