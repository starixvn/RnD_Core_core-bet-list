namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1334 class.
    /// </summary>
    [TestFixture]
    public class Choice1334Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1334();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.SetXGameYAndGameYAdd1Points;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.ticketStatus is always handicap1.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Arrange
            _ticket.Handicap1 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(_ticket.Handicap1.ToString(), _choice.Template.Handicap.handicap);
        }

        /// <summary>
        /// The value of Template.betTeam is always under.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs1_BetTeamIsUnderResource()
        {
            // Arrange
            _ticket.BetTeam = "1";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format("<span class=\"underdog\">{0}</span>", CoreBetList.under);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always Over.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs2_BetTeamIsOverResource()
        {
            // Arrange
            _ticket.BetTeam = "2";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format("<span class=\"favorite\">{0}</span>", CoreBetList.over);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always Between handicap 2 and handicap1.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamNotIs1AndNotIs2_BetTeamIsExactlyResource()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format("<span class=\"underdog\">{0}</span>", CoreBetList.exactly);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
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