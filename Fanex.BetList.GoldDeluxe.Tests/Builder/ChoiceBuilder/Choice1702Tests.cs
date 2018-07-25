namespace Fanex.BetList.GoldDeluxe.Tests.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1702Tests
    {
        private const string BetTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;{1}&nbsp;@&nbsp;<span class='handicap custom'>{2}</span></div>";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1702();
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
        public void BuildLeague_WhenCalled_SetLeagueNameIsBaccaratAndGameId()
        {
            // Arrange
            _ticket.BetCheck = "B71305030130";
            _ticketHelper.GetBetTypeNameById(Arg.Any<object>()).Returns("Roulette");

            // Action
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string ExpectedLeagueName = "Roulette&nbsp;B71305030130";
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
        public void BuildBetTeam_TicketDataIsEmpty_SetBetTeamIsEmpty()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TicketData_SetBetTeam1()
        {
            // Arrange
            const string DecimalOddsType = "4";
            const string RefNo = "1234";
            var expectedBetTeam = string.Format(BetTeamFormat, "Player Pair", 1, 11);
            _ticket.TransId = 1234;
            _ticket.OddsType = DecimalOddsType;

            var ticketData = new List<ITicketData> { new TicketData { RefNo = RefNo, Odds = 11, Stake = 1, BetTeam = "2" } };
            _ticketHelper.GetResourceData("GD_Roulette", "2").Returns("Player Pair");

            // Action
            _choice.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TicketData_SetBetTeam2()
        {
            // Arrange
            string expectedBetTeam = string.Format(BetTeamFormat, "Player Pair", 1, 11);
            expectedBetTeam += string.Format(BetTeamFormat, "Big", 2, 22);

            const string DecimalOddsType = "4";
            const string RefNo = "1234";
            _ticket.TransId = 1234;
            _ticket.OddsType = DecimalOddsType;

            var ticketData = new List<ITicketData>
            {
                new TicketData { RefNo = RefNo, Odds = 11, Stake = 1, BetTeam = "2" },
                new TicketData { RefNo = RefNo, Odds = 22, Stake = 2, BetTeam = "3" }
            };
            _ticketHelper.GetResourceData("GD_Roulette", "2").Returns("Player Pair");
            _ticketHelper.GetResourceData("GD_Roulette", "3").Returns("Big");

            // Action
            _choice.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TicketDataWithOddsIsZero_ShowHyphen()
        {
            // Arrange
            const string DecimalOddsType = "4";
            const string RefNo = "1234";
            string expectedBetTeam = string.Format(BetTeamFormat, "Player Pair", 1, "-");
            _ticket.TransId = 1234;
            _ticket.OddsType = DecimalOddsType;

            var ticketData = new List<ITicketData> { new TicketData { RefNo = RefNo, Odds = 0, Stake = 1, BetTeam = "2" } };
            _ticketHelper.GetResourceData("GD_Roulette", "2").Returns("Player Pair");

            // Action
            _choice.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }
    }
}