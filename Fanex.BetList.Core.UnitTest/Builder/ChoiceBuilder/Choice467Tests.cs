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

    public class Choice467Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;
        private bool _isShowScoreMap;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice467();
            _ticket = TestData.CreateTicket(BetTypes.HalfTimeFullTimeExactTotalGoals);
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = Substitute.For<List<ITicketData>>();
            _isShowScoreMap = false;
        }

        [TestCase("G1/G2", "1", "2")]
        [TestCase(" G2 / G3 ", "2", "3")]
        [TestCase("g2/G7+", "2", "7+")]
        [TestCase("G6/g10+", "6", "10+")]
        [TestCase("g7+/g7+", "7+", "7+")]
        public void BuildBetTeam_BetTeamIsOneOfCorrectValues_SplitTotalGoalsValuesInBetTeamAndCombineWithHalfTimeFullTimeTexts(
            string betTeam,
            string expectedHalfTimeTotalGoals,
            string expectedFullTimeTotalGoals)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, _isShowScoreMap);

            // Assert.
            var expectedDisplayedBetTeam = $"{CoreBetList.HalfTime} {expectedHalfTimeTotalGoals} / {CoreBetList.FullTime} {expectedFullTimeTotalGoals}";

            Assert.AreEqual(expectedDisplayedBetTeam, template.betTeam);
        }

        [TestCase("G1/G2/G3")]
        [TestCase("G/G2")]
        [TestCase("g2/G")]
        [TestCase("ABCD")]
        public void BuildBetTeam_BetTeamIsIncorrect_DisplayEmptyAsBetTeam(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, _isShowScoreMap);

            // Assert.
            Assert.IsEmpty(template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapToNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, _isShowScoreMap);

            // Assert.
            Assert.IsNull(template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameToFavorite()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, _isShowScoreMap);

            // Assert.
            const string favorite = "favorite";

            Assert.AreEqual(favorite, template.betTeamClassName);
        }
    }
}