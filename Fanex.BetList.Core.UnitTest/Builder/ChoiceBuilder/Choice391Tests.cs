namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice391Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice391();
            _ticket = TestData.CreateTicket(BetTypes.PenaltyShootoutExactTotalGoalsFirst10);
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("g4")]
        [TestCase("G4")]
        public void BuildBetTeam_BetTeamIsG4_BetChoiceIsZeroToFour(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(CoreBetList.ZeroToFour, template.betTeam);
        }

        [TestCase("g9+")]
        [TestCase("G9+")]
        public void BuildBetTeam_BetTeamIsG9AndOver_BetChoiceIsNineAndOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(CoreBetList.NineAndOver, template.betTeam);
        }

        [TestCase("g5")]
        [TestCase("G5")]
        public void BuildBetTeam_BetTeamStartsWithG_TrimGThenDisplayAsBetChoice(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string expectedBetChoice = "5";

            Assert.AreEqual(expectedBetChoice, template.betTeam);
        }

        [TestCase("g")]
        [TestCase("G")]
        public void BuildBetTeam_BetTeamIsOnlyG_BetChoiceIsEmpty(string invalidBetTeam)
        {
            // Arrange.
            _ticket.BetTeam = invalidBetTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsEmpty(template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamDoesNotStartWithG_BetChoiceIsEmpty()
        {
            // Arrange.
            const string invalidBetTeam = "5";

            _ticket.BetTeam = invalidBetTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsEmpty(template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsNull(template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsFavorite()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string favorite = "favorite";

            Assert.AreEqual(favorite, template.betTeamClassName);
        }
    }
}