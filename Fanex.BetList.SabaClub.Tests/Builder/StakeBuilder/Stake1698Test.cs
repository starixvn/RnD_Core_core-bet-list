namespace Fanex.BetList.SabaClub.Tests.Builder.StakeBuilder
{
    using Fanex.BetList.Core.Builder.StakeBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Stake1698Test
    {
        [Test]
        public void BuildStake_Always_ShowHyphenIcon()
        {
            // Arrange
            var ticket = new Ticket { Stake = 100 };
            var stake = new Stake1698();
            var ticketHelper = Substitute.For<ITicketHelper>();

            // Act
            var template = stake.Render(ticket, ticketHelper);

            // Assert
            Assert.AreEqual("-", template.stake);
        }
    }
}
