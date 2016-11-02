namespace Fanex.BetList.GlobalGaming.Tests.Builder.ChoiceBuilder
{
    using System;
    using System.Globalization;
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using Core.Resources;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1011Tests
    {
        private const string GameTypeRefId = "GG_Game_GT";
        private const string GameGroupRefId = "GG_Game_GG";

        private ITicketHelper _ticketHelper;
        private ITicket _ticket;
        private Choice1011 _choice;

        [SetUp]
        public void SetUp()
        {
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _choice = new Choice1011();
        }

        [TestCase("", "won")]
        [TestCase("", "running")]
        [TestCase("123", "won")]
        public void BuildBetTeam_FromBetCheckHasNoDataOrStatusIsNotRunning_BetTeamIsEmpty(string betCheck, string status)
        {
            // Arrange
            _ticket.BetCheck = betCheck;
            _ticket.Status = status;

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(template.betTeam);
        }

        [Test]
        public void BuildBetTeam_FromBetCheckHasDataAndStatusIsRunningAndMatchCodeContainsBuyAmount_BetTeamIsCoinsInBlock()
        {
            // Arrange
            _ticket.Status = "running";
            _ticket.BetCheck = "123";
            _ticket.MatchCode = "BuyAmount=0.1";
            _ticket.Currency = "UUS";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedCoinsInBlock = $"<div><span class=\"dark-orange\">{GlobalGamingResources.CoinsIn}</span>: UUS 0.1</div>";
            Assert.AreEqual(expectedCoinsInBlock, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_FromBetCheckHasDataAndStatusIsRunningAndMatchCodeContainsRemainingAmountIsNotZero_BetTeamIsRemainingBlock()
        {
            // Arrange
            _ticket.Status = "running";
            _ticket.BetCheck = "123";
            _ticket.MatchCode = "RemainingAmount=0.1";
            _ticket.Currency = "UUS";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedRemainingBlock = $"<div><span class=\"dark-orange\">{GlobalGamingResources.Remaining}</span>: UUS 0.1</div>";
            Assert.AreEqual(expectedRemainingBlock, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_FromBetCheckHasDataAndStatusIsRunningAndMatchCodeContainsRemainingAmountIsZero_BetTeamIsEmpty()
        {
            // Arrange
            _ticket.Status = "running";
            _ticket.BetCheck = "123";
            _ticket.MatchCode = "RemainingAmount=0";
            _ticket.Currency = "UUS";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(template.betTeam);
        }

        [Test]
        public void BuildBetTeam_FromBetCheckHasDataAndStatusIsRunningAndMatchCodeContainsRemainingAmountIsNotZeroAndBuyAmount_BetTeamContainsCoinsInAndRemaining()
        {
            // Arrange
            _ticket.Status = "running";
            _ticket.BetCheck = "123";
            _ticket.MatchCode = "RemainingAmount=1*buyAmount=1.01";
            _ticket.Currency = "UUS";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedBetTeamBlock = $"<div><span class=\"dark-orange\">{GlobalGamingResources.CoinsIn}</span>: UUS 1.01</div>";
            expectedBetTeamBlock += $"<div><span class=\"dark-orange\">{GlobalGamingResources.Remaining}</span>: UUS 1</div>";
            Assert.AreEqual(expectedBetTeamBlock, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_FromBetCheckHasDataAndStatusIsRunningAndMatchCodeContainsRemainingAmountIsZeroAndBuyAmount_BetTeamContainsCoinsIn()
        {
            // Arrange
            _ticket.Status = "running";
            _ticket.BetCheck = "123";
            _ticket.MatchCode = "RemainingAmount=0.0*buyAmount=1.01";
            _ticket.Currency = "UUS";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedBetTeamBlock = $"<div><span class=\"dark-orange\">{GlobalGamingResources.CoinsIn}</span>: UUS 1.01</div>";
            Assert.AreEqual(expectedBetTeamBlock, template.betTeam);
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
        public void BuildBetType_FromBetCheckHasNoData_BetTypeIsGameTypeBlock()
        {
            // Arrange
            var gameType = "GG Game Type";
            _ticket.BetCheck = null;
            _ticketHelper.GetResourceData(GameTypeRefId, Arg.Any<string>()).Returns(gameType);

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedGameTypeBlock = $"<div class=\"underdog\"><span class=\"colorHandicap\">{gameType}</span></div>";
            Assert.AreEqual(expectedGameTypeBlock, template.BetType.ToString());
        }

        [Test]
        public void BuildBetType_FromBetCheckHasData_BetTypeIsGameTypeBlockAndGamePlayId()
        {
            // Arrange
            var gameType = "GG Game Type";
            var gamePlayId = "123";
            _ticket.BetCheck = gamePlayId;
            _ticketHelper.GetResourceData(GameTypeRefId, Arg.Any<string>()).Returns(gameType);

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedGameTypeBlock = $"<div class=\"underdog\"><span class=\"colorHandicap\">{gameType}</span> {gamePlayId}</div>";
            Assert.AreEqual(expectedGameTypeBlock, template.BetType.ToString());
        }

        [Test]
        public void BuildMatch_Always_MatchIsGameGroupBlock()
        {
            // Arrange
            var gameGroupName = "GG Game Group Name";
            _ticketHelper.GetResourceData(GameGroupRefId, Arg.Any<string>()).Returns(gameGroupName);

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedGameGroupBlock = $"<div class=\"gamegroupname\">{gameGroupName}</div>";
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
        public void GetBetTypeName_Always_ReturnsGameType()
        {
            // Arrange
            var gameType = "GG Game Type";
            _ticketHelper.GetResourceData(GameTypeRefId, Arg.Any<string>()).Returns(gameType);

            // Act
            var betType = _choice.GetBetTypeName(_ticket, _ticketHelper, null);

            // Assert
            Assert.AreEqual(gameType, betType);
        }

        [TestCase("", "won")]
        [TestCase("", "running")]
        [TestCase("123", "won")]
        public void BuildRTFChoice_FromBetCheckHasNoDataOrStatusIsNotRunning_BetTeamIsEmpty(string betCheck, string status)
        {
            // Arrange
            _ticket.BetCheck = betCheck;
            _ticket.Status = status;

            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, Arg.Any<bool>(), new RTFHelper().Clone());

            // Assert
            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [Test]
        public void BuildRTFChoice_FromBetCheckHasDataAndStatusIsRunningAndMatchCodeContainsBuyAmount_BetTeamIsCoinsIn()
        {
            // Arrange
            _ticket.Status = "running";
            _ticket.BetCheck = "123";
            _ticket.MatchCode = "BuyAmount=0.1";
            _ticket.Currency = "UUS";

            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, Arg.Any<bool>(), new RTFHelper().Clone());

            // Assert
            var expectedCoinsIn = $"{GlobalGamingResources.CoinsIn}: UUS 0.1";
            Assert.AreEqual(expectedCoinsIn, _choice.Template.betTeam);
        }

        [Test]
        public void BuildRTFChoice_FromBetCheckHasDataAndStatusIsRunningAndMatchCodeContainsRemainingAmountIsNotZero_BetTeamIsRemaining()
        {
            // Arrange
            _ticket.Status = "running";
            _ticket.BetCheck = "123";
            _ticket.MatchCode = "RemainingAmount=0.1";
            _ticket.Currency = "UUS";

            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, Arg.Any<bool>(), new RTFHelper().Clone());

            // Assert
            var expectedRemaining = $"\n{GlobalGamingResources.Remaining}: UUS 0.1";
            Assert.AreEqual(expectedRemaining, _choice.Template.betTeam);
        }

        [Test]
        public void BuildRTFChoice_FromBetCheckHasDataAndStatusIsRunningAndMatchCodeContainsRemainingAmountIsZero_BetTeamIsEmpty()
        {
            // Arrange
            _ticket.Status = "running";
            _ticket.BetCheck = "123";
            _ticket.MatchCode = "RemainingAmount=0";
            _ticket.Currency = "UUS";

            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, Arg.Any<bool>(), new RTFHelper().Clone());

            // Assert
            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [Test]
        public void BuildRTFChoice_FromBetCheckHasDataAndStatusIsRunningAndMatchCodeContainsRemainingAmountIsNotZeroAndBuyAmount_BetTeamContainsCoinsInAndRemaining()
        {
            // Arrange
            _ticket.Status = "running";
            _ticket.BetCheck = "123";
            _ticket.MatchCode = "RemainingAmount=1*buyAmount=1.01";
            _ticket.Currency = "UUS";

            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, Arg.Any<bool>(), new RTFHelper().Clone());

            // Assert
            var expectedBetTeam = $"{GlobalGamingResources.CoinsIn}: UUS 1.01";
            expectedBetTeam += $"\n{GlobalGamingResources.Remaining}: UUS 1";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildRTFChoice_FromBetCheckHasDataAndStatusIsRunningAndMatchCodeContainsRemainingAmountIsZeroAndBuyAmount_BetTeamIsCoinsIn()
        {
            // Arrange
            _ticket.Status = "running";
            _ticket.BetCheck = "123";
            _ticket.MatchCode = "RemainingAmount=0.0*buyAmount=1.01";
            _ticket.Currency = "UUS";

            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, Arg.Any<bool>(), new RTFHelper().Clone());

            // Assert
            var expectedBetTeam = $"{GlobalGamingResources.CoinsIn}: UUS 1.01";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildRTFChoice_FromBetCheckHasNoData_BetTypeIsGameType()
        {
            // Arrange
            var gameType = "GG Game Type";
            _ticket.BetCheck = null;
            _ticketHelper.GetResourceData(GameTypeRefId, Arg.Any<string>()).Returns(gameType);

            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, Arg.Any<bool>(), new RTFHelper().Clone());

            // Assert
            Assert.AreEqual(gameType, _choice.Template.BetType.ToString());
        }

        [Test]
        public void BuildRTFChoice_FromBetCheckHasData_BetTypeIsGameTypeAndGamePlayId()
        {
            // Arrange
            var gameType = "GG Game Type";
            var gamePlayId = "123";
            _ticket.BetCheck = gamePlayId;
            _ticketHelper.GetResourceData(GameTypeRefId, Arg.Any<string>()).Returns(gameType);

            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, Arg.Any<bool>(), new RTFHelper().Clone());

            // Assert
            var expectedBetType = $"{gameType} {gamePlayId}";
            Assert.AreEqual(expectedBetType, _choice.Template.BetType.ToString());
        }

        [Test]
        public void BuildRTFChoice_Always_MatchIsGameGroup()
        {
            // Arrange
            var gameGroupName = "GG Game Group Name";
            _ticketHelper.GetResourceData(GameGroupRefId, Arg.Any<string>()).Returns(gameGroupName);

            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, Arg.Any<bool>(), new RTFHelper().Clone());

            // Assert
            Assert.AreEqual(gameGroupName, _choice.Template.Match.ToString());
        }
    }
}