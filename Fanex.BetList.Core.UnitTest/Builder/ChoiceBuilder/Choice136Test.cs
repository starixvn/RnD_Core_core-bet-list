namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for bet type 1H Home Team Odd/Even - 136.
    /// </summary>
    [TestFixture]
    public class Choice136Test
    {
#pragma warning disable S1144 // Unused private types or members should be removed

        private static object[] expectedBetTeams =
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
            _ticket.BetTypeId = BetTypes.First_Home_Team_OddEven;
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