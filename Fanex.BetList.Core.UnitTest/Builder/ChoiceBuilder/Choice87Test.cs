namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    ///  Unit testing class for bet type 87.
    /// </summary>
    public class Choice87Test
    {
        // ticket.Live, betId, ticket.BetTeam, expectedValue
        private static object[] betTeamNames =
        {
            new object[] { true, 0, "h", string.Join(null, CoreBetList.high, " [0]") },
            new object[] { false, 0, "h", string.Join(null, CoreBetList.high, " [0]") },
            new object[] { true, 1, "h", string.Join(null, CoreBetList.high, " [1]") },
            new object[] { false, 1, "h", string.Join(null, CoreBetList.high, " [0]") },
            new object[] { true, 0, "is not h", string.Join(null, CoreBetList.low, " [0]") },
            new object[] { false, 0, "is not h", string.Join(null, CoreBetList.low, " [0]") },
            new object[] { true, 1, "is not h", string.Join(null, CoreBetList.low, " [1]") },
            new object[] { false, 1, "is not h", string.Join(null, CoreBetList.low, " [0]") },
        };

        private Choice87 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice87();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Next_High_Low;
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
        public void BuildMatch_Always_SetHomeTeamIsJoinedByNumberGameNoResourceAndMatchCode()
        {
            // Arrange
            _ticket.MatchCode = "6969";

            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(string.Join(null, CoreBetList.numbergameno, "&nbsp;", _ticket.MatchCode), choiceTemplate.Match.homeTeam);
        }

        /// <summary>
        /// Check whether build choice function render choice template right.
        /// </summary>
        /// <param name="isLive"> Indicate ticket is live or not.</param>
        /// <param name="betId"> Bet id of the ticket.</param>
        /// <param name="ticketBetTeam"> Bet team of ticket.</param>
        /// <param name="expectedBetTeam"> Expected bet team rendered by build choice function.</param>
        [Test, TestCaseSource("betTeamNames")]
        public void BuildBetTeam_WhenCalled_SetValidNameForBetTeam(bool isLive, long betId, string ticketBetTeam, string expectedBetTeam)
        {
            // Arrange
            _ticket.IsLive = isLive;
            _ticket.BetId = betId;
            _ticket.BetTeam = ticketBetTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }
    }
}