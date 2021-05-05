
namespace Fanex.BetList.VGaming.Tests.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice3898Test
    {

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice3898();
            _ticket = TestData.CreateTicket(VGamingBetTypes.JackpotContribution);
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketHelper.GetBetTypeNameById(VGamingBetTypes.JackpotContribution).Returns("Jackpot Contribution");
        }

        [Test]
        public void BuildLeague_BetCheckIsEmpty_ShowDetailLink()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;
            var expectedJSFunction = "showVGamingDetails";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(template.League.ToString().Contains(expectedJSFunction));
        }

        [Test]
        public void BuildLeague_BetCheckHasValue_HideDetailLink()
        {
            // Arrange
            _ticket.BetCheck = "1212";
            var expectedJSFunction = "showVGamingDetails";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(template.League.ToString().Contains(expectedJSFunction));
        }

        [TestCase("")]
        [TestCase("121")]
        public void BuildLeague_Always_SetLeagueNameIsNull(string betCheck)
        {
            // Arrange
            _ticket.BetCheck = betCheck;

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(template.League.LeagueName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Alway_NullAndUnderdogClass()
        {
            // Arrange
            var expectedClass = "underdog";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(_choice.Template.Handicap.handicap);
            Assert.AreEqual(expectedClass, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeam_IsSummaryTicket_NotContainBetcheck()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;
            _ticket.OddsSpread = 45m;
            _ticket.Odds = 0.001m;
            _ticket.Currency = "AUD";
            var expectedBetTeam = $"<span class=\"handicap\">Jackpot Contribution</span>";
            expectedBetTeam += $"<div class=\"favorite\">Stake: 45.0000 AUD @ Rate 0.1000%</div>";

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetTeam, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_IsDetailTicket_ContainBetcheck()
        {
            // Arrange
            _ticket.OddsSpread = 45m;
            _ticket.Odds = 0.001m;
            _ticket.Currency = "AUD";
            _ticket.BetCheck = "123456";
            _ticket.BetdaqId = 990000000000011;
            var expectedBetTeam = $"<span class=\"handicap\">Jackpot Contribution</span> 123456";
            expectedBetTeam += "<div class=\"jackpotRefNo\">For Ref No: 990000000000011</div>";
            expectedBetTeam += $"<div class=\"favorite\">Stake: 45.0000 AUD @ Rate 0.1000%</div>";

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetTeam, template.betTeam);
        }


        [Test]
        public void BuildBetType_Alway_Null()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(_choice.Template.BetType);
        }

        [Test]
        public void BuildScore_Alway_Null()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(_choice.Template.Score);
        }

        public void BuildMatch_Alway_Null()
        {
            // Arrange

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Match);
        }


        [Test]
        public void AdjustBetTeamToRTF_Always_RemoveHtmlTab()
        {
            // Arrange
            var rtfHelper = new RTFHelper().Clone();
            _ticket.OddsSpread = 45m;
            _ticket.Odds = 0.001m;
            _ticket.Currency = "AUD";
            _ticket.BetCheck = "123456";
            _ticket.BetdaqId = 990000000000011;

            // Act.
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            // Assert.
            var linesInActualText = actualText[0].String.Split('\n');
            Assert.AreEqual("Jackpot Contribution 123456", linesInActualText[0]);
            Assert.AreEqual("For Ref No: 990000000000011", linesInActualText[1]);
            Assert.AreEqual("Stake: 45.0000 AUD @ Rate 0.1000%", linesInActualText[2]);
        }
    }
}
