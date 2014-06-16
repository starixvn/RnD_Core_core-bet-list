namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common.Enums;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1220 class.
    /// </summary>
    [TestFixture]
    public class Choice1220Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1220();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.VT_PlayerWin;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always underdog.
        /// </summary>
        [Test]
        public void BuildChoice_Always_SetBetTeamClassNameIsUnderdog()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("underdog", _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.ticketStatus is always void when Ticket.Status = void.
        /// </summary>
        [Test]
        public void BuildChoice_Always_SetHandicapBlockIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap);
        }
    }
}