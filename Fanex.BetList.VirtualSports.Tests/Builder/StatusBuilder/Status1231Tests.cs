namespace Fanex.BetList.VirtualSports.Tests.Builder.StatusBuilder
{
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status1231Tests
    {
        private Status1231 _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status1231();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildStatusResult_RaceIsEmpty_RaceIsZero()
        {
            // Arrange
            _ticket.Race = string.Empty;

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string RACE = "0";
            Assert.AreEqual(RACE, _status.Template.StatusResult.race);
        }

        [Test]
        public void BuildStatusResult_RaceIsNotEmpty_RaceIsRace()
        {
            // Arrange
            _ticket.Race = "10";

            // Act
            _status.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(_ticket.Race, _status.Template.StatusResult.race);
        }
    }
}