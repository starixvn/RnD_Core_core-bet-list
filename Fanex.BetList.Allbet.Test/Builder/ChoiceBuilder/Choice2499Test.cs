namespace Fanax.BetList.Allbet.Test.Builder.ChoiceBuilder
{
    using System;
    using System.Globalization;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice2499Test
    {
        private const string BetTypeName = "Baccarat";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        public ITicketHelper TicketHelper { get => _ticketHelper; set => _ticketHelper = value; }

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2499();
            _ticket = Substitute.For<ITicket>();
            TicketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTypeId = AllbetBetTypes.Baccarat;
            _ticket.TransId = 1;
            _ticket.CustId = 1000;

            TicketHelper.GetBetTypeNameById(AllbetBetTypes.Baccarat).Returns(BetTypeName);
        }


        [TestCase("")]
        [TestCase("comms=1*tableCode=B001*result=P;S6;H2:B;D10;D5 *results=B;8:P;5")]
        public void BuildBetTeam_ReturnEmpty(string transDes)
        {
            ////Arrange
            _ticket.TransDesc = transDes;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(string.IsNullOrEmpty(_choice.Template.betTeam));
        }

        [TestCase(BetStatus.Won, "1")]
        [TestCase(BetStatus.Lose, "1")]
        [TestCase(BetStatus.Draw, "1")]
        [TestCase(BetStatus.Refund, "1")]
        [TestCase(BetStatus.Reject, "2")]
        [TestCase(BetStatus.Void, "2")]
        [TestCase(BetStatus.Running, "3")]
        [TestCase(BetStatus.Waiting, "3")]
        public void BuildLeague_ForTodaySummaryTicket_ReturnDetailLinkHtml(string ticketStatus, string transactionType)
        {
            ////Arrange
            _ticket.MatchCode = string.Empty;
            _ticket.WinlostDate = DateTime.Today;
            _ticket.Status = ticketStatus;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            string expectedTemplate = BuildExpectedTemplateDetailLinkHtml(_ticket, transactionType);
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [Test]
        public void BuildLeague_LeagueNameIsBetTypeNameAndCampaignName()
        {
            // Arrange
            const string campaignName = "testCampaignName";
            
            _ticket.MatchCode = campaignName;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedLeagueName = string.Format(
                "<span class='allbet-bet-type-name'>{0}</span> <span class='allbet-game-id'>{1}</span>",
                BetTypeName,
                campaignName);
            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }

        private string BuildExpectedTemplateDetailLinkHtml(ITicket ticket, string transactionType)
        {
            return string.Format(
                "<div class=\"detail allbet-casino-detail\"" +
                " onclick='showTodayRunningBetsDetails(&quot;{0}&quot;,&quot;{1}&quot;,&quot;{2}&quot;,&quot;{3}&quot;);'>" +
                "<a class=\"detail-link\"" +
                " href=\"javascript:void(&quot;&quot;);\">Details</a></div>",
                ticket.TransId.ToString(),
                ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                ticket.CustId.ToString(),
                transactionType);
        }
    }
}
