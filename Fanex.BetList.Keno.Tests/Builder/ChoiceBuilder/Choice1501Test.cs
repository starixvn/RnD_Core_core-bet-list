namespace Fanex.BetList.Keno.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice1501Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1501();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = 1501;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapBlockIsNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsFavorite()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("favorite", template.betTeamClassName);
        }

        [Test]
        public void BuildMatch_Always_MatchBlockIsNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Match);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void BuildScore_Always_ScoreBlockIsNull(bool isLive)
        {
            // Arrange.
            _ticket.IsLive = isLive;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(template.Score);
        }

        [TestCase("betChoice=1705152116,3;referID=", "3")]
        [TestCase("BetChoice=1705152116,3;referID=", "3")]
        [TestCase("betchoice=1705152116,3;referID=", "3")]
        [TestCase("betChoice=1705152116,3;", "3")]
        [TestCase("betChoice=1705152116,3", "3")]
        public void BuildBetTeam_TransDescIsCorrectAndDoesNotContainSpecialMap_ParseAndSendCorrectChoiceIdToRemotingRef(string transDesc, string choiceId)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetKenoBetChoiceInfo(Arg.Is(choiceId));
        }

        [TestCase("betChoice=1705152116,33;referID=", "35", "Index Big Bets: {0} Range: {1}")]
        [TestCase("betChoice=1705152116,34;referID=", "36", "Index Small Bets: {0} Range: {1}")]
        public void BuildBetTeam_ChoiceIdIs33Or34_AppendBetsAndRangeToMemberChoice(string transDesc, string choiceId, string betChoice)
        {
            // Arrange.
            _ticket.Stake = 150;
            _ticket.BetCheck = "100";
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetKenoBetChoiceInfo(Arg.Is(choiceId)).Returns(new BetChoiceInfo(betChoice, choiceId));

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            string expectedBetTeam = string.Format(betChoice, 1.5, 100);

            Assert.AreEqual(expectedBetTeam, template.betTeam);
        }

        [TestCase("betChoice=1705152116,;referID=")]
        [TestCase("BetChoice=1705152116;referID=")]
        [TestCase(";referID=")]
        [TestCase("referID=")]
        [TestCase("")]
        [TestCase(null)]
        public void BuildBetTeam_TransDescIsIncorrect_BetTeamIsEmpty(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(string.Empty, template.betTeam);
        }

        [Test]
        public void BuildBetType_Always_GetBetTypeAssociateWithChoiceIdFromRemotingRef()
        {
            // Arrange.
            const string BetTypeName = "Bet Type Name";

            _ticketHelper.GetKenoBetTypeName(Arg.Any<string>()).Returns(BetTypeName);

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(BetTypeName, template.BetType.betTypeName);
        }

        [TestCase("betChoice=1705152116,3;referID=", "1705152116")]
        [TestCase("BetChoice=1705152116,3;referID=", "1705152116")]
        [TestCase("betchoice=1705152116,3;referID=", "1705152116")]
        [TestCase("betChoice=1705152116,3;", "1705152116")]
        [TestCase("betChoice=1705152116,3", "1705152116")]
        public void BuildLeague_TransDescIsCorrect_GameIdIsInLeagueName(string transDesc, string expectedGameId)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.True(template.League.LeagueName.leagueName.Contains($"[{expectedGameId}]"));
        }

        [TestCase("bet=1705152116,3;referID=")]
        [TestCase("=1705152116,3;referID=")]
        [TestCase("1705152116,3;referID=")]
        [TestCase("3;")]
        [TestCase(",3")]
        [TestCase(";referID=")]
        [TestCase("referID=")]
        [TestCase("")]
        [TestCase(null)]
        public void BuildLeague_TransDescIsIncorrect_GameIdInLeagueNameIsEmpty(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.True(template.League.LeagueName.leagueName.Contains("[]"));
        }
    }
}