namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Templates;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice22Test
    {
        private const string HomeTeamName = "Home team name";
        private const string AwayTeamName = "Away team name";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice22();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.HomeId = 1;
            _ticket.AwayId = 2;

            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns(HomeTeamName);
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns(AwayTeamName);
        }

        [Test]
        public void BuildBetTeam_TicketBetTeamIs1_BetTeamIsHomeTeamName()
        {
            // Arrange.
            _ticket.BetTeam = "1";

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(HomeTeamName, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TicketBetTeamIs2_BetTeamIsAwayTeamName()
        {
            // Arrange.
            _ticket.BetTeam = "2";

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(AwayTeamName, template.betTeam);
        }

        [TestCase("x")]
        [TestCase("X")]
        public void BuildBetTeam_TicketBetTeamIsX_BetTeamIsNone(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.None22, template.betTeam);
        }

        [TestCase("abc")]
        [TestCase("")]
        public void BuildBetTeam_TicketBetTeamIsNot1OrXOr2_BetTeamIsEmpty(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapToNull()
        {
            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsNull(template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameToFavorite()
        {
            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string Favorite = "favorite";

            Assert.AreEqual(Favorite, template.betTeamClassName);
        }
    }
}