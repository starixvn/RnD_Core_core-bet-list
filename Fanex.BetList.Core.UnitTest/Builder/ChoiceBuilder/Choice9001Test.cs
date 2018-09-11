namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.UnitTest.Common;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice9001Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = Substitute.For<ITicket>();
            _choice = new Choice9001();

            _ticket.BetTypeId = 9001;
            _ticket.RefNo = "8888888";
            _ticket.BetCheck = "80505";
            _ticket.BetId = 80505;
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