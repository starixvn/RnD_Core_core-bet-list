namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice3910Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = new Ticket();
            _choice = new Choice3910();
        }

        [TestCase("aos", "AOS")]
        [TestCase("25:0", "25-0")]
        public void BuildBetTeam_BetTeamValue_CorrectScore(string betTeamValue, string score)
        {
            //Arrage
            _ticket.BetTeam = betTeamValue;

            //Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            //Assert
            Assert.AreEqual(score, _choice.Template.betTeam);
        }

        [TestCase(801, "Set 1 Points Correct Score")]
        public void BuildBetType_BetChoice_CorrectBetTypeName(long betId, string betTypeName)
        {
            //Arrange
            _ticket.BetId = betId;
            _ticket.BetTypeId = 3910;
            _ticket.BetTeam = "23:25";
            _ticketHelper.GetBetTypeNameById(3910, Arg.Any<string>(), betId: betId).Returns<string>(betTypeName);

            //Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(betTypeName, _choice.Template.BetType.betTypeName);
        }
    }
}
