namespace Fanex.BetList.Allbet.Tests.Builder.ChoiceBuilder
{
    using System;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    class Choice2401Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2401();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_TransDecsHasDataOfBetChoice_ReturnBetChoiceName()
        {
            var choiceName = "Player";
            _ticket.TransDesc = "choice=1001*comms=1*tableCode=B001*result=P;S6;H2:B;D10;D5 *results=B;8:P;5";

            _ticketHelper.GetResourceData("AllBet_BT", "1001").Returns(choiceName);

            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.AreEqual(choiceName, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TransDecsDoesNotHasDataOfBetChoice_ReturnEmpty()
        {
            _ticket.TransDesc = "comms=1*tableCode=B001*result=P;S6;H2:B;D10;D5 *results=B;8:P;5";

            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [TestCase(0)]
        [TestCase(1)]
        public void BuildLeague_ForTodaySummaryTicket_ReturnDetailLinkHtml(int statusId)
        {
            _ticket.TransDesc = string.Empty;
            _ticket.TransId = 1;
            _ticket.TransDate = DateTime.Today;
            _ticket.StatusId = statusId;

            _choice.Render(_ticket, _ticketHelper, null, false);

            string expectedTemplate = BuildExpectedTemplateDetailLinkHtml(_ticket);

            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }

        [Test]
        public void BuildLeague_ForYesterdaySummaryTicket_ReturnDetailLinkHtml()
        {
            _ticket.TransDesc = string.Empty;
            _ticket.TransId = 1;
            _ticket.TransDate = DateTime.Today.AddDays(-1);

            _choice.Render(_ticket, _ticketHelper, null, false);

            string expectedTemplate = BuildExpectedTemplateDetailLinkHtml(_ticket);

            Assert.IsTrue(_choice.Template.League.ToString().Contains(expectedTemplate));
        }


        private string BuildExpectedTemplateDetailLinkHtml(ITicket ticket)
        {
            return string.Format(
                "<div class=\"detail\"" +
                " onclick='showTodayRunningBetsDetails(&quot;{0}&quot;,&quot;{1}&quot;,&quot;{2}&quot;);'>" +
                "<a style=\"color:#755200;font-weight:bold\"" +
                " href=\"javascript:void(&quot;&quot;);\">Details</a></div>",
                ticket.TransId,
                ticket.StatusId,
                ticket.TransDate);
        }
    }
}
