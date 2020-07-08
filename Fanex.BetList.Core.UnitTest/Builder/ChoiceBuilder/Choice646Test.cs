namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice646Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice646();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.MatchHandicapTotal;
            _ticket.HomeId = 1;
            _ticket.AwayId = 2;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketHelper.GetTeamNameById(Arg.Is<long>(i => i == 1)).Returns("Home Team");
            _ticketHelper.GetTeamNameById(Arg.Is<long>(i => i == 2)).Returns("Away Team");
        }

        [TestCase("")]
        [TestCase("h")]
        [TestCase("u")]
        [TestCase("o")]
        [TestCase("a")]
        [TestCase("hou")]
        public void BuildBetTeam_InvalidBetTeam_ReturnEmpty(string betteam)
        {
            // Arrange
            _ticket.BetTeam = betteam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [TestCase("ho", 197.5, -6.5, "<span class=\"favorite\"> Home Team</span><span class=\"handicap\"> -6.5</span><span class=\"underdog\"> &</span><span class=\"favorite\"> Over</span><span class=\"handicap\"> 197.5</span>")]
        [TestCase("ho", 197.5, 6.5, "<span class=\"underdog\"> Home Team</span><span class=\"handicap\"> 6.5</span><span class=\"underdog\"> &</span><span class=\"favorite\"> Over</span><span class=\"handicap\"> 197.5</span>")]
        [TestCase("HO", 197.5, 6.5, "<span class=\"underdog\"> Home Team</span><span class=\"handicap\"> 6.5</span><span class=\"underdog\"> &</span><span class=\"favorite\"> Over</span><span class=\"handicap\"> 197.5</span>")]

        [TestCase("hu", 197.5, -6.5, "<span class=\"favorite\"> Home Team</span><span class=\"handicap\"> -6.5</span><span class=\"underdog\"> &</span><span class=\"underdog\"> Under</span><span class=\"handicap\"> 197.5</span>")]
        [TestCase("hu", 197.5, 6.5, "<span class=\"underdog\"> Home Team</span><span class=\"handicap\"> 6.5</span><span class=\"underdog\"> &</span><span class=\"underdog\"> Under</span><span class=\"handicap\"> 197.5</span>")]
        [TestCase("HU", 197.5, 6.5, "<span class=\"underdog\"> Home Team</span><span class=\"handicap\"> 6.5</span><span class=\"underdog\"> &</span><span class=\"underdog\"> Under</span><span class=\"handicap\"> 197.5</span>")]

        [TestCase("ao", 197.5, -6.5, "<span class=\"underdog\"> Away Team</span><span class=\"handicap\"> 6.5</span><span class=\"underdog\"> &</span><span class=\"favorite\"> Over</span><span class=\"handicap\"> 197.5</span>")]
        [TestCase("ao", 197.5, 6.5, "<span class=\"favorite\"> Away Team</span><span class=\"handicap\"> -6.5</span><span class=\"underdog\"> &</span><span class=\"favorite\"> Over</span><span class=\"handicap\"> 197.5</span>")]
        [TestCase("AO", 197.5, 6.5, "<span class=\"favorite\"> Away Team</span><span class=\"handicap\"> -6.5</span><span class=\"underdog\"> &</span><span class=\"favorite\"> Over</span><span class=\"handicap\"> 197.5</span>")]

        [TestCase("au", 197.5, -6.5, "<span class=\"underdog\"> Away Team</span><span class=\"handicap\"> 6.5</span><span class=\"underdog\"> &</span><span class=\"underdog\"> Under</span><span class=\"handicap\"> 197.5</span>")]
        [TestCase("au", 197.5, 6.5, "<span class=\"favorite\"> Away Team</span><span class=\"handicap\"> -6.5</span><span class=\"underdog\"> &</span><span class=\"underdog\"> Under</span><span class=\"handicap\"> 197.5</span>")]
        [TestCase("AU", 197.5, 6.5, "<span class=\"favorite\"> Away Team</span><span class=\"handicap\"> -6.5</span><span class=\"underdog\"> &</span><span class=\"underdog\"> Under</span><span class=\"handicap\"> 197.5</span>")]

        [TestCase("ho", 197.5, 0, "<span class=\"underdog\"> Home Team</span><span class=\"handicap\"> 0</span><span class=\"underdog\"> &</span><span class=\"favorite\"> Over</span><span class=\"handicap\"> 197.5</span>")]
        [TestCase("hu", 197.5, 0, "<span class=\"underdog\"> Home Team</span><span class=\"handicap\"> 0</span><span class=\"underdog\"> &</span><span class=\"underdog\"> Under</span><span class=\"handicap\"> 197.5</span>")]
        [TestCase("ao", 197.5, 0, "<span class=\"underdog\"> Away Team</span><span class=\"handicap\"> 0</span><span class=\"underdog\"> &</span><span class=\"favorite\"> Over</span><span class=\"handicap\"> 197.5</span>")]
        [TestCase("au", 197.5, 0, "<span class=\"underdog\"> Away Team</span><span class=\"handicap\"> 0</span><span class=\"underdog\"> &</span><span class=\"underdog\"> Under</span><span class=\"handicap\"> 197.5</span>")]

        public void BuildBetTeam_ValidBetTeam_ReturnExpectedBetChoice(string betTeam, decimal hdp1, decimal hdp2, string expectedBetTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;
            _ticket.Handicap1 = hdp1;
            _ticket.Handicap2 = hdp2;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }
    }
}