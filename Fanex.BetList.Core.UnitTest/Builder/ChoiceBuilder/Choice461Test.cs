namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Templates;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice461Test
    {
        private const int BetTypeId = 461;

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice461();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypeId;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetType_Always_ShowParentBetTypeName()
        {
            // Arrange.
            const string ParentBetTypeId = "401";
            const string ParentBetTypeName = "Parent Bet Type";

            _ticketHelper.GetParentIdByBetTypeId(BetTypeId).Returns(ParentBetTypeId);
            _ticketHelper.GetBetTypeNameById(ParentBetTypeId, Arg.Any<object>()).Returns(ParentBetTypeName);

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(ParentBetTypeName, template.BetType.betTypeName);
        }
    }
}