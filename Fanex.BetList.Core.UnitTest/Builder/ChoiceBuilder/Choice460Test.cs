namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Templates;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice460Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice460();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
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
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameToUnderdog()
        {
            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string Underdog = "underdog";

            Assert.AreEqual(Underdog, template.betTeamClassName);
        }
    }
}