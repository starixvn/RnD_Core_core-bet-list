namespace Fanex.BetList.NumberGame.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using Core.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice89Test
    {
#pragma warning disable S1144 // Unused private types or members should be removed

        private static readonly object[] betTeamNames =
        {
            // ticket.Live, betId, ticket.BetTeam, expectedValue
            new object[] { true, 0, "1:1", string.Join(null, NumberGameLang.lblOverOdd,  " [0]") },
            new object[] { true, 0, "1:2", string.Join(null, NumberGameLang.lblOverEven,  " [0]") },
            new object[] { true, 0, "2:1", string.Join(null, NumberGameLang.lblUnderOdd,  " [0]") },
            new object[] { true, 0, "2:2", string.Join(null, NumberGameLang.lblUnderEven,  " [0]") },

            new object[] { false, 0, "1:1", string.Join(null, NumberGameLang.lblOverOdd,  " [0]") },
            new object[] { false, 0, "1:2", string.Join(null, NumberGameLang.lblOverEven,  " [0]") },
            new object[] { false, 0, "2:1", string.Join(null, NumberGameLang.lblUnderOdd,  " [0]") },
            new object[] { false, 0, "2:2", string.Join(null, NumberGameLang.lblUnderEven,  " [0]") },

            new object[] { true, 1, "1:1", string.Join(null, NumberGameLang.lblOverOdd, " [1]") },
            new object[] { true, 1, "1:2", string.Join(null, NumberGameLang.lblOverEven,  " [1]") },
            new object[] { true, 1, "2:1", string.Join(null, NumberGameLang.lblUnderOdd,  " [1]") },
            new object[] { true, 1, "2:2", string.Join(null, NumberGameLang.lblUnderEven,  " [1]") },

            new object[] { false, 1, "1:1", string.Join(null, NumberGameLang.lblOverOdd,  " [0]") },
            new object[] { false, 1, "1:2", string.Join(null, NumberGameLang.lblOverEven,  " [0]") },
            new object[] { false, 1, "2:1", string.Join(null, NumberGameLang.lblUnderOdd,  " [0]") },
            new object[] { false, 1, "2:2", string.Join(null, NumberGameLang.lblUnderEven, " [0]") }
        };

#pragma warning restore S1144 // Unused private types or members should be removed

        private Choice89 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice89();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = NumberGameBetTypes.NextCombo;
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
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, false);
            var actualHomeTeam = choiceTemplate.Match.homeTeam;

            // Assert
            var expectedHomeTeam = $"{NumberGameLang.numbergameno}&nbsp;{_ticket.MatchCode}";
            Assert.AreEqual(expectedHomeTeam, actualHomeTeam);
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

        [Test, TestCaseSource(nameof(betTeamNames))]
        public void BuildBetTeam_WhenCalled_SetValidNameForBetTeam(bool isLive, long betId, string ticketBetTeam, string expectedBetTeam)
        {
            // Arrange
            _ticket.IsLive = isLive;
            _ticket.BetId = betId;
            _ticket.BetTeam = ticketBetTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }
    }
}