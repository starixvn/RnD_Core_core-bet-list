namespace Fanex.BetList.Bullfighting.Tests.Builder.ChoiceBuilder
{
    using System.Text.RegularExpressions;
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using Core.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice1004Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1004();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("p", "Player")]
        [TestCase("P", "Player")]
        [TestCase("b", "Banker")]
        [TestCase("B", "Banker")]
        public void BuildBetTeam_Always_GetBetChoiceByCallingGetResourceDataOfTicketHelper(string betTeam, string betChoice)
        {
            // Arrange.
            _ticketHelper.GetResourceData("BF", betTeam).Returns(betChoice);
            _ticket.BetTeam = betTeam;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(betChoice, actualResult.betTeam);
        }

        [Test]
        public void BuildScore_IsLiveIsFalse_HideScore()
        {
            // Arrange.
            _ticket.IsLive = false;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsFalse(actualResult.Score.Visible);
        }

        [Test]
        public void BuildScore_IsLiveIsTrue_ShowLiveIndcator()
        {
            // Arrange.
            _ticket.IsLive = true;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            StringAssert.Contains($"[{CoreBetList.Live}]", actualResult.Score);
        }

        [Test]
        public void BuildScore_IsLiveIsTrue_ShowLiveIndcatorInCorrectHtmlTag()
        {
            // Arrange.
            _ticket.IsLive = true;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            string expectedHtml = $"<span class=\"favorite\"> [{CoreBetList.Live}]</span>";

            Assert.AreEqual(expectedHtml, actualResult.Score.ToString());
        }

        [TestCase("b")]
        [TestCase("B")]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIsB_BetTeamClassNameIsFavorite(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("favorite", actualResult.betTeamClassName);
        }

        [TestCase("p")]
        [TestCase("P")]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIsP_BetTeamClassNameIsUnderdog(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("underdog", actualResult.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(actualResult.Handicap);
        }

        [Test]
        public void BuildBetType_Always_BetTypeIsNull()
        {
            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(actualResult.BetType);
        }

        [Test]
        public void BuildLeague_Always_LeageNameIsReplacedByTicketBetCheck()
        {
            // Arrange.
            string betCheck = "TXN123";

            _ticket.BetCheck = betCheck;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(betCheck, actualResult.League.LeagueName.leagueName);
        }

        [Test]
        public void BuildMatch_Always_WrapMatchNameInCssClassNamedBullfightingMatch()
        {
            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            StringAssert.StartsWith("<div class=\"bullfighting-match\">", actualResult.Match);
        }

        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("banker=LangKaoJomPiChit(AiJamPen);;ArenaCode=GYN12")]
        [TestCase("BANKER=LangKaoJomPiChit(AiJamPen);ArenaCode=GYN12")]
        [TestCase(";Banker=LangKaoJomPiChit(AiJamPen);ArenaCode=GYN12")]
        [TestCase(";Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("OtherInfo=OtherThing;Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12;OtherInfo=OtherThing")]
        public void BuildMatch_TransDescContainsCorrectFormatsOfBanker_ShowBankerBeforeVs(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());
            var actualDisplayingText = new Regex("</?[^<>]*>").Replace(actualResult.Match, string.Empty);

            // Assert.
            var expectedDisplayingText = "LangKaoJomPiChit(AiJamPen) - vs -";

            StringAssert.Contains(expectedDisplayingText, actualDisplayingText);
        }

        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("banker=LangKaoJomPiChit(AiJamPen);;ArenaCode=GYN12")]
        [TestCase("BANKER=LangKaoJomPiChit(AiJamPen);ArenaCode=GYN12")]
        [TestCase(";Banker=LangKaoJomPiChit(AiJamPen);ArenaCode=GYN12")]
        [TestCase(";Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("OtherInfo=OtherThing;Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12;OtherInfo=OtherThing")]
        public void BuildMatch_TransDescContainsCorrectFormatsOfBanker_ShowBankerInCorrectHtmlTag(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            var expectedDisplayingHtml = "<span class=\"banker\">LangKaoJomPiChit(AiJamPen)</span>";

            StringAssert.Contains(expectedDisplayingHtml, actualResult.Match);
        }

        [TestCase("panker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase(";ArenaCode=GYN12")]
        [TestCase("Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase(";Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("abcBanker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        public void BuildMatch_TransDescContainsIncorrectBanker_DoNotShowBankerName(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            var expectedDisplayingHtml = "<span class=\"banker\"></span> - vs -";

            StringAssert.Contains(expectedDisplayingHtml, actualResult.Match);
        }

        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("player=WanMeChai(PaTong);;ArenaCode=GYN12")]
        [TestCase("PLAYER=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase(";Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase(";Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("OtherInfo=OtherThing;Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12;OtherInfo=OtherThing")]
        public void BuildMatch_TransDescContainsCorrectFormatsOfPlayer_ShowPlayerAfterVs(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());
            var actualDisplayingText = new Regex("</?[^<>]*>").Replace(actualResult.Match, string.Empty);

            // Assert.
            var expectedDisplayingText = "- vs - WanMeChai(PaTong)";

            StringAssert.Contains(expectedDisplayingText, actualDisplayingText);
        }

        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("player=WanMeChai(PaTong);;ArenaCode=GYN12")]
        [TestCase("PLAYER=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase(";Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase(";Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("OtherInfo=OtherThing;Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12;OtherInfo=OtherThing")]
        public void BuildMatch_TransDescContainsCorrectFormatsOfPlayer_ShowPlayerInCorrectHtmlTag(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            var expectedDisplayingHtml = "<span class=\"player\">WanMeChai(PaTong)</span>";

            StringAssert.Contains(expectedDisplayingHtml, actualResult.Match);
        }

        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Blayer=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase(";ArenaCode=GYN12")]
        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);ArenaCode=GYN12")]
        [TestCase(";Banker=LangKaoJomPiChit(AiJamPen);ArenaCode=GYN12")]
        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);abcPlayer=WanMeChai(PaTong);ArenaCode=GYN12")]
        public void BuildMatch_TransDescContainsIncorrectPlayer_DoNotShowPlayerName(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            var expectedDisplayingHtml = "- vs - <span class=\"player\"></span>";

            StringAssert.Contains(expectedDisplayingHtml, actualResult.Match);
        }

        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("Player=WanMeChai(PaTong);;arenaCode=GYN12")]
        [TestCase("Player=WanMeChai(PaTong);arenacode=GYN12")]
        [TestCase(";Player=WanMeChai(PaTong);ARENACODE=GYN12")]
        [TestCase(";Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("OtherInfo=OtherThing;Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12;OtherInfo=OtherThing")]
        public void BuildMatch_TransDescContainsCorrectFormatsOfArenaCode_ShowArenaCodeAfterTheArenaText(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());
            var actualDisplayingText = new Regex("</?[^<>]*>").Replace(actualResult.Match, string.Empty);

            // Assert.
            var expectedDisplayingText = $"{BullfightingLabels.Arena}: GYN12";

            StringAssert.Contains(expectedDisplayingText, actualDisplayingText);
        }

        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("Player=WanMeChai(PaTong);;arenaCode=GYN12")]
        [TestCase("Player=WanMeChai(PaTong);arenacode=GYN12")]
        [TestCase(";Player=WanMeChai(PaTong);ARENACODE=GYN12")]
        [TestCase(";Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("OtherInfo=OtherThing;Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12;OtherInfo=OtherThing")]
        public void BuildMatch_TransDescContainsCorrectFormatsOfArenaCode_ShowArenaCodeInCorrectHtml(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            var expectedDisplayingHtml = $"<span class=\"arena-code\">{BullfightingLabels.Arena}:</span> GYN12";

            StringAssert.Contains(expectedDisplayingHtml, actualResult.Match);
        }

        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);Arena=GYN12")]
        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);")]
        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);abcArenaCode=GYN12")]
        public void BuildMatch_TransDescContainsIncorrectArenaCode_DoNotShowArenaCode(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            var expectedDisplayingHtml = $"<span class=\"arena-code\">{BullfightingLabels.Arena}:</span>  - ";

            StringAssert.Contains(expectedDisplayingHtml, actualResult.Match);
        }

        [Test]
        public void BuildMatch_Always_CallGetLeagueName3rdToGetArenaName()
        {
            // Arrange.
            const int LeagueId = 1;
            const string LeagueName = "League Name";

            _ticket.LeagueId = LeagueId;
            _ticketHelper.GetLeagueName3rd(Arg.Is(LeagueId.ToString())).Returns(LeagueName);

            // Act.
            var actualResult = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            var expectedDisplayingText = $"- {LeagueName}";

            StringAssert.Contains(expectedDisplayingText, actualResult.Match);
        }

        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("BANKER=LangKaoJomPiChit(AiJamPen);PLAYER=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase(";banker=LangKaoJomPiChit(AiJamPen);player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("OtherInfo=OtherThing;Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12;OtherInfo=OtherThing")]
        public void BuildBetTeamsInfo_TransDescContainsCorrectBankerAndPalyerPatterns_MatchIsAtTheSecondRow(string transDesc)
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.TransDesc = transDesc;

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            var expectedMatchName = "LangKaoJomPiChit(AiJamPen) - vs - WanMeChai(PaTong)";

            Assert.AreEqual(expectedMatchName, linesInActualText[1]);
        }

        [TestCase("Banker=LangKaoJomPiChit(AiJamPen);abcPlayer=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("banker=LangKaoJomPiChit(AiJamPen);ArenaCode=GYN12")]
        [TestCase("BANKER=LangKaoJomPiChit(AiJamPen);;ArenaCode=GYN12")]
        public void BuildBetTeamsInfo_TransDescOnlyContainsCorrectBanker_OnlyBankerNameIsAtTheSecondRow(string transDesc)
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.TransDesc = transDesc;

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            var expectedMatchName = "LangKaoJomPiChit(AiJamPen) - vs - ";

            Assert.AreEqual(expectedMatchName, linesInActualText[1]);
        }

        [TestCase("abcBanker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase(";PLAYER=WanMeChai(PaTong);ArenaCode=GYN12")]
        public void BuildBetTeamsInfo_TransDescOnlyContainsCorrectPlayer_OnlyPlayerNameIsAtTheSecondRow(string transDesc)
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.TransDesc = transDesc;

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            var expectedMatchName = " - vs - WanMeChai(PaTong)";

            Assert.AreEqual(expectedMatchName, linesInActualText[1]);
        }

        [TestCase("abcBanker=LangKaoJomPiChit(AiJamPen);abcPlayer=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("ArenaCode=GYN12")]
        [TestCase(";;ArenaCode=GYN12")]
        public void BuildBetTeamsInfo_TransDescContainsIncorrectBankerAndPlayer_OnlyVsIsAtTheSecondRow(string transDesc)
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.TransDesc = transDesc;

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            var expectedMatchName = " - vs - ";

            Assert.AreEqual(expectedMatchName, linesInActualText[1]);
        }

        [TestCase("abcBanker=LangKaoJomPiChit(AiJamPen);Player=WanMeChai(PaTong);ArenaCode=GYN12")]
        [TestCase("Player=WanMeChai(PaTong);arenaCode=GYN12")]
        [TestCase(";Player=WanMeChai(PaTong);arenacode=GYN12")]
        [TestCase(";Player=WanMeChai(PaTong);ARENACODE=GYN12")]
        public void BuildBetTeamsInfo_TransDescContainsCorrectArenaCode_ShowArenaCodeAtTheThirdRow(string transDesc)
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.TransDesc = transDesc;

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            var expectedArenaInfo = $"{BullfightingLabels.Arena}: GYN12";

            StringAssert.Contains(expectedArenaInfo, linesInActualText[2]);
        }

        [Test]
        public void BuildBetTeamsInfo_Always_AppendArenaNameAtTheEndOfThirdRow()
        {
            // Arrange.
            const int LeagueId = 1;
            const string LeagueName = "League Name";
            var rtfHelper = new RTFHelper().Clone();

            _ticket.LeagueId = LeagueId;
            _ticketHelper.GetLeagueName3rd(Arg.Is(LeagueId.ToString())).Returns(LeagueName);

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedArenaNameText = $"- {LeagueName}";

            StringAssert.Contains(expectedArenaNameText, linesInActualText[2]);
        }
    }
}