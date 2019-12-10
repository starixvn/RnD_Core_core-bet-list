namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
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
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_BetTeamIs1_BetChoiceIsHome()
        {
            // Arrange.
            const string betToHome = "1";

            _ticket.BetTeam = betToHome;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.Home, template.betTeam);
        }

        [TestCase("x")]
        [TestCase("X")]
        public void BuildBetTeam_BetTeamIsX_BetChoiceIsDraw(string betToDraw)
        {
            // Arrange.
            _ticket.BetTeam = betToDraw;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.draw, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs2_BetChoiceIsAway()
        {
            // Arrange.
            const string betToAway = "2";

            _ticket.BetTeam = betToAway;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(CoreBetList.Away, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsNot1X2_BetChoiceIsEmpty()
        {
            // Arrange.
            const string invalidBetTeam = "A";

            _ticket.BetTeam = invalidBetTeam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(template.betTeam);
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