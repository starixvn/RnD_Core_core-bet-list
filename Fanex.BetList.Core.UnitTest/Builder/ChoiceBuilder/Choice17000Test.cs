namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice17000Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice17000();
            _ticket = TestData.CreateTicket(BetTypes.SabaLuckySpin);
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildMatch_Always_HideMatch()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Match);
        }

        [Test]
        public void BuildBetTeam_Always_ShowBetTypeNameAsBetTeam()
        {
            // Arrange.
            const string fakeBetTypeName = "Saba Lucky Spin";

            _ticketHelper.GetBetTypeNameById(Arg.Is(BetTypes.SabaLuckySpin)).Returns(fakeBetTypeName);

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(fakeBetTypeName, template.betTeam);
        }

        [Test]
        public void BuildScore_Always_HideScore()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Score);
        }

        [Test]
        public void BuildBetType_Always_HideBetType()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.BetType);
        }

        [Test]
        public void BuildLeague_Always_HideLeague()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.League.LeagueName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsBetType()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectedCssClassName = "bettype";

            Assert.AreEqual(expectedCssClassName, template.betTeamClassName);
        }

        [TestCase("<p37 i=\"123\" />", 123)]
        [TestCase("<p37 i=\"456\" t=\"2\"/>", 456)]
        public void BuildBetTeamClassNameAndHandicap_Always_SetPromotionIdInExtraInfoToHandicap(string extraInfo, int promotionId)
        {
            // Arrange.
            _ticket.ExtraInfo = extraInfo;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedDisplayingHandicap = $" - {promotionId}";

            Assert.AreEqual(expectedDisplayingHandicap, template.Handicap.handicap);
        }

        [Test]
        public void BuildEventKickoffDateTime_Always_HideEventDate()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.EventDate);
        }
    }
}