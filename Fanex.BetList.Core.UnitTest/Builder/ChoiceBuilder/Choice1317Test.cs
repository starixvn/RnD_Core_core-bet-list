namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

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

        [Test]
        public void BuildBetType_Always_SetBetTypeNameIsBetTypeName()
        {
            // Arrange
            const string BET_TYPE_NAME = "Bet type name";
            _ticketHelper.GetBetTypeNameById(Arg.Any<object>(), Arg.Any<object>(), Arg.Any<object>(), Arg.Any<object>()).Returns(BET_TYPE_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(BET_TYPE_NAME, _choice.Template.BetType.betTypeName);
        }
    }
}