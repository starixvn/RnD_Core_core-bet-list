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
        [TestCase("H1-5", "1-5")]
        [TestCase("H6-10", "6-10")]
        [TestCase("H11-15", "11-15")]
        [TestCase("H16-20", "16-20")]
        [TestCase("H21-25", "21-25")]
        [TestCase("H26+", "26+")]
        [TestCase("A1-5", "1-5")]
        [TestCase("A6-10", "6-10")]
        [TestCase("A11-15", "11-15")]
        [TestCase("A16-20", "16-20")]
        [TestCase("A21-25", "21-25")]
        [TestCase("A26+", "26+")]
        public void BuildBetTeam_ForEachBetChoice_CallGetResourceData(string betTeam, string resourceId)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;
            _ticketHelper.GetResourceData(Arg.Any<string>(), Arg.Any<string>()).Returns("Bet Team");

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is<string>(refId => refId == BasketballRefId), Arg.Is<string>(resId => resId == resourceId));
        }

        [TestCase("h1-5")]
        [TestCase("h6-10")]
        [TestCase("h11-15")]
        [TestCase("h16-20")]
        [TestCase("h21-25")]
        [TestCase("h26+")]
        [TestCase("H1-5")]
        [TestCase("H6-10")]
        [TestCase("H11-15")]
        [TestCase("H16-20")]
        [TestCase("H21-25")]
        [TestCase("H26+")]
        public void BuildBetTeam_BetTeamStartWithH_BetChoiceStartsWithHome(string betTeam)
        {
            // Arrange.
            var resourceId = betTeam.Substring(1);

            _ticket.BetTeam = betTeam;
            _ticketHelper.GetResourceData(Arg.Any<string>(), Arg.Is(resourceId)).Returns("{0} " + resourceId);

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual($"Home {resourceId}", _choice.Template.betTeam);
        }

        [TestCase("a1-5")]
        [TestCase("a6-10")]
        [TestCase("a11-15")]
        [TestCase("a16-20")]
        [TestCase("a21-25")]
        [TestCase("a26+")]
        [TestCase("A1-5")]
        [TestCase("A6-10")]
        [TestCase("A11-15")]
        [TestCase("A16-20")]
        [TestCase("A21-25")]
        [TestCase("A26+")]
        public void BuildBetTeam_BetTeamStartWithA_BetChoiceStartsWithAway(string betTeam)
        {
            // Arrange.
            var resourceId = betTeam.Substring(1);

            _ticket.BetTeam = betTeam;
            _ticketHelper.GetResourceData(Arg.Any<string>(), Arg.Is(resourceId)).Returns("{0} " + resourceId);

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual($"Away {resourceId}", _choice.Template.betTeam);
        }
    }
}