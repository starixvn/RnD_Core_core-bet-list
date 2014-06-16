namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common.Enums;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice26 class.
    /// </summary>
    [TestFixture]
    public class Choice26Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice26();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Both_One_Neither;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.Handicap.handicap is always null.
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
        /// The value of Template.betTeam is always Both.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamIsCharacterB_BetTeamIsBothResource()
        {
            // Arrange
            _ticket.BetTeam = "b";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.lblBoth, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always One.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamIsCharacterO_BetTeamIsOneResource()
        {
            // Arrange
            _ticket.BetTeam = "o";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.lblOne, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always No Goal.
        /// </summary>
        [Test]
        public void BuildChoice_BetTeamIsCharacterN_BetTeamIsNoGoalResource()
        {
            // Arrange
            _ticket.BetTeam = "n";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.nogoal, _choice.Template.betTeam);
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