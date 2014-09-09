namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Status test class for bet type 10.
    /// </summary>
    public class Status10Test
    {
        private Status10 _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status10();
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

        [Test]
        public void BuildStatusResult_Always_SetIsOutRightIs1()
        {
            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string IS_OUTRIGHT = "1";
            Assert.AreEqual(IS_OUTRIGHT, _status.Template.StatusResult.isoutright);
        }
    }
}