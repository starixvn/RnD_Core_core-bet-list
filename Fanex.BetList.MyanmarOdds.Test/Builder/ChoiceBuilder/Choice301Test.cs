namespace Fanex.BetList.MyanmarOdds.Test.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice301Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice301();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildMatch_WhenCalled_ScoreIsNull()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsNull(_choice.Template.Score);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_WhenCalled_OddsIsNull()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsNull(_choice.Template.Handicap.Odds);
        }

        [TestCase(0, "")]
        [TestCase(-1, "favorite")]
        [TestCase(4, "")]
        public void BuildBetTeamClassNameAndHandicap_WhenCalled_SetPercentageValue(decimal oddsSpread, string cssClass)
        {
            _ticket.Handicap1 = 0;
            _ticket.Handicap2 = 1;
            _ticket.OddsSpread = oddsSpread;

            _choice.Render(_ticket, _ticketHelper, null, false);

            string expectedHandicap = string.Format("-1 <span class='{0}'>({1})</span>", cssClass, oddsSpread);
            Assert.AreEqual(expectedHandicap, _choice.Template.Handicap.handicap);
        }
    }
}