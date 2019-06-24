namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice136Test
    {
#pragma warning disable S1144 // Unused private types or members should be removed

        private static readonly object[] expectedBetTeams =
        {
            new object[] { "o", CoreBetList.odd, "Betting on that total number of goals scored by Home Team in First Half will be odd." },
            new object[] { "e",  CoreBetList.even, "Betting on that total number of goals scored by Home Team in First Half will be even." }
        };

#pragma warning restore S1144 // Unused private types or members should be removed

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice136();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.FirstHalfHomeTeamOddEven;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test, TestCaseSource("expectedBetTeams")]
        public void BuildBetTeam_Always_SetRightBetTeam(string betTeam, string expectedResult, string expectedMessage)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(expectedResult, _choice.Template.betTeam, expectedMessage);
        }
    }
}