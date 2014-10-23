namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    ///  Test Class Status for Bet Type 154.
    /// </summary>
    public class Status154Test
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status154();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        public void BuildStatusResult_BetIdIsNull_BetIdIsZero()
        {
            // Arrange
            _ticket.BetId = null;

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("0", _status.Template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_BetIdIsAnyValue_BetIdIsBetId()
        {
            // Arrange
            _ticket.BetId = 80101;

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("80101", _status.Template.StatusResult.betId);
        }
    }
}