namespace Fanex.BetList.RNGWar.Tests.Builder.ChoiceBuilder
{
    using System;
    using System.Globalization;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice5500Test
    {
        private const string BetTypeName = "RNG War";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice5500();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTypeId = 5500;
            _ticket.TransId = 1;
            _ticket.CustId = 1000;

            _ticketHelper.GetBetTypeNameById(5500).Returns(BetTypeName);
        }


        [Test]
        public void BuildLeague_Always_SetLeagueNameIsBetTypeName()
        {
            // Arrange
            _ticket.BetCheck = "transId3rd";
            var bettypeName = "<span class='sport'>RNG War</span>";
            bettypeName += "<div class='handicap'>transId3rd</div>";

            // Act
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(bettypeName, act.League.LeagueName.leagueName);
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
            _ticket.BetCheck = string.Empty;
            _ticket.WinlostDate = DateTime.Today;
            _ticket.Status = ticketStatus;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            string expectedTemplate = BuildExpectedTemplateDetailLinkHtml(_ticket, transactionType);
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [TestCase(BetStatus.Won, "1")]
        [TestCase(BetStatus.Lose, "1")]
        [TestCase(BetStatus.Draw, "1")]
        [TestCase(BetStatus.Refund, "1")]
        [TestCase(BetStatus.Reject, "2")]
        [TestCase(BetStatus.Void, "2")]
        [TestCase(BetStatus.Running, "3")]
        [TestCase(BetStatus.Waiting, "3")]
        public void BuildLeague_ForYesterdaySummaryTicket_ReturnDetailLinkHtml(string ticketStatus, string transactionType)
        {
            ////Arrange
            _ticket.BetCheck = string.Empty;
            _ticket.WinlostDate = DateTime.Today.AddDays(-1);
            _ticket.Status = ticketStatus;

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
            _ticket.WinlostDate = DateTime.Today.AddDays(-2);

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            string expectedTemplate = BuildExpectedTemplateDetailLinkHtml(_ticket, "0");
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [Test]
        public void BuildBetType_Always_SetEmpty()
        {
            // Arrange


            // Act
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsEmpty(act.BetType.betTypeName);
        }

        [Test]
        public void BuildSport_Always_SetEmpty()
        {
            // Arrange


            // Act
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsEmpty(act.League.sportTypeName);
        }

        [Test]
        public void BuildScore_Always_SetNull()
        {
            // Arrange


            // Act
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(act.Score);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Arrange


            // Act
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(act.Handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetbetTeamClassNameIsEmpty()
        {
            // Arrange


            // Act
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsEmpty(act.betTeamClassName);
        }

        [Test]
        public void BuildMatch_Always_SetIsNull()
        {
            // Arrange


            // Act
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(act.Match);
        }

        private string BuildExpectedTemplateDetailLinkHtml(ITicket ticket, string transactionType)
        {
            return string.Format(
                "<div class=\"detail rngwar-casino-detail\"" +
                " onclick='showRNGWARBetDetails(&quot;{0}&quot;,&quot;{1}&quot;,&quot;{2}&quot;,&quot;{3}&quot;);'>" +
                "<a style=\"color:#755200;font-weight:bold\"" +
                " href=\"javascript:void(&quot;&quot;);\">Details</a></div>",
                ticket.TransId.ToString(),
                ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                ticket.CustId.ToString(),
                transactionType);
        }
    }
}