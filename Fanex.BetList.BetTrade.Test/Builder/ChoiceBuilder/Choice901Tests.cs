namespace Fanex.BetList.BetTrade.Test.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice901Tests
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
            _ticket.BetTypeId = BetTradeBetTypes.Sold;
            _ticket.BetId = 1;

            _choice.Render(_ticket, _ticketHelper, Arg.Any<List<ITicketData>>(), Arg.Any<bool>());

            Assert.AreEqual(BetTradeBetTypes.Sold, _ticket.BetTypeId);
        }

        [Test]
        public void RenderRTF_WhenCalled_DoNotChangeBetType()
        {
            var rtfHelper = new RTFHelper().Clone();

            _ticket.BetTypeId = BetTradeBetTypes.Sold;
            _ticket.BetId = 1;

            _choice.RenderRTF(_ticket, _ticketHelper, Arg.Any<List<ITicketData>>(), Arg.Any<bool>(), rtfHelper);

            Assert.AreEqual(BetTradeBetTypes.Sold, _ticket.BetTypeId);
        }
    }
}