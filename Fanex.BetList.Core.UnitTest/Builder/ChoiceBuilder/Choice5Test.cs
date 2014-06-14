namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common.Enums;
    using NSubstitute;
    using NUnit.Framework;
    using Fanex.BetList.Core.App_GlobalResources;

    /// <summary>
    /// Unit test for Choice5 class.
    /// </summary>
    [TestFixture]
    public class Choice5Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private const string BetTeamTemplate = " <span class=\"favorite\">{0}</span>&nbsp;{1} <span class=\"favorite\">&nbsp;{2}</span>";

        [SetUp]
        public void Setup()
        {
            _choice = new Choice5();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.FT_1X2;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.Handicap.handicap is always null.
        /// </summary>
        [Test]
        public void BuildChoice_Ticket_HandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        /// <summary>
        /// The value of Template.betTeam is always bet team.
        /// </summary>
        [Test]
        public void BuildChoice_HomeIdOfTicketNotZero_BetTeamAreFTResourceAndPointAndBetTeamToUpper()
        {
            // Arrange
            _ticket.BetTeam = "bet team";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = string.Format("{0}.{1}", CoreBetList.ft, _ticket.BetTeam.ToUpper());
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always favorite.
        /// </summary>
        [Test]
        public void BuildChoice_Ticket_BetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("favorite", _choice.Template.betTeamClassName);
        }
    }
}