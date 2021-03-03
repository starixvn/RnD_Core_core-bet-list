namespace Fanex.BetList.SportsLottery.Test.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status3611Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void Setup()
        {
            _status = new Status3611();
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

        [Test]
        public void BuildStatusResult_Always_BetIdIsCustId()
        {
            ////Arrange
            _ticket.CustId = 123;

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual("123", _status.Template.StatusResult.betId);
        }
    }
}