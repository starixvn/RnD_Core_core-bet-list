namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Templates;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice223Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice223();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.BetTeam = "2";
            _ticket.Handicap1 = 9;
            _ticket.Handicap2 = 2;
        }

        [Test]
        public void BuildBetTeam_Always_CallGetFastMarketSituation()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            _ticketHelper.Received().GetFastMarketSituation(_ticket.BetTypeId.ToString(), _ticket.BetTeam, _ticket.Handicap1, _ticket.Handicap2);
        }

        [Test]
        public void BuildBetTeam_Always_BetTeamIsTheResultOfGetFastMarketSituation()
        {
            // Arrange.
            const string fastMarketsChoice = "Goal (09:00-09:59)";

            _ticketHelper
                .GetFastMarketSituation(_ticket.BetTypeId.ToString(), _ticket.BetTeam, _ticket.Handicap1, _ticket.Handicap2)
                .Returns(fastMarketsChoice);

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(fastMarketsChoice, template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapToNull()
        {
            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameToFavorite()
        {
            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string Favorite = "favorite";

            Assert.AreEqual(Favorite, template.betTeamClassName);
        }
    }
}