namespace Fanex.BetList.SportsLottery.Tests.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice3611Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice3611();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = SportsLotteryBetTypes.RepresentativeBetTypeId;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketHelper.GetBetTypeNameById(SportsLotteryBetTypes.RepresentativeBetTypeId).Returns("Bettype");
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapBlockIsNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("favorite", template.betTeamClassName);
        }

        [Test]
        public void BuildMatch_Always_SetMatchToNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

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
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(template.Score);
        }
       
        [Test]
        public void BuildBetTeam_GetRefResource_ByTransDesc()
        {
            // Arrange.
            _ticketHelper.GetResourceData("SportsLotteryChoice", "3612_1").Returns("Small");
            _ticket.TransDesc = "betchoice=3612_1";

            // Act.
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("Small", act.betTeam);
        }
        

        [Test]
        public void BuildBetType_Always_Hide()
        {
            // Arrange.            

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(template.BetType.Visible);
        }

        [Test]
        public void BuildLeague_Alway_CorrectFormat()
        {
            // Arrange.
            _ticket.BetCheck = "BetCheck";            

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.True(template.League.LeagueName.leagueName.Contains($"Bettype [BetCheck]"));
        }
    }
}
