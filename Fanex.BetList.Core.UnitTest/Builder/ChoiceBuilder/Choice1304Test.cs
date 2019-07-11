namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1304Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1304();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.TotalSets;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetType_Always_SetBetTypeNameIsBetTypeName()
        {
            // Arrange
            const string expectedBetTypeName = "Bet type name";

            _ticketHelper
                .GetBetTypeNameById(_ticket.BetTypeId, _ticket.RefNo, _ticket.BetId, Arg.Any<object>())
                .Returns(expectedBetTypeName);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(expectedBetTypeName, _choice.Template.BetType.betTypeName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string favorite = "favorite";

            Assert.AreEqual(favorite, _choice.Template.betTeamClassName);
        }
    }
}