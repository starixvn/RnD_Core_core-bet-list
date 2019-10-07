namespace Fanex.BetList.NumberGame.Tests.Builder.ChoiceBuilder
{
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using Core.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice86Test
    {
#pragma warning disable S1144 // Unused private types or members should be removed

        private static readonly object[] liveScores =
        {
            // ticket.Live, betId, expectedValue
            new object[] { true, 0, " [0]" },
            new object[] { true, 2, " [2]" },
            new object[] { false, 0, string.Empty },
            new object[] { false, 1,  string.Empty }
        };

#pragma warning restore S1144 // Unused private types or members should be removed

        private Choice86 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice86();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = NumberGameBetTypes.OverUnder;
            _ticketHelper = Substitute.For<ITicketHelper>();
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
            Assert.IsTrue(string.IsNullOrEmpty(choiceTemplate.Handicap.handicap));
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
        public void BuildBetType_TicketIsNotLive_SetBetTypeNameIsFtResourcePointAndBetTypeName()
        {
            // Arrange
            const string betTypeName = "A Bettype Name";

            _ticketHelper.GetBetTypeNameById(Arg.Any<int>(), Arg.Any<object>()).Returns(betTypeName);
            _ticket.IsLive = false;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var extected = $"{CoreBetList.ft}. {betTypeName}";
            Assert.AreEqual(extected, _choice.Template.BetType.betTypeName);
        }

        [Test]
        public void BuildBetType_TicketIsLive_SetBetTypeNameIsNextResourceBreakingAndBetTypeName()
        {
            // Arrange
            const string betTypeName = "A Bettype Name";

            _ticketHelper.GetBetTypeNameById(Arg.Any<int>(), Arg.Any<object>()).Returns(betTypeName);
            _ticket.IsLive = true;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var exptected = $"{NumberGameLang.next}&nbsp;{betTypeName}";
            Assert.AreEqual(exptected, _choice.Template.BetType.betTypeName);
        }

        [Test, TestCaseSource(nameof(liveScores))]
        public void BuildBetTeam_WhenCalled_SetValidNameForBetTeam(bool isLive, long betId, string liveScore)
        {
            // Arrange
            _ticket.IsLive = isLive;
            _ticket.BetId = betId;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedBetTeam = CoreBetList.even + liveScore;
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }
    }
}