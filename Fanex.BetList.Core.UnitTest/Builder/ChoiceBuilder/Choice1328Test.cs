namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1328 class.
    /// </summary>
    [TestFixture]
    public class Choice1328Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1328();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.SetXGameYTotalPointsExact;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.ticketStatus is always void when Ticket.Status = void.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        /// <summary>
        /// The value of Template.betTeam is always 4 Points.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs01_BetTeamAreSpaceAnd4AndPointResource()
        {
            // Arrange
            _ticket.BetTeam = "01";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format("&nbsp;4 {0}", CoreBetList.points);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always 5 Points.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs02_BetTeamAreSpaceAnd5AndPointResource()
        {
            // Arrange
            _ticket.BetTeam = "02";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format("&nbsp;5 {0}", CoreBetList.points);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always 6 Points.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs03_BetTeamAreSpaceAnd6AndPointResource()
        {
            // Arrange
            _ticket.BetTeam = "03";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format("&nbsp;6 {0}", CoreBetList.points);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always 7 UP Points.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs04_BetTeamAreSpaceAnd7AndUpPointResource()
        {
            // Arrange
            _ticket.BetTeam = "04";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format("&nbsp;7 {0}", CoreBetList.upPoints);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always empty.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamNotAre01Or02Or03Or04_BetTeamIsEmpty()
        {
            // Arrange
            _ticket.BetTeam = "bet team";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("&nbsp;", _choice.Template.betTeam);
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