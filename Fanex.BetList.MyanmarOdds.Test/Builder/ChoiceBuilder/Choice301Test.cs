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

        [Test]
        public void BuildBetTeamClassNameAndHandicap_WhenCalled_SetPercentageValue()
        {
            _ticket.Handicap1 = 0;
            _ticket.Handicap2 = 1;
            _ticket.OddsSpread = 75;

            _choice.Render(_ticket, _ticketHelper, null, false);

            const string ExpectedHandicap = "-1 <span class='favorite'>(75)</span>";
            Assert.AreEqual(ExpectedHandicap, _choice.Template.Handicap.handicap);
        }
    }
}
