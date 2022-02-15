namespace Fanex.BetList.RnGCasino.Tests.Builder.StatusBuilder
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.RegularExpressions;
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using Core.Templates;
    using Core.Utils;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using NSubstitute;
    using NUnit.Framework;

    public class Status2101Test
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status2101();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_BetCheckIsEmpty_HideResultLink()
        {
            // Act
            Status_Template statusTemplate = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(statusTemplate.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_BetCheckIsNotEmpty_ShowResultLink()
        {
            // Arrange
            _ticket.BetCheck = "Player History Number";
            _ticket.Status = BetStatus.Won;

            // Act
            Status_Template statusTemplate = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(statusTemplate.StatusResult.Visible);
        }

        [Test]
        public void BuildResultLRF_Always_ContainStatusOnly()
        {
            // Act
            IRichTextString statusTemplate = _status.RenderRTF(_ticket, _ticketHelper, null, false, CreateRTFHelperStub());

            // Assert
            var htmlTagMatching = new Regex("</?div[^<>]*[^<>]*>");
            var containingHtmlMarkups = htmlTagMatching.IsMatch(statusTemplate.ToString());
            Assert.IsFalse(containingHtmlMarkups);
        }

        [Test]
        public void BuildResult_BetCheckIsEmpty_ResultIsAlwaysText()
        {
            // Act
            Status_Template statusTemplate = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var htmlTagMatching = new Regex("</?div[^<>]*[^<>]*>");
            var containingHtmlMarkups = htmlTagMatching.IsMatch(statusTemplate.result);
            Assert.IsFalse(containingHtmlMarkups);
        }

        [Test]
        public void BuildResult_BetCheckIsNotEmptyAndResultIsNotRunning_ResultIsText()
        {
            // Arrange
            _ticket.BetCheck = "Player History Number";
            _ticket.Status = BetStatus.Void;

            // Act
            Status_Template statusTemplate = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var htmlTagMatching = new Regex("</?div[^<>]*[^<>]*>");
            var containingHtmlMarkups = htmlTagMatching.IsMatch(statusTemplate.result);
            Assert.IsFalse(containingHtmlMarkups);
        }

        [Test]
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2100:CodeLineMustNotBeLongerThan", Justification = "Reviewed.")]
        public void BuildResult_BetCheckIsNotEmptyAndResultIsRunning_ResultIsALink()
        {
            // Arrange
            _ticket.BetCheck = "Player History Number";
            _ticket.Status = BetStatus.Running;
            _ticket.WinlostDate = new DateTime(2015, 12, 21);

            // Act
            Status_Template statusTemplate = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedMarkup = "<div class=\"result\" onclick=\"ViewResult(0, 0, 0, 0, 'Player History Number', '', '12/21/2015 12:00:00 AM', 0, 0, 0, 0);\">Running</div>";
            Assert.AreEqual(expectedMarkup, statusTemplate.result);
        }

        [Test]
        public void BuildResult_BetCheckIsNotEmptyAndResultIsRunning_ResultContainsBetCheck()
        {
            // Arrange
            _ticket.BetCheck = "Player History Number";
            _ticket.Status = BetStatus.Running;
            _ticket.WinlostDate = new DateTime(2015, 12, 21);

            // Act
            Status_Template statusTemplate = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var containingBetCheck = statusTemplate.result.Contains("Player History Number");
            Assert.IsTrue(containingBetCheck);
        }

        private RTFHelper CreateRTFHelperStub()
        {
            var workbook = new HSSFWorkbook();
            var rtfHelper = new RTFHelper
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