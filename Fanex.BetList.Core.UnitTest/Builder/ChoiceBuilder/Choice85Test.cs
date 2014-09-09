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
    public class Choice85Test
    {
        private static string spanClassWith37point5 = " <span style='color:#555555'>37.5</span> ";

        // ticket.Live, betId, expectedValue
        private static object[] liveScores =
        {
            new object[] { true, 0, " [0]" },
            new object[] { true, 2, " [2]" },
            new object[] { false, 0, " [0]" },
            new object[] { false, 1,  " [0]" }
        };

        private Choice85 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice85();
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
        ///  Test whether bet type is combined by core bet list resource full time and bet type name get from ticket.
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
            Assert.AreEqual(string.Join(null, CoreBetList.ft, ". ", betTypeName), _choice.Template.BetType.betTypeName);
        }

        /// <summary>
        ///  Test whether bet type is combined by core bet list resource next and bet type name get from ticket.
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
            Assert.AreEqual(string.Join(null, CoreBetList.next, "&nbsp;", betTypeName), _choice.Template.BetType.betTypeName);
        }

        /// <summary>
        ///  Check whether bet team is valid.
        /// </summary>
        /// <param name="isLive"> Is live ticket.</param>
        /// <param name="betId"> Bet id of ticket.</param>
        /// <param name="liveScore"> Live score string.</param>
        [Test, TestCaseSource("liveScores")]
        public void BuildBetTeam_Always_SetBetTeamIsValid(bool isLive, long betId, string liveScore)
        {
            // Arrange
            _ticket.IsLive = isLive;
            _ticket.BetId = betId;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.under + spanClassWith37point5 + liveScore, _choice.Template.betTeam);
        }

        [Test]
        public void BuildMatch_Always_SetHomeTeamIsJoinedByNumberGameNoResourceAndMatchCode()
        {
            // Arrange
            _ticket.MatchCode = "6969";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(string.Join(null, CoreBetList.numbergameno, "&nbsp;", _ticket.MatchCode), _choice.Template.Match.homeTeam);
        }
    }
}