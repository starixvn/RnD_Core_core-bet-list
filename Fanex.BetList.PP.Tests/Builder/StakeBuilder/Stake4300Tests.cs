namespace Fanex.BetList.PP.Tests.Builder.StakeBuilder
{
    using Fanex.BetList.Core.Builder.StakeBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Stake4300Tests
    {
        [Test]
        public void BuildStake_StakeIsZero_ReturnHyphen()
        {
            // Arrange
            var ticket = new Ticket { Stake = 0 };
            var stake = new Stake4300();
            var ticketHelper = Substitute.For<ITicketHelper>();

            // Act
            var template = stake.Render(ticket, ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }
    }
}