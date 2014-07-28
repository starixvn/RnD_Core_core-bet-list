namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status1501Test
    {
        private Status1501 _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status1501();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_Always_SetStatusResultVisible()
        {
            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(_status.Template.StatusResult.Visible);
        }
    }
}