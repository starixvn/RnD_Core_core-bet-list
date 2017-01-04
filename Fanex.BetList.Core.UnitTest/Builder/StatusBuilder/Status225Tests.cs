namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status225Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void SetUp()
        {
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _status = new Status225();
        }

        [Test]
        public void BuildStatusResult_WhenCurrentBetTypeIs225_StatusResultBetTypeIs221()
        {
            // Arrange
            var betType = 225;
            _ticket.BetTypeId = betType;

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("221", _status.Template.StatusResult.betType);
        }
    }
}