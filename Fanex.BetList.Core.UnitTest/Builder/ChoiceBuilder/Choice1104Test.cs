namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1104 class.
    /// </summary>
    [TestFixture]
    public class Choice1104Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1104();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.LiveCasino_LiveRoulette;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.League.LeagueName.leagueName is always Live Roulette and Ticket.ShowTime.
        /// </summary>
        [Test]
        public void BuildLeague_Always_SetLeagueNameAreLiveRouletteAndShowTime()
        {
            // Arrange
            _ticket.ShowTime = "show time";
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns("Live Roulette");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedLeagueName = string.Format("{0}&nbsp;{1}", "Live Roulette", _ticket.ShowTime);
            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }
    }
}