namespace Fanex.BetList.NumberGame.Tests.Builder.ChoiceBuilder
{
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using Core.Resources;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice85Test
    {
        private static readonly string spanClassWith37point5 = " <span style='color:#555555'>37.5</span> ";

        // ticket.Live, betId, expectedValue
#pragma warning disable S1144 // Unused private types or members should be removed

        private static readonly object[] liveScores =
        {
            new object[] { true, 0, " [0]" },
            new object[] { true, 2, " [2]" },
            new object[] { false, 0, " [0]" },
            new object[] { false, 1,  " [0]" }
        };

#pragma warning restore S1144 // Unused private types or members should be removed

        private Choice85 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice85();
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
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(string.Join(null, NumberGameLang.numbergameno, "&nbsp;", _ticket.MatchCode), _choice.Template.Match.homeTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
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
        public void BuildBetType_TicketIsNotLive_SetBetTypeNameIsFtResourcePointAndBetTypeName()
        {
            // Arrange
            const string betTypeName = "A Bettype Name";

            _ticketHelper.GetBetTypeNameById(Arg.Any<int>(), Arg.Any<object>()).Returns(betTypeName);
            _ticket.IsLive = false;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual($"{CoreBetList.ft}. {betTypeName}", _choice.Template.BetType.betTypeName);
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
            Assert.AreEqual($"{NumberGameLang.next}&nbsp;{betTypeName}", _choice.Template.BetType.betTypeName);
        }

        [Test, TestCaseSource(nameof(liveScores))]
        public void BuildBetTeam_Always_SetBetTeamIsValid(bool isLive, long betId, string liveScore)
        {
            // Arrange
            _ticket.IsLive = isLive;
            _ticket.BetId = betId;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.under + spanClassWith37point5 + liveScore, _choice.Template.betTeam);
        }
    }
}