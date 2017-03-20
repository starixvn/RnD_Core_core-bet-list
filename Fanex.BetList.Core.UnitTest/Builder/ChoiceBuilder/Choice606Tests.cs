namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice606Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = TestData.CreateTicket(606);
            _choice = new Choice606();
        }

        [Test]
        public void BuildBetType_Always_GetBetTypeNameById()
        {
            // Arrange
            _ticket.BetTypeId = 606;
            _ticket.RefNo = "10000";
            _ticket.BetCheck = "1";
            _ticket.BetId = 100;
            _ticketHelper.GetBetTypeNameById(_ticket.BetTypeId, _ticket.RefNo, _ticket.BetId, _ticket.BetCheck).Returns("Bet Type Name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("Bet Type Name", _choice.Template.BetType.betTypeName);
        }
    }
}