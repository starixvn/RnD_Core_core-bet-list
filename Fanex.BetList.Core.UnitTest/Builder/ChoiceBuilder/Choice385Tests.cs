namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice385Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice385();
            _ticket = TestData.CreateTicket(BetTypes.PenaltyShootout1X2RoundX);
            _ticket.HomeId = 1;
            _ticket.AwayId = 2;

            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns("Team name 1");
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns("Team name 2");
        }

        [TestCase("1", "Team name 1")]
        [TestCase("2", "Team name 2")]
        [TestCase("X", "Draw")]
        [TestCase("x", "Draw")]
        public void BuildBetTeam_Always_BetChoiceIsExpectedBetTeam(string betTeam, string expected)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expected, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsFavorite()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string favorite = "favorite";

            Assert.AreEqual(favorite, template.betTeamClassName);
        }

        [Test]
        public void BuildBetType_Always_SendBetIdAndBetCheckToGetCorrectBetTypeName()
        {
            // Arrange.
            const int betId = 801;
            const string betCheck = "801";
            const string expectedBetTypeName = "Penalty Shootout 1X2 - Round 1";

            _ticket.BetId = betId;
            _ticket.BetCheck = betCheck;

            _ticketHelper
                .GetBetTypeNameById(_ticket.BetTypeId, Arg.Any<object>(), _ticket.BetId, _ticket.BetCheck)
                .Returns(expectedBetTypeName);

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetTypeName, template.BetType.betTypeName);
        }
    }
}