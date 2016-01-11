namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice501Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice501();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.SportTypeId = 50;
            _ticket.BetTypeId = 501;
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_BetTeamClassNameIsUnderdog()
        {
            ////Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var betTeamClassName = template.betTeamClassName;
            var expectedBetTeamClassName = "underdog";
            Assert.AreEqual(expectedBetTeamClassName, betTeamClassName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_HandicapIsNull()
        {
            ////Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            ////Assert
            var handicap = template.Handicap;
            Assert.IsNull(handicap);
        }
    }
}