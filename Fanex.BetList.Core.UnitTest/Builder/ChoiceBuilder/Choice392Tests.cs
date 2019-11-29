namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice392Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice392();
            _ticket = TestData.CreateTicket(BetTypes.PenaltyShootoutCorrectScore);
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("haos")]
        [TestCase("HAOS")]
        public void BuildBetTeam_BetTeamIsHAOS_BetChoiceIsHomeToWinAnyOtherScore(string homeToWinWithAos)
        {
            // Arrange.
            _ticket.BetTeam = homeToWinWithAos;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(CoreBetList.HomeToWinAnyOtherScore, template.betTeam);
        }

        [TestCase("aaos")]
        [TestCase("AAOS")]
        public void BuildBetTeam_BetTeamIsAAOS_BetChoiceIsAwayToWinAnyOtherScore(string awayToWinWithAos)
        {
            // Arrange.
            _ticket.BetTeam = awayToWinWithAos;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(CoreBetList.AwayToWinAnyOtherScore, template.betTeam);
        }

        [TestCase("haos")]
        [TestCase("HAOS")]
        [TestCase("aaos")]
        [TestCase("AAOS")]
        public void BuildScore_BetTeamIsXAOSAndTransDescIsNotEmpty_ShowExcludingInfo(string aosBetTeam)
        {
            // Arrange.
            const string validTransDesc = "0-1,0-2,1-2";

            _ticket.BetTeam = aosBetTeam;
            _ticket.TransDesc = validTransDesc;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string formattedTransDesc = "0-1, 0-2, 1-2";

            var expectedExcludingHtml = $"<div class=\"excluding-choices\">{CoreBetList.Excluding}: {formattedTransDesc}</div>";

            Assert.AreEqual(expectedExcludingHtml, template.Score.ToString());
        }

        [Test]
        public void BuildScore_BetTeamIsNotXAOS_HideExcludingInfo()
        {
            // Arrange.
            const string validTransDesc = "0-1,0-2,1-2";
            const string notAosBetTeam = "1-2";

            _ticket.BetTeam = notAosBetTeam;
            _ticket.TransDesc = validTransDesc;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsFalse(template.Score.Visible);
        }

        [TestCase("haos")]
        [TestCase("HAOS")]
        [TestCase("aaos")]
        [TestCase("AAOS")]
        public void BuildScore_BetTeamIsXAOSButTransDescIsEmpty_HideExcludingInfo(string aosBetTeam)
        {
            // Arrange.
            _ticket.BetTeam = aosBetTeam;
            _ticket.TransDesc = string.Empty;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsFalse(template.Score.Visible);
        }
    }
}