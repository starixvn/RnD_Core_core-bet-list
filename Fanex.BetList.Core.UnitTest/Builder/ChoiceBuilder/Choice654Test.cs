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
    public class Choice654Test
    {
        private Choice654 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice654();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = new List<ITicketData>();
        }

        [Test]
        public void BuildBetTeam_WithOverBetTeam_ShouldSetCorrectBetTeam()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "O";
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
            var expectedBetTeam = $"{playerName} ({teamName}) - <span class=\"favorite\">Over</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithUnderBetTeam_ShouldSetCorrectBetTeam()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "U";
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
            var expectedBetTeam = $"{playerName} ({teamName}) - Under";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithLowerCaseOverBetTeam_ShouldSetCorrectBetTeam()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "o";
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
            var expectedBetTeam = $"{playerName} ({teamName}) - <span class=\"favorite\">Over</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithLowerCaseUnderBetTeam_ShouldSetCorrectBetTeam()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "u";
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
            var expectedBetTeam = $"{playerName} ({teamName}) - Under";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithEmptyTransDesc_ShouldHandleGracefully()
        {
            // Arrange
            _ticket.TransDesc = string.Empty;
            _ticket.BetTeam = "O";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Over"));
        }

        [Test]
        public void BuildBetTeam_WithNullTransDesc_ShouldHandleGracefully()
        {
            // Arrange
            _ticket.TransDesc = null;
            _ticket.BetTeam = "U";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Under"));
        }

        [Test]
        public void BuildBetTeam_WithInvalidJson_ShouldHandleGracefully()
        {
            // Arrange
            _ticket.TransDesc = "invalid json";
            _ticket.BetTeam = "O";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Over"));
        }

        [Test]
        public void BuildBetTeam_WithMissingPlayerId_ShouldHandleGracefully()
        {
            // Arrange
            var json = "{\"tid\":456}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = "O";
            _ticketHelper.GetTeamNameById(0).Returns("Unknown Player");
            _ticketHelper.GetTeamNameById(456).Returns("Lakers");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Unknown Player"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("Over"));
        }

        [Test]
        public void BuildBetTeam_WithMissingTeamId_ShouldHandleGracefully()
        {
            // Arrange
            var json = "{\"pid\":123}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = "U";
            _ticketHelper.GetTeamNameById(123).Returns("LeBron James");
            _ticketHelper.GetTeamNameById(0).Returns("Unknown Team");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Unknown Team"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("Under"));
        }

        [Test]
        public void BuildBetTeam_WithZeroIds_ShouldHandleGracefully()
        {
            // Arrange
            var json = "{\"pid\":0,\"tid\":0}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = "O";
            _ticketHelper.GetTeamNameById(0).Returns("Unknown");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Unknown"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("Over"));
        }

        [Test]
        public void BuildBetTeam_WithNegativeIds_ShouldHandleGracefully()
        {
            // Arrange
            var json = "{\"pid\":-1,\"tid\":-2}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = "U";
            _ticketHelper.GetTeamNameById(-1).Returns("Invalid Player");
            _ticketHelper.GetTeamNameById(-2).Returns("Invalid Team");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Invalid Player"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("Invalid Team"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("Under"));
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
            var expectedBetTeam = $"{playerName} ({teamName}) - Under";
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
            var expectedBetTeam = $"{playerName} ({teamName}) - Under";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithNullTeamNames_ShouldHandleGracefully()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "O";
            
            var json = $"{{\"pid\":{playerId},\"tid\":{teamId}}}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = betTeam;
            
            _ticketHelper.GetTeamNameById(playerId).Returns((string)null);
            _ticketHelper.GetTeamNameById(teamId).Returns(string.Empty);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Over"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("()"));
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
        public void Choice654_Inheritance_ShouldInheritFromChoice653()
        {
            // Assert
            Assert.IsTrue(typeof(Choice653).IsAssignableFrom(typeof(Choice654)));
        }

        [Test]
        public void Choice654_Template_ShouldBeInitialized()
        {
            // Assert
            Assert.IsNotNull(_choice.Template);
        }

        [Test]
        public void Render_WithValidData_ShouldReturnTemplate()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "O";
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
