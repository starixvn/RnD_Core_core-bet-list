namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1313 class.
    /// </summary>
    [TestFixture]
    public class Choice1313Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1313();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.SetXTotalGames3Way;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.betTeam is always Over.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs1_BetTeamIsOverResource()
        {
            // Arrange
            _ticket.BetTeam = "1";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.over, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always Over.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs2_BetTeamIsUnderResource()
        {
            // Arrange
            _ticket.BetTeam = "2";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.under, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always Between handicap 2 and handicap1.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamNotIs1AndNotIs2_BetTeamIsBetweenResourceHandicap2AndHandicap1()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format(CoreBetList.betweenXandY, _ticket.Handicap2, _ticket.Handicap1);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.BetType.betTypeName is always bet type name.
        /// </summary>
        [Test]
        public void BuildBetTeam_Always_SetBetTypeNameIsBetTypeName()
        {
            // Arrange
            _ticket.BetTypeId = 1;
            _ticket.BetId = 1;
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>(), Arg.Any<long>()).Returns("Bet type name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTypeName = _ticketHelper.GetBetTypeNameById(_ticket.BetTypeId, _ticket.BetId);
            Assert.AreEqual(expectedBetTypeName, _choice.Template.BetType.betTypeName);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always favorite when BetTeam = 1.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs1_BetTeamClassNameIsFavorite()
        {
            // Arrange
            _ticket.BetTeam = "1";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always favorite when BetTeam != 1.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_BetTeamIsNot1_BetTeamClassNameIsUnderdog()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_UNDERDOG = "underdog";
            Assert.AreEqual(CSS_CLASS_UNDERDOG, _choice.Template.betTeamClassName);
        }
    }
}