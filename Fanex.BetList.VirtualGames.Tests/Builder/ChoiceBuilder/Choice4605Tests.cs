namespace Fanex.BetList.VirtualGames.Tests.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice4605Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice4605();
            _ticket = TestData.CreateTicket(VirtualGamesBetTypes.SoccerMines);
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase(4608)]
        [TestCase(4609)]
        [TestCase(4610)]
        public void BuildBetTeam_TowerGame_DontShowBetChoice(int bettype)
        {
            // Arrange.
            _ticket.BetTypeId = bettype;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(template.betTeam);
        }

        [TestCase("betChoice=1", "1")]
        [TestCase("BetChoice=2;referID=", "2")]
        [TestCase("referId=111,betchoice=3", "3")]
        [TestCase("BETCHOICE=4,referID=3;", "4")]
        [TestCase("betChoice=5,", "5")]
        public void BuildBetTeam_TransDescContainsBetChoice_SendBetChoiceIdToRemotingRefWithAssociateRefId(string transDesc, string betChoiceId)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is(Choice4601.ChoiceRefId), Arg.Is(betChoiceId));
        }

        [TestCase("")]
        [TestCase("betChoiceId=1")]
        [TestCase("referId=111")]
        [TestCase("BetChoiceId=4,referID=3;")]
        public void BuildBetTeam_TransDescDoesNotContainBetChoice_DoNotCallRemotingRefToGetBetChoice(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper
                .DidNotReceive()
                .GetResourceData(Arg.Is(Choice4601.ChoiceRefId), Arg.Any<string>());
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
            const string expectedCssClassName = "favorite";

            Assert.AreEqual(expectedCssClassName, template.betTeamClassName);
        }

        [Test]
        public void BuildScore_Always_ScoreBlockIsNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(template.Score);
        }

        [Test]
        public void BuildMatch_Always_MatchBlockIsNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Match);
        }

        [Test]
        public void BuildLeague_ValidBetCheck_AppendBetCheckToLeagueNameAsGameId()
        {
            // Arrange.
            const string betCheck = "LB022001010002";

            _ticket.BetCheck = betCheck;
            
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedGameId = $"[{betCheck}]";

            Assert.AreEqual(template.League.LeagueName.leagueName, expectedGameId);
        }
    }
}