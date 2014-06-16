namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common.Enums;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice127 class.
    /// </summary>
    [TestFixture]
    public class Choice127Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice127();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes._1h_First_Goal_Last_Goal;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.ticketStatus is always void when Ticket.Status = void.
        /// </summary>
        [Test]
        public void BuildChoice_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        /// <summary>
        /// The value of Template.betTeam is always ConvertByBetType.GetBetTeam.
        /// </summary>
        [Test]
        public void BuildChoice_Always_SetBetTeamIsEmpty()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(_choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always favorite.
        /// </summary>
        [Test]
        public void BuildChoice_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("favorite", _choice.Template.betTeamClassName);
        }
    }
}