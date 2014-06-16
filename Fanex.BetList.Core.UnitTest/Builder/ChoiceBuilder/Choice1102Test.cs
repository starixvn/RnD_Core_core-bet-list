namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common.Enums;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1102 class.
    /// </summary>
    [TestFixture]
    public class Choice1102Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private List<ITicketData> _ticketDatas;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1102();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Roulette;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketDatas = Substitute.For<List<ITicketData>>();
        }

        /// <summary>
        /// The value of Template.League.LeagueName.leagueName is always Party Roulette and Ticket.ShowTime.
        /// </summary>
        [Test]
        public void BuildChoice_Always_SetLeagueNameArePartyRouletteResourceAndShowTime()
        {
            // Arrange
            _ticket.ShowTime = "show time";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedLeagueName = string.Format("{0}&nbsp;{1}", CoreLang.PartyRoulette, _ticket.ShowTime);
            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }

        /// <summary>
        /// The value of Template.betTeam is always empty when TicketData is null.
        /// </summary>
        [Test]
        public void BuildChoice_TicketDataIsNull_BetTeamIsEmpty()
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
        public void BuildChoice_TicketDataIsNull_HandicapIsVisible()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsFalse(_choice.Template.Handicap.Visible);
        }

        /// <summary>
        /// The value of Template.BetType.betTypeName is always Party Roulette when TicketData is null.
        /// </summary>
        [Test]
        public void BuildChoice_TicketDataIsNull_BetTypeNameIsPartyRouletteResource()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreLang.PartyRoulette, _choice.Template.BetType.betTypeName);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always empty when TicketData is null.
        /// </summary>
        [Test]
        public void BuildChoice_TicketDataIsNull_BetTeamClassNameIsEmpty()
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
        public void BuildChoice_TicketDataIsNull_MatchVSIsVisible()
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
        public void BuildChoice_RefNoEqualRefNoData_BetTeamIsBetTeamFormat()
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
        /// The value of Template.Handicap.Visible is always visible.
        /// </summary>
        [Test]
        public void BuildChoice_RefNoEqualRefNoData_HandicapIsVisible()
        {
            // Arrange
            var refNo = "1";
            _ticket.RefNo = refNo;
            _ticket.OddsType = "1";
            var ticketData = Substitute.For<TicketData>();
            ticketData.RefNo = refNo;
            ticketData.Odds = 1;
            ticketData.BetTeam = string.Empty;
            _ticketDatas.Add(ticketData);

            // Act
            _choice.Render(_ticket, _ticketHelper, _ticketDatas, Arg.Any<bool>());

            // Assert
            Assert.IsFalse(_choice.Template.Handicap.Visible);
        }

        /// <summary>
        /// The value Template.BetType.betTypeName of is empty when ticket data is not null.
        /// </summary>
        [Test]
        public void BuildChoice_RefNoEqualRefNoData_BetTypeNameIsEmpty()
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

        /// <summary>
        /// The value of Template.betTeamClassName is always empty.
        /// </summary>
        [Test]
        public void BuildChoice_RefNoEqualRefNoData_BetTeamClassNameIsEmpty()
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
            Assert.IsEmpty(_choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.Match.VS.Visible is always false.
        /// </summary>
        [Test]
        public void BuildChoice_RefNoEqualRefNoData_MatchVSIsVisible()
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
            Assert.IsFalse(_choice.Template.Match.VS.Visible);
        }
    }
}