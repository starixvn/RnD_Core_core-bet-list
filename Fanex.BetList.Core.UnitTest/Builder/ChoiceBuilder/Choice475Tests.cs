namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice475Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice475();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("o")]
        [TestCase("O")]
        public void BuildBetTeam_TicketBetTeamIsO_DisplayedBetTeamIsOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.over, template.betTeam);
        }

        [TestCase("u")]
        [TestCase("U")]
        public void BuildBetTeam_TicketBetTeamIsU_DisplayedBetTeamIsUnder(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.under, template.betTeam);
        }

        [TestCase("e")]
        [TestCase("E")]
        public void BuildBetTeam_TicketBetTeamIsE_DisplayedBetTeamIsExact(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.Exact, template.betTeam);
        }

        [TestCase("a")]
        [TestCase("H")]
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
            const decimal handicap1 = 1;

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