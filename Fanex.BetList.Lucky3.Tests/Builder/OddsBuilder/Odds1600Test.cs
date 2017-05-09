namespace Fanex.BetList.Lucky3.Tests.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Core.Builder.OddsBuilder;
    using Core.Entities;
    using Core.Templates;
    using NPOI.SS.UserModel;
    using NSubstitute;
    using NUnit.Framework;

    public class Odds1600Test
    {
        private IOdds _odds;
        private ITicket _ticket;
        private List<ITicketData> _ticketData;
        private GetCachePropertyById _getOddsTypeNameFunc;

        [SetUp]
        public void Setup()
        {
            _odds = new Odds1600();
            _ticket = Substitute.For<ITicket>();
            _ticketData = Substitute.For<List<ITicketData>>();
            _getOddsTypeNameFunc = Substitute.For<GetCachePropertyById>();
        }

        [Test]
        public void Render_Always_OddsIsHyphen()
        {
            // Act.
            Odds_Template oddsTemplate = _odds.Render(_ticket, _ticketData, _getOddsTypeNameFunc);

            // Assert.
            Assert.AreEqual("-", oddsTemplate.ToString());
        }

        [Test]
        public void RenderRTF_Always_OddsIsHyphen()
        {
            // Act.
            IRichTextString oddsTemplate = _odds.RenderRTF(_ticket, _ticketData, _getOddsTypeNameFunc, new RTFHelper());

            // Assert.
            Assert.AreEqual("-", oddsTemplate.String);
        }
    }
}