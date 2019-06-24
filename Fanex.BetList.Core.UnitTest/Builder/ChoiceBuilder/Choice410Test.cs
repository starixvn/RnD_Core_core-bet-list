namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice410Test
    {
        private const string BetTeamTemplate = "{0}<span class=\"underdog\"> {1} </span>{2}";

        private ITicket _ticket;
        private IChoice _choice;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void SetUp()
        {
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.FirstHalfDoubleChanceOwn;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _choice = new Choice410();
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

        [Test]
        public void BuildBetType_Always_ShowParentBetTypeName()
        {
            // Arrange.
            var parentBetTypeId = BetTypes.FirstHalfDoubleChance.ToString();
            var parentBetTypeName = "Parent Bet Type";
            _ticketHelper.GetParentIdByBetTypeId(Arg.Any<object>()).Returns(parentBetTypeId);
            _ticketHelper.GetBetTypeNameById(parentBetTypeId, Arg.Any<object>()).Returns(parentBetTypeName);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(parentBetTypeName, _choice.Template.BetType.betTypeName);
        }
    }
}