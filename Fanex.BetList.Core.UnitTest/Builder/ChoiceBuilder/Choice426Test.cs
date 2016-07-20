namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice426Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice426();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("H1", "HomeTeam to Win by 1 Goal")]
        [TestCase("H2+", "HomeTeam to Win by 2up Goals")]
        [TestCase("D", "Score Draw")]
        [TestCase("A1", "AwayTeam to Win by 1 Goal")]
        [TestCase("A2+", "AwayTeam to Win by 2up Goals")]
        [TestCase("NG", "No Goal")]
        public void BuildBetTeam_BetTeam_TemplateBetTeam(string betTeam, string expected)
        {
            //Arrange
            _ticket.HomeId = 123;
            _ticket.AwayId = 456;
            _ticket.BetTeam = betTeam;
            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns<string>("HomeTeam");
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns<string>("AwayTeam");

            //Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            //Assert
            Assert.AreEqual(expected, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }
    }
}
