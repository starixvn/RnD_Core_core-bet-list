namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice601Tests
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
            _choice = new Choice601();
        }

        [TestCase("h1-2", "1-2")]
        [TestCase("h3-6", "3-6")]
        [TestCase("h7-9", "7-9")]
        [TestCase("h10-13", "10-13")]
        [TestCase("h14-16", "14-16")]
        [TestCase("h17-20", "17-20")]
        [TestCase("h21+", "21+")]
        [TestCase("a1-2", "1-2")]
        [TestCase("a3-6", "3-6")]
        [TestCase("a7-9", "7-9")]
        [TestCase("a10-13", "10-13")]
        [TestCase("a14-16", "14-16")]
        [TestCase("a17-20", "17-20")]
        [TestCase("a21+", "21+")]
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

        [TestCase("h1-2")]
        [TestCase("h3-6")]
        [TestCase("h7-9")]
        [TestCase("h10-13")]
        [TestCase("h14-16")]
        [TestCase("h17-20")]
        [TestCase("h21+")]
        [TestCase("a1-2")]
        [TestCase("a3-6")]
        [TestCase("a7-9")]
        [TestCase("a10-13")]
        [TestCase("a14-16")]
        [TestCase("a17-20")]
        [TestCase("a21+")]
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