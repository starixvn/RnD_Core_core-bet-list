namespace Fanex.BetList.NumberGame.Test.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status3601Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void Setup()
        {
            _status = new Status3601();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }
       
        [Test]
        public void BuildStatusResult_Always_RefNoIsRefNo()
        {
            ////Arrange
            _ticket.RefNo = "refnooo";

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual("'refnooo'", _status.Template.StatusResult.refNo);
        }
    }
}