namespace Fanex.BetList.VirtualSports.Tests.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Core.App_GlobalResources;
    using Core.Builder.OddsBuilder;
    using Core.Constants;
    using Core.Entities;
    using Core.Templates;
    using Core.UnitTest.Common;
    using Core.Utils;
    using NUnit.Framework;

    public class Odds1233Tests
    {
        private Odds1233 _odds;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds1233();
            _ticket = TestData.CreateTicket(InspireVirtualRacingBetTypes.WinPlace);
        }

        [Test]
        public void Render_FoundTicketDataHasData_ShowOddsContent()
        {
            // Arrange
            var ticketData = new List<ITicketData>();
            var winTicketData = TestData.CreateTicketData(InspireVirtualRacingBetTypes.Win, _ticket.RefNo, "draw");
            var placeTicketData = TestData.CreateTicketData(InspireVirtualRacingBetTypes.Place, _ticket.RefNo, "draw");
            ticketData.Add(winTicketData);
            ticketData.Add(placeTicketData);

            // Act
            Odds_Template template = _odds.Render(_ticket, ticketData, GetCachePropertyByIdStub);

            // Assert
            var oddsWin = string.Format("<span class='blue' style='font-weight:normal;'>Win: <b>{0}</b></span>", winTicketData.Odds);
            var oddsPlace = string.Format("<span class='' style='font-weight:normal;'>Place: <b>{0}</b></span>", placeTicketData.Odds);
            var expected = string.Format("{0}<br>{1}", oddsWin, oddsPlace);
            Assert.AreEqual(expected, template.odds);
        }

        [Test]
        public void Render_FoundTicketDataHasNoData_NotShowOddsContent()
        {
            // Arrange
            var ticketData = new List<ITicketData>();
            var winTicketData = TestData.CreateTicketData(InspireVirtualRacingBetTypes.Win, _ticket.TransId.ToString(), "draw");
            var placeTicketData = TestData.CreateTicketData(InspireVirtualRacingBetTypes.Place, _ticket.TransId.ToString(), "draw");
            ticketData.Add(winTicketData);
            ticketData.Add(placeTicketData);

            // Act
            Odds_Template template = _odds.Render(_ticket, ticketData, GetCachePropertyByIdStub);

            // Assert
            var expected = "-";
            Assert.AreEqual(expected, template.odds);
        }

        [Test]
        public void Render_TicketDataHasNoData_NotShowOddsContent()
        {
            // Act
            Odds_Template template = _odds.Render(_ticket, null, GetCachePropertyByIdStub);

            // Assert
            var expected = "-";
            Assert.AreEqual(expected, template.odds);
        }

        private static string GetCachePropertyByIdStub(object id)
        {
            return id.ToString();
        }

        [Test]
        public void BuildRTFOdds_TemplateOddsIsHyphen_ReturnHyphen()
        {
            // Act
            _ticket.Odds = 0;

            // Arrange
            var actualRTF = _odds.RenderRTF(_ticket, null, GetCachePropertyByIdStub, new RTFHelper().Clone());

            // Assert
            Assert.AreEqual("-", actualRTF.ToString());
        }

        [Test]
        public void BuildRTFOdds_TemplateOddsIsNotHyphen_ReturnWinPlaceOddsRTF()
        {
            // Act
            var ticketData = new List<ITicketData>();
            var winTicketData = TestData.CreateTicketData(InspireVirtualRacingBetTypes.Win, _ticket.RefNo, "draw");
            var placeTicketData = TestData.CreateTicketData(InspireVirtualRacingBetTypes.Place, _ticket.RefNo, "draw");
            ticketData.Add(winTicketData);
            ticketData.Add(placeTicketData);

            // Arrange
            var actualRTF = _odds.RenderRTF(_ticket, ticketData, GetCachePropertyByIdStub, new RTFHelper().Clone());

            // Assert
            Assert.AreEqual(
                $"{CoreBetList.win}: {Formatter.FormatNumber3(winTicketData.Odds)}\n" +
                $"{CoreBetList.place}: {Formatter.FormatNumber3(placeTicketData.Odds)}",
                actualRTF.ToString());
        }
    }
}