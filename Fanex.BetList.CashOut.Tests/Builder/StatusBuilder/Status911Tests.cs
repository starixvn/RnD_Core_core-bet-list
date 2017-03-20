namespace Fanex.BetList.CashOut.Tests.Builder.StatusBuilder
{
    using Core.App_GlobalResources;
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using Core.UnitTest.Common;
    using Core.Utils;
    using NPOI.HSSF.UserModel;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status911Tests
    {
        private const string ResultHtmlFormat = "{0}<br/><div style='font-weight: normal;'>({1})</div>";

        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;
        private RTFHelper _rtfHelper;

        [SetUp]
        public void Setup()
        {
            _ticket = TestData.CreateTicket(911);
            _ticketHelper = Substitute.For<ITicketHelper>();
            _status = new Status911();
            _rtfHelper = CreateRTFHelper();
        }

        [TestCase("Void")]
        [TestCase("Refund")]
        public void BuildResult_StatusIsVoidOrRefund_ShowCashOutBelowStatus(string status)
        {
            // Arrange
            _ticket.Status = status;
            var expectedTemplate = string.Format(ResultHtmlFormat, status, CoreBetList.CashOut);

            // Act
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedTemplate, template.result);
        }

        [TestCase("Win")]
        [TestCase("Lose")]
        [TestCase("Draw")]
        [TestCase("Reject")]
        [TestCase("Waiting")]
        [TestCase("Running")]
        public void BuildResult_StatusIsNotVoidOrRefund_ShowCashOutInstead(string status)
        {
            // Arrange
            _ticket.Status = status;
            var expectedTemplate = CoreBetList.CashOut;

            // Act
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedTemplate, template.result);
        }

        [Test]
        public void BuildStatusResult_Always_Hide()
        {
            // Act
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(template.StatusResult.Visible);
        }

        [TestCase("Void")]
        [TestCase("Refund")]
        public void BuildResultLRF_StatusIsVoidOrRefund_RemoveHtmlTag(string status)
        {
            // Arrange
            _ticket.Status = status;
            var expectedTemplate = $"{status}\n({CoreBetList.CashOut})";

            // Act
            var template = _status.RenderRTF(_ticket, _ticketHelper, null, false, _rtfHelper);

            // Assert
            Assert.IsTrue(template.String.Contains(expectedTemplate));
        }

        [TestCase("Win")]
        [TestCase("Lose")]
        [TestCase("Draw")]
        [TestCase("Reject")]
        [TestCase("Waiting")]
        [TestCase("Running")]
        public void BuildResultLRF_StatusIsNotVoidOrRefund_RemoveHtmlTag(string status)
        {
            // Arrange
            _ticket.Status = status;
            var expectedTemplate = CoreBetList.CashOut;

            // Act
            var template = _status.RenderRTF(_ticket, _ticketHelper, null, false, _rtfHelper);

            // Assert
            Assert.IsTrue(template.String.Contains(expectedTemplate));
        }

        [Test]
        public void BuildResultLRF_Always_SetPosFont()
        {
            // Act
            var template = _status.RenderRTF(_ticket, _ticketHelper, null, false, _rtfHelper);

            // Assert
            Assert.AreEqual(template.GetFontAtIndex(0), _rtfHelper.PosFont.Index);
        }

        private RTFHelper CreateRTFHelper()
        {
            var workbook = new HSSFWorkbook();

            return new RTFHelper()
            {
                NegFont = workbook.CreateFont(),
                NormalFont = workbook.CreateFont(),
                PosFont = workbook.CreateFont(),
                NegFontCrossed = workbook.CreateFont(),
                NormalFontCrossed = workbook.CreateFont(),
                PosFontCrossed = workbook.CreateFont(),
                RTFRenderer = new RtfTextRender()
            };
        }
    }
}