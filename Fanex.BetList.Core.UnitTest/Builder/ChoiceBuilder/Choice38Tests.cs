namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice38Tests
    {
        private Choice38 choice;
        private ITicket ticket;
        private ITicketHelper ticketHelper;

        [SetUp]
        public void SetUp()
        {
            choice = new Choice38();
        }

        [Test]
        public void BuildBetTeam_Always_Empty()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}