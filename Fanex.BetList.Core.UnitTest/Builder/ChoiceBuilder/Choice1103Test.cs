namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1103 class.
    /// </summary>
    [TestFixture]
    public class Choice1103Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1103();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Sicbo;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.League.LeagueName.leagueName is always Party Baccarat and Ticket.ShowTime.
        /// </summary>
        [Test]
        public void BuildLeague_Always_SetLeagueNameAreLiveBaccaratAndShowTime()
        {
            // Arrange
            _ticket.ShowTime = "show time";
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns("Live Baccarat");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedLeagueName = string.Format("{0}&nbsp;{1}", "Live Baccarat", _ticket.ShowTime);
            Assert.AreEqual(expectedLeagueName, _choice.Template.League.LeagueName.leagueName);
        }
    }
}