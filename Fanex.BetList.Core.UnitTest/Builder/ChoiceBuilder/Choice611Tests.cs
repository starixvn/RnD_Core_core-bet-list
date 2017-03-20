namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice611Tests
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = TestData.CreateTicket(611);
            _choice = new Choice611();
        }

        [Test]
        public void BuildBetTeam_BetTeamIsOdd_ShowOdd()
        {
            // Arrange
            _ticket.BetTeam = "o";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Arrange
            Assert.AreEqual("Odd", _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsEven_ShowEven()
        {
            // Arrange
            _ticket.BetTeam = "e";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Arrange
            Assert.AreEqual("Even", _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_IsBetOdd_BetTeamClassNameIsFavorite()
        {
            // Arrange
            _ticket.BetTeam = "o";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("favorite", _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_IsBetEven_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = "e";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("underdog", _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }
    }
}