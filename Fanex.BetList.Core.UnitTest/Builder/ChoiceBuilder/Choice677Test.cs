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
    public class Choice677Test
    {
        private Choice677 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketData;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice677();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketData = new List<ITicketData>();
        }

        [Test]
        public void BuildBetTeam_WithYesBetTeam_ShouldSetCorrectBetTeam()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "Y";
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
            var expectedBetTeam = $"{playerName} ({teamName}) - <span class=\"favorite\">Yes</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithNoBetTeam_ShouldSetCorrectBetTeam()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "N";
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
            var expectedBetTeam = $"{playerName} ({teamName}) - <span class=\"favorite\">No</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithLowerCaseYesBetTeam_ShouldSetCorrectBetTeam()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "y";
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
            var expectedBetTeam = $"{playerName} ({teamName}) - <span class=\"favorite\">Yes</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithLowerCaseNoBetTeam_ShouldSetCorrectBetTeam()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "n";
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
            var expectedBetTeam = $"{playerName} ({teamName}) - <span class=\"favorite\">No</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithEmptyTransDesc_ShouldHandleGracefully()
        {
            // Arrange
            _ticket.TransDesc = string.Empty;
            _ticket.BetTeam = "Y";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Yes"));
        }

        [Test]
        public void BuildBetTeam_WithNullTransDesc_ShouldHandleGracefully()
        {
            // Arrange
            _ticket.TransDesc = null;
            _ticket.BetTeam = "N";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("No"));
        }

        [Test]
        public void BuildBetTeam_WithInvalidJson_ShouldHandleGracefully()
        {
            // Arrange
            _ticket.TransDesc = "invalid json";
            _ticket.BetTeam = "Y";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Yes"));
        }

        [Test]
        public void BuildBetTeam_WithMissingPlayerId_ShouldHandleGracefully()
        {
            // Arrange
            var json = "{\"tid\":456}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = "Y";
            _ticketHelper.GetTeamNameById(0).Returns("Unknown Player");
            _ticketHelper.GetTeamNameById(456).Returns("Lakers");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Unknown Player"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("Yes"));
        }

        [Test]
        public void BuildBetTeam_WithMissingTeamId_ShouldHandleGracefully()
        {
            // Arrange
            var json = "{\"pid\":123}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = "N";
            _ticketHelper.GetTeamNameById(123).Returns("LeBron James");
            _ticketHelper.GetTeamNameById(0).Returns("Unknown Team");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Unknown Team"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("No"));
        }

        [Test]
        public void BuildBetTeam_WithZeroIds_ShouldHandleGracefully()
        {
            // Arrange
            var json = "{\"pid\":0,\"tid\":0}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = "Y";
            _ticketHelper.GetTeamNameById(0).Returns("Unknown");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Unknown"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("Yes"));
        }

        [Test]
        public void BuildBetTeam_WithNegativeIds_ShouldHandleGracefully()
        {
            // Arrange
            var json = "{\"pid\":-1,\"tid\":-2}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = "N";
            _ticketHelper.GetTeamNameById(-1).Returns("Invalid Player");
            _ticketHelper.GetTeamNameById(-2).Returns("Invalid Team");

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Invalid Player"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("Invalid Team"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("No"));
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
            var expectedBetTeam = $"{playerName} ({teamName}) - <span class=\"favorite\">Yes</span>";
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
            var expectedBetTeam = $"{playerName} ({teamName}) - <span class=\"favorite\">Yes</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithNullTeamNames_ShouldHandleGracefully()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "Y";
            
            var json = $"{{\"pid\":{playerId},\"tid\":{teamId}}}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = betTeam;
            
            _ticketHelper.GetTeamNameById(playerId).Returns((string)null);
            _ticketHelper.GetTeamNameById(teamId).Returns(string.Empty);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Yes"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("()"));
        }

        [Test]
        public void BuildBetTeam_WithOtherBetTeamValues_ShouldDefaultToNo()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var betTeam = "X";
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
            var expectedBetTeam = $"{playerName} ({teamName}) - <span class=\"favorite\">Yes</span>";
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
        public void Choice677_Inheritance_ShouldInheritFromChoice653()
        {
            // Assert
            Assert.IsTrue(typeof(Choice653).IsAssignableFrom(typeof(Choice677)));
        }

        [Test]
        public void Choice677_Template_ShouldBeInitialized()
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
            var betTeam = "Y";
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
