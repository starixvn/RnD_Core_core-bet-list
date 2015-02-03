namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    ///  Unit test for Choice172.
    /// </summary>
    [TestFixture]
    public class Choice172Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice172();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("HH", "Home team name/Home team name")]
        [TestCase("HD", "Home team name/Draw")]
        [TestCase("HA", "Home team name/Away team name")]
        [TestCase("AH", "Away team name/Home team name")]
        [TestCase("AD", "Away team name/Draw")]
        [TestCase("AA", "Away team name/Away team name")]
        [TestCase("NO", "None")]
        [TestCase("hh", "Home team name/Home team name")]
        [TestCase("hd", "Home team name/Draw")]
        [TestCase("ha", "Home team name/Away team name")]
        [TestCase("ah", "Away team name/Home team name")]
        [TestCase("ad", "Away team name/Draw")]
        [TestCase("aa", "Away team name/Away team name")]
        [TestCase("no", "None")]
        [TestCase("abc", "")]
        public void BuildBetTeam_BetTeam_TemplateBetTeam(string betTeam, string expected)
        {
            // Arrange
            _ticket.HomeId = 123;
            _ticket.AwayId = 456;
            _ticket.BetTeam = betTeam;
            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns<string>("Home team name");
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns<string>("Away team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
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
