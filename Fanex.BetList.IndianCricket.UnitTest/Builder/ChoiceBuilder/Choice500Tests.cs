namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice500Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice500();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.SportTypeId = 50;
            _ticket.BetTypeId = 500;
        }

        [Test]
        public void BuildBetTeam_Always_BetTeamIsEmpty()
        {
            ////Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var betTeam = template.betTeam;
            Assert.AreEqual(string.Empty, betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsEmpty()
        {
            ////Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var betTeamClassName = template.betTeamClassName;
            Assert.AreEqual(string.Empty, betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            ////Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var handicap = template.Handicap;
            Assert.IsNull(handicap);
        }

        [Test]
        public void BuildScore_Always_ScoreIsNull()
        {
            ////Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var score = template.Score;
            Assert.IsNull(score);
        }
    }
}