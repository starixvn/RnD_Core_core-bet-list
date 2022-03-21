namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice9638Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice9638();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Home1stInnsWicketXMethodofDismissal6Way;
            _ticket.BetId = 99;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketHelper.GetBetTypeNameById(BetTypes.Home1stInnsWicketXMethodofDismissal6Way, Arg.Any<object>(), (long?)99, Arg.Any<object>()).Returns("Bettype name 99");
        }

        [TestCase("c", "Caught")]
        [TestCase("b", "Bowled")]
        [TestCase("lbw", "LBW")]
        [TestCase("ro", "Run Out")]
        [TestCase("s", "Stumped")]
        [TestCase("ot", "Others")]
        public void BuildBetTeam_Always_ShowCorrectText(string betteam, string teamName)
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
