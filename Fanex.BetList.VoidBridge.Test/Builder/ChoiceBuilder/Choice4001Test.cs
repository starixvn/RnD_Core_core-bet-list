namespace Fanex.BetList.VoidBridge.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice4001Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup() {
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _choice = new Choice4001();
        }


    }
}
