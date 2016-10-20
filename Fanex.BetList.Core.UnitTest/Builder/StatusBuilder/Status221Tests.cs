namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status221Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void SetUp()
        {
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _status = new Status221();
        }

        [Test]
        public void BuildStatusResult_FromHandicap1_BetIdIsHandicap1()
        {
            // Arrange
            var handicap1 = 10;
            _ticket.Handicap1 = handicap1;

            // Act
            _status.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(handicap1.ToString(), _status.Template.StatusResult.betId);
        }
    }
}