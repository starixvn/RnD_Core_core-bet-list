namespace Fanex.BetList.Indo4D.Tests.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Status5001Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status5001();
            _ticket = TestData.CreateTicket(Indo4DBetTypes.FourD);
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_Always_HideResultLink()
        {
            // Act.
            var statusTemplate = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsFalse(statusTemplate.StatusResult.Visible);
        }
    }
}