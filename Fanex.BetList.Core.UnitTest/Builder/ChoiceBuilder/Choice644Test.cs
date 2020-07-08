namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice644Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice644();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.QuarterCorrectScore;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("")]
        public void BuildBetTeam_InvalidBetTeam_ReturnEmpty(string betteam)
        {
            // Arrange
            _ticket.BetTeam = betteam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsEmpty(_choice.Template.betTeam);
        }       

        [TestCase("2:1", "2-1")]
        [TestCase("3:0", "3-0")]
        [TestCase("3:1", "3-1")]
        [TestCase("4:0", "4-0")]
        [TestCase("2:2", "2-2")]
        [TestCase("1:2", "1-2")]
        [TestCase("0:3", "0-3")]
        [TestCase("1:3", "1-3")]
        [TestCase("0:4", "0-4")]
        [TestCase("aos", "AOS")]
        public void BuildBetTeam_ValidBetTeam_ReturnExpectedBetChoice(string betTeam, string expectedBetChoice)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedBetChoice, _choice.Template.betTeam);
        }

        [Test]
        public void BuildScore_IsNotAOS_HideExcluding()
        {
            // Arrange
            _ticket.BetTeam = "1:0";

            // Act
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(act.Score.ToString());

        }
        
        [TestCase("2:1,3:0", "<div class=\"excluding-choices\">Excluding: 2-1, 3-0</div>")]
        public void BuildScore_TransDesc_ShowExcluding(string transDesc, string expectedExcluding)
        {
            // Arrange
            _ticket.BetTeam = "aos";
            _ticket.TransDesc = transDesc;

            // Act
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedExcluding, act.Score.ToString());

        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }
    }
}