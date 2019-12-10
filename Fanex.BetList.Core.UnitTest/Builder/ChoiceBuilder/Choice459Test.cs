namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Templates;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice459Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice459();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_TicketBetTeamIs1_BetTeamIsHtAsian1()
        {
            // Arrange.
            _ticket.BetTeam = "1";

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string ExpectedBetOption = $"{CoreBetList.HtAsian}.1";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TicketBetTeamIs2_BetTeamIsHtAsian2()
        {
            // Arrange.
            _ticket.BetTeam = "2";

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string ExpectedBetOption = $"{CoreBetList.HtAsian}.2";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [TestCase("X")]
        [TestCase("x")]
        public void BuildBetTeam_TicketBetTeamIsX_BetTeamIsHtAsianX(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string ExpectedBetOption = $"{CoreBetList.HtAsian}.X";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }
    }
}