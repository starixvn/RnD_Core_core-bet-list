namespace Fanex.BetList.GoldDeluxe.Tests.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1701Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1701();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket.BetTypeId = 1701;
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
            _ticketHelper.GetBetTypeNameById(Arg.Any<object>()).Returns("Baccarat");

            // Action
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string ExpectedLeagueName = "Baccarat&nbsp;B71305030130";
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
            const string ExpectedBetTeam =
                "<div><span class='favorite'>Player Pair</span>&nbsp;<span class='stake'>1</span>&nbsp;@&nbsp;<span class='handicap custom'>11</span></div>";
            const string DecimalOddsType = "4";
            const string RefNo = "1234";
            _ticket.TransId = 1234;
            _ticket.OddsType = DecimalOddsType;

            var ticketData = new List<ITicketData> { new TicketData { RefNo = RefNo, Odds = 11, Stake = 1, BetTeam = "2", BetTypeId = 1701 } };
            _ticketHelper.GetResourceData("GD_Baccarat", "2").Returns("Player Pair");

            // Action
            _choice.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            Assert.AreEqual(ExpectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TicketData_SetBetTeam2()
        {
            // Arrange
            string expectedBetTeam =
                "<div><span class='favorite'>Player Pair</span>&nbsp;<span class='stake'>1</span>&nbsp;@&nbsp;<span class='handicap custom'>11</span></div>" +
                "<div><span class='favorite'>Big</span>&nbsp;<span class='stake'>2</span>&nbsp;@&nbsp;<span class='handicap custom'>22</span></div>";

            const string DecimalOddsType = "4";
            const string RefNo = "1234";
            _ticket.TransId = 1234;
            _ticket.OddsType = DecimalOddsType;

            var ticketData = new List<ITicketData>
            {
                new TicketData { RefNo = RefNo, Odds = 11, Stake = 1, BetTeam = "2", BetTypeId = 1701 },
                new TicketData { RefNo = RefNo, Odds = 22, Stake = 2, BetTeam = "3", BetTypeId = 1701 }
            };

            _ticketHelper.GetResourceData("GD_Baccarat", "2").Returns("Player Pair");
            _ticketHelper.GetResourceData("GD_Baccarat", "3").Returns("Big");

            // Action
            _choice.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TwoTicketDatasHaveSameRefNoButDifferentBetType_BetTeamContainsGoldDeluxe()
        {
            // Arrange
            string expectedBetTeam =
                "<div><span class='favorite'>Player Pair</span>&nbsp;<span class='stake'>1</span>&nbsp;@&nbsp;<span class='handicap custom'>11</span></div>";

            const string DecimalOddsType = "4";
            const string RefNo = "1234";
            _ticket.TransId = 1234;
            _ticket.OddsType = DecimalOddsType;

            var goldDeluxeTicketData = new TicketData { RefNo = RefNo, Odds = 11, Stake = 1, BetTeam = "2", BetTypeId = 1701 };
            var otherTicketData = new TicketData { RefNo = RefNo, Odds = 22, Stake = 2, BetTeam = "3", BetTypeId = 1101 };
            var ticketData = new List<ITicketData> { goldDeluxeTicketData, otherTicketData };

            _ticketHelper.GetResourceData("GD_Baccarat", "2").Returns("Player Pair");
            _ticketHelper.GetResourceData("GD_Baccarat", "3").Returns("Big");

            // Action
            _choice.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TicketDataWithOddsIsZero_ShowHyphen()
        {
            // Arrange
            const string ExpectedBetTeam =
                "<div><span class='favorite'>Player Pair</span>&nbsp;<span class='stake'>1</span>&nbsp;@&nbsp;<span class='handicap custom'>-</span></div>";
            const string DecimalOddsType = "4";
            const string RefNo = "1234";
            _ticket.TransId = 1234;
            _ticket.OddsType = DecimalOddsType;

            var ticketData = new List<ITicketData> { new TicketData { RefNo = RefNo, Odds = 0, Stake = 1, BetTeam = "2", BetTypeId = 1701 } };
            _ticketHelper.GetResourceData("GD_Baccarat", "2").Returns("Player Pair");

            // Action
            _choice.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            Assert.AreEqual(ExpectedBetTeam, _choice.Template.betTeam);
        }
    }
}