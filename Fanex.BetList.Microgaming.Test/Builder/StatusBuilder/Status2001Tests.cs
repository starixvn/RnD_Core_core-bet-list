namespace Fanex.BetList.Microgaming.Test.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status2001Tests
    {
        [Test]
        public void BuildStatusResult_BetIdParam_ContainsBetCheckAndCustId()
        {
            // Arrange
            var ticketHelper = Substitute.For<ITicketHelper>();
            var ticket = new Ticket();
            ticket.Status = "win";
            ticket.BetId = 10;
            ticket.BetCheck = "1";
            ticket.CustId = 111;

            // Act
            var template = new Status2001().Render(ticket, ticketHelper, null, true);

            // Assert
            var expectedLink = $"10,'1',111";
            Assert.AreEqual(expectedLink, template.StatusResult.betId);
        }
    }
}