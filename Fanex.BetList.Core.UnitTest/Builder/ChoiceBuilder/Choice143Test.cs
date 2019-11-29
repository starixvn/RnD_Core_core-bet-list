namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System.Globalization;
    using App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Templates;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice143Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice143();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("HO")]
        [TestCase("ho")]
        [TestCase("Ho")]
        [TestCase("hO")]
        public void BuildBetTeam_BetTeamIsHO_BetChoiceIsHomeSlashOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            string expectedBetOption = $"{CoreBetList.Home}/{CoreBetList.over}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("HU")]
        [TestCase("hu")]
        [TestCase("Hu")]
        [TestCase("hU")]
        public void BuildBetTeam_BetTeamIsHU_BetChoiceIsHomeSlashUnder(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            string expectedBetOption = $"{CoreBetList.Home}/{CoreBetList.under}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("AO")]
        [TestCase("ao")]
        [TestCase("Ao")]
        [TestCase("aO")]
        public void BuildBetTeam_BetTeamIsAO_BetChoiceIsAwaySlashOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            string expectedBetOption = $"{CoreBetList.Away}/{CoreBetList.over}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("AU")]
        [TestCase("au")]
        [TestCase("Au")]
        [TestCase("aU")]
        public void BuildBetTeam_BetTeamIsAU_BetChoiceAwaySlashUnder(string betTeam)
        {
            // Arrange..
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            string expectedBetOption = $"{CoreBetList.Away}/{CoreBetList.under}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("DO")]
        [TestCase("do")]
        [TestCase("Do")]
        [TestCase("dO")]
        public void BuildBetTeam_BetTeamIsDO_BetChoiceIsDrawSlashOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            string expectedBetOption = $"{CoreBetList.draw}/{CoreBetList.over}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("DU")]
        [TestCase("du")]
        [TestCase("Du")]
        [TestCase("dU")]
        public void BuildBetTeam_BetTeamIsDU_BetChoiceIsDrawSlashUnder(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            string expectedBetOption = $"{CoreBetList.draw}/{CoreBetList.under}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamLengthIsNot2_BetTeamIsNotSet()
        {
            // Arrange.
            var defaultBetTeamValue = _choice.Template.betTeam;

            _ticket.BetTeam = "a bet team";

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(defaultBetTeamValue, template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapToHandicap1()
        {
            // Arrange.
            const decimal ObservedHandicap1 = 1.5M;

            _ticket.Handicap1 = ObservedHandicap1;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(ObservedHandicap1.ToString(CultureInfo.InvariantCulture), template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameToFavorite()
        {
            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string Favorite = "favorite";

            Assert.AreEqual(Favorite, template.betTeamClassName);
        }
    }
}