namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1311Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1311();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.SetXWinner3rd;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetType_Always_SetBetTypeNameIsParentBetTypeName()
        {
            // Arrange
            _ticketHelper.GetBetTypeNameById(Arg.Any<object>(), Arg.Any<object>(), Arg.Any<object>(), Arg.Any<object>()).Returns("Bet type name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("Bet type name", _choice.Template.BetType.betTypeName);
        }
    }
}