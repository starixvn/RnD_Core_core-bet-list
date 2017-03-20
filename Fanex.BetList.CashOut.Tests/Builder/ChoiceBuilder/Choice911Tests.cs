namespace Fanex.BetList.CashOut.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using Core.UnitTest.Common;
    using Core.Utils;
    using NPOI.HSSF.UserModel;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice911Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;
        private RTFHelper _rtfHelper;

        [SetUp]
        public void Setup()
        {
            _ticket = TestData.CreateTicket(911);
            _ticket.BetTypeId = 911;
            _choice = new Choice911();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _rtfHelper = CreateRTFHelper();
        }

        [Test]
        public void Render_Always_ReturnCashOutLabelWithOriginalBetTypeTemplate()
        {
            // Arrange
            _ticket.OriginalBetType = 6;
            _ticket.RefNo = "10000";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            _ticket.BetTypeId = 6;
            var cashOutLabelHtml = "<div class=\"underdog\">Cash out from No.: 10000</div>";
            var choice = new Choice6();
            var originalBetTypeTemplate = choice.Render(_ticket, _ticketHelper, null, false);
            var expectedTemplate = cashOutLabelHtml + originalBetTypeTemplate;

            Assert.AreEqual(expectedTemplate, template.ToString());
        }

        [Test]
        public void GetBetTypeName_Always_ReturnBetTypeNameOfOriginalBetType()
        {
            // Arrange
            _ticket.OriginalBetType = 1;
            var expectedBetTypeName = "Handicap";
            _ticketHelper.GetBetTypeNameById(1, Arg.Any<object>()).Returns(expectedBetTypeName);

            // Act
            var betTypeName = _choice.GetBetTypeName(_ticket, _ticketHelper, null);

            // Assert
            Assert.AreEqual(expectedBetTypeName, betTypeName);
        }

        [Test]
        public void RenderRTF_Always_ReturnCashOutLabelWithOriginalBetTypeTemplate()
        {
            // Arrange
            _ticket.OriginalBetType = 1;
            _ticket.RefNo = "10000";

            // Act
            var template = _choice.RenderRTF(_ticket, _ticketHelper, null, false, _rtfHelper);

            // Assert
            var choice = new Choice1();
            var cashOutLabel = "Cash out from No.: 10000";
            var originalBetTypeTemplate = choice.RenderRTF(_ticket, _ticketHelper, null, false, _rtfHelper);
            var expectedTemplate = cashOutLabel + "\n" + originalBetTypeTemplate[0].String;
            Assert.AreEqual(expectedTemplate, template[0].String);
        }

        [Test]
        public void RenderRTF_CashOutLabel_HasPostFont()
        {
            // Arrange
            _ticket.OriginalBetType = 1;
            _ticket.RefNo = "10000";

            // Act
            var template = _choice.RenderRTF(_ticket, _ticketHelper, null, false, _rtfHelper);

            // Assert
            Assert.AreEqual(template[0].GetFontAtIndex(0), _rtfHelper.PosFont.Index);
        }

        [Test]
        public void RenderRTF_OriginalBetTypeTemplate_DoesNotChangeFont()
        {
            // Arrange
            _ticket.OriginalBetType = 1;
            _ticket.RefNo = "10000";

            // Act
            var template = _choice.RenderRTF(_ticket, _ticketHelper, null, false, _rtfHelper);

            // Assert
            var choice = new Choice1();
            var originalBetTypeTemplate = choice.RenderRTF(_ticket, _ticketHelper, null, false, _rtfHelper);
            Assert.AreEqual(originalBetTypeTemplate[0].NumFormattingRuns, template[0].NumFormattingRuns - 1);
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