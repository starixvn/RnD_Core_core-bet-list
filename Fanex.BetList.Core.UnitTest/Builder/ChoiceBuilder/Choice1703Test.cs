namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice1703Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1703();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildMatch_WhenCalled_AlwaysNull()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsNull(_choice.Template.Match);
        }

        [Test]
        public void BuildBetType_WhenCalled_AlwaysNull()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsNull(_choice.Template.BetType);
        }

        [Test]
        public void BuildScore_WhenCalled_AlwaysHide()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.False(_choice.Template.Score.Visible);
        }

        [Test]
        public void BuildLeague_WhenCalled_SetSportNameIsGoldDeluxe()
        {
            // Arrange
            const string Sport = "Gold Deluxe";
            _ticketHelper.GetSportNameById(Arg.Any<object>()).Returns(Sport);

            // Action
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(Sport, _choice.Template.League.sportTypeName);
        }

        [Test]
        public void BuildLeague_WhenCalled_SetLeagueNameIsParlayBaccarat()
        {
            // Arrange       
            _ticket.Status = "RUNNING";
            _ticketHelper.GetBetTypeNameById(Arg.Any<object>()).Returns("Parlay Baccarat");

            // Action
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string ExpectedLeagueName = "Parlay Baccarat";
            Assert.AreEqual(ExpectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_WhenCalled_AlwaysSetBetTeamClassNameIsEmpty()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsEmpty(_choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_WhenCalled_AlwaysHideHandicap()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsFalse(_choice.Template.Handicap.Visible);
        }

        [Test]
        public void BuildBetTeam_WhenCalled_SetBetTeamIsEmpty()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsEmpty(_choice.Template.betTeam);
        }
    }
}
