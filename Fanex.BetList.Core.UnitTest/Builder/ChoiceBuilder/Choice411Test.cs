namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice411Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice411();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.FirstHalfDrawNoBetOwn;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetType_Always_ShowParentBetTypeName()
        {
            // Arrange.
            var parentBetTypeId = BetTypes.FirstHalfDrawNoBet.ToString();
            var parentBetTypeName = "Parent Bet Type";
            _ticketHelper.GetParentIdByBetTypeId(Arg.Any<object>()).Returns(parentBetTypeId);
            _ticketHelper.GetBetTypeNameById(parentBetTypeId, Arg.Any<object>()).Returns(parentBetTypeName);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(parentBetTypeName, _choice.Template.BetType.betTypeName);
        }
    }
}