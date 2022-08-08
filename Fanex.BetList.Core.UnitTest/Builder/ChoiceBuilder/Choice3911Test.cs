namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice3911Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = new Ticket();
            _choice = new Choice3911();
        }

        [TestCase(801, "Set 1 Winning Margin")]
        public void BuildBetType_BetChoice_CorrectBetTypeName(long betId, string betTypeName)
        {
            //Arrange
            _ticket.BetId = betId;
            _ticket.BetTypeId = 3911;
            _ticket.BetTeam = "A12+";
            _ticketHelper.GetBetTypeNameById(3911, Arg.Any<string>(), betId: betId).Returns<string>(betTypeName);

            //Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(betTypeName, _choice.Template.BetType.betTypeName);
        }

        [TestCase("H2", "Home To Win By 2 Points")]
        [TestCase("H3-4", "Home To Win By 3 - 4 Points")]
        [TestCase("H5-7", "Home To Win By 5 - 7 Points")]
        [TestCase("H8-11", "Home To Win By 8 - 11 Points")]
        [TestCase("H12+", "Home To Win By 12+ Points")]
        [TestCase("A2", "Away To Win By 2 Points")]
        [TestCase("A3-4", "Away To Win By 3 - 4 Points")]
        [TestCase("A5-7", "Away To Win By 5 - 7 Points")]
        [TestCase("A8-11", "Away To Win By 8 - 11 Points")]
        [TestCase("A12+", "Away To Win By 12+ Points")]
        [TestCase("A", "")]
        [TestCase("H123123", "")]
        public void BuildBetTeam_BetTeamValue_ReturnChoiceName(string betTeamValue, string choiceValue)
        {
            //Arrange
            _ticket.BetTeam = betTeamValue;

            //Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(choiceValue, _choice.Template.betTeam);
        }
    }
}
