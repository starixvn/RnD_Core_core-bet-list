namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice3905Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = new Ticket();
            _choice = new Choice3905();
        }

        [TestCase(801, "Set 1 Total Points Over/Under (Race to 15 Points)")]
        public void BuildBetType_BetChoice_CorrectBetTypeName(long betId, string betTypeName)
        {
            //Arrange
            _ticket.BetId = betId;
            _ticket.BetTypeId = 3905;
            _ticket.BetTeam = "O";
            _ticketHelper.GetBetTypeNameById(3905, Arg.Any<string>(), betId: betId).Returns<string>(betTypeName);

            //Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(betTypeName, _choice.Template.BetType.betTypeName);
        }

    }
}
