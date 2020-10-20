namespace Fanex.BetList.SexyGaming.Test.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status3401Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void Setup()
        {
            _status = new Status3401();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_TrandescDataIsEmpty_HideStatusResultLink()
        {
            ////Arrange
            _ticket.TransDesc = string.Empty;

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_TrandescDataIsNotEmpty_ShowStatusResultLink()
        {
            ////Arrange
            _ticket.BetId = 1;
            _ticket.CustId = 123;
            _ticket.TransDesc = "TransDesc";
            _ticket.MatchCode = "MatchCode";

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual("1, 'MatchCode', 123, 'TransDesc'", _status.Template.StatusResult.betId);
        }
    }
}