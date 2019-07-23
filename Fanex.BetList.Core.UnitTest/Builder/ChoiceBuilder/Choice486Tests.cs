namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice486Tests
    {
        private const string HomeTeamName = "Home team";
        private const string AwayTeamName = "Away team";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            const int homeTeamId = 1;
            const int awayTeamId = 2;

            _choice = new Choice486();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket.HomeId = homeTeamId;
            _ticket.AwayId = awayTeamId;

            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns(HomeTeamName);
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns(AwayTeamName);
        }

        [TestCase("h")]
        [TestCase("H")]
        public void BuildBetTeam_TicketBetTeamIsH_ShowHomeTeamNameAsBetTeam(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(HomeTeamName, template.betTeam);
        }

        [TestCase("a")]
        [TestCase("A")]
        public void BuildBetTeam_TicketBetTeamIsA_ShowAwayTeamNameAsBetTeam(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(AwayTeamName, template.betTeam);
        }

        [TestCase("n")]
        [TestCase("N")]
        public void BuildBetTeam_TicketBetTeamIsN_ShowNeitherAsBetTeam(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.lblNeither, template.betTeam);
        }

        [TestCase("o")]
        [TestCase("U")]
        public void BuildBetTeam_TicketBetTeamIsNotOneOfExpectedValue_DisplayedBetTeamIsEmpty(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_DisplayedHandicapIsTicketHandicap1()
        {
            // Arrange.
            const decimal handicap1 = 5;

            _ticket.Handicap1 = handicap1;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedHandicap = ConvertByBetType.Hdp(handicap1);

            Assert.AreEqual(expectedHandicap, _choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsFavorite()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string favorite = "favorite";

            Assert.AreEqual(favorite, _choice.Template.betTeamClassName);
        }
    }
}