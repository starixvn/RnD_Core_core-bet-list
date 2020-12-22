namespace Fanex.BetList.NumberGame.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using Core.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice3601Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice3601();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = Happy5LinkedGamesBetTypes.OverUnder;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketHelper.GetResourceData(Choice3601.BetChoiceRefId, "3601_0").Returns("choice0");
            _ticketHelper.GetResourceData(Choice3601.BetChoiceRefId, "3601_1").Returns("choice1");
        }

        [TestCase(null)]
        [TestCase("")]
        public void BuildBetTeam_TransDescIsNullOrEmpty_BetChoiceIsEmpty(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TransDescIsNotNullOrEmpty_CallRemotingRefToGetBetChoice()
        {
            // Arrange.
            _ticket.TransDesc = "BetContent=01221";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper
                .Received()
                .GetResourceData(Arg.Is(Choice3601.BetChoiceRefId), Arg.Any<string>());
        }

        [Test]
        public void BuildBetTeam_Always_TrimTransDescBeforeCallingRemotingRef()
        {
            // Arrange.
            _ticket.TransDesc = "BetContent=01221 ";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.

            _ticketHelper
                .DidNotReceive()
                .GetResourceData(Arg.Is(Choice3601.BetChoiceRefId), Arg.Is<string>(betTeam => string.IsNullOrEmpty(betTeam)));
        }

        [Test]
        public void BuildBetTeam_Always_SupportSingeChoice()
        {
            // Arrange.
            _ticket.TransDesc = "BetContent=0";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("[choice0]", _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_Always_SupportMultipChoice()
        {
            // Arrange.
            _ticket.TransDesc = "BetContent=01";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("[choice0], [choice1]", _choice.Template.betTeam);
        }

        [Test]
        public void BuildMatch_Always_MatchIsCombinedFromBetCheck()
        {
            // Arrange.
            _ticket.BetCheck = "P20-2101-00130";

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedMatch = string.Format(NumberGameLang.Number, "00130");
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
            const string betTeam = "BetContent=0";
            const string matchCode = "00130";

            _ticket.BetTypeId = Happy5BetTypes.BigSmall;
            _ticket.TransDesc = betTeam;
            _ticket.BetCheck = $"p20-2101-{matchCode}";

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, new RTFHelper().Clone());
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var expectedMatch = string.Format(NumberGameLang.Number, matchCode);

            Assert.AreEqual(expectedMatch, linesInActualText[1]);
        }

        [Test]
        public void BuildLeague_Always_ReturnEmpty()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(template.League.LeagueName.leagueName);

        }
    }
}