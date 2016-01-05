namespace Fanex.BetList.RnGCasino.Tests.Builder.ChoiceBuilder
{
    using System;
    using System.Globalization;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using Core.Templates;
    using Core.Utils;
    using NPOI.HSSF.UserModel;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2101Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2101();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildScore_Always_SetScoreBlockToNull()
        {
            // Act
            Choice_Template choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(choiceTemplate.Score);
        }

        [Test]
        public void BuildBetTeam_TransDescIsCorrectRefId_ShowGameTypeFromRemotingRef()
        {
            // Arrange
            _ticket.TransDesc = "1";
            _ticketHelper.GetResourceData("RNG_Casino_GT", "1").Returns("Baccarat");

            // Act
            Choice_Template choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            bool containingGameType = choiceTemplate.betTeam.Contains("Baccarat");
            Assert.IsTrue(containingGameType);
        }

        [Test]
        public void BuildBetTeam_TransDescIsCorrectRefId_HasColorHandicapCssClass()
        {
            // Arrange
            _ticket.TransDesc = "1";
            _ticketHelper.GetResourceData("RNG_Casino_GT", "1").Returns("Baccarat");

            // Act
            Choice_Template choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            bool containingColorHandicapCssClasss = choiceTemplate.betTeam.Contains("colorHandicap");
            Assert.IsTrue(containingColorHandicapCssClasss);
        }

        [Test]
        public void BuildBetTeam_TransDescIsCorrectRefId_WrapGameTypeWithColorHandicap()
        {
            // Arrange
            _ticket.TransDesc = "1";
            _ticketHelper.GetResourceData("RNG_Casino_GT", "1").Returns("Baccarat");

            // Act
            Choice_Template choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedMarkup = "<span class=\"colorHandicap\">Baccarat</span>";
            bool containingExpectedMarkup = choiceTemplate.betTeam.Contains(expectedMarkup);
            Assert.IsTrue(containingExpectedMarkup);
        }

        [Test]
        public void BuildBetTeam_BetCheckIsEmpty_NotContainPlayerHistoryNumber()
        {
            // Arrange
            _ticket.TransDesc = "1";
            _ticketHelper.GetResourceData("RNG_Casino_GT", "1").Returns("Baccarat");

            // Act
            Choice_Template choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedMarkup = "<span class=\"colorHandicap\">Baccarat</span> ";
            Assert.AreEqual(expectedMarkup, choiceTemplate.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetCheckIsNotEmpty_ContainPlayerHistoryNumber()
        {
            // Arrange
            _ticket.BetCheck = "Player History Number";

            // Act
            Choice_Template choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            bool containingPlayerHistoryNumber = choiceTemplate.betTeam.Contains("Player History Number");
            Assert.IsTrue(containingPlayerHistoryNumber);
        }

        [Test]
        public void BuildMatch_Always_SetBetTypeToNull()
        {
            // Act
            Choice_Template choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(choiceTemplate.BetType);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapToNull()
        {
            // Act
            Choice_Template choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNullOrEmpty(choiceTemplate.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeam_Always_SetBetTeamCssClassNameToUnderdog()
        {
            // Act
            Choice_Template choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("underdog", choiceTemplate.betTeamClassName);
        }

        [Test]
        public void BuildMatch_Always_SetMatchToNull()
        {
            // Act
            Choice_Template choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(choiceTemplate.Match);
        }

        [Test]
        public void AdjustBetTeamToRTF_NotContainPlayerHistoryNumber_RemoveHtmlMarkup()
        {
            // Arrange
            _ticket.TransDesc = "1";
            _ticketHelper.GetResourceData("RNG_Casino_GT", "1").Returns("Baccarat");

            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, Arg.Any<bool>(), CreateRTFHelperStub());

            // Assert
            Assert.AreEqual("Baccarat", _choice.Template.betTeam);
        }

        [Test]
        public void AdjustBetTeamToRTF_ContainPlayerHistoryNumber_RemoveHtmlMarkup()
        {
            // Arrange
            _ticket.TransDesc = "1";
            _ticket.BetCheck = "Player History Number";
            _ticketHelper.GetResourceData("RNG_Casino_GT", "1").Returns("Baccarat");

            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, Arg.Any<bool>(), CreateRTFHelperStub());

            // Assert
            Assert.AreEqual("Baccarat Player History Number", _choice.Template.betTeam);
        }

        [Test]
        public void BuildLeague_Always_ReturnFromRemotingRef()
        {
            // Arrange
            _ticket.TransDesc = "1";
            _ticketHelper.GetResourceData("RNG_Casino_GG", "1").Returns("Baccarat");

            // Act
            Choice_Template choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            bool containingGameGroup = choiceTemplate.League.LeagueName.leagueName.Contains("Baccarat");
            Assert.IsTrue(containingGameGroup);
        }

        [Test]
        public void BuildLeague_BetCheckIsNotEmpty_NotContainDetailLink()
        {
            // Arrange
            _ticket.BetCheck = "Player History Number";

            // Act
            Choice_Template choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            bool containingDetailsLink = choiceTemplate.League.ToString().Contains("Details");
            Assert.IsFalse(containingDetailsLink);
        }

        [Test]
        public void BuildLeague_BetCheckIsEmpty_ContainDetailLink()
        {
            // Act
            Choice_Template choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            bool containingDetailsLink = choiceTemplate.League.ToString().Contains("Details");
            Assert.IsTrue(containingDetailsLink);
        }

        [Test]
        public void BuildLeague_BetCheckIsEmpty_DetailLinkContainsCustIdTransIdStatus()
        {
            // Arrange
            var winLostDate = DateTime.Now;
            _ticket.TransId = 123;
            _ticket.CustId = 456;
            _ticket.WinlostDate = winLostDate;

            // Act
            Choice_Template choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedJsFunction = string.Format("showRnGCasinoDetails(123, '{0}', 456, 0);", winLostDate.ToString(CultureInfo.InvariantCulture));
            bool containingExpectedJsFunction = choiceTemplate.League.ToString().Contains(expectedJsFunction);
            Assert.IsTrue(containingExpectedJsFunction);
        }

        private RTFHelper CreateRTFHelperStub()
        {
            var workbook = new HSSFWorkbook();
            var rtfHelper = new RTFHelper()
            {
                NegFont = workbook.CreateFont(),
                NormalFont = workbook.CreateFont(),
                PosFont = workbook.CreateFont(),
                NegFontCrossed = workbook.CreateFont(),
                NormalFontCrossed = workbook.CreateFont(),
                PosFontCrossed = workbook.CreateFont(),
                RTFRenderer = new RtfTextRender()
            };

            return rtfHelper;
        }
    }
}