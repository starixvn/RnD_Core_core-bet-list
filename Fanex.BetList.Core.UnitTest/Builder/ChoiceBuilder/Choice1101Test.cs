namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1101 class.
    /// </summary>
    [TestFixture]
    public class Choice1101Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketDatas;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1101();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Baccarat;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketDatas = Substitute.For<List<ITicketData>>();
        }

        /// <summary>
        /// The value of Template.League.LeagueName.leagueName is always Party Baccarat and Ticket.ShowTime.
        /// </summary>
        [Test]
        public void BuildLeague_Always_SetLeagueNameArePartyBaccaratResourceAndShowTime()
        {
            // Arrange
            _ticket.ShowTime = "show time";
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns("Party Baccarat");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedLeagueName = string.Format("{0}&nbsp;{1}", "Party Baccarat", _ticket.ShowTime);
            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }

        /// <summary>
        /// The value of Template.betTeam is always empty when TicketData is null.
        /// </summary>
        [Test]
        public void BuildBetTeam_TicketDataIsNull_BetTeamIsEmpty()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(_choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.Handicap.Visible is always false when TicketData is null.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapBlockIsVisible()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsFalse(_choice.Template.Handicap.Visible);
        }

        /// <summary>
        /// The value of Template.BetType.betTypeName is always Party Baccarat when TicketData is null.
        /// </summary>
        [Test]
        public void BuildBetType_TicketDataIsNull_BetTypeNameIsPartyBaccaratResource()
        {
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns("Party Baccarat");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("Party Baccarat", _choice.Template.BetType.betTypeName);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always empty when TicketData is null.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsEmpty()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(_choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.Match.VS.Visible is always false when TicketData is null.
        /// </summary>
        [Test]
        public void BuildMatch_Always_SetMatchVSBlockIsVisible()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsFalse(_choice.Template.Match.VS.Visible);
        }

        /// <summary>
        /// The value of Template.betTeam depend on ticket data founded.
        /// </summary>
        [Test]
        public void BuildBetTeam_TicketDataHasRefNoEqualRefNoOfTicket_BetTeamIsBetTeamFormat()
        {
            // Arrange
            var refNo = "1";
            _ticket.RefNo = refNo;
            _ticket.OddsType = "1";
            var ticketData = Substitute.For<TicketData>();
            ticketData.RefNo = refNo;
            ticketData.Odds = 1;
            ticketData.BetTeam = string.Empty;
            ticketData.Stake = 1;
            _ticketDatas.Add(ticketData);
            var betTeamFormat = "<div><span class='favorite'>{0}</span>&nbsp;<span class='stake'>{1}</span>&nbsp;@&nbsp;<span class='handicap custom'>{2}</span></div>";

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketDatas, Arg.Any<bool>());

            // Assert
            var expectedBetTeam = string.Format(betTeamFormat, string.Empty, ticketData.Stake, ticketData.Odds);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value Template.BetType.betTypeName of is empty when ticket data is not null.
        /// </summary>
        [Test]
        public void BuildBetType_TicketDataHasRefNoEqualRefNoOfTicket_BetTypeNameIsEmpty()
        {
            // Arrange
            _ticket.RefNo = "1";
            _ticket.OddsType = "1";
            var ticketData = Substitute.For<TicketData>();
            ticketData.RefNo = _ticket.RefNo;
            ticketData.Odds = 1;
            ticketData.BetTeam = string.Empty;
            _ticketDatas.Add(ticketData);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketDatas, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(_choice.Template.BetType.betTypeName);
        }
    }
}