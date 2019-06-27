namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using Fanex.BetList.Core.Utils.TicketHelperExt;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice468Tests
    {
        private const string ScoreBoxDescription = "score box description";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;
        private bool _isShowScoreMap;

        [SetUp]
        public void Setup()
        {
            const string homeTeamName = "home team";
            const string awayTeamName = "away team";

            _choice = new Choice468();
            _ticket = TestData.CreateTicket(BetTypes.ScoreBoxHandicap);
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = Substitute.For<List<ITicketData>>();
            _isShowScoreMap = false;

            _ticketHelper.GetResourceData(ScoreBoxExtensions.ScoreBoxRefId, Arg.Any<string>()).Returns(ScoreBoxDescription);
            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns(homeTeamName);
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns(awayTeamName);

            _ticket.BetTeam = "A";
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

        [Test]
        public void BuildBetType_BetCheckIsValid_AppendScoreBoxDescriptionAfterBetTypeName()
        {
            // Arrange.
            _ticket.BetCheck = "0:5";

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, _isShowScoreMap);
            var generatedHtml = template.BetType.ToString();

            // Assert.
            var expectedHtml = string.Format(Choice468.ScoreBoxDescriptionHtmlFormat, ScoreBoxDescription);

            StringAssert.EndsWith(expectedHtml, generatedHtml);
        }

        [Test]
        public void BuildBetType_BetCheckIsEmpty_OnlyShowBetTypeName()
        {
            // Arrange.
            _ticket.BetCheck = string.Empty;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, _isShowScoreMap);
            var generatedHtml = template.BetType.ToString();

            // Assert.
            var unexpectedHtml = string.Format(Choice468.ScoreBoxDescriptionHtmlFormat, ScoreBoxDescription);

            StringAssert.DoesNotContain(unexpectedHtml, generatedHtml);
        }

        [Test]
        public void BuildBetTypeName_BetCheckIsValid_ScoreBoxDescriptionsGoAfterBetTypeName()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.BetCheck = "0:1.55";

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, _ticketData, _isShowScoreMap, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert.
            var thirdLine = linesInActualText[2];

            Assert.AreEqual(ScoreBoxDescription, thirdLine);
        }
    }
}