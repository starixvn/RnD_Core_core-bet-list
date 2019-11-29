namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1317Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1317();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.SetXCorrectScore;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("haos")]
        [TestCase("HAOS")]
        public void BuildBetTeam_BetTeamIsHAOS_BetChoiceIsHomeToWinAnyOtherScore(string homeToWinWithAos)
        {
            // Arrange.
            _ticket.BetTeam = homeToWinWithAos;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(CoreBetList.HomeToWinAnyOtherScore, _choice.Template.betTeam);
        }

        [TestCase("aaos")]
        [TestCase("AAOS")]
        public void BuildBetTeam_BetTeamIsAAOS_BetChoiceIsAwayToWinAnyOtherScore(string awayToWinWithAos)
        {
            // Arrange.
            _ticket.BetTeam = awayToWinWithAos;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(CoreBetList.AwayToWinAnyOtherScore, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsAStringOfTwoCharacters_SeparateThemByColon()
        {
            // Arrange.
            const string betTeam = "12";

            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string displayedBetChoice = "1:2";

            Assert.AreEqual(displayedBetChoice, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsNotInAcceptableList_BetChoiceIsEmpty()
        {
            // Arrange.
            const string invalidBetTeam = "xyz";

            _ticket.BetTeam = invalidBetTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [Test]
        public void BuildBetType_Always_SendBetTypeIdRefNoBetIdAndBetCheckToGetBetTypeNameByIdOfTicketHelper()
        {
            // Arrange.
            const string refNo = "100001";
            const long betId = 801;
            const string betCheck = "801";

            _ticket.RefNo = refNo;
            _ticket.BetId = betId;
            _ticket.BetCheck = betCheck;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            _ticketHelper
                .Received()
                .GetBetTypeNameById(
                    Arg.Is(BetTypes.SetXCorrectScore),
                    Arg.Is(refNo),
                    Arg.Is(betId),
                    Arg.Is(betCheck));
        }
    }
}