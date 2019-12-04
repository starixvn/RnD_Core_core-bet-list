namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Common;
    using Core.Builder.StatusBuilder;
    using Core.Constants;
    using Entities;
    using NUnit.Framework;
    using Stubs;

    public class Status486Tests
    {
        private Status486 _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelperStub;

        [SetUp]
        public void Setup()
        {
            _status = new Status486();
            _ticket = TestData.CreateTicket(BetTypes.CornersRace);
            _ticketHelperStub = new TicketHelperStub();
        }

        [TestCase(11.00, "811")]
        [TestCase(5.00, "805")]
        public void BuildStatusResult_BetId_IsHandicap1Value(decimal handicap1, string expectedBetId)
        {
            // Arrange
            _ticket.Handicap1 = handicap1;

            // Act
            var act = _status.Render(_ticket, _ticketHelperStub, null, true);

            // Assert
            Assert.AreEqual(expectedBetId, act.StatusResult.betId);
        }
    }
}