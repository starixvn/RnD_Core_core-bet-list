namespace Fanex.BetList.ESports.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice9002Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = Substitute.For<ITicket>();
            _choice = new Choice9002();

            _ticket.BetCheck = "80102";
            _ticket.BetId = 80102;
        }

        [Test]
        public void BuildBetType_Always_PassBetIdAndBetCheckToRemotingRefWhenGettingBetTypeName()
        {
            // Arrange.
            const string betTypeName = "Bet Type Name";

            _ticketHelper
                .GetBetTypeNameById(
                    Arg.Is(_ticket.BetTypeId),
                    Arg.Is(_ticket.RefNo),
                    Arg.Is(_ticket.BetId),
                    Arg.Is(_ticket.BetCheck))
                .Returns(betTypeName);

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(betTypeName, template.BetType.betTypeName);
        }
    }
}