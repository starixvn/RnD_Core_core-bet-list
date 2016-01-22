namespace Fanex.BetList.Allbet.Tests.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using NPOI.HSSF.UserModel;
    using NSubstitute;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class Choice2401Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2401();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_TransDecsHasDataOfBetChoice_ReturnBetChoiceName()
        {
            var choiceName = "Player";
            _ticket.TransDesc = "choice=1001*comms=1*tableCode=B001*result=P;S6;H2:B;D10;D5 *results=B;8:P;5";

            _ticketHelper.GetResourceData("AllBet_BT", "1001").Returns(choiceName);

            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.AreEqual(choiceName, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TransDecsDoesNotHasDataOfBetChoice_ReturnEmpty()
        {
            _ticket.TransDesc = "comms=1*tableCode=B001*result=P;S6;H2:B;D10;D5 *results=B;8:P;5";

            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsNullOrEmpty(_choice.Template.betTeam);
        }

        [Test]
        public void BuildBetType_TransDecsIsEmpty_ReturnEmpty()
        {
            _ticket.TransDesc = string.Empty;

            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsNullOrEmpty(_choice.Template.BetType.betTypeName);
        }

        [Test]
        public void BuildBetType_TransDecsIsNotEmpty_ReturnBetId()
        {
            _ticket.TransDesc = "Transdesc";
            _ticket.BetId = 123;

            _choice.Render(_ticket, _ticketHelper, null, false);

            var betIdMarkup = string.Format("<span class='allbet-bet-id'>{0}</span>", "BetID: " + 123);

            Assert.AreEqual(betIdMarkup, _choice.Template.BetType.betTypeName);
        }

        [TestCase("WON", "1")]
        [TestCase("LOSE", "1")]
        [TestCase("DRAW", "1")]
        [TestCase("REFUND", "1")]
        [TestCase("REJECT", "2")]
        [TestCase("VOID", "2")]
        [TestCase("RUNNING", "3")]
        [TestCase("WAITING", "3")]
        public void BuildLeague_ForTodaySummaryTicket_ReturnDetailLinkHtml(string ticketStatus, string transactionType)
        {
            _ticket.TransDesc = string.Empty;
            _ticket.TransId = 1;
            _ticket.WinlostDate = DateTime.Today;
            _ticket.Status = ticketStatus;
            _ticket.CustId = 1000;

            _choice.Render(_ticket, _ticketHelper, null, false);

            string expectedTemplate = BuildExpectedTemplateDetailLinkHtml(_ticket, transactionType);

            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [Test]
        public void BuildLeague_ForYesterdaySummaryTicket_ReturnDetailLinkHtml()
        {
            _ticket.TransDesc = string.Empty;
            _ticket.TransId = 1;
            _ticket.WinlostDate = DateTime.Today.AddDays(-1);
            _ticket.CustId = 1000;

            _choice.Render(_ticket, _ticketHelper, null, false);

            string expectedTemplate = BuildExpectedTemplateDetailLinkHtml(_ticket, "0");

            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        private string BuildExpectedTemplateDetailLinkHtml(ITicket ticket, string transactionType)
        {
            return string.Format(
                "<div class=\"detail\"" +
                " onclick='showTodayRunningBetsDetails(&quot;{0}&quot;,&quot;{1}&quot;,&quot;{2}&quot;,&quot;{3}&quot;);'>" +
                "<a style=\"color:#755200;font-weight:bold\"" +
                " href=\"javascript:void(&quot;&quot;);\">Details</a></div>",
                ticket.TransId.ToString(),
                ticket.WinlostDate.ToString(),
                ticket.CustId.ToString(),
                transactionType);
        }

        [Test]
        public void BuildLeague_ForLeagueName_ReturnBetTypeName()
        {
            _ticket.BetTypeId = 2401;
            _ticketHelper.GetBetTypeNameById(2401).Returns("Baccarat");

            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsTrue(_choice.Template.League.LeagueName.leagueName.Contains("Baccarat"));
        }

        [TestCase("")]
        [TestCase("Trandesc")]
        public void RenderRTF_ForLeagueName_RemoveHtmlTag(string trandesc)
        {
            _ticket.TransDesc = trandesc;

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

            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            Assert.IsFalse(_choice.Template.League.LeagueName.leagueName.Contains("</span>"));
        }

        [Test]
        public void RenderRTF_ForBetTypeNameOfDetailTicket_RemoveHtmlTag()
        {
            _ticket.TransDesc = "transdesc";

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

            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            Assert.IsFalse(_choice.Template.BetType.betTypeName.Contains("</span>"));
        }
    }
}
