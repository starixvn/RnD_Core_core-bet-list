namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

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
            
            var json = $"{{\"pid\":{playerId},\"tid\":{teamId}}}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = betTeam;
            
            _ticketHelper.GetTeamNameById(playerId).Returns(playerName);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName} - <span class=\"favorite\">Over</span>";
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
            
            var json = $"{{\"pid\":{playerId},\"tid\":{teamId}}}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = betTeam;
            
            _ticketHelper.GetTeamNameById(playerId).Returns(playerName);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName} - Under";
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
            
            var json = $"{{\"pid\":{playerId},\"tid\":{teamId}}}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = betTeam;
            
            _ticketHelper.GetTeamNameById(playerId).Returns(playerName);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName} - <span class=\"favorite\">Over</span>";
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
            
            var json = $"{{\"pid\":{playerId},\"tid\":{teamId}}}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = betTeam;
            
            _ticketHelper.GetTeamNameById(playerId).Returns(playerName);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName} - Under";
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

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Unknown Player"));
            Assert.IsTrue(_choice.Template.betTeam.Contains("Over"));
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

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Invalid Player"));
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

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName} - Under";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_WithNullBetTeam_ShouldHandleGracefully()
        {
            // Arrange
            var playerId = 123;
            var teamId = 456;
            var playerName = "LeBron James";
            
            var json = $"{{\"pid\":{playerId},\"tid\":{teamId}}}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = null;
            
            _ticketHelper.GetTeamNameById(playerId).Returns(playerName);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            var expectedBetTeam = $"{playerName} - Under";
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

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.betTeam);
            Assert.IsTrue(_choice.Template.betTeam.Contains("Over"));
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
        public void BuildBetTeamClassNameAndHandicap_Always_ShouldSetHandicapToValue()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.IsNotNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_WithHandicap1Value_ShouldSetCorrectHandicap()
        {
            // Arrange
            _ticket.Handicap1 = 1.5m;
            _ticket.Handicap2 = 2.0m;

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual("underdog", _choice.Template.betTeamClassName);
            // Since Choice654 overrides and sets betTeamClassName to "underdog", 
            // the base class will set handicap to positive Handicap2 (2.0)
            Assert.AreEqual("2", _choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_WithHandicap2GreaterThanHandicap1_ShouldSetHandicap2Value()
        {
            // Arrange
            _ticket.Handicap1 = 1.0m;
            _ticket.Handicap2 = 2.5m;

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual("underdog", _choice.Template.betTeamClassName);
            // Since betTeamClassName is "underdog", handicap should be positive Handicap2
            Assert.AreEqual("2.5", _choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_WithHandicap1GreaterThanHandicap2_ShouldSetHandicap1Value()
        {
            // Arrange
            _ticket.Handicap1 = 3.0m;
            _ticket.Handicap2 = 1.5m;

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual("underdog", _choice.Template.betTeamClassName);
            // Since betTeamClassName is "underdog", handicap should be positive Handicap1
            Assert.AreEqual("3", _choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_WithEqualHandicapValues_ShouldSetHandicap1Value()
        {
            // Arrange
            _ticket.Handicap1 = 2.0m;
            _ticket.Handicap2 = 2.0m;

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual("underdog", _choice.Template.betTeamClassName);
            // When equal, it uses Handicap1
            Assert.AreEqual("2", _choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_WithZeroHandicapValues_ShouldSetZeroHandicap()
        {
            // Arrange
            _ticket.Handicap1 = 0.0m;
            _ticket.Handicap2 = 0.0m;

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual("underdog", _choice.Template.betTeamClassName);
            Assert.AreEqual("0", _choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_WithNegativeHandicapValues_ShouldSetCorrectHandicap()
        {
            // Arrange
            _ticket.Handicap1 = -1.5m;
            _ticket.Handicap2 = -2.0m;

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual("underdog", _choice.Template.betTeamClassName);
            // Since betTeamClassName is "underdog", handicap should be positive (absolute value of the larger)
            Assert.AreEqual("-1.5", _choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_WithDecimalHandicapValues_ShouldSetCorrectHandicap()
        {
            // Arrange
            _ticket.Handicap1 = 0.75m;
            _ticket.Handicap2 = 1.25m;

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketData, false);

            // Assert
            Assert.AreEqual("underdog", _choice.Template.betTeamClassName);
            // Since betTeamClassName is "underdog", handicap should be positive Handicap2
            Assert.AreEqual("1.25", _choice.Template.Handicap.handicap);
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
            
            var json = $"{{\"pid\":{playerId},\"tid\":{teamId}}}";
            _ticket.TransDesc = json;
            _ticket.BetTeam = betTeam;
            
            _ticketHelper.GetTeamNameById(playerId).Returns(playerName);

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
