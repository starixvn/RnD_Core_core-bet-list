namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common;
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Utils;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice610Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = TestData.CreateTicket(BetTypes.QuarterXOverUnder);
            _choice = new Choice610();
        }

        [Test]
        public void BuildBetTeam_BetTeamIsOver_ShowOver()
        {
            // Arrange
            _ticket.BetTeam = "o";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Arrange
            Assert.AreEqual("Over", _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsUnder_ShowUnder()
        {
            // Arrange
            _ticket.BetTeam = "u";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Arrange
            Assert.AreEqual("Under", _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_IsBetOver_BetTeamClassNameIsFavorite()
        {
            // Arrange
            _ticket.BetTeam = "o";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("favorite", _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_IsBetUnder_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = "u";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("underdog", _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicap1Value()
        {
            // Arrange
            _ticket.Handicap1 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedHandicap = ConvertByBetType.Hdp(1);
            Assert.AreEqual(expectedHandicap, _choice.Template.Handicap.handicap);
        }
    }
}