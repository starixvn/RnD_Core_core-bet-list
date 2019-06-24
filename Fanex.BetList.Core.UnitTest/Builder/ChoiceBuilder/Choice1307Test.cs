namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1307Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1307();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.MatchTotalGames3Way;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_BetTeamIs1_BetTeamIsOverResource()
        {
            // Arrange
            _ticket.BetTeam = "1";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.over, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs2_BetTeamIsUnderResource()
        {
            // Arrange
            _ticket.BetTeam = "2";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.under, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamNotIs1AndNotIs2_BetTeamIsBetweenResourceHandicap2AndHandicap1()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format(CoreBetList.betweenXandY, _ticket.Handicap2, _ticket.Handicap1);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_Always_SetBetTypeNameIsBetTypeName()
        {
            // Arrange
            _ticketHelper.GetBetTypeNameById(Arg.Any<object>(), Arg.Any<object>(), Arg.Any<object>(), Arg.Any<object>()).Returns("Bet type name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("Bet type name", _choice.Template.BetType.betTypeName);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs1_BetTeamClassNameIsFavorite()
        {
            // Arrange
            _ticket.BetTeam = "1";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIsNot1_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_UNDERDOG = "underdog";
            Assert.AreEqual(CSS_CLASS_UNDERDOG, _choice.Template.betTeamClassName);
        }
    }
}