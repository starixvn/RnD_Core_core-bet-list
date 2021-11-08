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
            var bettypeName = "<span class=\"gamegroupname\"> RNG War</span>";

            // Act
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(act.League.LeagueName.ToString().Contains(bettypeName));
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
        public void BuildBetTeam_SummarTicket_SetBetTeamIsEmpty()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;

            // Act
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsEmpty(act.betTeam);
        }

        [Test]
        public void BuildBetTeam_DetailTicket_SetBetTeamIsTransId3rd()
        {
            // Arrange
            _ticket.BetCheck = "TransId 3rd";

            // Act
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("TransId 3rd", act.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetbetTeamClassNameIsUnderdog()
        {
            // Arrange


            // Act
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("underdog", act.betTeamClassName);
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


       
        [Test]
        public void BuildMatchInfo_Always_SecondRowIsSportTypeAndGameGroup()
        {
            // Arrange.
            const int sportTypeId = 167;
            const string sportName = "Sport name";

            var rtfHelper = new RTFHelper().Clone();

            _ticket.SportTypeId = sportTypeId;
            _ticketHelper.GetSportNameById(Arg.Is(sportTypeId)).Returns(sportName);

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectation = $"{sportName} {BetTypeName}";

            Assert.AreEqual(expectation, linesInActualText[1]);
        }
        
        private string BuildExpectedTemplateDetailLinkHtml(ITicket ticket, string transactionType)
        {
            return string.Format(
                "<div class=\"detail rngwar-casino-detail\"" +
                " onclick='showRNGWARBetDetails(&quot;{0}&quot;,&quot;{1}&quot;,&quot;{2}&quot;,&quot;{3}&quot;);'>" +
                "<a class=\"detail-link\"" +
                " href=\"javascript:void(&quot;&quot;);\">Details</a></div>",
                ticket.TransId.ToString(),
                ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                ticket.CustId.ToString(),
                transactionType);
        }
    }
}