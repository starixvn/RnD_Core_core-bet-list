namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1302Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1302();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.MatchCorrectScore3rd;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_LengthBetTeamNotEqual2_BetTeamIsEmpty()
        {
            // Arrange
            _ticket.BetTeam = "bet team";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("&nbsp;", _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsAB_BetTeamIsAColonB()
        {
            // Arrange
            _ticket.BetTeam = "ab";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("&nbsp;a:b", _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedTeamClassName = "favorite";
            Assert.AreEqual(expectedTeamClassName, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }
    }
}