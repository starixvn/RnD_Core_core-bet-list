namespace Fanex.BetList.PP.Tests.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice4300Tests
    {
        private const string FakeGameType = "Game Type";
        private const string FakeGameGroup = "Game Group";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice4300();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.TransDesc = "4300";
            _ticketHelper.GetResourceData(Arg.Is(Choice4101.GameTypeRefId), Arg.Is(_ticket.TransDesc)).Returns(FakeGameType);
            _ticketHelper.GetResourceData(Arg.Is(Choice4101.GameGroupRefId), Arg.Is(_ticket.TransDesc)).Returns(FakeGameGroup);
        }

        [Test]
        public void BuildBetTeam_BetCheckIsNull_NotShowBetCheck()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetOptionHtml = $"<span class=\"handicap\">{FakeGameType}</span>";
            Assert.AreEqual(expectedBetOptionHtml, template.betTeam);
        }

        [TestCase("won")]
        [TestCase("lose")]
        [TestCase("draw")]
        public void BuildBetTeam_StatusIsWon_NotShowBetCheck(string status)
        {
            // Arrange
            _ticket.BetCheck = string.Empty;
            _ticket.Status = status;

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetOptionHtml = $"<span class=\"handicap\">{FakeGameType}</span>";
            Assert.AreEqual(expectedBetOptionHtml, template.betTeam);
        }
    }
}