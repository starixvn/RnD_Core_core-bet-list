namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice9400Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice9400();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.SuperOverWinner;
            _ticket.HomeId = 1;
            _ticket.AwayId = 2;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketHelper.GetTeamNameById((long)1).Returns("Team name 1");
            _ticketHelper.GetTeamNameById((long)2).Returns("Team name 2");
        }


        [TestCase("h","Team name 1")]
        [TestCase("H", "Team name 1")]
        [TestCase("a", "Team name 2")]
        [TestCase("A", "Team name 2")]
        public void BuildBetTeam_Always_ShowTeamName(string betteam, string teamName)
        {
            // Arrange            
            _ticket.BetTeam = betteam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(teamName, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsUnderdog()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeamClassName = "underdog";
            Assert.AreEqual(expectedBetTeamClassName, _choice.Template.betTeamClassName);
        }
    }
}