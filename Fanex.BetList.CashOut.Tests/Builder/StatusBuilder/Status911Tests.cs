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

        [Test]
        public void BuildCashOutHistory_WhenHideHistory_HideCashOutHistory()
        {
            // Arrange
            _ticketHelper.HideStatusCashOutHistory.Returns(true);

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(_status.Template.CashOutHistory.Visible);
        }

        [Test]
        public void BuildCashOutHistory_WhenShowHistory_ShowCashOutHistory()
        {
            // Arrange
            _ticketHelper.HideStatusCashOutHistory.Returns(false);

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(_status.Template.CashOutHistory.Visible);
        }

        [Test]
        public void BuildCashOutHistory_WhenShowHistory_TransIdIsTransId()
        {
            // Arrange
            _ticketHelper.HideStatusCashOutHistory.Returns(false);
            _ticket.TransId = 100;

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(_ticket.TransId.ToString(), _status.Template.CashOutHistory.transId);
        }

        [Test]
        public void BuildCashOutHistory_WhenShowHistory_RefNoIsRefNo()
        {
            // Arrange
            _ticketHelper.HideStatusCashOutHistory.Returns(false);
            _ticket.RefNo = "100";

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(_ticket.RefNo, _status.Template.CashOutHistory.refNo);
        }

        [Test]
        public void BuildCashOutHistory_WhenShowHistory_BetTypeIsBetType()
        {
            // Arrange
            _ticketHelper.HideStatusCashOutHistory.Returns(false);
            _ticket.BetTypeId = 1;

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(_ticket.BetTypeId.ToString(), _status.Template.CashOutHistory.betType);
        }

        [Test]
        public void BuildCashOutHistory_WhenShowHistory_BetdaqIdIsBetdaqId()
        {
            // Arrange
            _ticketHelper.HideStatusCashOutHistory.Returns(false);
            _ticket.BetdaqId = 1;

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(_ticket.BetdaqId.ToString(), _status.Template.CashOutHistory.betdaqId);
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

        [Test]
        public void BuildResult_Always_DisableDetectAbnormalBet()
        {
            // Arrange
            _ticket.Status = BetStatus.Void;
            _ticket.ExtraInfo = "<p1 ab=\"1\" />";

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual(
                $"{CoreBetList.voided}<br/><div style='font-weight: normal;'>({CoreBetList.CashOut})</div>",
                _status.Template.result);
        }

        private RTFHelper CreateRTFHelper()
        {
            var workbook = new HSSFWorkbook();

            return new RTFHelper
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