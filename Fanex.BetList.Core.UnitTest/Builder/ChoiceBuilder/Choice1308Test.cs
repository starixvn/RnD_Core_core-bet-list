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
            const string expectedBetTypeName = "Bet type name";
            const string parentBetTypeId = "1";

            _ticketHelper.GetParentIdByBetTypeId(_ticket.BetTypeId).Returns(parentBetTypeId);
            _ticketHelper.GetBetTypeNameById(parentBetTypeId, Arg.Any<object>()).Returns(expectedBetTypeName);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(expectedBetTypeName, _choice.Template.BetType.betTypeName);
        }
    }
}