namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice15Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice15();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.FirstHalf1X2;
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
        public void BuildBetTeam_Always_SetBetTeamAreHtResourceAndPointAndBetTeamToUpper()
        {
            // Arrange
            _ticket.BetTeam = "bet team";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = string.Format("{0}.{1}", CoreBetList.ht, _ticket.BetTeam.ToUpperInvariant());
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
    }
}