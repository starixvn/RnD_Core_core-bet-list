namespace Fanex.BetList.CockFighting.Tests.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1003Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1003();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("Betchoice=1;CockOwner=a,b", "1", "Meron1")]
        [TestCase("Betchoice=2;CockOwner=a,b", "2", "Wala1")]
        [TestCase("Betchoice=3;CockOwner=a,b", "3", "BDD1")]
        [TestCase("Betchoice=4;CockOwner=a,b", "4", "FTD1")]
        public void BuildBetTeam_FromTransDesc_ReturnValidMemberChoice(string transDesc, string betChoiceId, string expectedMemberChoice)
        {
            _ticket.TransDesc = transDesc;
            _ticketHelper.GetResourceData("CF", betChoiceId).Returns(expectedMemberChoice);

            var actualMemberChoice = _choice.Render(_ticket, _ticketHelper, null, false).betTeam;

            Assert.AreEqual(expectedMemberChoice, actualMemberChoice);
        }

        [TestCase("Betchoice=10;CockOwner=a,b", "")]
        [TestCase("Betchoice=0;CockOwner=a,b", "")]
        public void BuildBetTeam_BetChoiceIsNotRangeFrom1To4_ReturnEmptyMemberChoice(string transDesc, string expectedMemberChoice)
        {
            _ticket.TransDesc = transDesc;

            var actualMemberChoice = _choice.Render(_ticket, _ticketHelper, null, false).betTeam;

            Assert.AreEqual(expectedMemberChoice, actualMemberChoice);
        }

        [TestCase("Betchoice1=10;CockOwner=a,b")]
        [TestCase("")]
        public void BuildBetTeam_TransDescDoesNotContainBetChoice_ReturnEmptyMemberChoice(string transDesc)
        {
            _ticket.TransDesc = transDesc;

            var actualMemberChoice = _choice.Render(_ticket, _ticketHelper, null, false).betTeam;

            Assert.AreEqual(string.Empty, actualMemberChoice);
        }

        [TestCase("Betchoice=10,CockOwner=a,b")]
        public void BuildBetTeam_TransDescContainsInvalidData_ReturnEmptyMemberChoice(string transDesc)
        {
            _ticket.TransDesc = transDesc;

            var actualMemberChoice = _choice.Render(_ticket, _ticketHelper, null, false).betTeam;

            Assert.AreEqual(string.Empty, actualMemberChoice);
        }

        [TestCase("Betchoice=1;CockOwner=ABC,b", "ABC")]
        [TestCase("Betchoice=1;CockOwner=c,b", "c")]
        public void BuildMatch_FromTransDesc_ReturnValidHomeTeam(string transDesc, string expectHomeTeam)
        {
            _ticket.TransDesc = transDesc;

            var actualHomeTeam = _choice.Render(_ticket, _ticketHelper, null, false).Match.homeTeam;

            Assert.AreEqual(expectHomeTeam, actualHomeTeam);
        }

        [TestCase("Betchoice=1;CockOwne1r=ABC,b")]
        public void BuildMatch_TransDescDoesNotContainCockOwner_ReturnEmptyMatch(string transDesc)
        {
            _ticket.TransDesc = transDesc;

            var actualMatch = _choice.Render(_ticket, _ticketHelper, null, false).Match;

            Assert.AreEqual(string.Empty, actualMatch.homeTeam);
            Assert.AreEqual(string.Empty, actualMatch.awayTeam);
        }

        [TestCase("Betchoice=1;CockOwner=ABC,DEF", "DEF")]
        [TestCase("Betchoice=1;CockOwner=c,b", "b")]
        public void BuildMatch_FromTransDesc_ReturnValidAwayTeam(string transDesc, string expectAwayTeam)
        {
            _ticket.TransDesc = transDesc;

            var actualAwayTeam = _choice.Render(_ticket, _ticketHelper, null, false).Match.awayTeam;

            Assert.AreEqual(expectAwayTeam, actualAwayTeam);
        }

        [TestCase("1", "Arena1")]
        [TestCase("2", "Arena2")]
        public void BuildLeague_FromBetCheck_GetArenaNameFromRemotingRef(string betCheck, string expectedArenaName)
        {
            _ticket.BetCheck = betCheck;
            _ticketHelper.GetLeagueNameById(Arg.Any<string>()).Returns(expectedArenaName);

            var actualArenaName = _choice.Render(_ticket, _ticketHelper, null, false).League.LeagueName.leagueName;

            Assert.IsTrue(actualArenaName.Contains(expectedArenaName));
        }

        [Test]
        public void BuildLeague_FromMatchId_ReturnArenaNameWithFightNo()
        {
            _ticket.MatchId = 12121;
            _ticketHelper.GetLeagueNameById(Arg.Any<string>()).Returns("Arena1");

            var actualArenaNameAndFightNo = _choice.Render(_ticket, _ticketHelper, null, false).League.LeagueName.leagueName;

            Assert.AreEqual("Arena1 [Fight No. 12121]", actualArenaNameAndFightNo);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_MemberChoiceIsMeron_BetTeamClassIsMeronChoice()
        {
            _ticket.TransDesc = "Betchoice=1;CockOwne1r=ABC,b";

            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.AreEqual("meron-choice", _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_MemberChoiceIsWala_BetTeamClassIsWalaChoice()
        {
            _ticket.TransDesc = "Betchoice=2;CockOwne1r=ABC,b";

            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.AreEqual("wala-choice", _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_MemberChoiceIsBDD_BetTeamClassIsBDDChoice()
        {
            _ticket.TransDesc = "Betchoice=3;CockOwne1r=ABC,b";

            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.AreEqual("bdd-choice", _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_MemberChoiceIsFTD_BetTeamClassIsFTDChoice()
        {
            _ticket.TransDesc = "Betchoice=4;CockOwne1r=ABC,b";

            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.AreEqual("ftd-choice", _choice.Template.betTeamClassName);
        }
    }
}