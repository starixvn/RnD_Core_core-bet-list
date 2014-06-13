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
        /// The ticket league name is Party Baccarat resource with show time.
        /// </summary>
        [Test]
        public void BuildChoice_Ticket_LeagueNameIsPartyBaccaratResourceWithShowTime()
        {
            // Arrange
            _ticket.ShowTime = "show time";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedLeagueName = string.Format("{0}&nbsp;{1}", CoreLang.PartyBaccarat, _ticket.ShowTime);
            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }

        [Test]
        public void BuildChoice_TicketDataIsNull_BetTeamIsEmpty()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [Test]
        public void BuildChoice_TicketDataIsNull_HandicapIsVisible()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsFalse(_choice.Template.Handicap.Visible);
        }

        [Test]
        public void BuildChoice_TicketDataIsNull_BetTypeNameIsPartyBaccaratResource()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreLang.PartyBaccarat, _choice.Template.BetType.betTypeName);
        }

        [Test]
        public void BuildChoice_TicketDataIsNull_BetTeamClassNameIsEmpty()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(_choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildChoice_TicketDataIsNull_MatchVSIsVisible()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsFalse(_choice.Template.Match.VS.Visible);
        }

        [Test]
        public void BuildChoice_RefNoOfTicketEqualRefNoOfTicketData_BetTeamIsBetTeamFormat()
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

        [Test]
        public void BuildChoice_RefNoOfTicketEqualRefNoOfTicketData_HandicapIsVisible()
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

        [Test]
        public void BuildChoice_RefNoOfTicketEqualRefNoOfTicketData_BetTypeNameIsEmpt()
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
            Assert.IsEmpty(_choice.Template.BetType.betTypeName);
        }

        [Test]
        public void BuildChoice_RefNoOfTicketEqualRefNoOfTicketData_BetTeamClassNameIsEmpty()
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
            Assert.IsEmpty(_choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildChoice_RefNoOfTicketEqualRefNoOfTicketData_MatchVSIsVisible()
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
            Assert.IsFalse(_choice.Template.Match.VS.Visible);
        }
    }
}
