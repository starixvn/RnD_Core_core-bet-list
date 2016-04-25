namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice222Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice222();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.Correct_Score;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_Handicape1IsLessThan10_ShowCorrectTime()
        {
            _ticket.Handicap1 = 0;

            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsTrue(_choice.Template.betTeam.Contains("(00:00-04:59)"));
        }

        [Test]
        public void BuildBetTeam_Handicape1IsGreaterThan10_ShowCorrectTime()
        {
            _ticket.Handicap1 = 60;

            _choice.Render(_ticket, _ticketHelper, null, false);

            Assert.IsTrue(_choice.Template.betTeam.Contains("(60:00-64:59)"));
        }

    }
}
