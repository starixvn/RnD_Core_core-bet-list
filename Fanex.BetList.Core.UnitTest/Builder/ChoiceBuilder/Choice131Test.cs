namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for bet type Home Team Total Goal 131.
    /// </summary>
    [TestFixture]
    public class Choice131Test
    {
        private static object[] expectedBetTeams =
        {
            new object[] { "0-1", "0-1", "Betting on that 0 or 1 goal will be scored by Home Team." },
            new object[] { "2-3",  "2-3", "Betting on the total number of goals in Half Time will be 1 goal." },
            new object[] { "4&over", CoreBetList.FourAndOver, "Betting on that at least 4 goals will be scored by Home Team." }
        };

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice131();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Home_Team_Total_Goal;
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
            Assert.AreEqual(string.Join(null, new string[] { "&nbsp;", expectedResult }), _choice.Template.betTeam, expectedMessage);
        }
    }
}