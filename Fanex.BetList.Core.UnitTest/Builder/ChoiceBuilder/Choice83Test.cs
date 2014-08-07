namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Choice83Test
    {
        private Choice83 _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice83();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes._1st_Ball_OE;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildMatch_Always_SetMatchVSBlockIsNull()
        {
            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(choiceTemplate.Match.VS);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsEmpty()
        {
            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNullOrEmpty(choiceTemplate.Handicap.handicap);
        }

        [Test]
        public void BuildChoice_BetTeamIsh_SetBetTeamIsOddResource()
        {
            // Arrange
            _ticket.BetTeam = "h";

            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.odd, choiceTemplate.betTeam);
        }

        [Test]
        public void BuildChoice_BetTeamIsNoth_SetBetTeamIsEvenResource()
        {
            // Arrange
            _ticket.BetTeam = "h87898";

            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.even, choiceTemplate.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("favorite", choiceTemplate.betTeamClassName);
        }

        [Test]
        public void BuildChoice_Always_SetHomeTeamIsJoinedByNumberGameNoResourceAndMatchCode()
        {
            // Arrange
            _ticket.MatchCode = "6969";

            // Act
            var choiceTemplate = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(string.Join(null, new string[] { CoreBetList.numbergameno, "&nbsp;", _ticket.MatchCode }), choiceTemplate.Match.homeTeam);
        }
    }
}