namespace Fanex.BetList.NumberGame.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using Core.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice3604Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice3604();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = Happy5LinkedGamesBetTypes.Soccer5;
            _ticket.SportTypeId = 168;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketHelper.GetResourceData(Choice3601.BetChoiceRefId, "3604_1").Returns("Soccer 5");
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
        public void BuildBetTeam_Always_SupportSingeChoice()
        {
            // Arrange.
            _ticket.TransDesc = "BetContent=3604_1";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("Soccer 5", _choice.Template.betTeam);
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
        public void BuildLeague_Always_ReturnEmpty()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(template.League.LeagueName.leagueName);

        }
    }
}