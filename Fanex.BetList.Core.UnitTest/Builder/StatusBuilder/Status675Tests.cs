namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status675Tests
    {
        private IStatus _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status675();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_BetIdIsNullAndBetTeamIsNull_BetIdIsNull()
        {
            // Arrange
            _ticket.BetId = null;
            _ticket.BetTeam = null;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual("", _status.Template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_BetIdIsNullAndBetTeamIsEmpty_BetIdIsEmpty()
        {
            // Arrange
            _ticket.BetId = null;
            _ticket.BetTeam = "";

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual("", _status.Template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_BetIdIsNullAndBetTeamHasValue_BetIdIsBetTeam()
        {
            // Arrange
            _ticket.BetId = null;
            _ticket.BetTeam = "TeamA";

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual("TeamA", _status.Template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_BetIdHasValueAndBetTeamIsNull_BetIdIsBetId()
        {
            // Arrange
            _ticket.BetId = 12345;
            _ticket.BetTeam = null;

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual("12345", _status.Template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_BetIdHasValueAndBetTeamIsEmpty_BetIdIsBetId()
        {
            // Arrange
            _ticket.BetId = 12345;
            _ticket.BetTeam = "";

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual("12345", _status.Template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_BetIdAndBetTeamBothHaveValues_BetIdIsConcatenated()
        {
            // Arrange
            _ticket.BetId = 12345;
            _ticket.BetTeam = "TeamA";

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual("12345TeamA", _status.Template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_BetIdIsZeroAndBetTeamHasValue_BetIdIsConcatenated()
        {
            // Arrange
            _ticket.BetId = 0;
            _ticket.BetTeam = "TeamB";

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual("0TeamB", _status.Template.StatusResult.betId);
        }

        [Test]
        public void BuildStatusResult_BetIdIsNegativeAndBetTeamHasValue_BetIdIsConcatenated()
        {
            // Arrange
            _ticket.BetId = -1;
            _ticket.BetTeam = "TeamC";

            // Act
            _status.Render(_ticket, _ticketHelper, null, true);

            // Assert
            Assert.AreEqual("-1TeamC", _status.Template.StatusResult.betId);
        }
    }
}
