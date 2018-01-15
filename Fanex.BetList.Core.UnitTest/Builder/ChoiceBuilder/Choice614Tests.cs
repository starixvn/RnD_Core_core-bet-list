namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice614Tests
    {
        private const string BasketballRefId = "WMBasketball";

        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = TestData.CreateTicket(614);
            _choice = new Choice614();
        }

        [TestCase("h1-4", "1-4")]
        [TestCase("h5-8", "5-8")]
        [TestCase("h9+", "9+")]
        [TestCase("d", "D")]
        [TestCase("a1-4", "1-4")]
        [TestCase("a5-8", "5-8")]
        [TestCase("a9+", "9+")]
        [TestCase("H1-4", "1-4")]
        [TestCase("H5-8", "5-8")]
        [TestCase("H9+", "9+")]
        [TestCase("D", "D")]
        [TestCase("A1-4", "1-4")]
        [TestCase("A5-8", "5-8")]
        [TestCase("A9+", "9+")]
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

        [TestCase("h1-4")]
        [TestCase("h5-8")]
        [TestCase("h9+")]
        [TestCase("H1-4")]
        [TestCase("H5-8")]
        [TestCase("H9+")]
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

        [TestCase("a1-4")]
        [TestCase("a5-8")]
        [TestCase("a9+")]
        [TestCase("A1-4")]
        [TestCase("A5-8")]
        [TestCase("A9+")]
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

        [TestCase("d")]
        [TestCase("D")]
        public void BuildBetTeam_BetTeamIsD_BetChoiceIsDraw(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;
            _ticketHelper.GetResourceData(Arg.Any<string>(), Arg.Is("D")).Returns("Draw");

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("Draw", _choice.Template.betTeam);
        }
    }
}