namespace Fanex.BetList.GDG.Test.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status5701Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IStatus _status;

        [SetUp]
        public void Setup()
        {
            _status = new Status5701();
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
            _ticket.BetId = 123;
            _ticket.CustId = 1234;
            _ticket.TransDesc = "transdesc";
            _ticket.BetCheck = "BetCheck";
            _ticket.MatchCode = "MatchCode";

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual("123, 'MatchCode', 1234, 'BetCheck'", _status.Template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_Always_ShowSiteIdFromLeague()
        {
            ////Arrange
            _ticket.TransDesc = "transdesc";
            _ticket.BetTeam = "SiteId";

            ////Act
            _status.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual("SiteId", _status.Template.StatusResult.league);
        }
    }
}