namespace Fanex.BetList.GlobalGaming.Tests.Builder.ChoiceBuilder
{
    using System;
    using System.Globalization;
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1011Tests
    {
        private ITicketHelper _ticketHelper;
        private ITicket _ticket;
        private IChoice _choice;

        [SetUp]
        public void SetUp()
        {
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _choice = new Choice1011();
        }

        [Test]
        public void BuildBetTeam_FromBetCheckHasNoData_BetTeamIsGameTypeBlock()
        {
            // Arrange
            var gameType = "GG Game Type";
            _ticket.BetCheck = null;
            _ticketHelper.GetResourceData("Fishing_World_GT", Arg.Any<string>()).Returns(gameType);

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedGameTypeBlock = $"<span class='colorHandicap'>{gameType}</span>";
            Assert.AreEqual(expectedGameTypeBlock, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_FromBetCheckHasData_BetTeamIsGameTypeBlockAndGamePlayId()
        {
            // Arrange
            var gameType = "GG Game Type";
            var gamePlayId = "123";
            _ticket.BetCheck = gamePlayId;
            _ticketHelper.GetResourceData("Fishing_World_GT", Arg.Any<string>()).Returns(gameType);

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedGameTypeBlock = $"<span class='colorHandicap'>{gameType}</span> {gamePlayId}";
            Assert.AreEqual(expectedGameTypeBlock, template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsUnderdog()
        {
            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("underdog", template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(template.Handicap);
        }

        [Test]
        public void BuildBetType_Always_BetTypeIsNull()
        {
            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(template.BetType);
        }

        [Test]
        public void BuildMatch_Always_MatchIsGameGroupBlock()
        {
            // Arrange
            var gameGroupName = "GG Game Group Name";
            _ticketHelper.GetResourceData("Fishing_World_GG", Arg.Any<string>()).Returns(gameGroupName);

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedGameGroupBlock = $"<div class='gamegroupname'>{gameGroupName}</div>";
            Assert.AreEqual(expectedGameGroupBlock, template.Match.ToString());
        }

        [Test]
        public void BuildScore_Always_ScoreIsNull()
        {
            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(template.Score);
        }

        [Test]
        public void BuildLeague_FromBetCheckHasData_LeagueIsNull()
        {
            // Arrange
            _ticket.BetCheck = "123";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(template.League);
        }

        [Test]
        public void BuildLeague_FromBetCheckHasNoDataAndTicketIsPreviousDays_LeagueIsDetailsLinkWithTransTypeIsZero()
        {
            // Arrange
            _ticket.BetCheck = null;
            _ticket.WinlostDate = DateTime.Now.AddDays(-2);
            _ticket.TransId = 123456;
            _ticket.CustId = 123;

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedDetailsLink = CreateGlobalGamingDetailsLink(_ticket, "0");
            Assert.AreEqual(expectedDetailsLink, template.League.ToString());
        }

        [TestCase("Won", "1")]
        [TestCase("Lose", "1")]
        [TestCase("Draw", "1")]
        [TestCase("Refund", "1")]
        [TestCase("REJECT", "2")]
        [TestCase("VOID", "2")]
        [TestCase("running", "3")]
        [TestCase("waiting", "3")]
        public void BuildLeague_FromBetCheckHasNoDataAndTicketIsTodayAndStatus_LeagueIsDetailsLinkWithTransType(string status, string transType)
        {
            // Arrange
            _ticket.BetCheck = null;
            _ticket.WinlostDate = DateTime.Now;
            _ticket.TransId = 123456;
            _ticket.CustId = 123;
            _ticket.Status = status;

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedDetailsLink = CreateGlobalGamingDetailsLink(_ticket, transType);
            Assert.AreEqual(expectedDetailsLink, template.League.ToString());
        }

        private string CreateGlobalGamingDetailsLink(ITicket ticket, string transType)
        {
            var link = string.Format("<a style=\"color:#755200;font-weight:bold\" href=\"javascript:void('');\">{0}</a>", CoreBetList.details);

            return string.Format(
                "<div class=\"detail global-gaming-detail\" onclick=\"showGlobalGaming('{0}', '{1}', '{2}', '{3}');\">{4}</div>",
                ticket.TransId,
                ticket.WinlostDate.ToString(CultureInfo.InvariantCulture),
                ticket.CustId,
                transType,
                link);
        }

        [Test]
        public void BuildRTFChoice_FromBetCheckHasNoData_FirstLineIsGameType()
        {
            // Arrange
            var gameType = "GG Game Type";
            _ticket.BetCheck = null;
            _ticketHelper.GetResourceData("Fishing_World_GT", Arg.Any<string>()).Returns(gameType);

            // Act
            var rtf = _choice.RenderRTF(_ticket, _ticketHelper, null, Arg.Any<bool>(), new RTFHelper().Clone());

            // Assert
            var lines = rtf[0].ToString().Split('\n');
            var expectedFirstLine = gameType;
            Assert.AreEqual(expectedFirstLine, lines[0]);
        }

        [Test]
        public void BuildRTFChoice_FromBetCheckHasData_FirstLineIsGameTypeAndPlayId()
        {
            // Arrange
            var playId = "123";
            var gameType = "GG Game Type";
            _ticket.BetCheck = playId;
            _ticketHelper.GetResourceData("Fishing_World_GT", Arg.Any<string>()).Returns(gameType);

            // Act
            var rtf = _choice.RenderRTF(_ticket, _ticketHelper, null, Arg.Any<bool>(), new RTFHelper().Clone());

            // Assert
            var lines = rtf[0].ToString().Split('\n');
            var expectedFirstLine = gameType + " " + playId;
            Assert.AreEqual(expectedFirstLine, lines[0]);
        }

        [Test]
        public void BuildRTFChoice_Always_SecondLineIsGameGroup()
        {
            // Arrange
            var gameGroup = "GG Game Group";
            _ticketHelper.GetResourceData("Fishing_World_GG", Arg.Any<string>()).Returns(gameGroup);

            // Act
            var rtf = _choice.RenderRTF(_ticket, _ticketHelper, null, Arg.Any<bool>(), new RTFHelper().Clone());

            // Assert
            var lines = rtf[0].ToString().Split('\n');
            var expectedSecondLine = gameGroup;
            Assert.AreEqual(expectedSecondLine, lines[1]);
        }
    }
}