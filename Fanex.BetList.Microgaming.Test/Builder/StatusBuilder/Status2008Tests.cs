namespace Fanex.BetList.Microgaming.Test.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status2008Tests
    {
        [TestCase(" ")]
        [TestCase(null)]
        public void BuildStatusResult_TransDescIsNullOrWhiteSpace_HideStatusResult(string transDesc)
        {
            // Arrange
            var ticketHelper = Substitute.For<ITicketHelper>();
            var ticket = new Ticket
            {
                Status = "WIN",
                TransDesc = transDesc
            };

            // Act
            var template = new Status2008().Render(ticket, ticketHelper, null, true);

            // Assert
            Assert.False(template.StatusResult.Visible);
        }

        [Test]
        public void BuildStatusResult_TransDescIsNotNullOrWhiteSpace_ShowStatusResult()
        {
            // Arrange
            var ticketHelper = Substitute.For<ITicketHelper>();
            var ticket = new Ticket
            {
                Status = "WIN",
                TransDesc = "534534534"
            };

            // Act
            var template = new Status2008().Render(ticket, ticketHelper, null, true);

            // Assert
            Assert.True(template.StatusResult.Visible);
        }

        [TestCase(10, 10)]
        [TestCase(null, 0)]
        public void BuildStatusResult_TransDescIsNotNullOrWhiteSpace_BetIdParamContainsBetCheckAndCustId(int? betId, int expectedBetId)
        {
            // Arrange
            var ticketHelper = Substitute.For<ITicketHelper>();
            var ticket = new Ticket
            {
                BetId = betId,
                BetCheck = "10",
                CustId = 100,
                Status = "WIN",
                TransDesc = "534534534"
            };

            // Act
            var template = new Status2008().Render(ticket, ticketHelper, null, true);

            // Assert
            var expectedLink = $"{expectedBetId},'10',100";
            Assert.AreEqual(expectedLink, template.StatusResult.betId);
        }
    }
}