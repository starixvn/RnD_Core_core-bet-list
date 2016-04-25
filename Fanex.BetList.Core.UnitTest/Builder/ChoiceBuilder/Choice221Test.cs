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
        public void BuildBetTeam_WhenCalled_ShowSituationText()
        {
            ////Arrange
            _ticket.Handicap2 = 2;
            _ticket.BetTeam = "2";
            _ticketHelper.GetFastMarketSituation(_ticket.Handicap2, _ticket.BetTeam).Returns("Goal");

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains("Goal"));
        }

        [Test]
        public void BuildBetTeam_Handicape1IsLessThan10_ShowCorrectTime()
        {
            ////Arrange
            _ticket.Handicap1 = 9;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains("(09:00-09:59)"));
        }

        [Test]
        public void BuildBetTeam_Handicape1IsGreaterThan10_ShowCorrectTime()
        {
            ////Arrange
            _ticket.Handicap1 = 60;

            ////Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            Assert.IsTrue(_choice.Template.betTeam.Contains("(60:00-60:59)"));
        }
    }
}
