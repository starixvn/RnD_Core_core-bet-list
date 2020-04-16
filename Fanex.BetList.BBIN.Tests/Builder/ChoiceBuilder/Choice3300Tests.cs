namespace Fanex.BetList.BBIN.Tests.Builder.ChoiceBuilder
{
    using System;
    using System.Globalization;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice3300Tests
    {
        ////Choice=*site=BEST-ODDS* TABLECODE = B001 * gamename = 3003 * result = D; S7:T;H10* results = D; S7:T;H10* winResult = D; S7:T;H10
        private const int BetTypeId = 3300;
        private const string BetTypeName = "BBIN Live";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice3300();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTypeId = BetTypeId;
            _ticket.TransId = 1;
            _ticket.CustId = 1000;

            _ticketHelper.GetBetTypeNameById(BetTypeId).Returns(BetTypeName);
        }

        [Test]
        public void BuildBetTeam_TransDecsHasDataOfGameName_ReturnGameName()
        {
            // Arrange.
            const string gameName = "Dragon Tiger";

            _ticket.TransDesc = "Choice=*site=BEST-ODDS*TABLECODE=B001*gamename=3003*result=D;S7:T;H10*results=D;S7:T;H10*winResult=D;S7:T;H10";
            _ticketHelper.GetResourceData("BBIN_GT", "3003").Returns(gameName);

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(gameName, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TransDecsDoesNotHasDataOfGameName_ReturnEmpty()
        {
            // Arrange.
            _ticket.TransDesc = "Choice=*site=BEST-ODDS*TABLECODE=B001*result=D;S7:T;H10*results=D;S7:T;H10*winResult=D;S7:T;H10";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsTrue(string.IsNullOrEmpty(_choice.Template.betTeam));
        }

        [Test]
        public void BuildBetType_TransDecsIsEmpty_ReturnEmpty()
        {
            // Arrange.
            _ticket.TransDesc = string.Empty;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsTrue(string.IsNullOrEmpty(_choice.Template.BetType.betTypeName));
        }

        [Test]
        public void BuildBetType_TransDecsIsNotEmpty_ReturnBetCheckData()
        {
            // Arrange.
            _ticket.TransDesc = "Transdesc";
            _ticket.BetCheck = "123";
            var betIdMarkup = string.Format("<span class='bbin-bet-id'>{0}</span>", "123");

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(betIdMarkup, _choice.Template.BetType.betTypeName);
        }

        [Test]
        public void BuildMatch_Always_HideMatch()
        {
            // Arrange & Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Match);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_ReturnBetTeamClassNameUnderdog()
        {
            // Arrange & Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("underdog", template.betTeamClassName);
        }

        [TestCase(BetStatus.Won, "1")]
        [TestCase(BetStatus.Lose, "1")]
        [TestCase(BetStatus.Draw, "1")]
        [TestCase(BetStatus.Refund, "1")]
        [TestCase(BetStatus.Reject, "2")]
        [TestCase(BetStatus.Void, "2")]
        [TestCase(BetStatus.Waiting, "3")]
        public void BuildLeague_ForSummaryTicket_ReturnDetailLinkHtml(string ticketStatus, string transactionType)
        {
            // Arrange.
            _ticket.TransDesc = string.Empty;
            _ticket.WinlostDate = DateTime.Today;
            _ticket.Status = ticketStatus;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            string expectedTemplate = BuildExpectedTemplateDetailLinkHtml(_ticket, transactionType);
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [Test]
        public void BuildLeague_ForSummaryTicket_ReturnEmptyDetailLink()
        {
            // Arrange.
            _ticket.TransDesc = string.Empty;
            _ticket.WinlostDate = DateTime.Today;
            _ticket.Status = BetStatus.Running;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedLeagueName = string.Format("<span class='bbin-bet-type-name'>{0}</span>", BetTypeName);
            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }

        [Test]
        public void BuildLeague_ForLeagueName_ReturnBetTypeName()
        {
            // Arrange & Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsTrue(_choice.Template.League.LeagueName.leagueName.Contains(BetTypeName));
        }

        [TestCase("")]
        [TestCase("Trandesc")]
        public void RenderRTF_ForLeagueName_RemoveHtmlTag(string trandesc)
        {
            // Arrange.
            _ticket.TransDesc = trandesc;
            var rtfHelper = new RTFHelper().Clone();

            // Act.
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            // Assert.
            Assert.IsFalse(_choice.Template.League.LeagueName.leagueName.Contains("</span>"));
        }

        [Test]
        public void RenderRTF_ForBetTypeNameOfDetailTicket_RemoveHtmlTag()
        {
            // Arrange.
            _ticket.TransDesc = "transdesc";
            var rtfHelper = new RTFHelper().Clone();

            // Act.
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            // Assert.
            Assert.IsFalse(_choice.Template.BetType.betTypeName.Contains("</span>"));
        }

        [Test]
        public void RemoveHtmlTagFromLeagueName_FromSettledTicket_LeagueNameIsBetTypeNameAndGameTableIdAndGameId()
        {
            // Arrange.
            const string gameId = "12345";
            const string gameTableId = "R001";

            var rtfHelper = new RTFHelper()
            {
                RTFRenderer = new RtfTextRender()
            };

            _ticket.TransDesc = $"Choice=*site=BEST-ODDS*TABLECODE={gameTableId}*gamename=3003*result=D;S7:T;H10*results=D;S7:T;H10*winResult=D;S7:T;H10";
            _ticket.BetTeam = gameId;

            // Act.
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            // Assert.
            var expectedLeagueName = string.Format("{0} {1} {2}", BetTypeName, gameTableId, gameId);

            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }

        private static string BuildExpectedTemplateDetailLinkHtml(ITicket ticket, string transactionType)
            => string.Format(
                "<div class=\"detail bbin-detail\"" +
                " onclick='showBBINBetDetails(&quot;{0}&quot;,&quot;{1}&quot;,&quot;{2}&quot;,&quot;{3}&quot;);'>" +
                "<a style=\"color:#755200;font-weight:bold\"" +
                " href=\"javascript:void(&quot;&quot;);\">Details</a></div>",
                ticket.TransId.ToString(),
                ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                ticket.CustId.ToString(),
                transactionType);
    }
}
