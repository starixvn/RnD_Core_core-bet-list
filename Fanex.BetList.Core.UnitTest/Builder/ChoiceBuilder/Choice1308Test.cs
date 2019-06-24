namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1308Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1308();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.MatchGamesHandicap3rd;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetType_Always_SetBetTypeNameIsParentBetTypeName()
        {
            // Arrange
            const string BET_TYPE_NAME = "Bet type name";
            _ticketHelper.GetBetTypeNameById(Arg.Any<object>(), Arg.Any<object>()).Returns(BET_TYPE_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(BET_TYPE_NAME, _choice.Template.BetType.betTypeName);
        }
    }
}