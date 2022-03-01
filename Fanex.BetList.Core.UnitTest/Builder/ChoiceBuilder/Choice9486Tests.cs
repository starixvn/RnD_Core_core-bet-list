namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice9486Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice9486();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("0", 8.50d, "8.5")]
        [TestCase("2", 4.5000d, "4.5")]
        [TestCase("7", 3.5000d, "> 3.5")]
        public void BuildBetTeam_Always_ShowCorrectText(string betteam, decimal handicap, string teamName)
        {
            // Arrange
            _ticket.BetTeam = betteam;
            _ticket.Handicap1 = handicap;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(teamName, _choice.Template.betTeam);
        }
    }
}
