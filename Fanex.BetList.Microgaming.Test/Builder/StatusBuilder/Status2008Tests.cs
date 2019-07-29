namespace Fanex.BetList.Microgaming.Test.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status2008Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status2008();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket.Status = BetStatus.Won;
            _ticket.TransDesc = "534534534";
        }

        [TestCase(" ")]
        [TestCase(null)]
        public void BuildStatusResult_TransDescIsNullOrWhiteSpace_HideStatusResult(string transDesc)
        {
            // Arrange
            _ticket.TransDesc = transDesc;

            // Act
            var template = _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.False(template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_TransDescIsNotNullOrWhiteSpace_ShowStatusResult()
        {
            // Act
            var template = _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.True(template.StatusResult.Visible);
        }

        [TestCase(10, 10)]
        [TestCase(null, 0)]
        public void BuildStatusResult_TransDescIsNotNullOrWhiteSpace_BetIdParamContainsBetCheckAndCustId(int? betId, int expectedBetId)
        {
            // Arrange
            _ticket.BetId = betId;
            _ticket.BetCheck = "10";
            _ticket.CustId = 100;

            // Act
            var template = _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            var expectedLink = $"{expectedBetId},'10',100";
            Assert.AreEqual(expectedLink, template.StatusResult.betId);
        }
    }
}