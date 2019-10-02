namespace Fanex.BetList.CashOut.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
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
            _ticket = TestData.CreateTicket(CashOutBetTypes.CashOut);
            _choice = new Choice911();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _rtfHelper = CreateRTFHelper();
        }

        [Test]
        public void Render_Always_ReturnCashOutLabelWithOriginalBetTypeTemplate()
        {
            // Arrange
            _ticket.OriginalBetType = BetTypes.TotalGoal;
            _ticket.RefNo = "10000";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string cashOutLabelHtml = "<div class=\"underdog\">Cash Out from No.: 10000</div>";
            _ticket.BetTypeId = BetTypes.TotalGoal;
            var choice = new Choice6();
            var originalBetTypeTemplate = choice.Render(_ticket, _ticketHelper, null, false);
            originalBetTypeTemplate.betTeam = cashOutLabelHtml + originalBetTypeTemplate.betTeam;

            Assert.AreEqual(originalBetTypeTemplate.ToString(), template.ToString());
        }

        [Test]
        public void GetBetTypeName_Always_ReturnBetTypeNameOfOriginalBetType()
        {
            // Arrange
            const string expectedBetTypeName = "Handicap";

            _ticket.OriginalBetType = BetTypes.Handicap;
            _ticketHelper.GetBetTypeNameById(BetTypes.Handicap, Arg.Any<object>()).Returns(expectedBetTypeName);

            // Act
            var betTypeName = _choice.GetBetTypeName(_ticket, _ticketHelper);

            // Assert
            Assert.AreEqual(expectedBetTypeName, betTypeName);
        }

        [Test]
        public void RenderRTF_Always_ReturnCashOutLabelWithOriginalBetTypeTemplate()
        {
            // Arrange
            _ticket.OriginalBetType = BetTypes.Handicap;
            _ticket.RefNo = "10000";

            // Act
            var template = _choice.RenderRTF(_ticket, _ticketHelper, null, false, _rtfHelper);

            // Assert
            const string cashOutLabel = "Cash Out from No.: 10000";

            var choice = new Choice1();
            var originalBetTypeTemplate = choice.RenderRTF(_ticket, _ticketHelper, null, false, _rtfHelper);
            var expectedTemplate = cashOutLabel + "\n" + originalBetTypeTemplate[0].String;
            Assert.AreEqual(expectedTemplate, template[0].String);
        }

        [Test]
        public void RenderRTF_CashOutLabel_HasPostFont()
        {
            // Arrange
            _ticket.OriginalBetType = BetTypes.Handicap;
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
            _ticket.OriginalBetType = BetTypes.Handicap;
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