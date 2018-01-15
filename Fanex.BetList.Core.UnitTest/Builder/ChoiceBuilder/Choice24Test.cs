namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice24Test
    {
        private const string BetTeamTemplate = "{0}<span class=\"underdog\"> {1} </span>{2}";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice24();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Double_Change;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [TestCase(null)]
        [TestCase("")]
        public void BuildBetTeam_BetTeamIsNullOrEmpty_BetChoiceIsEmptyString(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamHasMoreThanTwoCharacters_BetChoiceIsEmptyString()
        {
            // Arrange.
            _ticket.BetTeam = "1XA";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [TestCase("1x")]
        [TestCase("1X")]
        public void BuildBetTeam_BetTeamIs1x_BetChoiceIsHomeOrDraw(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectedBetTeam = string.Format(BetTeamTemplate, CoreBetList.Home, CoreBetList.lblOr, CoreBetList.lblDraw);

            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [TestCase("2x")]
        [TestCase("2X")]
        public void BuildBetTeam_BetTeamIs2x_BetChoiceIsAwayOrDraw(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectedBetTeam = string.Format(BetTeamTemplate, CoreBetList.Away, CoreBetList.lblOr, CoreBetList.lblDraw);

            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs12_BetChoiceIsHomeOrAway()
        {
            // Arrange.
            _ticket.BetTeam = "12";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectedBetTeam = string.Format(BetTeamTemplate, CoreBetList.Home, CoreBetList.lblOr, CoreBetList.Away);

            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }
    }
}