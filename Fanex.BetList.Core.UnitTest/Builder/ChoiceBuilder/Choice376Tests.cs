namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice376Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;
        private bool _isShowScoreMap;
        const string homeTeamName = "home team";
        const string awayTeamName = "away team";
        const string emptyBettype = "<div class=\"bettype\"> </div>";

        [SetUp]
        public void Setup()
        {


            _choice = new Choice376();
            _ticket = TestData.CreateTicket(BetTypes.PenaltyShootoutComboFirst10);
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = Substitute.For<List<ITicketData>>();
            _isShowScoreMap = false;

            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns(homeTeamName);
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns(awayTeamName);
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
        public void BuildBetTeamClassNameAndHandicap_Always_SetCssIsBetType()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, _isShowScoreMap);

            // Assert.
            Assert.AreEqual("bettype", template.betTeamClassName);
        }

        [Test]
        public void BuildBetType_Always_SetBettypeNameIsEmpty()
        {
            // Arrange.

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, _isShowScoreMap);

            // Assert.
            Assert.IsEmpty(template.BetType.betTypeName);
        }

        [Test]
        public void BuildBetType_BetCheckIsEmpty_BetTypeIsEmpty()
        {
            // Arrange.
            _ticket.BetCheck = string.Empty;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, _isShowScoreMap);

            // Assert.
            Assert.AreEqual(emptyBettype, template.BetType.ToString());
        }

        [Test]
        public void BuildBetType_BetTeamIsEmpty_BetTypeIsEmpty()
        {
            // Arrange.
            _ticket.BetCheck = string.Empty;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, _isShowScoreMap);

            // Assert.
            Assert.AreEqual(emptyBettype, template.BetType.ToString());
        }

        [Test]
        public void BuildBetTypeName_Always_OrderAscByRound()
        {
            // Arrange.
            _ticket.BetCheck = "1.3,1.3,1.2,1.2";
            _ticket.BetTeam = "SSSS";
            var expectedBetType = emptyBettype + string.Format(Choice376.OptionHtmlFormat, $"{homeTeamName} Round 1: Goal @1.30");
            expectedBetType += string.Format(Choice376.OptionHtmlFormat, $"{awayTeamName} Round 1: Goal @1.20");
            expectedBetType += string.Format(Choice376.OptionHtmlFormat, $"{homeTeamName} Round 2: Goal @1.30");
            expectedBetType += string.Format(Choice376.OptionHtmlFormat, $"{awayTeamName} Round 2: Goal @1.20");

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, _isShowScoreMap);

            // Assert.
            Assert.AreEqual(expectedBetType, template.BetType.ToString());
        }

        [Test]
        public void BuildBetTypeName_OptionIsHyphen_Hide()
        {
            // Arrange.
            _ticket.BetCheck = "1.3,1.3";
            _ticket.BetTeam = "-S";
            var expectedBetType = emptyBettype + string.Format(Choice376.OptionHtmlFormat, string.Empty);
            expectedBetType += string.Format(Choice376.OptionHtmlFormat, $"{awayTeamName} Round 1: Goal @1.30");

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, _isShowScoreMap);

            // Assert.
            Assert.AreEqual(expectedBetType, template.BetType.ToString());
        }

        [Test]
        public void BuildBetTypeName_OddsIsEmpty_Hide()
        {
            // Arrange.
            _ticket.BetCheck = ",1.3";
            _ticket.BetTeam = "SS";
            var expectedBetType = emptyBettype + string.Format(Choice376.OptionHtmlFormat, string.Empty);
            expectedBetType += string.Format(Choice376.OptionHtmlFormat, $"{awayTeamName} Round 1: Goal @1.30");

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, _ticketData, _isShowScoreMap);

            // Assert.
            Assert.AreEqual(expectedBetType, template.BetType.ToString());
        }
    }
}