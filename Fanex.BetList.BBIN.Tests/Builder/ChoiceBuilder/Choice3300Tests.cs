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
        ////Choice=2,1:35,5.00,0.00@5,1:35,5.00,0.00*site=BEST-ODDS* TABLECODE = B001 * gamename = 3003 * result = D; S7:T;H10* results = D; S7:T;H10* winResult = D; S7:T;H10
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
        public void BuildBetTeam_TransDecsHasChoice_ReturnChoiceName()
        {
            // Arrange.
            const string choiceName = "banker";
            var betTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;<span class='handicap custom'>({1} {2})</span></div>";
            var expectedBetTeam = string.Format(betTeamFormat, choiceName, "RM", 5);
            _ticket.Currency = "RM";
            _ticket.TransDesc = "Choice=2,1:35,5.00,0.00*site=BEST-ODDS*TABLECODE=B001*gamename=3003*result=D;S7:T;H10*results=D;S7:T;H10*winResult=D;S7:T;H10";
            _ticketHelper.GetResourceData("BBIN_BT", "30030002").Returns(choiceName);

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TransDecsHasChoices_ReturnChoiceNames()
        {
            // Arrange.
            var choice1 = "choice name 1";
            var choice2 = "choice name 2";
            var betTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;<span class='handicap custom'>({1} {2})</span></div><div><span class='favorite'>{3}</span>&nbsp;<span class='handicap custom'>({4} {5})</span></div>";
            var expectedBetTeam = string.Format(betTeamFormat, choice1, "RM", 5, choice2, "RM", 10);
            _ticket.Currency = "RM";
            _ticket.TransDesc = "Choice=2,1:35,5.00,0.00@5,1:35,10.00,0.00*site=BEST-ODDS*TABLECODE=B001*gamename=3003*result=D;S7:T;H10*results=D;S7:T;H10*winResult=D;S7:T;H10";
            _ticketHelper.GetResourceData("BBIN_BT", "30030002").Returns(choice1);
            _ticketHelper.GetResourceData("BBIN_BT", "30030005").Returns(choice2);

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [TestCase("Choice=*site=BEST-ODDS*TABLECODE=B001*result=D;S7:T;H10*results=D;S7:T;H10*winResult=D;S7:T;H10")]
        [TestCase("Choice=2,1:35*site=BEST-ODDS*TABLECODE=B001*result=D;S7:T;H10*results=D;S7:T;H10*winResult=D;S7:T;H10")]        
        public void BuildBetTeam_TransDecsDoesHasInvalidChoice_ReturnEmptySection(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

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
        public void BuildBetType_DetailTicket_ReturnBetCheckData()
        {
            // Arrange.
            _ticket.TransDesc = "Transdesc";
            _ticket.BetCheck = "123";
            var betIdMarkup = string.Format("<span class='bbin-bet-id'>BetID: {0}</span>", "123");

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
        public void BuildLeague_SummaryTicket_ReturnBetTypeName()
        {
            // Arrange & Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsTrue(_choice.Template.League.LeagueName.leagueName.Contains(BetTypeName));
        }

        [Test]
        public void BuildLeague_DetailTicket_NotContaintBetTypeName()
        {
            // Arrange
            _ticket.TransDesc = "Choice=2,1:35,5.00,0.00*site=BEST-ODDS*TABLECODE=B001*gamename=3003*result=D;S7:T;H10*results=D;S7:T;H10*winResult=D;S7:T;H10";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsFalse(_choice.Template.League.LeagueName.leagueName.Contains(BetTypeName));
        }

        [Test]
        public void BuildLeague_DetailTicket_ShowGameNameAndTableCodeAndGameId()
        {
            // Arrange
            _ticket.BetTeam = "game id";
            _ticketHelper.GetResourceData("BBIN_GT", "3003").Returns("Game name");
            _ticket.TransDesc = "Choice=2,1:35,5.00,0.00*site=BEST-ODDS*TABLECODE=B001*gamename=3003*result=D;S7:T;H10*results=D;S7:T;H10*winResult=D;S7:T;H10";
            var expectedLeague = $"Game name <span class=\"gamegroupname\">B001</span> <span class='bbin-game-id'>game id</span>";
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedLeague, _choice.Template.League.LeagueName.leagueName);
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
        public void RemoveHtmlTagFromLeagueName_FromSettledTicket_LeagueNameIsGameNameAndGameTableIdAndGameId()
        {
            // Arrange.
            const string gameId = "12345";
            const string gameTableId = "R001";

            var rtfHelper = new RTFHelper()
            {
                RTFRenderer = new RtfTextRender()
            };

            _ticket.TransDesc = $"Choice=2,1:35,5.00,0.00*site=BEST-ODDS*TABLECODE={gameTableId}*gamename=3003*result=D;S7:T;H10*results=D;S7:T;H10*winResult=D;S7:T;H10";
            _ticket.BetTeam = gameId;
            _ticketHelper.GetResourceData("BBIN_GT", "3003").Returns("Game name");
            // Act.
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            // Assert.
            var expectedLeagueName = string.Format("{0} {1} {2}", "Game name", gameTableId, gameId);

            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }

        private static string BuildExpectedTemplateDetailLinkHtml(ITicket ticket, string transactionType)
            => string.Format(
                "<div class=\"detail bbin-detail\"" +
                " onclick='showBBINBetDetails(&quot;{0}&quot;,&quot;{1}&quot;,&quot;{2}&quot;,&quot;{3}&quot;);'>" +
                "<a class=\"detail-link\"" +
                " href=\"javascript:void(&quot;&quot;);\">Details</a></div>",
                ticket.TransId.ToString(),
                ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                ticket.CustId.ToString(),
                transactionType);
    }
}
