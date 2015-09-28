namespace Fanex.BetList.AGCasino.Tests.Builder.ChoiceBuilder
{
    using System;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using NPOI.HSSF.UserModel;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1801Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1801();
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
               "<div class=\"detail\" onclick='showAGCasinoDetails(&quot;1&quot;,&quot;{1}&quot;,&quot;10000&quot;,&quot;{0}&quot;);'><a style=\"color:#755200;font-weight:bold\" href=\"javascript:void(&quot;&quot;);\">Details</a></div>",
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
            string expectedTemplate = string.Format("<div class=\"detail\" onclick='showAGCasinoDetails(&quot;1&quot;,&quot;{0}&quot;,&quot;10000&quot;,&quot;0&quot;);'><a style=\"color:#755200;font-weight:bold\" href=\"javascript:void(&quot;&quot;);\">Details</a></div>", _ticket.WinlostDate);

            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [Test]
        public void BuildBetTeam_TransDecsHasDataOfBetChoice_ReturnBetChoice()
        {
            //// Arrange
            var choiceName = "Choice Name";
            _ticket.BetTypeId = 1801;
            _ticket.TransDesc = "{\"round\":\"DSP\",\"tableCode\":\"va21\",\"game\":\"BAC\",\"type\":\"1\"}";

            _ticketHelper.GetResourceData("AG_Baccarat", "1").Returns(choiceName);

            //// Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            //// Assert
            Assert.AreEqual(choiceName, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TransDecsDoesNotDataOfBetChoice_ReturnEmpty()
        {
            //// Arrange
            _ticket.BetTypeId = 1801;
            _ticket.TransDesc = "{\"round\":\"DSP\",\"tableCode\":\"va21\",\"game\":\"BAC\"}";

            //// Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            //// Assert
            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [Test]
        public void RenderRTF_SubTicket_RemoveHtmlTag()
        {
            //// Arrange
            _ticket.BetTypeId = 1801;
            _ticket.TransDesc = "{\"round\":\"DSP\",\"tableCode\":\"va21\",\"game\":\"BAC\",\"type\":\"1\"}";

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
            Assert.IsTrue(!_choice.Template.League.LeagueName.leagueName.Contains("<span class='ag-casino-bet-type'>"));
        }
    }
}