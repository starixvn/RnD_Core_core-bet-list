namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice14Test
    {
#pragma warning disable S1144 // Unused private types or members should be removed

        private static readonly object[] expectedBetTeams =
        {
            new object[] { "0:0", CoreBetList.NoGoal14 },
            new object[] { "1:1", CoreBetList.HomeFG },
            new object[] { "1:2", CoreBetList.HomeLG },
            new object[] { "2:1", CoreBetList.AwayFG },
            new object[] { "2:2", CoreBetList.AwayLG }
        };

#pragma warning restore S1144 // Unused private types or members should be removed

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice14();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.FirstGoalLastGoal;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeam_Always_SetBetTeamIsEmpty()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }

        [Test, TestCaseSource("expectedBetTeams")]
        public void BuildBetTeam_Always_SetRightBetTeam(string betTeam, string expectedResult)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedResult, _choice.Template.betTeam);
        }
    }
}