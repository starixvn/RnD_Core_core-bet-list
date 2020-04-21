namespace Fanex.BetList.Indo4D.Tests.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice5001Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice5001();
            _ticket = TestData.CreateTicket(Indo4DBetTypes.FourD);
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("")]
        [TestCase("drawtype,drawdate,BetContent")]
        [TestCase("drawtype;drawdate;")]
        [TestCase("drawtype;drawdate")]
        [TestCase("drawtype;")]
        [TestCase("drawtype")]
        public void BuildBetTeam_InvalidTransDesc_BetChoiceIsEmpty(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsTrue(string.IsNullOrWhiteSpace(act.betTeam));
        }

        [TestCase("drawtype;drawdate;BetContent", "BetContent")]
        [TestCase("drawtype|drawdate|BetContent", "BetContent")]
        public void BuildBetTeam_InvalidTransDesc_BetChoiceIsEmpty(string transDesc, string expectedBetChoice)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetChoice, act.betTeam);
        }

        [TestCase("")]
        [TestCase("drawtype,drawdate,BetContent")]
        [TestCase("drawtype;drawdate")]
        [TestCase("drawtype;")]
        [TestCase("drawtype")]
        public void BuildLeague_InvalidTransDesc_BetChoiceIsEmpty(string transDesc)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsTrue(string.IsNullOrWhiteSpace(act.League.LeagueName.leagueName));
        }

        [TestCase("drawtype;drawdate;BetContent", "drawtype drawdate")]
        [TestCase("drawtype|drawdate|BetContent", "drawtype drawdate")]
        public void BuildLeague_InvalidTransDesc_BetChoiceIsEmpty(string transDesc, string expectedBetChoice)
        {
            // Arrange.
            _ticket.TransDesc = transDesc;

            // Act.
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetChoice, act.League.LeagueName.leagueName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsOption()
        {
            // Arrange.
            _ticket.TransDesc = "SG4D;2020/03/24;2480;B;L";
            var expectedOption = "<div>Lucky pick / Box</div>";

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedOption, template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsFavorite()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectedCssClassName = "favorite";

            Assert.AreEqual(expectedCssClassName, template.betTeamClassName);
        }

        [Test]
        public void BuildScore_Always_ScoreBlockIsNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(template.Score);
        }

        [Test]
        public void BuildMatch_Always_MatchBlockIsNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Match);
        }
    }
}