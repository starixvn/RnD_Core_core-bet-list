namespace Fanex.BetList.Microgaming.Test.Builder.ChoiceBuilder
{
    using System;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using NPOI.HSSF.UserModel;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice2008Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2008();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("WON", "1")]
        [TestCase("LOSE", "1")]
        [TestCase("DRAW", "1")]
        [TestCase("REFUND", "1")]
        [TestCase("REJECT", "2")]
        [TestCase("VOID", "2")]
        [TestCase("RUNNING", "3")]
        [TestCase("WAITING", "3")]
        public void BuildLeague_MainTicketWinlostIsToday_ReturnDetailtButtonHtml(string ticketStatus, string transctionType)
        {
            //// Arrange
            _ticket.TransDesc = string.Empty;
            _ticket.TransId = 1;
            _ticket.WinlostDate = DateTime.Today;
            _ticket.Status = ticketStatus;
            _ticket.CustId = 10000;

            //// Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            //// Assert
            string expectedTemplate =
                string.Format(
               "<div class=\"detail\" onclick='showMicroGamingRnGBetsDetails(&quot;1&quot;,&quot;{1}&quot;,&quot;10000&quot;,&quot;{0}&quot;);'><a style=\"color:#755200;font-weight:bold\" href=\"javascript:void(&quot;&quot;);\">Details</a></div>",
                transctionType,
                _ticket.WinlostDate);
            
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [Test]
        public void BuildLeague_MainTicketWinlostIsYesterday_ReturnDetailtButtonHtml()
        {
            //// Arrange
            _ticket.TransDesc = string.Empty;
            _ticket.TransId = 1;
            _ticket.WinlostDate = DateTime.Today.AddDays(-1);
            _ticket.CustId = 10000;

            //// Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            //// Assert
            string expectedTemplate = string.Format("<div class=\"detail\" onclick='showMicroGamingRnGBetsDetails(&quot;1&quot;,&quot;{0}&quot;,&quot;10000&quot;,&quot;0&quot;);'><a style=\"color:#755200;font-weight:bold\" href=\"javascript:void(&quot;&quot;);\">Details</a></div>", _ticket.WinlostDate);
            
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [Test]
        public void BuildLeague_SubTicket_ReturnGameName()
        {
            //// Arrange
            var gameName = "Game Name";
            _ticket.TransDesc = "1";
            _ticketHelper.GetResourceData("MG_Casino", "1").Returns(gameName);

            //// Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            //// Assert
            var expectedGameNameFormat = "<span class='micro-gaming-game-name'> Game Name</span>";
            Assert.IsTrue(_choice.Template.League.LeagueName.leagueName.Contains(expectedGameNameFormat));
        }

        [Test]
        public void RenderRTF_SubTicket_RemoveHtmlTag()
        {
            //// Arrange
            _ticket.TransDesc = "1";

            var gameName = "Game Name";
            _ticketHelper.GetResourceData("MG_Casino", "1").Returns(gameName);
            var betTypeName = "Baccarat";
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns(betTypeName);
            var workbook = new HSSFWorkbook();
            var rtfHelper = new RTFHelper()
            {
                NegFont = workbook.CreateFont(),
                NormalFont = workbook.CreateFont(),
                PosFont = workbook.CreateFont(),
                NegFontCrossed = workbook.CreateFont(),
                NormalFontCrossed = workbook.CreateFont(),
                PosFontCrossed = workbook.CreateFont(),
                RTFRenderer = new RtfTextRender()
            };

            //// Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            //// Assert
            var expectedLeagueName = string.Format("{0} {1}", betTypeName, gameName);
            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }

        [Test]
        public void RenderRTF_MainTicket_RemoveHtmlTag()
        {
            //// Arrange
            _ticket.TransDesc = string.Empty;
            var betTypeName = "Baccarat";
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns(betTypeName);
            var workbook = new HSSFWorkbook();
            var rtfHelper = new RTFHelper()
            {
                NegFont = workbook.CreateFont(),
                NormalFont = workbook.CreateFont(),
                PosFont = workbook.CreateFont(),
                NegFontCrossed = workbook.CreateFont(),
                NormalFontCrossed = workbook.CreateFont(),
                PosFontCrossed = workbook.CreateFont(),
                RTFRenderer = new RtfTextRender()
            };

            //// Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            //// Assert
            Assert.AreEqual(betTypeName, _choice.Template.League.LeagueName.leagueName);
        }
    }
}