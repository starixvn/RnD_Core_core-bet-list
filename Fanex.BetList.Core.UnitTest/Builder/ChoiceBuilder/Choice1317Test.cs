namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1317 class.
    /// </summary>
    [TestFixture]
    public class Choice1317Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1317();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.SetXCorrectScore;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.betTeam is Home To Win Any Other Score when BetTeam is HAOS.
        /// </summary>
        [Test]
        public void BuildBetTeam_LengthBetTeamIsHAOS_BetTeamIsHomeToWinAnyOtherScore()
        {
            // Arrange
            _ticket.BetTeam = "haos";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("&nbsp;Home To Win Any Other Score", _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is Away To Win Any Other Score when BetTeam is AAOS.
        /// </summary>
        [Test]
        public void BuildBetTeam_LengthBetTeamIsAAOS_BetTeamIsAwayToWinAnyOtherScore()
        {
            // Arrange
            _ticket.BetTeam = "aaos";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("&nbsp;Away To Win Any Other Score", _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always [BetTeam[0]:BetTeam[1]] when BetTeam.Length == 2.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIsAB_BetTeamIsAColonB()
        {
            // Arrange
            _ticket.BetTeam = "ab";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("&nbsp;a:b", _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is empty when BetTeam.Length != 2 and not HAOS or AAOS.
        /// </summary>
        [Test]
        public void BuildBetTeam_LengthBetTeamNotEqual2HAOSAAOS_BetTeamIsEmpty()
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
        public void BuildBetType_Always_SetBetTypeNameIsBetTypeName()
        {
            // Arrange
            _ticket.BetId = 801;
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>(), Arg.Any<object>(), Arg.Any<object>()).Returns("Bet type name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTypeName = _ticketHelper.GetBetTypeNameById(_ticket.BetTypeId, _ticket.BetId, _ticket.BetCheck);
            Assert.AreEqual(expectedBetTypeName, _choice.Template.BetType.betTypeName);
        }
    }
}