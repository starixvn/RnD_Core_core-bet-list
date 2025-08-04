namespace Fanex.BetList.SexyGaming.Tests.Builder.ChoiceBuilder
{
    using System;
    using System.Globalization;
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice11500Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice11500();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket.BetTypeId = 11500;
        }

        [Test]
        public void BuildBetTeam_SummaryTicket_CallGetResourceDataOfTicketHelperToGetExpectedBetOption()
        {
            _ticket.BetCheck = null;
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.DidNotReceive().GetResourceData(Arg.Is(Choice11500.ChoiceRefPrefix), Arg.Is(_ticket.BetTypeId.ToString()));
        }

        [Test]
        public void BuildBetTeam_DetailTicket_CallGetResourceDataOfTicketHelperToGetExpectedBetOption()
        {
            _ticket.BetCheck = "Detail";
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetResourceData(Arg.Is(Choice11500.ChoiceRefPrefix), Arg.Is(_ticket.BetTypeId.ToString()));
        }
    }
}