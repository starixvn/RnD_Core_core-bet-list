namespace Fanex.BetList.Voidbridge.Tests.Builder.ChoiceBuilder
{
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Globalization;

    [TestFixture]
    public class Choice4001Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _choice = new Choice4001();
        }

        [Test]
        public void BuildSport_SportTypeIdHasData_ReturnSportTypeNameBlock()
        {
            ////Arrange
            _ticket.SportTypeId = 1;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Voidbridge");

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var expectedSportTypeNameBlock = string.Format("<span class='voidbridge-sporttypename'>{0}</span>", "Voidbridge");
            Assert.AreEqual(_choice.Template.League.sportTypeName, expectedSportTypeNameBlock);
        }

        [Test]
        public void BuildBetTeam_WhenHasData_ReturnGameTypeNameBlock()
        {
            ////Arrange
            _ticket.TransDesc = "1";
            _ticketHelper.GetResourceData("VB_Casino_GT", _ticket.TransDesc).Returns("Bushido Code");

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var expectedGameTypeNameBlock = string.Format("<span class='colorHandicap'>{0}</span>", "Bushido Code");
            Assert.AreEqual(expectedGameTypeNameBlock, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_ForDetailTicket_ShowGamePlayId()
        {
            ////Arrange
            _ticket.BetCheck = "123";

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var expectedGamePlayId = "123";
            Assert.IsTrue(_choice.Template.betTeam.Contains(expectedGamePlayId));
        }

        [Test]
        public void BuildLeague_WhenHasData_ReturnGameGroupNameBlock()
        {
            ////Arrange
            _ticket.TransDesc = "1";
            _ticketHelper.GetResourceData("VB_Casino_GG", _ticket.TransDesc).Returns("Table Game");

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var expectedGameGroupNameBlock = string.Format("<span class='gamegroupname'>{0}</span>", "Table Game");
            Assert.AreEqual(expectedGameGroupNameBlock, _choice.Template.League.LeagueName.leagueName);
        }

        [TestCase("WON", "1")]
        [TestCase("LOSE", "1")]
        [TestCase("DRAW", "1")]
        [TestCase("REFUND", "1")]
        [TestCase("REJECT", "2")]
        [TestCase("VOID", "2")]
        [TestCase("RUNNING", "3")]
        [TestCase("WAITING", "3")]
        public void BuildLeague_ForTodaySummaryTicket_ReturnDetailLink(string ticketStatus, string transactionType)
        {
            ////Arrange
            _ticket.BetCheck = string.Empty;
            _ticket.TransId = 1;
            _ticket.CustId = 2;
            _ticket.Status = ticketStatus;
            _ticket.WinlostDate = DateTime.Today;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            string expectedDetailLinkMarkup = BuildVoidBridgeDetailsLink(transactionType);
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedDetailLinkMarkup));
        }

        [TestCase("WON", "1")]
        [TestCase("LOSE", "1")]
        [TestCase("DRAW", "1")]
        [TestCase("REFUND", "1")]
        [TestCase("REJECT", "2")]
        [TestCase("VOID", "2")]
        [TestCase("RUNNING", "3")]
        [TestCase("WAITING", "3")]
        public void BuildLeague_ForYesterdaySummaryTicket_ReturnDetailLink(string ticketStatus, string transactionType)
        {
            ////Arrange
            _ticket.BetCheck = string.Empty;
            _ticket.TransId = 1;
            _ticket.CustId = 2;
            _ticket.Status = ticketStatus;
            _ticket.WinlostDate = DateTime.Today.AddDays(-1);

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            string expectedDetailLinkMarkup = BuildVoidBridgeDetailsLink(transactionType);
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedDetailLinkMarkup));
        }

        [Test]
        public void BuildLeague_ForPreviousDaysSummaryTicket_ReturnDetailLink()
        {
            ////Arrange
            _ticket.BetCheck = string.Empty;
            _ticket.TransId = 1;
            _ticket.CustId = 2;
            _ticket.WinlostDate = DateTime.Today.AddDays(-2);

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            string expectedDetailLinkMarkup = BuildVoidBridgeDetailsLink("0");
            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedDetailLinkMarkup));
        }

        [Test]
        public void BuildLeague_ForDetailTicket_NotShowDetailLink()
        {
            ////Arrange
            _ticket.BetCheck = "1";

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            string expectedDetailLinkMarkup = BuildVoidBridgeDetailsLink("0");
            Assert.IsFalse(_choice.Template.League.ToString().Contains(expectedDetailLinkMarkup));
        }

        private string BuildVoidBridgeDetailsLink(string transactionType)
        {
            string[] detailsFunctionDivCompositions = new string[]
            {
                "<div class=\"detail voidbridge-detail\" onclick=\"showVoidBridgeDetails(",
                _ticket.TransId.ToString(CultureInfo.InvariantCulture),
                ", ",
                "'",
                _ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                "'",
                ", ",
                _ticket.CustId.ToString(CultureInfo.InvariantCulture),
                ", ",
                transactionType,
                ");\">",
                "<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void('');\">",
                CoreBetList.details,
                "</a>",
                "</div>"
            };
            string detailLinkMarkup = string.Join(null, detailsFunctionDivCompositions);

            return detailLinkMarkup;
        }

        [Test]
        public void AdjustBetTeamToRTF_ForSportTypeName_RemoveHtmlTag()
        {
            ////Arrange
            _ticket.SportTypeId = 1;
            _ticketHelper.GetSportNameById(_ticket.SportTypeId).Returns("Voidbridge");

            ////Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, new RTFHelper().Clone());

            ////Assert
            Assert.AreEqual(_choice.Template.League.sportTypeName, "Voidbridge");
        }

        [Test]
        public void AdjustBetTeamToRTF_ForGameGroupName_RemoveHtmlTag()
        {
            ////Arrange
            _ticket.TransDesc = "1";
            _ticketHelper.GetResourceData("VB_Casino_GG", _ticket.TransDesc).Returns("video slot");

            ////Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, new RTFHelper().Clone());

            ////Assert
            Assert.AreEqual("video slot", _choice.Template.League.LeagueName.leagueName);
        }

        [Test]
        public void AdjustBetTeamToRTF_ForGameTypeName_RemoveHtmlTag()
        {
            ////Arrange
            _ticket.BetCheck = string.Empty;
            _ticket.TransDesc = "1";
            _ticketHelper.GetResourceData("VB_Casino_GT", _ticket.TransDesc).Returns("Amazon 1");

            ////Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, new RTFHelper().Clone());

            ////Assert
            Assert.AreEqual(_choice.Template.betTeam, "Amazon 1");
        }
    }
}