namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1235 class.
    /// </summary>
    [TestFixture]
    public class Choice1235Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1235();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.VT_ScoreBet;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.betTeam is always Win-0 when bet team of ticket is 1.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs1_BetTeamIsVirtualTennisScoreBetWin0Resource()
        {
            // Arrange
            _ticket.BetTeam = "1";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.virtualtennis_scorebet_win_0, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always Win-15 when bet team of ticket is 2.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs2_BetTeamIsVirtualTennisScoreBetWin15Resource()
        {
            // Arrange
            _ticket.BetTeam = "2";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.virtualtennis_scorebet_win_15, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always Win-30 when bet team of ticket is 3.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs3_BetTeamIsVirtualTennisScoreBetWin30Resource()
        {
            // Arrange
            _ticket.BetTeam = "3";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.virtualtennis_scorebet_win_30, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always Win-40 when bet team of ticket is 4.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs4_BetTeamIsVirtualTennisScoreBetWin40Resource()
        {
            // Arrange
            _ticket.BetTeam = "4";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.virtualtennis_scorebet_win_40, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always 0-Win when bet team of ticket is 5.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs1_BetTeamIsVirtualTennisScoreBet0WinResource()
        {
            // Arrange
            _ticket.BetTeam = "5";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.virtualtennis_scorebet_0_win, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always 15-Win when bet team of ticket is 6.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs2_BetTeamIsVirtualTennisScoreBet15WinResource()
        {
            // Arrange
            _ticket.BetTeam = "6";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.virtualtennis_scorebet_15_win, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always 30-Win when bet team of ticket is 7.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs7_BetTeamIsVirtualTennisScoreBet30WinResource()
        {
            // Arrange
            _ticket.BetTeam = "7";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.virtualtennis_scorebet_30_win, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always 40-Win when bet team of ticket is 8.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs4_BetTeamIsVirtualTennisScoreBetWin0Resource()
        {
            // Arrange
            _ticket.BetTeam = "8";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.virtualtennis_scorebet_40_win, _choice.Template.betTeam);
        }
    }
}