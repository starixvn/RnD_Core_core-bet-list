namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Charge Commission
    /// </summary>
    [TestFixture]
    public class Status500Tests
    {
        private Status500 _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _status = new Status500();
        }

        [Test]
        public void Render_Always_StatusResultIsNull()
        {
            ////Act
            var template = _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var statusResult = template.StatusResult;
            Assert.IsNull(statusResult);
        }
    }
}