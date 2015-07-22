namespace Fanex.BetList.MetricGaming.Test.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice801Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice801();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("betchoice=1;BettypeTime=xxx;Time=xxx;SettledTime=xxx;GameResult=xxx;GameResultTimexxx")]
        [TestCase("GameResult=xxx;GameResultTimexxx;betchoice=1;BettypeTime=xxx;Time=xxx;SettledTime=xxx;")]
        public void BuildBetTeam_WhenCalled_ReturnBetChoiceName(string transDesc)
        {
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("SuperLive", "1").Returns("No Goal");

            _choice.Render(_ticket, _ticketHelper, null, false);

            const string ExpectedBetTeam = "No Goal";
            Assert.AreEqual(ExpectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_WhenCalled_AlwaysSetBetTeamClassNameIsFavorite()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            const string ExpectedBetTeamClassName = "favorite";
            Assert.AreEqual(ExpectedBetTeamClassName, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_WhenCalled_AlwaysSetHandicapIsBettypeTime()
        {
            _ticket.TransDesc = "betchoice=xxx;BettypeTime=06:50-07:50;Time=xxx;SettledTime=xxx;GameResult=xxx;GameResultTimexxx";

            _choice.Render(_ticket, _ticketHelper, null, false);

            const string ExpectedHandicap = "06:50-07:50";
            Assert.AreEqual(ExpectedHandicap, _choice.Template.Handicap.handicap);
        }
    }
}
