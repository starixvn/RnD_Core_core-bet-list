namespace Fanex.BetList.KenoLottery.Tests.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice1031Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1031();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = 1031;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapBlockIsNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsNull(template.Handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual("favorite", template.betTeamClassName);
        }

        [Test]
        public void BuildMatch_Always_SetMatchToNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsNull(template.Match);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void BuildScore_Always_ScoreBlockIsNull(bool isLive)
        {
            // Arrange.
            _ticket.IsLive = isLive;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(template.Score);
        }

        [Test]
        public void BuildBetTeam_Always_GetBetChoiceAssociateWithChoiceIdFromRemotingRef()
        {
            // Arrange.
            const string BetChoice = "Bet Choice";
            string anyBetTypeId;

            _ticketHelper.GetLotteryBetChoiceName(Arg.Any<string>(), out anyBetTypeId).Returns(BetChoice);

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(BetChoice, template.betTeam);
        }

        [Test]
        public void BuildBetType_Always_GetBetTypeAssociateWithChoiceIdFromRemotingRef()
        {
            // Arrange.
            const string BetTypeName = "Bet Type Name";

            _ticketHelper.GetLotteryBetTypeName(Arg.Any<string>()).Returns(BetTypeName);

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(BetTypeName, template.BetType.betTypeName);
        }
    }
}