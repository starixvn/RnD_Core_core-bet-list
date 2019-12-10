namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Templates;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice458Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice458();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_TicketBetTeamIs1_BetTeamIsFtAsian1()
        {
            // Arrange.
            _ticket.BetTeam = "1";

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string ExpectedBetOption = $"{CoreBetList.FtAsian}.1";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_TicketBetTeamIs2_BetTeamIsFtAsian2()
        {
            // Arrange.
            _ticket.BetTeam = "2";

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string ExpectedBetOption = $"{CoreBetList.FtAsian}.2";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [TestCase("X")]
        [TestCase("x")]
        public void BuildBetTeam_TicketBetTeamIsX_BetTeamIsFtAsianX(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string ExpectedBetOption = $"{CoreBetList.FtAsian}.X";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
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