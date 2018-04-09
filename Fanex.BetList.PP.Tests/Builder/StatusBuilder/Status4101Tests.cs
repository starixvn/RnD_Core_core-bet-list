namespace Fanex.BetList.PP.Tests.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using Core.Templates;
    using NSubstitute;
    using NUnit.Framework;

    public class Status4101Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status4101();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase(null)]
        [TestCase("")]
        public void Render_TicketBetCheckIsNullOrEmpty_HideResultLink(string betCheck)
        {
            // Arrange.
            _ticket.BetCheck = betCheck;

            // Act.
            Status_Template template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.False(template.StatusResult.Visible);
        }

        [Test]
        public void Render_TicketBetCheckIsNotNullOrEmpty_ShowResultLink()
        {
            // Arrange.
            _ticket.BetCheck = "BetCheck";

            // Act.
            Status_Template template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.True(template.StatusResult.Visible);
        }

        [Test]
        public void Render_TicketBetCheckIsNotNullOrEmpty_AppendBetCheckAndCustIdAfterBetIdInParameterListOfResultLink()
        {
            // Arrange.
            _ticket.BetId = 123;
            _ticket.BetCheck = "BetCheck";
            _ticket.CustId = 456;

            // Act.
            Status_Template template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedParameters = $"123, 'BetCheck', 456";

            Assert.AreEqual(expectedParameters, template.StatusResult.betId);
        }
    }
}