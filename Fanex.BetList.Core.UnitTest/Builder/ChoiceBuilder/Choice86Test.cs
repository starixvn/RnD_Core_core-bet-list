namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Choice86Test
    {
        private static object[] liveScores =
        {
            // ticket.Live, betId, expectedValue
            new object[] { true, 0, " [0]" },
            new object[] { true, 2, " [2]" },
            new object[] { false, 0, string.Empty },
            new object[] { false, 1,  string.Empty }
        };

        private Choice86 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice86();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.N_Over_Under;
            _ticketHelper = Substitute.For<ITicketHelper>();
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

        /// <summary>
        ///  Check build choice function render template choice has bet type name is full time resource and bet type name.
        /// </summary>
        [Test]
        public void BuildBetType_TicketIsNotLive_SetBetTypeNameIsFtResourcePointAndBetTypeName()
        {
            // Arrange
            var betTypeName = "A Bettype Name";
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns(betTypeName);
            _ticket.IsLive = false;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var extected = string.Join(null, CoreBetList.ft, ". ", betTypeName);
            Assert.AreEqual(extected, _choice.Template.BetType.betTypeName);
        }

        /// <summary>
        ///  Check build choice function render template choice has bet type name is next resource and bet type name.
        /// </summary>
        [Test]
        public void BuildBetType_TicketIsLive_SetBetTypeNameIsNextResourceBreakingAndBetTypeName()
        {
            // Arrange
            var betTypeName = "A Bettype Name";
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns(betTypeName);
            _ticket.IsLive = true;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var exptected = string.Join(null, CoreBetList.next, "&nbsp;", betTypeName);
            Assert.AreEqual(exptected, _choice.Template.BetType.betTypeName);
        }

        /// <summary>
        /// Check whether build choice function return valid bet team or not.
        /// </summary>
        /// <param name="isLive"> Is live ticket.</param>
        /// <param name="betId"> Bet id of ticket.</param>
        /// <param name="liveScore"> Live score status of ticket.</param>
        [Test, TestCaseSource("liveScores")]
        public void BuildBetTeam_WhenCalled_SetValidNameForBetTeam(bool isLive, long betId, string liveScore)
        {
            // Arrange
            _ticket.IsLive = isLive;
            _ticket.BetId = betId;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedBetTeam = CoreBetList.even + liveScore;
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        ///  Check build choice function set home team block is number game resource and match code.
        /// </summary>
        [Test]
        public void BuildMatch_Always_SetHomeTeamIsJoinedByNumberGameNoResourceAndMatchCode()
        {
            // Arrange
            _ticket.MatchCode = "6969";

            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());
            var actualHomeTeam = choiceTemplate.Match.homeTeam;

            // Assert
            var expectedHomeTeam = string.Join(null, CoreBetList.numbergameno, "&nbsp;", _ticket.MatchCode);
            Assert.AreEqual(expectedHomeTeam, actualHomeTeam);
        }
    }
}