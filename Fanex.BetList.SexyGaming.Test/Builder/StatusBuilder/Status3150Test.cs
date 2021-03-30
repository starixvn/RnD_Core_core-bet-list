namespace Fanex.BetList.SexyGaming.Test.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status3150Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void Setup()
        {
            _status = new Status3150();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("")]
        [TestCase("TransDesc")]
        public void BuildStatusResult_Always_HideStatusResultLink(string transDesc)
        {
            ////Arrange
            _ticket.TransDesc = transDesc;

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(string.IsNullOrEmpty(_status.Template.StatusResult.ToString()));
        }
    }
}