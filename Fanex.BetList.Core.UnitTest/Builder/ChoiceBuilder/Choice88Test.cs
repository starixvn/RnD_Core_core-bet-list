namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    ///  Unit testing class for bet type 88.
    /// </summary>
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
            _ticket.BetTypeId = BetTypes.Warrior;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildScore_Always_SetScoreBlockIsNull()
        {
            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(choiceTemplate.Score);
        }

        [Test]
        public void BuildMatch_Always_SetMatchVSBlockIsNull()
        {
            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(choiceTemplate.Match.VS);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsEmpty()
        {
            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNullOrEmpty(choiceTemplate.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("favorite", choiceTemplate.betTeamClassName);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsh_SetBetTeamIsWarrior2ndBallResource()
        {
            // Arrange
            _ticket.BetTeam = "h";

            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.warrior2ndBall, choiceTemplate.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsNoth_SetBetTeamIsWarrior3rdBallResource()
        {
            // Arrange
            _ticket.BetTeam = "h87898";

            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.warrior3rdBall, choiceTemplate.betTeam);
        }

        /// <summary>
        ///  Build choice function always set bet team by combined number game no and match code.
        /// </summary>
        [Test]
        public void BuildMatch_Always_SetHomeTeamIsJoinedByNumberGameNoResourceAndMatchCode()
        {
            // Arrange
            _ticket.MatchCode = "6969";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedHomeTeam = string.Join(null, CoreBetList.numbergameno, "&nbsp;", _ticket.MatchCode);
            Assert.AreEqual(expectedHomeTeam, _choice.Template.Match.homeTeam);
        }
    }
}