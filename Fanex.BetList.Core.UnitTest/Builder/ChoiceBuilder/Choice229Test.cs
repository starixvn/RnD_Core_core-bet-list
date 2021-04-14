namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice229Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice229();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.TimeMachineHDP;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket.Handicap1 = 14;
        }        

        [TestCase("h","favorite")]
        [TestCase("H", "favorite")]
        [TestCase("a", "underdog")]
        [TestCase("A", "underdog")]
        public void BuildBetTeamClassNameAndHandicap_HomeIsFavorite_ClassNameEqualExpected(string betteam, string expectedClass)
        {
            // Arrange
            _ticket.OddsSpread = -0.5m;
            _ticket.BetTeam = betteam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedClass, _choice.Template.betTeamClassName);
        }

        [TestCase("h", "underdog")]
        [TestCase("H", "underdog")]
        [TestCase("a", "favorite")]
        [TestCase("A", "favorite")]
        public void BuildBetTeamClassNameAndHandicap_AwayIsFavorite_ClassNameEqualExpected(string betteam, string expectedClass)
        {
            // Arrange
            _ticket.OddsSpread = 0.5m;
            _ticket.BetTeam = betteam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedClass, _choice.Template.betTeamClassName);
        }

        [TestCase("h", "-0.5<span class='favorite'> (00:00 - 15:00)</span>")]
        [TestCase("H", "-0.5<span class='favorite'> (00:00 - 15:00)</span>")]
        [TestCase("a", "0.5<span class='favorite'> (00:00 - 15:00)</span>")]
        [TestCase("A", "0.5<span class='favorite'> (00:00 - 15:00)</span>")]
        public void BuildBetTeamClassNameAndHandicap_HomeIsFavorite_HandicapEqualExpected(string betteam, string expectedHandicap)
        {
            // Arrange            
            _ticket.OddsSpread = -0.5m;
            _ticket.BetTeam = betteam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedHandicap, _choice.Template.Handicap.handicap);
        }

        [TestCase("h", "0.5<span class='favorite'> (00:00 - 15:00)</span>")]
        [TestCase("H", "0.5<span class='favorite'> (00:00 - 15:00)</span>")]
        [TestCase("a", "-0.5<span class='favorite'> (00:00 - 15:00)</span>")]
        [TestCase("A", "-0.5<span class='favorite'> (00:00 - 15:00)</span>")]
        public void BuildBetTeamClassNameAndHandicap_AwayIsFavorite_HandicapEqualExpected(string betteam, string expectedHandicap)
        {
            // Arrange            
            _ticket.OddsSpread = 0.5m;
            _ticket.BetTeam = betteam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedHandicap, _choice.Template.Handicap.handicap);
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