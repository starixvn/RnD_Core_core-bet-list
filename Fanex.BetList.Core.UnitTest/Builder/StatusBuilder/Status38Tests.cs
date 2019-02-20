namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Builder.StatusBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status38Tests
    {
        [Test]
        public void BuildStatusResult_Race_IsBetCheck()
        {
            // Arrange
            var ticket = new Ticket { BetCheck = "123", Status = "lose" };
            var status = new Status38();

            // Act
            var template = status.Render(ticket, Substitute.For<ITicketHelper>(), null, false);

            // Assert
            Assert.AreEqual("123", template.StatusResult.betId);
        }
    }
}