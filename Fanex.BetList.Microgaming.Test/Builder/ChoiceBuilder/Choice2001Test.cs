namespace Fanex.BetList.Microgaming.Test.Builder.ChoiceBuilder
{
    using NUnit.Framework;
    using NSubstitute;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;

    public class Choice2001Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2001();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildMatch_WhenCalled_AlwaysVisible()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsFalse(_choice.Template.Match.Visible);
        }

        [Test]
        public void BuildBetTeam_WhenCalled_SetBetTeamIsEmpty()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsEmpty(_choice.Template.betTeam);
        }

        [Test]
        public void BuildBetType_WhenCalled_AlwaysVisible()
        {
            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsFalse(_choice.Template.BetType.Visible);
        }

        [Test]
        public void BuildSport_WhenCalled_ReturnMicroGamming()
        {
            _ticketHelper.GetSportNameById(Arg.Any<int>()).Returns("Microgaming");
            _choice.Render(_ticket, _ticketHelper, null, false);

            const string ExpectedSport = "Microgaming";
            Assert.AreEqual(ExpectedSport, _choice.Template.League.sportTypeName);
        }

        [Test]
        public void BuildLeague_WhenCalled_ReturnBetTypeNameAndGameName()
        {
            _ticketHelper.GetBetTypeNameById(Arg.Any<int>()).Returns("Baccarat");
            _ticketHelper.GetResourceData("MCG_LiveCasino", Arg.Any<string>()).Returns("MP Baccarat");
            _choice.Render(_ticket, _ticketHelper, null, false);

            const string ExpectedLeague = "Baccarat&nbsp;<span class='underdog'>MP Baccarat</span>";
            Assert.AreEqual(ExpectedLeague, _choice.Template.League.LeagueName.leagueName);
        }
    }
}
