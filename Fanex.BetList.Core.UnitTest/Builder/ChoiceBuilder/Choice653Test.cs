namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using BetListNPOI.SS.UserModel;
    using BetListNPOI.HSSF.UserModel;
    using BetListNPOI.SS.Util;
    using BetListNPOI.POIFS.FileSystem;
    using Core.Builder.SystemParlayBuilder;
    using Core.Templates;

    [TestFixture]
    public class Choice653Test
    {
        private Choice653 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice653();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = new List<ITicketData>();
        }

        [Test]
        public void BuildBetTeam_WithValidTransDesc_ShouldSetCorrectBetTeam()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "Over 20.5";
            var playerName = "LeBron James";
            var teamName = "Lakers";
            
            var json = $"{{\"pid\":{playerId},\"tid\":{teamId}}}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = betTeam;
            
            _ticketHelper.GetTeamNameById(playerId).Returns(playerName);
            _ticketHelper.GetTeamNameById(teamId).Returns(teamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName} ({teamName}) - <span class=\"favorite\">{betTeam}</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithEmptyTransDesc_ShouldHandleGracefully()
        {
            // Arrange
            _ticket.TransDesc = string.Empty;
            _ticket.BetTeam = "Over 20.5";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Over 20.5"));
        }

        [Test]
        public void BuildBetTeam_WithNullTransDesc_ShouldHandleGracefully()
        {
            // Arrange
            _ticket.TransDesc = null;
            _ticket.BetTeam = "Over 20.5";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Over 20.5"));
        }

        [Test]
        public void BuildBetTeam_WithInvalidJson_ShouldHandleGracefully()
        {
            // Arrange
            _ticket.TransDesc = "invalid json";
            _ticket.BetTeam = "Over 20.5";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Over 20.5"));
        }

        [Test]
        public void BuildBetTeam_WithMissingPlayerId_ShouldHandleGracefully()
        {
            // Arrange
            var json = "{\"tid\":456}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = "Over 20.5";
            _ticketHelper.GetTeamNameById(0).Returns("Unknown Player");
            _ticketHelper.GetTeamNameById(456).Returns("Lakers");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Unknown Player"));
        }

        [Test]
        public void BuildBetTeam_WithMissingTeamId_ShouldHandleGracefully()
        {
            // Arrange
            var json = "{\"pid\":123}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = "Over 20.5";
            _ticketHelper.GetTeamNameById(123).Returns("LeBron James");
            _ticketHelper.GetTeamNameById(0).Returns("Unknown Team");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Unknown Team"));
        }

        [Test]
        public void BuildBetTeam_WithZeroIds_ShouldHandleGracefully()
        {
            // Arrange
            var json = "{\"pid\":0,\"tid\":0}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = "Over 20.5";
            _ticketHelper.GetTeamNameById(0).Returns("Unknown");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Unknown"));
        }

        [Test]
        public void BuildBetTeam_WithNegativeIds_ShouldHandleGracefully()
        {
            // Arrange
            var json = "{\"pid\":-1,\"tid\":-2}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = "Over 20.5";
            _ticketHelper.GetTeamNameById(-1).Returns("Invalid Player");
            _ticketHelper.GetTeamNameById(-2).Returns("Invalid Team");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Invalid Player"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("Invalid Team"));
        }

        [Test]
        public void BuildBetTeam_WithSpecialCharactersInBetTeam_ShouldEscapeCorrectly()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "Over 20.5 & Under 30.5";
            var playerName = "LeBron James";
            var teamName = "Lakers";
            
            var json = $"{{\"pid\":{playerId},\"tid\":{teamId}}}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = betTeam;
            
            _ticketHelper.GetTeamNameById(playerId).Returns(playerName);
            _ticketHelper.GetTeamNameById(teamId).Returns(teamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName} ({teamName}) - <span class=\"favorite\">{betTeam}</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithEmptyBetTeam_ShouldHandleGracefully()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = string.Empty;
            var playerName = "LeBron James";
            var teamName = "Lakers";
            
            var json = $"{{\"pid\":{playerId},\"tid\":{teamId}}}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = betTeam;
            
            _ticketHelper.GetTeamNameById(playerId).Returns(playerName);
            _ticketHelper.GetTeamNameById(teamId).Returns(teamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName} ({teamName}) - <span class=\"favorite\"></span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithNullBetTeam_ShouldHandleGracefully()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var playerName = "LeBron James";
            var teamName = "Lakers";
            
            var json = $"{{\"pid\":{playerId},\"tid\":{teamId}}}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = null;
            
            _ticketHelper.GetTeamNameById(playerId).Returns(playerName);
            _ticketHelper.GetTeamNameById(teamId).Returns(teamName);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName} ({teamName}) - <span class=\"favorite\"></span>";
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
        public void Render_WithValidData_ShouldReturnTemplate()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "Over 20.5";
            var playerName = "LeBron James";
            var teamName = "Lakers";
            
            var json = $"{{\"pid\":{playerId},\"tid\":{teamId}}}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = betTeam;
            
            _ticketHelper.GetTeamNameById(playerId).Returns(playerName);
            _ticketHelper.GetTeamNameById(teamId).Returns(teamName);

            // Act
            var result = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_choice.Template, result);
        }

        [Test]
        public void BasketballPlayerInfo_Constructor_ShouldInitializeWithDefaultValues()
        {
            // Act
            var playerInfo = new BasketballPlayerInfo();

            // Assert
            Assert.AreEqual(0, playerInfo.PlayerId);
            Assert.AreEqual(0, playerInfo.TeamId);
        }

        [Test]
        public void BasketballPlayerInfo_ConstructorWithValidJson_ShouldParseCorrectly()
        {
            // Arrange
            var json = "{\"pid\":123,\"tid\":456}";

            // Act
            var playerInfo = new BasketballPlayerInfo(json);

            // Assert
            Assert.AreEqual(123, playerInfo.PlayerId);
            Assert.AreEqual(456, playerInfo.TeamId);
        }

        [Test]
        public void BasketballPlayerInfo_ConstructorWithEmptyJson_ShouldHandleGracefully()
        {
            // Arrange
            var json = string.Empty;

            // Act
            var playerInfo = new BasketballPlayerInfo(json);

            // Assert
            Assert.AreEqual(0, playerInfo.PlayerId);
            Assert.AreEqual(0, playerInfo.TeamId);
        }

        [Test]
        public void BasketballPlayerInfo_ConstructorWithNullJson_ShouldHandleGracefully()
        {
            // Arrange
            string json = null;

            // Act
            var playerInfo = new BasketballPlayerInfo(json);

            // Assert
            Assert.AreEqual(0, playerInfo.PlayerId);
            Assert.AreEqual(0, playerInfo.TeamId);
        }

        [Test]
        public void BasketballPlayerInfo_ConstructorWithInvalidJson_ShouldHandleGracefully()
        {
            // Arrange
            var json = "invalid json";

            // Act
            var playerInfo = new BasketballPlayerInfo(json);

            // Assert
            Assert.AreEqual(0, playerInfo.PlayerId);
            Assert.AreEqual(0, playerInfo.TeamId);
        }

        [Test]
        public void BasketballPlayerInfo_ConstructorWithPartialJson_ShouldHandleGracefully()
        {
            // Arrange
            var json = "{\"pid\":123}";

            // Act
            var playerInfo = new BasketballPlayerInfo(json);

            // Assert
            Assert.AreEqual(123, playerInfo.PlayerId);
            Assert.AreEqual(0, playerInfo.TeamId);
        }

        [Test]
        public void BasketballPlayerInfo_ConstructorWithExtraFields_ShouldIgnoreExtraFields()
        {
            // Arrange
            var json = "{\"pid\":123,\"tid\":456,\"extra\":\"value\"}";

            // Act
            var playerInfo = new BasketballPlayerInfo(json);

            // Assert
            Assert.AreEqual(123, playerInfo.PlayerId);
            Assert.AreEqual(456, playerInfo.TeamId);
        }

        [Test]
        public void BasketballPlayerInfo_DataMemberAttributes_ShouldBeCorrectlyApplied()
        {
            // Arrange
            var playerInfo = new BasketballPlayerInfo();
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

        [Test]
        public void Choice653_Inheritance_ShouldInheritFromChoice1()
        {
            // Assert
            Assert.IsTrue(typeof(Choice1).IsAssignableFrom(typeof(Choice653)));
        }

        [Test]
        public void Choice653_Template_ShouldBeInitialized()
        {
            // Assert
            Assert.IsNotNull(_choice.Template);
        }

        [Test]
        public void Render_WithIsShowScoreMapTrue_ShouldIncludeScoreMap()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "Over 20.5";
            var playerName = "LeBron James";
            var teamName = "Lakers";
            
            var json = $"{{\"pid\":{playerId},\"tid\":{teamId}}}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = betTeam;
            _ticket.MatchId = 789;
            _ticket.BetTypeId = 653;
            _ticket.IsLive = true;
            
            _ticketHelper.GetTeamNameById(playerId).Returns(playerName);
            _ticketHelper.GetTeamNameById(teamId).Returns(teamName);

            // Act
            var result = _choice.Render(_ticket, _ticketHelper, _ticketData, true);

            // Assert
            Assert.IsNotNull(result.ScoreMapIcon);
            Assert.AreEqual("789", result.ScoreMapIcon.matchId);
            Assert.AreEqual("653", result.ScoreMapIcon.betTypeId);
            Assert.AreEqual("1", result.ScoreMapIcon.liveindicator);
        }

        [Test]
        public void Render_WithIsShowScoreMapFalse_ShouldNotIncludeScoreMap()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "Over 20.5";
            var playerName = "LeBron James";
            var teamName = "Lakers";
            
            var json = $"{{\"pid\":{playerId},\"tid\":{teamId}}}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = betTeam;
            
            _ticketHelper.GetTeamNameById(playerId).Returns(playerName);
            _ticketHelper.GetTeamNameById(teamId).Returns(teamName);

            // Act
            var result = _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNull(result.ScoreMapIcon);
        }

        [Test]
        public void GetBetTypeName_WithValidData_ShouldReturnBetTypeName()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "Over 20.5";
            var playerName = "LeBron James";
            var teamName = "Lakers";
            var betTypeName = "Player Points";
            
            var json = $"{{\"pid\":{playerId},\"tid\":{teamId}}}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = betTeam;
            _ticket.BetTypeId = 653;
            _ticket.RefNo = "12345";
            
            _ticketHelper.GetTeamNameById(playerId).Returns(playerName);
            _ticketHelper.GetTeamNameById(teamId).Returns(teamName);
            _ticketHelper.GetBetTypeNameById(653, "12345").Returns(betTypeName);

            // Act
            var result = _choice.GetBetTypeName(_ticket, _ticketHelper, _ticketData);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(betTypeName, result);
        }

        [Test]
        public void BuildBetTeam_WithNullTeamNames_ShouldHandleGracefully()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "Over 20.5";
            
            var json = $"{{\"pid\":{playerId},\"tid\":{teamId}}}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = betTeam;
            
            _ticketHelper.GetTeamNameById(playerId).Returns((string)null);
            _ticketHelper.GetTeamNameById(teamId).Returns(string.Empty);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Over 20.5"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("()"));
        }
    }
}
