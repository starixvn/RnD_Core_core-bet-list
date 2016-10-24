namespace Fanex.BetList.Allbet.Tests.Builder.ChoiceBuilder
{
    using System;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using Core.Utils;
    using NPOI.HSSF.UserModel;
    using NSubstitute;
    using NUnit.Framework;

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
            ////Arrange
            var choiceName = "Player";
            _ticket.TransDesc = "choice=1001*comms=1*tableCode=B001*result=P;S6;H2:B;D10;D5 *results=B;8:P;5";
            _ticketHelper.GetResourceData("AllBet_BT", "1001").Returns(choiceName);

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.AreEqual(choiceName, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TransDecsDoesNotHasDataOfBetChoice_ReturnEmpty()
        {
            ////Arrange
            _ticket.TransDesc = "comms=1*tableCode=B001*result=P;S6;H2:B;D10;D5 *results=B;8:P;5";

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(string.IsNullOrEmpty(_choice.Template.betTeam));
        }

        [Test]
        public void BuildBetType_TransDecsIsEmpty_ReturnEmpty()
        {
            ////Arrange
            _ticket.TransDesc = string.Empty;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(string.IsNullOrEmpty(_choice.Template.BetType.betTypeName));
        }

        [Test]
        public void BuildBetType_TransDecsIsNotEmpty_ReturnBetId()
        {
            ////Arrange
            _ticket.TransDesc = "Transdesc";
            _ticket.BetCheck = "123";

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var betIdMarkup = string.Format("<span class='allbet-bet-id'>{0}</span>", "Bet ID: " + "123");
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
            ////Arrange
            _ticket.TransDesc = string.Empty;
            _ticket.TransId = 1;
            _ticket.WinlostDate = DateTime.Today;
            _ticket.Status = ticketStatus;
            _ticket.CustId = 1000;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            string expectedTemplate = BuildExpectedTemplateDetailLinkHtml(_ticket, transactionType);
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [TestCase("WON", "1")]
        [TestCase("LOSE", "1")]
        [TestCase("DRAW", "1")]
        [TestCase("REFUND", "1")]
        [TestCase("REJECT", "2")]
        [TestCase("VOID", "2")]
        [TestCase("RUNNING", "3")]
        [TestCase("WAITING", "3")]
        public void BuildLeague_ForYesterdaySummaryTicket_ReturnDetailLinkHtml(string ticketStatus, string transactionType)
        {
            ////Arrange
            _ticket.TransDesc = string.Empty;
            _ticket.TransId = 1;
            _ticket.WinlostDate = DateTime.Today.AddDays(-1);
            _ticket.Status = ticketStatus;
            _ticket.CustId = 1000;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            string expectedTemplate = BuildExpectedTemplateDetailLinkHtml(_ticket, transactionType);
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [Test]
        public void BuildLeague_ForPreviousDaysSummaryTicket_ReturnDetailLinkHtml()
        {
            ////Arrange
            _ticket.TransDesc = string.Empty;
            _ticket.TransId = 1;
            _ticket.WinlostDate = DateTime.Today.AddDays(-2);
            _ticket.CustId = 1000;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            string expectedTemplate = BuildExpectedTemplateDetailLinkHtml(_ticket, "0");
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [Test]
        public void BuildLeague_FromSettledTicket_LeagueNameIsBetTypeNameAndGameTableIdAndGameId()
        {
            // Arrange
            var gameId = "12345";
            var gameTableId = "R001 ";
            var betType = 123;
            var betTypeName = "Bet Type Name";
            _ticket.BetTypeId = betType;
            _ticket.TransDesc = $"choice=1001*comms=100*site=BEST-ODDS*TableCode={gameTableId}*state=0*result=B;S6;H2:P;D10;D5*results=B;8:P;5*winResult=1001;1004";
            _ticket.MatchCode = gameId;
            _ticketHelper.GetBetTypeNameById(betType).Returns(betTypeName);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedLeagueName = string.Format(
                "<span class='allbet-bet-type-name'>{0}</span> <span class=\"gamegroupname\">{1}</span> <span class='allbet-game-id'>{2}</span>",
                betTypeName,
                gameTableId,
                gameId);
            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }

        private string BuildExpectedTemplateDetailLinkHtml(ITicket ticket, string transactionType)
        {
            return string.Format(
                "<div class=\"detail allbet-casino-detail\"" +
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
            ////Arrange
            _ticket.BetTypeId = 2401;
            _ticketHelper.GetBetTypeNameById(2401).Returns("Baccarat");

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(_choice.Template.League.LeagueName.leagueName.Contains("Baccarat"));
        }

        [TestCase("")]
        [TestCase("Trandesc")]
        public void RenderRTF_ForLeagueName_RemoveHtmlTag(string trandesc)
        {
            ////Arrange
            _ticket.TransDesc = trandesc;
            var workbook = new HSSFWorkbook();
            var rtfHelper = new RTFHelper().Clone();

            ////Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            ////Assert
            Assert.IsFalse(_choice.Template.League.LeagueName.leagueName.Contains("</span>"));
        }

        [Test]
        public void RenderRTF_ForBetTypeNameOfDetailTicket_RemoveHtmlTag()
        {
            ////Arrange
            _ticket.TransDesc = "transdesc";
            var workbook = new HSSFWorkbook();
            var rtfHelper = new RTFHelper().Clone();

            ////Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            ////Assert
            Assert.IsFalse(_choice.Template.BetType.betTypeName.Contains("</span>"));
        }

        [Test]
        public void RemoveHtmlTagFromLeagueName_FromSettledTicket_LeagueNameIsBetTypeNameAndGameTableIdAndGameId()
        {
            // Arrange
            var gameId = "12345";
            var gameTableId = "R001 ";
            var betType = 123;
            var betTypeName = "Bet Type Name";
            var rtfHelper = new RTFHelper()
            {
                RTFRenderer = new RtfTextRender()
            };
            _ticket.BetTypeId = betType;
            _ticket.TransDesc = $"choice=1001*comms=100*site=BEST-ODDS*tableCode={gameTableId}*state=0*result=B;S6;H2:P;D10;D5*results=B;8:P;5*winResult=1001;1004";
            _ticket.MatchCode = gameId;
            _ticketHelper.GetBetTypeNameById(betType).Returns(betTypeName);

            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, Arg.Any<bool>(), rtfHelper);

            // Assert
            var expectedLeagueName = string.Format("{0} {1} {2}", betTypeName, gameTableId, gameId);
            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }
    }
}