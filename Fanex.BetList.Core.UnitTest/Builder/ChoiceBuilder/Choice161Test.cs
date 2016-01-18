namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    ///  Unit test for Choice161.
    /// </summary>
    [TestFixture]
    public class Choice161Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice161();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes._3rdExactHomeTeamGoals;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("g0")]
        [TestCase("G0")]
        public void BuildBetTeam_BetTeamIsG0_TemplateBetTeamIs0Goals(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.ZeroGoals, _choice.Template.betTeam);
        }

        [TestCase("g1")]
        [TestCase("G1")]
        public void BuildBetTeam_BetTeamIsG1_TemplateBetTeamIs1Goal(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.OneGoal, _choice.Template.betTeam);
        }

        [TestCase("g2")]
        [TestCase("G2")]
        public void BuildBetTeam_BetTeamIsG2_TemplateBetTeamIs2Goals(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.TwoGoals, _choice.Template.betTeam);
        }

        [TestCase("g3")]
        [TestCase("G3")]
        public void BuildBetTeam_BetTeamIsG3_TemplateBetTeamIs3GoalsAndOver(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.ThreeGoalsAndOver, _choice.Template.betTeam);
        }

        [TestCase("abc")]
        [TestCase("")]
        public void BuildBetTeam_BetTeamIsInvalid_TemplateBetTeamIsEmpty(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }
    }
}