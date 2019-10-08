namespace Fanex.BetList.GoldDeluxe.Tests.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1702Tests
    {
        private const string BetTypeName = "Roulette";
        private const string SportTypeName = "Gold Deluxe";
        private const string BetTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;{1}&nbsp;@&nbsp;<span class='handicap custom'>{2}</span></div>";
        private const string RefNo = "1234";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            const int sportTypeId = 255;

            _choice = new Choice1702();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTypeId = GoldDeluxeBetTypes.Roulette;
            _ticket.SportTypeId = sportTypeId;
            _ticket.TransId = 1234;
            _ticket.OddsType = "1";

            _ticketHelper.GetBetTypeNameById(GoldDeluxeBetTypes.Roulette).Returns(BetTypeName);
            _ticketHelper.GetSportNameById(sportTypeId).Returns(SportTypeName);
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
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(SportTypeName, _choice.Template.League.sportTypeName);
        }

        [Test]
        public void BuildLeague_WhenCalled_SetLeagueNameIsBaccaratAndGameId()
        {
            // Arrange
            const string betCheck = "B71305030130";

            _ticket.BetCheck = betCheck;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedLeagueName = $"{BetTypeName}&nbsp;{betCheck}";

            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
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
            var ticketData = new List<ITicketData>
            {
                new TicketData
                {
                    RefNo = RefNo,
                    Odds = 11,
                    Stake = 1,
                    BetTeam = "2",
                    BetTypeId = GoldDeluxeBetTypes.Roulette
                }
            };

            _ticketHelper.GetResourceData("GD_Roulette", "2").Returns("Player Pair");

            // Act
            _choice.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            var expectedBetTeam = string.Format(BetTeamFormat, "Player Pair", 1, 11);

            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TicketData_SetBetTeam2()
        {
            // Arrange
            var ticketData = new List<ITicketData>
            {
                new TicketData
                {
                    RefNo = RefNo,
                    Odds = 11,
                    Stake = 1,
                    BetTeam = "2",
                    BetTypeId = GoldDeluxeBetTypes.Roulette
                },
                new TicketData
                {
                    RefNo = RefNo,
                    Odds = 22,
                    Stake = 2,
                    BetTeam = "3",
                    BetTypeId = GoldDeluxeBetTypes.Roulette
                }
            };

            _ticketHelper.GetResourceData("GD_Roulette", "2").Returns("Player Pair");
            _ticketHelper.GetResourceData("GD_Roulette", "3").Returns("Big");

            // Act
            _choice.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            var expectedBetTeam = string.Format(BetTeamFormat, "Player Pair", 1, 11) + string.Format(BetTeamFormat, "Big", 2, 22);

            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TicketDataWithOddsIsZero_ShowHyphen()
        {
            // Arrange
            var ticketData = new List<ITicketData>
            {
                new TicketData
                {
                    RefNo = RefNo,
                    Odds = 0,
                    Stake = 1,
                    BetTeam = "2",
                    BetTypeId = GoldDeluxeBetTypes.Roulette
                }
            };

            _ticketHelper.GetResourceData("GD_Roulette", "2").Returns("Player Pair");

            // Act
            _choice.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            var expectedBetTeam = string.Format(BetTeamFormat, "Player Pair", 1, "-");

            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }
    }
}