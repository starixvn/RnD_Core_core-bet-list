namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1235Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1235();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_BetTeamIs1_BetTeamIsVirtualTennisScoreBetWin0Resource()
        {
            // Arrange
            _ticket.BetTeam = "1";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.virtualtennis_scorebet_win_0, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs2_BetTeamIsVirtualTennisScoreBetWin15Resource()
        {
            // Arrange
            _ticket.BetTeam = "2";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.virtualtennis_scorebet_win_15, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs3_BetTeamIsVirtualTennisScoreBetWin30Resource()
        {
            // Arrange
            _ticket.BetTeam = "3";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.virtualtennis_scorebet_win_30, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs4_BetTeamIsVirtualTennisScoreBetWin40Resource()
        {
            // Arrange
            _ticket.BetTeam = "4";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.virtualtennis_scorebet_win_40, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs5_BetTeamIsVirtualTennisScoreBet0WinResource()
        {
            // Arrange
            _ticket.BetTeam = "5";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.virtualtennis_scorebet_0_win, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs6_BetTeamIsVirtualTennisScoreBet15WinResource()
        {
            // Arrange
            _ticket.BetTeam = "6";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.virtualtennis_scorebet_15_win, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs7_BetTeamIsVirtualTennisScoreBet30WinResource()
        {
            // Arrange
            _ticket.BetTeam = "7";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.virtualtennis_scorebet_30_win, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs8_BetTeamIsVirtualTennisScoreBetWin40Resource()
        {
            // Arrange
            _ticket.BetTeam = "8";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.virtualtennis_scorebet_40_win, _choice.Template.betTeam);
        }
    }
}