namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice675Test
    {
        private Choice675 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice675();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.PlayerPerformanceandOthers;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = new List<ITicketData>();
        }

        [Test]
        public void BuildBetTeam_ValidTransDesc_ReturnFormattedBetTeam()
        {
            // Arrange
            const string transDesc = "{\"pid\":\"123\",\"pname\":\"John Doe\",\"isH\":true,\"tid\":1,\"s\":[{\"t\":\"points\",\"value\":\"25\"}],\"r\":[{\"t\":\"assists\",\"value\":\"8\"}]}";
            const string playerName = "John Doe";
            const string teamName = "Lakers";

            _ticket.TransDesc = transDesc;
            _ticketHelper.GetTeamNameById("123").Returns(playerName);
            _ticketHelper.GetTeamNameById(1).Returns(teamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains(playerName));
            Assert.IsTrue(_choice.Template.betTeam.Contains("<span class=\"favorite\">"));
        }

        [Test]
        public void BuildBetTeam_EmptyTransDesc_HandleGracefully()
        {
            // Arrange
            _ticket.TransDesc = string.Empty;
            _ticketHelper.GetTeamNameById(Arg.Any<string>()).Returns(string.Empty);
            _ticketHelper.GetTeamNameById(Arg.Any<int>()).Returns(string.Empty);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_NullTransDesc_HandleGracefully()
        {
            // Arrange
            _ticket.TransDesc = null;
            _ticketHelper.GetTeamNameById(Arg.Any<string>()).Returns(string.Empty);
            _ticketHelper.GetTeamNameById(Arg.Any<int>()).Returns(string.Empty);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_InvalidJsonTransDesc_HandleGracefully()
        {
            // Arrange
            const string invalidJson = "invalid json string";
            _ticket.TransDesc = invalidJson;
            _ticketHelper.GetTeamNameById(Arg.Any<string>()).Returns(string.Empty);
            _ticketHelper.GetTeamNameById(Arg.Any<int>()).Returns(string.Empty);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_ComplexPlayerStats_HandleCorrectly()
        {
            // Arrange
            const string transDesc = "{\"pid\":\"789\",\"pname\":\"Mike Johnson\",\"isH\":true,\"tid\":3,\"s\":[{\"t\":\"points\",\"value\":\"30\"},{\"t\":\"rebounds\",\"value\":\"15\"},{\"t\":\"assists\",\"value\":\"10\"}],\"r\":[{\"t\":\"steals\",\"value\":\"3\"},{\"t\":\"blocks\",\"value\":\"2\"}]}";
            const string playerName = "Mike Johnson";
            const string teamName = "Bulls";

            _ticket.TransDesc = transDesc;
            _ticketHelper.GetTeamNameById("789").Returns(playerName);
            _ticketHelper.GetTeamNameById(3).Returns(teamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains(playerName));
            Assert.IsTrue(_choice.Template.betTeam.Contains("<span class=\"favorite\">"));
        }

        [Test]
        public void BuildBetTeam_PlayerWithNoStats_HandleEmptyLists()
        {
            // Arrange
            const string transDesc = "{\"pid\":\"999\",\"pname\":\"Empty Player\",\"isH\":false,\"tid\":4,\"s\":[],\"r\":[]}";
            const string playerName = "Empty Player";

            _ticket.TransDesc = transDesc;
            _ticketHelper.GetTeamNameById("999").Returns(playerName);
            _ticketHelper.GetTeamNameById(4).Returns("Heat");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains(playerName));
            Assert.IsTrue(_choice.Template.betTeam.Contains("<span class=\"favorite\">"));
        }

        [Test]
        public void BuildBetTeam_PlayerWithSingleStat_HandleCorrectly()
        {
            // Arrange
            const string transDesc = "{\"pid\":\"111\",\"pname\":\"Test Player\",\"isH\":true,\"tid\":5,\"s\":[{\"t\":\"points\",\"value\":\"20\"}]}";
            const string playerName = "Test Player";
            const string teamName = "Warriors";

            _ticket.TransDesc = transDesc;
            _ticketHelper.GetTeamNameById("111").Returns(playerName);
            _ticketHelper.GetTeamNameById(5).Returns(teamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains(playerName));
            Assert.IsTrue(_choice.Template.betTeam.Contains("<span class=\"favorite\">"));
        }

        [Test]
        public void BuildBetTeam_PlayerWithMixedStats_HandleCorrectly()
        {
            // Arrange
            const string transDesc = "{\"pid\":\"222\",\"pname\":\"Mixed Player\",\"isH\":false,\"tid\":6,\"s\":[{\"t\":\"rebounds\",\"value\":\"8\"}],\"r\":[{\"t\":\"assists\",\"value\":\"5\"}]}";
            const string playerName = "Mixed Player";
            const string teamName = "Celtics";

            _ticket.TransDesc = transDesc;
            _ticketHelper.GetTeamNameById("222").Returns(playerName);
            _ticketHelper.GetTeamNameById(6).Returns(teamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains(playerName));
            Assert.IsTrue(_choice.Template.betTeam.Contains("<span class=\"favorite\">"));
        }

        [Test]
        public void BuildBetTeam_PlayerWithSpecialCharacters_HandleCorrectly()
        {
            // Arrange
            const string transDesc = "{\"pid\":\"333\",\"pname\":\"Player-O'Connor\",\"isH\":true,\"tid\":7,\"s\":[{\"t\":\"points\",\"value\":\"18\"}]}";
            const string playerName = "Player-O'Connor";
            const string teamName = "Rockets";

            _ticket.TransDesc = transDesc;
            _ticketHelper.GetTeamNameById("333").Returns(playerName);
            _ticketHelper.GetTeamNameById(7).Returns(teamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains(playerName));
            Assert.IsTrue(_choice.Template.betTeam.Contains("<span class=\"favorite\">"));
        }

        [Test]
        public void BuildBetTeam_PlayerWithHighValues_HandleCorrectly()
        {
            // Arrange
            const string transDesc = "{\"pid\":\"444\",\"pname\":\"High Scorer\",\"isH\":false,\"tid\":8,\"s\":[{\"t\":\"points\",\"value\":\"50\"},{\"t\":\"rebounds\",\"value\":\"25\"}],\"r\":[{\"t\":\"assists\",\"value\":\"15\"}]}";
            const string playerName = "High Scorer";
            const string teamName = "Nets";

            _ticket.TransDesc = transDesc;
            _ticketHelper.GetTeamNameById("444").Returns(playerName);
            _ticketHelper.GetTeamNameById(8).Returns(teamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains(playerName));
            Assert.IsTrue(_choice.Template.betTeam.Contains("<span class=\"favorite\">"));
        }
    }
}
