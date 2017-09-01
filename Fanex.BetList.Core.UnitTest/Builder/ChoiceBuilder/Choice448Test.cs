namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Templates;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice448Test
    {
        private const string HomeTeamName = "Home team name";
        private const string AwayTeamName = "Away team name";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice448();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.HomeId = 1;
            _ticket.AwayId = 2;

            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns(HomeTeamName);
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns(AwayTeamName);
        }

        [TestCase("h")]
        [TestCase("H")]
        public void BuildBetTeam_TicketBetTeamIsH_BetTeamIsHomeTeamName(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(HomeTeamName, template.betTeam);
        }

        [TestCase("a")]
        [TestCase("A")]
        public void BuildBetTeam_TicketBetTeamIsA_BetTeamIsAwayTeamName(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(AwayTeamName, template.betTeam);
        }

        [TestCase("ng")]
        [TestCase("NG")]
        [TestCase("Ng")]
        [TestCase("nG")]
        public void BuildBetTeam_TicketBetTeamIsNG_BetTeamIsNoGoal(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(CoreBetList.nogoal, template.betTeam);
        }

        [TestCase("abc")]
        [TestCase("")]
        public void BuildBetTeam_TicketBetTeamIsNotHOrAOrNG_BetTeamIsEmpty(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
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