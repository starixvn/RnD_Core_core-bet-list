namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Templates;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice452Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice452();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("1H")]
        [TestCase("1h")]
        public void BuildBetTeam_TicketBetTeamIs1H_BetTeamIsFirstHalf(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.FirstHalf, template.betTeam);
        }

        [TestCase("2H")]
        [TestCase("2h")]
        public void BuildBetTeam_TicketBetTeamIs2H_BetTeamIsSecondHalf(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.SecondHalf, template.betTeam);
        }

        [TestCase("abc")]
        [TestCase("")]
        [TestCase(null)]
        public void BuildBetTeam_TicketBetTeamIsNotInExpectedValueList_BetTeamIsEmpty(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapToNull()
        {
            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.Null(template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameToFavorite()
        {
            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string Favorite = "favorite";

            Assert.AreEqual(Favorite, template.betTeamClassName);
        }
    }
}