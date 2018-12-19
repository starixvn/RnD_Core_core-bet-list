namespace Fanex.BetList.NumberGame.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using Core.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice8101Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice8101();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase(null)]
        [TestCase("")]
        public void BuildBetTeam_BetTeamInTicketIsNullOrEmpty_BetChoiceIsEmpty(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamInTicketIsNotNullOrEmpty_CallRemotingRefToGetBetChoice()
        {
            // Arrange.
            _ticket.BetTeam = "b";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper
                .Received()
                .GetResourceData(Arg.Is(Choice8101.BetChoiceRefId), Arg.Any<string>());
        }

        [Test]
        public void BuildBetTeam_Always_LowercaseBetTeamBeforeCallingRemotingRef()
        {
            // Arrange.
            _ticket.BetTeam = "B";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBetTeam = _ticket.BetTeam.ToLowerInvariant();

            _ticketHelper
                .Received()
                .GetResourceData(Arg.Is(Choice8101.BetChoiceRefId), Arg.Is<string>(betTeam => betTeam == expectedBetTeam));
        }

        [Test]
        public void BuildMatch_Always_MatchIsCombinedFromMatchCode()
        {
            // Arrange.
            const string matchCode = "1234";

            _ticket.MatchCode = matchCode;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedMatch = string.Format(NumberGameLang.Number, matchCode);
            var expectedMatchHtml = $"<div class=\"match\">{expectedMatch}</div>";

            Assert.AreEqual(expectedMatchHtml, template.Match.ToString());
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
        public void BuildScore_Always_HideScore()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Score);
        }

        [Test]
        public void BuildBetTeamsInfo_Always_ThirdRowIsMatch()
        {
            // Arrange.
            const string betTeam = "b";
            const string betChoice = "Big";
            const string matchCode = "2222";
            const string betTypeName = "Big/Small";
            const int betTypeId = 8101;

            _ticket.BetTypeId = betTypeId;
            _ticket.BetTeam = betTeam;
            _ticket.MatchCode = matchCode;

            _ticketHelper
                .GetResourceData(Arg.Is(Choice8101.BetChoiceRefId), Arg.Is(betTeam))
                .Returns(betChoice);
            _ticketHelper.GetBetTypeNameById(Arg.Is(betTypeId), Arg.Any<string>()).Returns(betTypeName);

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, new RTFHelper().Clone());
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedMatch = string.Format(NumberGameLang.Number, matchCode);

            Assert.AreEqual(expectedMatch, linesInActualText[2]);
        }
    }
}