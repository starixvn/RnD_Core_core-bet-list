namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice9490Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice9490();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.HomeOverXBallYExactRuns;
            _ticket.BetId = 99;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketHelper.GetBetTypeNameById(BetTypes.HomeOverXBallYExactRuns, Arg.Any<object>(), (long?)99, Arg.Any<object>()).Returns("Bettype name 99");
        }


        [TestCase("0", "0")]
        [TestCase("1", "1")]
        [TestCase("2", "2")]
        [TestCase("3", "3")]
        [TestCase("4", "4")]
        [TestCase("AO", "AO")]
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