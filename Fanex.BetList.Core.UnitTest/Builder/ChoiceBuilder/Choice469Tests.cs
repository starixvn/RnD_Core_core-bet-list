namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice469Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;
        private bool _isShowScoreMap;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice469();
            _ticket = TestData.CreateTicket(BetTypes.ScoreBoxOverUnder);
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = Substitute.For<List<ITicketData>>();
            _isShowScoreMap = false;
        }

        [TestCase("o")]
        [TestCase("O")]
        public void BuildBetTeam_TicketBetTeamIsO_ShowEqualOverAsBetTeam(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, _isShowScoreMap);

            // Assert.
            var expectedDisplayedBetTeam = $"{CoreBetList.Equal}/{CoreBetList.over}";

            Assert.AreEqual(expectedDisplayedBetTeam, template.betTeam);
        }

        [TestCase("u")]
        [TestCase("U")]
        public void BuildBetTeam_TicketBetTeamIsU_ShowEqualUnderAsBetTeam(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, _isShowScoreMap);

            // Assert.
            var expectedDisplayedBetTeam = $"{CoreBetList.Equal}/{CoreBetList.under}";

            Assert.AreEqual(expectedDisplayedBetTeam, template.betTeam);
        }
    }
}