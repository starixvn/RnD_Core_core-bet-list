namespace Fanex.BetList.CashOut.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice912Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticket = TestData.CreateTicket(912);
            _ticket.BetTypeId = 912;
            _choice = new Choice912();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void Render_Always_ReturnCashOutLabelWithOriginalBetTypeTemplate()
        {
            // Arrange
            _ticket.OriginalBetType = 6;
            _ticket.RefNo = "10000";
            _ticket.BetdaqId = 324345345324;

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            _ticket.BetTypeId = 6;
            var cashOutLabelHtml = "<div class=\"underdog\">Bought from No.: 324345345324</div>";
            var choice = new Choice6();
            var originalBetTypeTemplate = choice.Render(_ticket, _ticketHelper, null, false);
            originalBetTypeTemplate.betTeam = cashOutLabelHtml + originalBetTypeTemplate.betTeam;

            Assert.AreEqual(originalBetTypeTemplate.ToString(), template.ToString());
        }
    }
}