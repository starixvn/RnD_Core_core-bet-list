namespace Fanex.BetList.RnGCasino.Tests.Builder.StatusBuilder
{
    using System;
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using Fanex.BetList.Core.Utils;
    using NSubstitute;
    using NUnit.Framework;

    public class Status2199Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status2199();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_TicketIsDetailedAndStatusIsRunning_ShowResultLink()
        {
            // Arrange
            _ticket.BetCheck = "@34242342";
            _ticket.Status = "running";
            _ticket.MatchId = 1;
            _ticket.BetTypeId = 2199;
            _ticket.Race = "1";
            _ticket.SportTypeId = 299;
            _ticket.LeagueId = 1;
            _ticket.UserName = "harrison";
            _ticket.WinlostDate = DateTime.Today;

            // Act
            var status = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.True(status.StatusResult.Visible);
            Assert.AreEqual("1", status.StatusResult.matchId);
            Assert.AreEqual("2199", status.StatusResult.betType);
            Assert.AreEqual("1", status.StatusResult.race);
            Assert.AreEqual("299", status.StatusResult.sportType);
            Assert.AreEqual("1", status.StatusResult.league);
            Assert.AreEqual("harrison", status.StatusResult.userName);
            Assert.AreEqual(Formatter.FullDateFormat(DateTime.Today), status.StatusResult.winlostDate);
        }

        [TestCase("running", "Running")]
        [TestCase("won", "Won")]
        [TestCase("lose", "Lose")]
        [TestCase("draw", "Draw")]
        public void BuildResult_Always_ShowResult(string resultStatus, string expectedResultStatus)
        {
            // Arrange
            _ticket.Status = resultStatus;

            // Act
            var status = _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedResultStatus, status.result);
        }
    }
}