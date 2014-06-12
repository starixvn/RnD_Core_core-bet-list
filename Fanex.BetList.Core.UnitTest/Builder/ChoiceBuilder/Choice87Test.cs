namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.UnitTest.Common.Enums;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice87Test
    {
        private Choice87 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice87();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Next_High_Low;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildChoice_ScoreBlockIsNull()
        {
            // Act            
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(choiceTemplate.Score);
        }

        [Test]
        public void BuildChoice_MatchVSBlockIsNull()
        {
            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());            

            // Assert
            Assert.IsNull(choiceTemplate.Match.VS);
        }

        [Test]
        public void BuildChoice_HandicapIsEmpty()
        {
            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());
            
            // Assert            
            Assert.IsNullOrEmpty(choiceTemplate.Handicap.handicap);
        }             

        [Test]
        public void BuildChoice_BetTeamCssClassNameIsFavorite()
        {
            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());
            
            // Assert            
            Assert.AreEqual("favorite", choiceTemplate.betTeamClassName);
        }

        [Test]
        public void GetLiveScore_TicketIsNotLive_ReturnZeroBetweenSquareBrackets()
        {
            //// Arrange
            _ticket.IsLive = false;
            _ticket.BetId = Arg.Any<long>();

            // Act            
            var actualLiveScore = _choice.GetLiveScore(_ticket);

           // Assert
            Assert.AreEqual(" [0]", actualLiveScore);
        }

        [Test]
        public void GetLiveScore_TicketIsLiveAndBetIdIsNull_ReturnZeroBetweenSquareBrackets()
        {
            //// Arrange
            _ticket.IsLive = true;
            _ticket.BetId = null;

            // Act            
            var actualLiveScore = _choice.GetLiveScore(_ticket);

           // Assert
            Assert.AreEqual(" [0]", actualLiveScore);
        }

        [Test]
        public void GetLiveScore_TicketIsLiveAndBetIdIsNotNull_ReturnBetIdStringBetweenSquareBrackets()
        {
            // Arrange
            _ticket.IsLive = true;
            _ticket.BetId = 1234;

            // Act            
            var actualLiveScore = _choice.GetLiveScore(_ticket);

           // Assert
            var expectedLiveScore = string.Join(null, new string[] { " [", _ticket.BetId.ToString(), "]" });
            Assert.AreEqual(expectedLiveScore, actualLiveScore);
        }

        [Test]
        public void GetBetTeam_BetTeamIsh_BetTeamIsJoinedByHighResourceAndLiveScore()
        { 
            // Arrange
            _ticket.BetTeam = "h";
            var liveScoreResult = "livescore result";
            _choice = Substitute.For<Choice87>();
            _choice.GetLiveScore(_ticket).Returns(liveScoreResult);

            // Act
            var actualBetTeam = _choice.GetBetTeam(_ticket, liveScoreResult);

            // Assert
            Assert.AreEqual(string.Join(null, CoreBetList.high, liveScoreResult), actualBetTeam);
        }

        [Test]
        public void BuildChoice_BetTeamIsNoth_BetTeamIsJoinedLowResourceAndLiveScore()
        {
            // Arrange
            _ticket.BetTeam = "h87898";            
            var liveScoreResult = "livescore result";
            _choice = Substitute.For<Choice87>();
            _choice.GetLiveScore(_ticket).Returns(liveScoreResult);

            // Act
            var actualBetTeam = _choice.GetBetTeam(_ticket, liveScoreResult);

            // Assert
            Assert.AreEqual(string.Join(null, CoreBetList.low, liveScoreResult), actualBetTeam);            
        }        

        [Test]
        public void BuildChoice_HomeTeamIsJoinedByNumberGameNoResourceAndMatchCode()
        {
            // Arrange
            _ticket.MatchCode = "6969";

            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());
            var actualHomeTeam = choiceTemplate.Match.homeTeam;

            // Assert
            var expectedHomeTeam = string.Join(null, new string[] { CoreLang.numbergameno, "&nbsp;", _ticket.MatchCode });
            Assert.AreEqual(expectedHomeTeam, actualHomeTeam);
        }
    }
}