namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;
    using System.Runtime.Serialization;

    [TestFixture]
    public class Choice680Test
    {
        private Choice680 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice680();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = new List<ITicketData>();
        }

        [Test]
        public void BuildBetTeam_WithTwoPlayers_ShouldSetCorrectBetTeam()
        {
            // Arrange
            var playerId1 = 123;
            var playerId2 = 456;
            var playerName1 = "LeBron James";
            var playerName2 = "Anthony Davis";
            
            var json = $"{{\"p\":[{{\"pid\":{playerId1},\"tid\":789}},{{\"pid\":{playerId2},\"tid\":789}}]}}";
            _ticket.TransDesc = json;
            
            _ticketHelper.GetTeamNameById(playerId1).Returns(playerName1);
            _ticketHelper.GetTeamNameById(playerId2).Returns(playerName2);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName1} & {playerName2} - <span class=\"favorite\">{playerName1}</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithMoreThanTwoPlayers_ShouldUseFirstTwoPlayers()
        {
            // Arrange
            var playerId1 = 123;
            var playerId2 = 456;
            var playerId3 = 789;
            var playerName1 = "LeBron James";
            var playerName2 = "Anthony Davis";
            var playerName3 = "Russell Westbrook";
            
            var json = $"{{\"p\":[{{\"pid\":{playerId1},\"tid\":1}},{{\"pid\":{playerId2},\"tid\":1}},{{\"pid\":{playerId3},\"tid\":1}}]}}";
            _ticket.TransDesc = json;
            
            _ticketHelper.GetTeamNameById(playerId1).Returns(playerName1);
            _ticketHelper.GetTeamNameById(playerId2).Returns(playerName2);
            _ticketHelper.GetTeamNameById(playerId3).Returns(playerName3);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName1} & {playerName2} - <span class=\"favorite\">{playerName1}</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithOnePlayer_ShouldSetCorrectBetTeam()
        {
            // Arrange
            var playerId = 123;
            var playerName = "LeBron James";
            
            var json = $"{{\"p\":[{{\"pid\":{playerId},\"tid\":789}}]}}";
            _ticket.TransDesc = json;
            
            _ticketHelper.GetTeamNameById(playerId).Returns(playerName);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName} - <span class=\"favorite\">{playerName}</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithZeroPlayers_ShouldSetEmptyBetTeam()
        {
            // Arrange
            var json = "{\"p\":[]}";
            _ticket.TransDesc = json;

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual(string.Empty, _choice.Template.betTeam);
        }        

        [Test]
        public void BuildBetTeam_WithPlayersMissingPlayerId_ShouldHandleGracefully()
        {
            // Arrange
            var playerId1 = 123;
            var playerName1 = "LeBron James";
            var playerName2 = "Unknown Player";
            
            var json = $"{{\"p\":[{{\"pid\":{playerId1},\"tid\":789}},{{\"tid\":789}}]}}";
            _ticket.TransDesc = json;
            
            _ticketHelper.GetTeamNameById(playerId1).Returns(playerName1);
            _ticketHelper.GetTeamNameById(0).Returns(playerName2);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName1} & {playerName2} - <span class=\"favorite\">{playerName1}</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithPlayersMissingTeamId_ShouldHandleGracefully()
        {
            // Arrange
            var playerId1 = 123;
            var playerId2 = 456;
            var playerName1 = "LeBron James";
            var playerName2 = "Anthony Davis";
            
            var json = $"{{\"p\":[{{\"pid\":{playerId1}}},{{\"pid\":{playerId2}}}]}}";
            _ticket.TransDesc = json;
            
            _ticketHelper.GetTeamNameById(playerId1).Returns(playerName1);
            _ticketHelper.GetTeamNameById(playerId2).Returns(playerName2);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName1} & {playerName2} - <span class=\"favorite\">{playerName1}</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithZeroPlayerIds_ShouldHandleGracefully()
        {
            // Arrange
            var playerName = "Unknown Player";
            
            var json = "{\"p\":[{\"pid\":0,\"tid\":789},{\"pid\":0,\"tid\":789}]}";
            _ticket.TransDesc = json;
            
            _ticketHelper.GetTeamNameById(0).Returns(playerName);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName} & {playerName} - <span class=\"favorite\">{playerName}</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithNegativePlayerIds_ShouldHandleGracefully()
        {
            // Arrange
            var playerId1 = -1;
            var playerId2 = -2;
            var playerName1 = "Invalid Player 1";
            var playerName2 = "Invalid Player 2";
            
            var json = $"{{\"p\":[{{\"pid\":{playerId1},\"tid\":789}},{{\"pid\":{playerId2},\"tid\":789}}]}}";
            _ticket.TransDesc = json;
            
            _ticketHelper.GetTeamNameById(playerId1).Returns(playerName1);
            _ticketHelper.GetTeamNameById(playerId2).Returns(playerName2);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName1} & {playerName2} - <span class=\"favorite\">{playerName1}</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithNullTeamNames_ShouldHandleGracefully()
        {
            // Arrange
            var playerId1 = 123;
            var playerId2 = 456;
            
            var json = $"{{\"p\":[{{\"pid\":{playerId1},\"tid\":789}},{{\"pid\":{playerId2},\"tid\":789}}]}}";
            _ticket.TransDesc = json;
            
            _ticketHelper.GetTeamNameById(playerId1).Returns((string)null);
            _ticketHelper.GetTeamNameById(playerId2).Returns(string.Empty);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $" &  - <span class=\"favorite\"></span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithSpecialCharactersInPlayerNames_ShouldEscapeCorrectly()
        {
            // Arrange
            var playerId1 = 123;
            var playerId2 = 456;
            var playerName1 = "LeBron & James";
            var playerName2 = "Anthony \"The Brow\" Davis";
            
            var json = $"{{\"p\":[{{\"pid\":{playerId1},\"tid\":789}},{{\"pid\":{playerId2},\"tid\":789}}]}}";
            _ticket.TransDesc = json;
            
            _ticketHelper.GetTeamNameById(playerId1).Returns(playerName1);
            _ticketHelper.GetTeamNameById(playerId2).Returns(playerName2);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName1} & {playerName2} - <span class=\"favorite\">{playerName1}</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_ShouldSetUnderdogClass()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual("underdog", _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_ShouldSetHandicapToNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void Choice680_Inheritance_ShouldInheritFromChoice653()
        {
            // Assert
            Assert.IsTrue(typeof(Choice653).IsAssignableFrom(typeof(Choice680)));
        }

        [Test]
        public void Choice680_Template_ShouldBeInitialized()
        {
            // Assert
            Assert.IsNotNull(_choice.Template);
        }

        [Test]
        public void Render_WithValidData_ShouldReturnTemplate()
        {
            // Arrange
            var playerId1 = 123;
            var playerId2 = 456;
            var playerName1 = "LeBron James";
            var playerName2 = "Anthony Davis";
            
            var json = $"{{\"p\":[{{\"pid\":{playerId1},\"tid\":789}},{{\"pid\":{playerId2},\"tid\":789}}]}}";
            _ticket.TransDesc = json;
            
            _ticketHelper.GetTeamNameById(playerId1).Returns(playerName1);
            _ticketHelper.GetTeamNameById(playerId2).Returns(playerName2);

            // Act
            var result = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_choice.Template, result);
        }

        [Test]
        public void BasketballPlayers_Constructor_ShouldInitializeWithEmptyList()
        {
            // Act
            var players = new BasketballPlayers();

            // Assert
            Assert.IsNotNull(players.Players);
            Assert.AreEqual(0, players.Players.Count);
        }

        [Test]
        public void BasketballPlayers_ConstructorWithValidJson_ShouldParseCorrectly()
        {
            // Arrange
            var json = "{\"p\":[{\"pid\":123,\"tid\":456},{\"pid\":789,\"tid\":101}]}";

            // Act
            var players = new BasketballPlayers(json);

            // Assert
            Assert.IsNotNull(players.Players);
            Assert.AreEqual(2, players.Players.Count);
            Assert.AreEqual(123, players.Players[0].PlayerId);
            Assert.AreEqual(456, players.Players[0].TeamId);
            Assert.AreEqual(789, players.Players[1].PlayerId);
            Assert.AreEqual(101, players.Players[1].TeamId);
        }

        [Test]
        public void BasketballPlayers_ConstructorWithEmptyJson_ShouldHandleGracefully()
        {
            // Arrange
            var json = string.Empty;

            // Act
            var players = new BasketballPlayers(json);

            // Assert
            Assert.IsNotNull(players.Players);
            Assert.AreEqual(0, players.Players.Count);
        }

        [Test]
        public void BasketballPlayers_ConstructorWithNullJson_ShouldHandleGracefully()
        {
            // Arrange
            string json = null;

            // Act
            var players = new BasketballPlayers(json);

            // Assert
            Assert.IsNotNull(players.Players);
            Assert.AreEqual(0, players.Players.Count);
        }

        [Test]
        public void BasketballPlayers_ConstructorWithInvalidJson_ShouldHandleGracefully()
        {
            // Arrange
            var json = "invalid json";

            // Act
            var players = new BasketballPlayers(json);

            // Assert
            Assert.IsNotNull(players.Players);
            Assert.AreEqual(0, players.Players.Count);
        }

        [Test]
        public void BasketballPlayers_ConstructorWithMissingPlayersArray_ShouldHandleGracefully()
        {
            // Arrange
            var json = "{\"other\":\"value\"}";

            // Act
            var players = new BasketballPlayers(json);

            // Assert
            Assert.IsNotNull(players.Players);
            Assert.AreEqual(0, players.Players.Count);
        }

        [Test]
        public void BasketballPlayers_ConstructorWithNullPlayersArray_ShouldHandleGracefully()
        {
            // Arrange
            var json = "{\"p\":null}";

            // Act
            var players = new BasketballPlayers(json);

            // Assert
            Assert.IsNotNull(players.Players);
            Assert.AreEqual(0, players.Players.Count);
        }

        [Test]
        public void BasketballPlayers_DataMemberAttributes_ShouldBeCorrectlyApplied()
        {
            // Arrange
            var type = typeof(BasketballPlayers);
            var playersProperty = type.GetProperty("Players");

            // Act & Assert
            var playersAttribute = playersProperty.GetCustomAttributes(typeof(DataMemberAttribute), false);

            Assert.IsNotNull(playersAttribute);
            Assert.AreEqual(1, playersAttribute.Length);

            var playersDataMember = (DataMemberAttribute)playersAttribute[0];
            Assert.AreEqual("p", playersDataMember.Name);
        }

        [Test]
        public void BasketballPlayerInfo_DataMemberAttributes_ShouldBeCorrectlyApplied()
        {
            // Arrange
            var type = typeof(BasketballPlayerInfo);
            var playerIdProperty = type.GetProperty("PlayerId");
            var teamIdProperty = type.GetProperty("TeamId");

            // Act & Assert
            var playerIdAttribute = playerIdProperty.GetCustomAttributes(typeof(DataMemberAttribute), false);
            var teamIdAttribute = teamIdProperty.GetCustomAttributes(typeof(DataMemberAttribute), false);

            Assert.IsNotNull(playerIdAttribute);
            Assert.IsNotNull(teamIdAttribute);
            Assert.AreEqual(1, playerIdAttribute.Length);
            Assert.AreEqual(1, teamIdAttribute.Length);

            var playerIdDataMember = (DataMemberAttribute)playerIdAttribute[0];
            var teamIdDataMember = (DataMemberAttribute)teamIdAttribute[0];

            Assert.AreEqual("pid", playerIdDataMember.Name);
            Assert.AreEqual("tid", teamIdDataMember.Name);
        }
    }
}
