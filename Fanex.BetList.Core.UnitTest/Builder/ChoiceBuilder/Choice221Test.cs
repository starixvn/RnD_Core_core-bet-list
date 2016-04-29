namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice221Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice221();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Correct_Score;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_ForOneMinute_ShowBetChoice()
        {
            ////Arrange
            _ticket.BetTypeId = 221;
            _ticket.BetTeam = "2";
            _ticket.Handicap1 = 9;
            _ticket.Handicap2 = 2;
            _ticketHelper.GetFastMarketSituation(_ticket.BetTypeId.ToString(), _ticket.BetTeam, _ticket.Handicap1, _ticket.Handicap2).Returns("Goal (09:00-09:59)");

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.AreEqual("Goal (09:00-09:59)",_choice.Template.betTeam);
        }
    }
}
