namespace Fanex.BetList.RnGCasino.Tests.Builder.StatusBuilder
{
    using System;
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
            Status_Template statusTemplate = _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsFalse(statusTemplate.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_BetCheckIsNotEmpty_ShowResultLink()
        {
            // Arrange
            _ticket.BetCheck = "Player History Number";

            // Act
            Status_Template statusTemplate = _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsTrue(statusTemplate.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_BetCheckIsNotEmpty_RefNoIsStringOfBetCheck()
        {
            // Arrange
            _ticket.BetCheck = "Player History Number";

            // Act
            Status_Template statusTemplate = _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedRefNo = "'Player History Number'";
            Assert.AreEqual(expectedRefNo, statusTemplate.StatusResult.refNo);
        }

        [Test]
        public void BuildResultLRF_Always_ContainStatusOnly()
        {
            // Act
            IRichTextString statusTemplate = _status.RenderRTF(_ticket, _ticketHelper, null, Arg.Any<bool>(), CreateRTFHelperStub());

            // Assert
            var htmlTagMatching = new Regex("</?div[^<>]*[^<>]*>");
            var containingHtmlMarkups = htmlTagMatching.IsMatch(statusTemplate.ToString());
            Assert.IsFalse(containingHtmlMarkups);
        }

        [Test]
        public void BuildResult_BetCheckIsEmpty_ResultIsAlwaysText()
        {
            // Act
            Status_Template statusTemplate = _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var htmlTagMatching = new Regex("</?div[^<>]*[^<>]*>");
            var containingHtmlMarkups = htmlTagMatching.IsMatch(statusTemplate.result.ToString());
            Assert.IsFalse(containingHtmlMarkups);
        }

        [Test]
        public void BuildResult_BetCheckIsNotEmptyAndResultIsNotRunning_ResultIsText()
        {
            // Arrange
            _ticket.BetCheck = "Player History Number";
            _ticket.Status = "void";

            // Act
            Status_Template statusTemplate = _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var htmlTagMatching = new Regex("</?div[^<>]*[^<>]*>");
            var containingHtmlMarkups = htmlTagMatching.IsMatch(statusTemplate.result.ToString());
            Assert.IsFalse(containingHtmlMarkups);
        }

        [Test]
        public void BuildResult_BetCheckIsNotEmptyAndResultIsRunning_ResultIsALink()
        {
            // Arrange
            _ticket.BetCheck = "Player History Number";
            _ticket.Status = "running";
            _ticket.WinlostDate = new DateTime(2015, 12, 21);

            // Act
            Status_Template statusTemplate = _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedMarkup = "<div class=\"result\" onclick=\"ViewResult(0, 0, 0, 0, 'Player History Number', '', '12/21/2015 12:00:00 AM', 0, 0, 0, 0);\">Running</div>";
            Assert.AreEqual(expectedMarkup, statusTemplate.result);
        }

        [Test]
        public void BuildResult_BetCheckIsNotEmptyAndResultIsRunning_ResultContainsBetCheck()
        {
            // Arrange
            _ticket.BetCheck = "Player History Number";
            _ticket.Status = "running";
            _ticket.WinlostDate = new DateTime(2015, 12, 21);

            // Act
            Status_Template statusTemplate = _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var containingBetCheck = statusTemplate.result.Contains("Player History Number");
            Assert.IsTrue(containingBetCheck);
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