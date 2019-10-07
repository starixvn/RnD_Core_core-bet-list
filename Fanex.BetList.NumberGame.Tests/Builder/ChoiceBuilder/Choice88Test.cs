namespace Fanex.BetList.NumberGame.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using Core.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice88Test
    {
        private Choice88 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice88();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = NumberGameBetTypes.Warrior;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildScore_Always_SetScoreBlockIsNull()
        {
            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(choiceTemplate.Score);
        }

        [Test]
        public void BuildMatch_Always_SetMatchVSBlockIsNull()
        {
            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(choiceTemplate.Match.VS);
        }

        [Test]
        public void BuildMatch_Always_SetHomeTeamIsJoinedByNumberGameNoResourceAndMatchCode()
        {
            // Arrange
            _ticket.MatchCode = "6969";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedHomeTeam = $"{NumberGameLang.numbergameno}&nbsp;{_ticket.MatchCode}";
            Assert.AreEqual(expectedHomeTeam, _choice.Template.Match.homeTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsEmpty()
        {
            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(choiceTemplate.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("favorite", choiceTemplate.betTeamClassName);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsh_SetBetTeamIsWarrior2ndBallResource()
        {
            // Arrange
            _ticket.BetTeam = "h";

            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(NumberGameLang.warrior2ndBall, choiceTemplate.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsNoth_SetBetTeamIsWarrior3rdBallResource()
        {
            // Arrange
            _ticket.BetTeam = "h87898";

            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(NumberGameLang.warrior3rdBall, choiceTemplate.betTeam);
        }
    }
}