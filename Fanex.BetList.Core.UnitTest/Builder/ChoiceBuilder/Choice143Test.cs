namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Core.Templates;
    using Entities;
    using Fanex.BetList.Core.App_GlobalResources;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for bet type 1H Home Team Odd/Even - 136.
    /// </summary>
    [TestFixture]
    public class Choice143Test
    {
        private const string HomeTeamName = "Chelsea";
        private const string AwayTeamName = "Arsenal";

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice143();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.HomeId = 1;
            _ticket.AwayId = 2;

            _ticketHelper.GetTeamNameById(Arg.Is(_ticket.HomeId)).Returns(HomeTeamName);
            _ticketHelper.GetTeamNameById(Arg.Is(_ticket.AwayId)).Returns(AwayTeamName);
        }

        [TestCase("HO")]
        [TestCase("ho")]
        [TestCase("Ho")]
        [TestCase("hO")]
        public void BuildBetTeam_TicketBetTeamIsHO_TemplateBetTeamIsHomeTeamNameAndOver(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetOption = $"{HomeTeamName} <span class=\"underdog\">&</span> {CoreBetList.over}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("HU")]
        [TestCase("hu")]
        [TestCase("Hu")]
        [TestCase("hU")]
        public void BuildBetTeam_TicketBetTeamIsHU_TemplateBetTeamIsHomeTeamNameAndUnder(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetOption = $"{HomeTeamName} <span class=\"underdog\">&</span> {CoreBetList.under}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("AO")]
        [TestCase("ao")]
        [TestCase("Ao")]
        [TestCase("aO")]
        public void BuildBetTeam_TicketBetTeamIsAO_TemplateBetTeamIsAwayTeamNameAndOver(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetOption = $"{AwayTeamName} <span class=\"underdog\">&</span> {CoreBetList.over}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("AU")]
        [TestCase("au")]
        [TestCase("Au")]
        [TestCase("aU")]
        public void BuildBetTeam_TicketBetTeamIsAU_TemplateBetTeamIsAwayTeamNameAndUnder(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetOption = $"{AwayTeamName} <span class=\"underdog\">&</span> {CoreBetList.under}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("DO")]
        [TestCase("do")]
        [TestCase("Do")]
        [TestCase("dO")]
        public void BuildBetTeam_TicketBetTeamIsDO_TemplateBetTeamIsDrawAndOver(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetOption = $"{CoreBetList.draw} <span class=\"underdog\">&</span> {CoreBetList.over}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("DU")]
        [TestCase("du")]
        [TestCase("Du")]
        [TestCase("dU")]
        public void BuildBetTeam_TicketBetTeamIsDU_TemplateBetTeamIsDrawAndUnder(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetOption = $"{CoreBetList.draw} <span class=\"underdog\">&</span> {CoreBetList.under}";

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
            Assert.AreEqual(ObservedHandicap1.ToString(), template.Handicap.handicap);
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