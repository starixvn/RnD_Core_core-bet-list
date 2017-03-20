namespace Fanex.BetList.CashOut.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice912Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticket = TestData.CreateTicket(912);
            _ticket.BetTypeId = 912;
            _choice = new Choice912();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void Render_Always_ReturnBoughtLabel()
        {
            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(template.ToString().Contains("Bought"));
        }
    }
}