namespace Fanex.BetList.BetTrade.Test.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice901Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice901();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void Render_WhenCalled_DoNotChangeBetType()
        {
            _ticket.BetTypeId = 901;
            _ticket.BetId = 1;

            _choice.Render(_ticket, _ticketHelper, Arg.Any<List<ITicketData>>(), Arg.Any<bool>());

            const int BetType = 901;
            Assert.AreEqual(BetType, _ticket.BetTypeId);
        }

        [Test]
        public void RenderRTF_WhenCalled_DoNotChangeBetType()
        {
            _ticket.BetTypeId = 901;
            _ticket.BetId = 1;

            _choice.Render(_ticket, _ticketHelper, Arg.Any<List<ITicketData>>(), Arg.Any<bool>());

            const int BetType = 901;
            Assert.AreEqual(BetType, _ticket.BetTypeId);
        }
    }
}
