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

        [Test]
        public void BuildStatusResult_BetCheckDataIsEmpty_HideStatusResultLink()
        {
            ////Arrange
            _ticket.BetCheck = string.Empty;

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }

        [Test]
        public void BuildStatusResult_BetCheckIsNotEmpty_ShowStatusResultLink()
        {
            ////Arrange
            _ticket.BetId = 123;
            _ticket.CustId = 1234;
            _ticket.TransDesc = "transdesc";
            _ticket.BetCheck = "BetCheck";
            _ticket.MatchCode = "MatchCode";

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual("123, '', 1234, 'MatchCode'", _status.Template.StatusResult.betId);
        }
    }
}