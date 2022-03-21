namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice9547Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice9547();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.HomeGroup1XRunsFancy;
            _ticket.Handicap1 = 1;
            _ticket.Handicap2 = 2;
            _ticket.BetId = 99;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketHelper.GetBetTypeNameById(BetTypes.HomeGroup1XRunsFancy, Arg.Any<object>(), (long?)99, Arg.Any<object>()).Returns("Bettype name 99");
        }


        [TestCase("y", "Yes")]
        [TestCase("Y", "Yes")]
        [TestCase("n", "No")]
        [TestCase("N", "No")]
        public void BuildBetTeam_Always_ShowTeamName(string betteam, string teamName)
        {
            // Arrange
            _ticket.BetTeam = betteam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(teamName, _choice.Template.betTeam);
        }

        [TestCase("y", "2")]
        [TestCase("Y", "2")]
        [TestCase("n", "1")]
        [TestCase("N", "1")]
        public void BuildBetTeamClassNameAndHandicap_Always_ShowHandicap(string betteam, string handicap)
        {
            // Arrange
            _ticket.BetTeam = betteam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(handicap, _choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeamClassName = "favorite";
            Assert.AreEqual(expectedBetTeamClassName, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetType_Always_ShowBetTypeNameAndReplaceX()
        {
            // Arrange

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("Bettype name 99", _choice.Template.BetType.betTypeName);
        }
    }
}