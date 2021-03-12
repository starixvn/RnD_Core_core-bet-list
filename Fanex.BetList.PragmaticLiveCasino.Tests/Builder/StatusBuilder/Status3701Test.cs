namespace Fanex.BetList.PragmaticLiveCasino.Test.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status3701Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void Setup()
        {
            _status = new Status3701();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase(BetStatus.Won, "1, '', 123, 'MatchCode'")]
        [TestCase(BetStatus.Lose, "1, '', 123, 'MatchCode'")]
        [TestCase(BetStatus.Draw, "1, '', 123, 'MatchCode'")]
        [TestCase(BetStatus.Running, "0")]
        [TestCase(BetStatus.Waiting, "0")]
        public void BuildStatusResult_ReturnCorrectResult(string ticketStatus, string expectedResult)
        {
            ////Arrange
            _ticket.BetId = 1;
            _ticket.CustId = 123;
            _ticket.MatchCode = "MatchCode";
            _ticket.Status = ticketStatus;

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual(expectedResult, _status.Template.StatusResult.betId);
        }
    }
}