namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice186Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice186();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_BetTeamIsInvalid_BetChoiceIsEmpty()
        {
            // Arrange.
            _ticket.BetTeam = "abc";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [TestCase("HD")]
        [TestCase("hd")]
        [TestCase("hD")]
        [TestCase("Hd")]
        public void BuildBetTeam_BetTeamIsHD_BetChoiceIsHomeOrDraw(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string expectation = "Home<span class=\"underdog\"> or </span>Draw";

            Assert.AreEqual(expectation, _choice.Template.betTeam);
        }

        [TestCase("HA")]
        [TestCase("ha")]
        [TestCase("hA")]
        [TestCase("Ha")]
        public void BuildBetTeam_BetTeamIsHA_BetChoiceIsHomeOrAway(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string expectation = "Home<span class=\"underdog\"> or </span>Away";

            Assert.AreEqual(expectation, _choice.Template.betTeam);
        }

        [TestCase("DA")]
        [TestCase("da")]
        [TestCase("dA")]
        [TestCase("Da")]
        public void BuildBetTeam_BetTeamIsDA_BetChoiceIsDrawOrAway(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string expectation = "Away<span class=\"underdog\"> or </span>Draw";

            Assert.AreEqual(expectation, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsFavorite()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }
    }
}