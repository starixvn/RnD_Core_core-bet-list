namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice418Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice418();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("YO", "Yes & Over")]
        [TestCase("YU", "Yes & Under")]
        [TestCase("NO", "No & Over")]
        [TestCase("NU", "No & Under")]
        public void BuildBetTeam_BetTeam_TemplateBetTeam(string betTeam, string expected)
        {
            ////Arrange
            _ticket.BetTeam = betTeam;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            ////Assert
            Assert.AreEqual(expected, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Handicap_ShowTotalGoals()
        {
            ////Arrange
            _ticket.Handicap1 = 2;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            ////Assert
            Assert.AreEqual(_choice.Template.Handicap.handicap, "2");
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            ////Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            ////Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }
    }
}
