namespace Fanex.BetList.NumberGame.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using Core.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice87Test
    {
#pragma warning disable S1144 // Unused private types or members should be removed

        // ticket.Live, betId, ticket.BetTeam, expectedValue
        private static readonly object[] betTeamNames =
        {
            new object[] { true, 0, "h", string.Join(null, NumberGameLang.high, " [0]") },
            new object[] { false, 0, "h", string.Join(null, NumberGameLang.high, " [0]") },
            new object[] { true, 1, "h", string.Join(null, NumberGameLang.high, " [1]") },
            new object[] { false, 1, "h", string.Join(null, NumberGameLang.high, " [0]") },
            new object[] { true, 0, "is not h", string.Join(null, NumberGameLang.low, " [0]") },
            new object[] { false, 0, "is not h", string.Join(null, NumberGameLang.low, " [0]") },
            new object[] { true, 1, "is not h", string.Join(null, NumberGameLang.low, " [1]") },
            new object[] { false, 1, "is not h", string.Join(null, NumberGameLang.low, " [0]") },
        };

#pragma warning restore S1144 // Unused private types or members should be removed

        private Choice87 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice87();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = NumberGameBetTypes.NextHighLow;
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

            // Assert
            Assert.AreEqual($"{NumberGameLang.numbergameno}&nbsp;{_ticket.MatchCode}", choiceTemplate.Match.homeTeam);
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