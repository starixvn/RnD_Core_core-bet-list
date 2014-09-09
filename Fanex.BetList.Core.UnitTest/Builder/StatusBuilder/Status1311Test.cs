namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    ///  Test Class Status for Bet Type 1311.
    /// </summary>
    public class Status1311Test
    {
        private Status1311 _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status1311();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase(1)]
        [TestCase(12)]
        public void BuildStatusResult_BetIdHasLengthLessThanOrEqual2_RefNoOfIsZero(int betId)
        {
            // Arrange
            _ticket.BetId = betId;

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert            
            Assert.AreEqual("0", _status.Template.StatusResult.refNo);
        }

        [Test]
        public void BuildStatusResult_BetIdHasLengthGreaterThan2_RefNoOfIsSubstringOfBetIdFromIndexOneAndLengthIsTwo()
        {
            // Arrange
            _ticket.BetId = 1536;

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert            
            Assert.AreEqual("53", _status.Template.StatusResult.refNo);
        }
    }
}