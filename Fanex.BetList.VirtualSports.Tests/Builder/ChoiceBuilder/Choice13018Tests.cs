namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice13018Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice13018();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_WhenBetTeamIsH_SetsBetTeamToOdd()
        {
            // Arrange
            _ticket.BetTeam = BetTeamValue.H;

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.odd, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WhenBetTeamIsNotH_SetsBetTeamToEven()
        {
            // Arrange
            _ticket.BetTeam = "any_other_value";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.even, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WhenBetTeamIsEmptyString_SetsBetTeamToEven()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.even, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WhenBetTeamIsNull_SetsBetTeamToEven()
        {
            // Arrange
            _ticket.BetTeam = null;

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.even, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WhenBetTeamIsLowerCaseH_SetsBetTeamToOdd()
        {
            // Arrange
            _ticket.BetTeam = "h";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.odd, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WhenBetTeamIsUpperCaseH_SetsBetTeamToOdd()
        {
            // Arrange
            _ticket.BetTeam = "H";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.odd, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithDifferentBetTeamValues_SetsCorrectBetTeam()
        {
            // Arrange
            var testCases = new[]
            {
                new { BetTeam = BetTeamValue.H, Expected = CoreBetList.odd },
                new { BetTeam = "x", Expected = CoreBetList.even },
                new { BetTeam = "1", Expected = CoreBetList.even },
                new { BetTeam = "2", Expected = CoreBetList.even },
                new { BetTeam = "draw", Expected = CoreBetList.even },
                new { BetTeam = "home", Expected = CoreBetList.even },
                new { BetTeam = "away", Expected = CoreBetList.even }
            };

            foreach (var testCase in testCases)
            {
                // Arrange
                _ticket.BetTeam = testCase.BetTeam;

                // Act
                var template = _choice.Render(_ticket, _ticketHelper, null, false);

                // Assert
                Assert.AreEqual(testCase.Expected, template.betTeam, 
                    $"Expected {testCase.Expected} when BetTeam is '{testCase.BetTeam}'");
            }
        }

        [Test]
        public void BuildBetTeam_WithTicketData_SetsBetTeamCorrectly()
        {
            // Arrange
            _ticket.BetTeam = BetTeamValue.H;
            var ticketData = new List<ITicketData> { Substitute.For<ITicketData>() };

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            Assert.AreEqual(CoreBetList.odd, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithNullTicketData_SetsBetTeamCorrectly()
        {
            // Arrange
            _ticket.BetTeam = "any_value";
            List<ITicketData> ticketData = null;

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            Assert.AreEqual(CoreBetList.even, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithEmptyTicketDataList_SetsBetTeamCorrectly()
        {
            // Arrange
            _ticket.BetTeam = BetTeamValue.H;
            var ticketData = new List<ITicketData>();

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            Assert.AreEqual(CoreBetList.odd, template.betTeam);
        }

        [Test]
        public void Render_Always_SetsBetTeamBasedOnBetTeamValue()
        {
            // Arrange
            _ticket.BetTeam = BetTeamValue.H;

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(CoreBetList.odd, template.betTeam);
        }
    }
} 