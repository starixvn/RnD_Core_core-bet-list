namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice602Tests
    {
        private const string BasketballRefId = "WMBasketball";

        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = new Ticket();
            _choice = new Choice602();
        }

        [TestCase("h1-5", "1-5")]
        [TestCase("h6-10", "6-10")]
        [TestCase("h11-15", "11-15")]
        [TestCase("h16-20", "16-20")]
        [TestCase("h21-25", "21-25")]
        [TestCase("h26+", "26+")]
        [TestCase("a1-5", "1-5")]
        [TestCase("a6-10", "6-10")]
        [TestCase("a11-15", "11-15")]
        [TestCase("a16-20", "16-20")]
        [TestCase("a21-25", "21-25")]
        [TestCase("a26+", "26+")]
        public void BuildBetTeam_ForEachBetChoice_CallGetResourceData(string betTeam, string resourceId)
        {
            // Arrange
            _ticket.BetTeam = betTeam;
            _ticketHelper.GetResourceData(Arg.Any<string>(), Arg.Any<string>()).Returns("Bet Team");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            _ticketHelper.Received().GetResourceData(Arg.Is<string>(refId => refId == BasketballRefId), Arg.Is<string>(resId => resId == resourceId));
        }

        [TestCase("h1-5")]
        [TestCase("h6-10")]
        [TestCase("h11-15")]
        [TestCase("h16-20")]
        [TestCase("h21-25")]
        [TestCase("h26+")]
        [TestCase("a1-5")]
        [TestCase("a6-10")]
        [TestCase("a11-15")]
        [TestCase("a16-20")]
        [TestCase("a21-25")]
        [TestCase("a26+")]
        public void BuildBetTeam_Always_ShowBetTeam(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;
            _ticketHelper.GetResourceData(Arg.Any<string>(), Arg.Any<string>()).Returns("Bet Team");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("Bet Team", _choice.Template.betTeam);
        }
    }
}