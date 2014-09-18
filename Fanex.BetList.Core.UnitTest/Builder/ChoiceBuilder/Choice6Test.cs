namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice6 class.
    /// </summary>
    [TestFixture]
    public class Choice6Test
    {
        private const string BetTeamTemplate = " <span class=\"favorite\">{0}</span>&nbsp;{1} <span class=\"favorite\">&nbsp;{2}</span>";
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice6();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Total_Goal;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.Handicap.handicap is always null.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        /// <summary>
        /// The value of Template.betTeam is always bet team.
        /// </summary>
        [Test]
        public void BuildBetTeam_HomeIdNotZero_BetTeamAreSpaceAndBetTeam()
        {
            // Arrange
            _ticket.BetTeam = "bet team";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format("&nbsp;{0}", _ticket.BetTeam);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always favorite.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

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
            Assert.AreEqual(string.Join(null, new string[] { "&nbsp;", CoreBetList.FourAndOver }), _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs7dashOver_SetTemplateBetTeamIsResource7AndOver()
        {
            // Arrange
            _ticket.BetTeam = "7-over";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(string.Join(null, new string[] { "&nbsp;", CoreBetList.SevenAndOver }), _choice.Template.betTeam);
        }
    }
}