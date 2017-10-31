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

        [TestCase("YO")]
        [TestCase("Yo")]
        [TestCase("yO")]
        [TestCase("yo")]
        public void BuildBetTeam_BetTeamIsYO_TemplateBetTeamIsYesAndOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            var expectedHtml = "Yes <span class=\"underdog\">&</span> Over";

            Assert.AreEqual(expectedHtml, _choice.Template.betTeam);
        }

        [TestCase("YU")]
        [TestCase("Yu")]
        [TestCase("yU")]
        [TestCase("yu")]
        public void BuildBetTeam_BetTeamIsYU_TemplateBetTeamIsYesAndUnder(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            var expectedHtml = "Yes <span class=\"underdog\">&</span> Under";

            Assert.AreEqual(expectedHtml, _choice.Template.betTeam);
        }

        [TestCase("NO")]
        [TestCase("No")]
        [TestCase("nO")]
        [TestCase("no")]
        public void BuildBetTeam_BetTeamIsNO_TemplateBetTeamIsNoAndOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            var expectedHtml = "No <span class=\"underdog\">&</span> Over";

            Assert.AreEqual(expectedHtml, _choice.Template.betTeam);
        }

        [TestCase("NU")]
        [TestCase("Nu")]
        [TestCase("nU")]
        [TestCase("nu")]
        public void BuildBetTeam_BetTeamIsNU_TemplateBetTeamIsNoAndUnder(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            var expectedHtml = "No <span class=\"underdog\">&</span> Under";

            Assert.AreEqual(expectedHtml, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Handicap_ShowTotalGoals()
        {
            // Arrange.
            _ticket.Handicap1 = 2;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(_choice.Template.Handicap.handicap, "2");
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }
    }
}