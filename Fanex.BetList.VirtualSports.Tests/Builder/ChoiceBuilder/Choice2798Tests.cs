namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2798Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2798();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildSport_Always_SetsSportTypeNameToDummySportFromResourceData()
        {
            // Arrange
            const string expectedSportTypeName = "Dummy Sport Type";
            _ticketHelper.GetResourceData("DummySport", "9908").Returns(expectedSportTypeName);

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedSportTypeName, template.League.sportTypeName);
            _ticketHelper.Received(1).GetResourceData("DummySport", "9908");
        }

        [Test]
        public void BuildBetTeam_Always_SetsBetTeamToEmptyString()
        {
            // Arrange
            var ticketData = new List<ITicketData>();

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            Assert.AreEqual(string.Empty, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithNullTicketData_SetsBetTeamToEmptyString()
        {
            // Arrange
            List<ITicketData> ticketData = null;

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            Assert.AreEqual(string.Empty, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithEmptyTicketDataList_SetsBetTeamToEmptyString()
        {
            // Arrange
            var ticketData = new List<ITicketData>();

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            Assert.AreEqual(string.Empty, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithNonEmptyTicketDataList_SetsBetTeamToEmptyString()
        {
            // Arrange
            var ticketData = new List<ITicketData>
            {
                Substitute.For<ITicketData>()
            };

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            Assert.AreEqual(string.Empty, template.betTeam);
        }

        [Test]
        public void Render_Always_CallsGetResourceDataWithCorrectParameters()
        {
            // Arrange
            const string expectedSportTypeName = "Test Sport";
            _ticketHelper.GetResourceData("DummySport", "9908").Returns(expectedSportTypeName);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            _ticketHelper.Received(1).GetResourceData("DummySport", "9908");
        }

        [Test]
        public void Render_Always_SetsSportTypeNameAndBetTeamCorrectly()
        {
            // Arrange
            const string expectedSportTypeName = "Virtual Sport Type";
            _ticketHelper.GetResourceData("DummySport", "9908").Returns(expectedSportTypeName);

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedSportTypeName, template.League.sportTypeName);
            Assert.AreEqual(string.Empty, template.betTeam);
        }
    }
} 