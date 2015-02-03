namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    ///  Unit test for Choice169.
    /// </summary>
    [TestFixture]
    public class Choice169Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice169();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("1-15", "1-15 Min")]
        [TestCase("abc", "abc Min")]
        [TestCase("None", "None")]
        [TestCase("NONE", "None")]
        [TestCase("none", "None")]
        public void BuildBetTeam_BetTeam_TemplateBetTeam(string betTeam, string expected)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

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
