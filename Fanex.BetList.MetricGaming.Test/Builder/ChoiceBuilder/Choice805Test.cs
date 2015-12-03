namespace Fanex.BetList.MetricGaming.Test.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice805Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice805();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_WhenCalled_HandicapIsVisible()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsFalse(_choice.Template.Handicap.Visible);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_WhenCalled_AlwaysSetBetTeamClassNameIsFavorite()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            const string ExpectedBetTeamClassName = "favorite";
            Assert.AreEqual(ExpectedBetTeamClassName, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeam_WhenCalled_ReturnBetChoiceName()
        {
            _ticket.TransDesc = "betchoice=9;BettypeTime=ASE,57;Time=xxx;SettledTime=xxx;GameResult=xxx;GameResultTimexxx";
            _ticketHelper.GetResourceData("SuperLive", "9").Returns("Over");
            _ticketHelper.GetTeamNameById("ASE").Returns("BetTeam");

            _choice.Render(_ticket, _ticketHelper, null, false);

            const string ExpectedBetTeam = "BetTeam Over 57%";
            Assert.AreEqual(ExpectedBetTeam, _choice.Template.betTeam);
        }
    }
}
