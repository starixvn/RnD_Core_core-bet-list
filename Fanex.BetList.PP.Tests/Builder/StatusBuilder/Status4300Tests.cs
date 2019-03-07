namespace Fanex.BetList.PP.Tests.Builder.StatusBuilder
{
    using Core.App_GlobalResources;
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using Core.Templates;
    using Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    public class Status4300Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status4300();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("won")]
        [TestCase("lose")]
        [TestCase("draw")]
        public void BuildStatusResult_StatusIsWon_HideResultLink(string status)
        {
            // Arrange.
            _ticket.Status = status;

            // Act.
            Status_Template template = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.False(template.StatusResult.Visible);
        }
    }
}