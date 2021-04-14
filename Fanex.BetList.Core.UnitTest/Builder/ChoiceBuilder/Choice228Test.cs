namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice228Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice228();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.OverUnder;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_BetTeamIsCharacterH_BetTeamIsOver()
        {
            // Arrange
            _ticket.BetTeam = "o";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = "Over";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsCharacterA_BetTeamIsUnder()
        {
            // Arrange
            _ticket.BetTeam = "u";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = "Under";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Over_betTeamClassNameIsFavorite()
        {
            // Arrange
            _ticket.BetTeam = "o";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("favorite", _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Under_betTeamClassNameIsFavorite()
        {
            // Arrange
            _ticket.BetTeam = "u";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("underdog", _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Handicap_CorrectFormat()
        {
            // Arrange
            _ticket.Handicap1 = 14;
            _ticket.OddsSpread = 0.5m;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedFormat = "0.5<span class='favorite'> (00:00 - 15:00)</span>";
            Assert.AreEqual(expectedFormat, _choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetFigure_Always_RemoveHTML()
        {
            // Arrange
            var rtfHelper = new RTFHelper().Clone();
            _ticket.Handicap1 = 14;
            _ticket.OddsSpread = 0.5m;

            // Act
            _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);

            // Assert
            Assert.IsFalse(_choice.Template.Handicap.handicap.Contains("<span"));
            Assert.IsFalse(_choice.Template.Handicap.handicap.Contains("</span>"));
        }
    }
}