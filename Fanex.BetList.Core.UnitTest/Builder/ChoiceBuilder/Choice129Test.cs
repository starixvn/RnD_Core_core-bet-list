namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for bet type First Half Exact Goal.
    /// </summary>
    [TestFixture]
    public class Choice129Test
    {
        private static readonly object[] ExpectedBetTeams =
        {
            new object[] { "0", CoreBetList.NoGoals, "Betting on the total number of goals in Half Time will be no goals." },
            new object[] { "1", CoreBetList.OneGoal, "Betting on the total number of goals in Half Time will be 1 goal." },
            new object[] { "2", CoreBetList.TwoGoals, "Betting on the total number of goals in Half Time will be 2 goals." },
            new object[] { "3", CoreBetList.ThreeGoals, "Betting on the total number of goals in Half Time will be 3 goals." },
            new object[] { "4", CoreBetList.FourGoals, "Betting on the total number of goals in Half Time will be 4 goals." },
            new object[] { "4&over", CoreBetList.FourAndOver, "Betting on that at least 4 goals will be scored in Half Time." },
            new object[] { "5&over", CoreBetList.FiveAndOver, "Betting on that at least 5 goals will be scored in Half Time." }
        };

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice129();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.FirstHalft_Exact_Goal;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test, TestCaseSource("ExpectedBetTeams")]
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