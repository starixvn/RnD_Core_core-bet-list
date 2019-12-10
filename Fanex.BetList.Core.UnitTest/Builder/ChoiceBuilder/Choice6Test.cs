namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice6Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice6();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.TotalGoal;
            _ticketHelper = Substitute.For<ITicketHelper>();
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
        public void BuildBetTeam_HomeIdNotZero_BetTeamAreSpaceAndBetTeam()
        {
            // Arrange
            _ticket.BetTeam = "bet team";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = string.Format("&nbsp;{0}", "bet team");
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
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

        [Test]
        public void BuildBetTeam_BetTeamIs4dashOver_SetTemplateBetTeamIsResource4AndOver()
        {
            // Arrange
            _ticket.BetTeam = "4-over";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(string.Join(null, new[] { "&nbsp;", CoreBetList.FourAndOver }), _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs7dashOver_SetTemplateBetTeamIsResource7AndOver()
        {
            // Arrange
            _ticket.BetTeam = "7-over";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(string.Join(null, new[] { "&nbsp;", CoreBetList.SevenAndOver }), _choice.Template.betTeam);
        }
    }
}