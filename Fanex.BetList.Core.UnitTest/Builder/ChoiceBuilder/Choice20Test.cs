namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice20: Money line.
    /// </summary>
    [TestFixture]
    public class Choice20Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice20();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Money_Line;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Handicap is always null.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        /// <summary>
        /// The value of bet team class is always 'underdog'.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsUnderdog()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeamClassName = "underdog";
            Assert.AreEqual(expectedBetTeamClassName, _choice.Template.betTeamClassName);
        }
    }
}