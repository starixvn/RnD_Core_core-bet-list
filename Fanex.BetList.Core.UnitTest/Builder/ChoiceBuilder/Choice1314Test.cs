namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1314 class.
    /// </summary>
    [TestFixture]
    public class Choice1314Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1314();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.SetXWillThereBeATiebreak;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.betTeam is always Draw when bet team of ticket is h.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIsCharacterh_BetTeamIsLableYesResource()
        {
            // Arrange
            _ticket.BetTeam = "h";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.lblYes, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always Draw when bet team of ticket is not h.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIsNotCharacterh_BetTeamIsLableNoResource()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.lblNo, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.BetType.betTypeName is always bet type name.
        /// </summary>
        [Test]
        public void BuildBetTeam_Always_SetBetTypeNameIsBetTypeName()
        {
            // Arrange
            _ticket.BetTypeId = 1;
            _ticket.BetId = 1;
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>(), Arg.Any<long>()).Returns("Bet type name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTypeName = _ticketHelper.GetBetTypeNameById(_ticket.BetTypeId, _ticket.BetId);
            Assert.AreEqual(expectedBetTypeName, _choice.Template.BetType.betTypeName);
        }
    }
}