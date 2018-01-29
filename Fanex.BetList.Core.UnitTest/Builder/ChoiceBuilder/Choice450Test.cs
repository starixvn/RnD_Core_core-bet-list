namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System.Globalization;
    using App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Templates;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice450Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice450();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("OO")]
        [TestCase("Oo")]
        [TestCase("oO")]
        [TestCase("oo")]
        public void BuildBetTeam_TicketBetTeamIsOO_BetTeamIsOddAndOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectedBetOption = $"{CoreBetList.odd} <span class=\"underdog\">&</span> {CoreBetList.over}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("OU")]
        [TestCase("Ou")]
        [TestCase("oU")]
        [TestCase("ou")]
        public void BuildBetTeam_TicketBetTeamIsOU_BetTeamIsOddAndUnder(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectedBetOption = $"{CoreBetList.odd} <span class=\"underdog\">&</span> {CoreBetList.under}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("EO")]
        [TestCase("Eo")]
        [TestCase("eO")]
        [TestCase("eo")]
        public void BuildBetTeam_TicketBetTeamIsEO_BetTeamIsEvenAndOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectedBetOption = $"{CoreBetList.even} <span class=\"underdog\">&</span> {CoreBetList.over}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("EU")]
        [TestCase("Eu")]
        [TestCase("eU")]
        [TestCase("eu")]
        public void BuildBetTeam_TicketBetTeamIsEU_BetTeamIsEvenAndUnder(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectedBetOption = $"{CoreBetList.even} <span class=\"underdog\">&</span> {CoreBetList.under}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("abc")]
        [TestCase("UO")]
        [TestCase("OE")]
        [TestCase("UE")]
        [TestCase("OxO")]
        [TestCase("OxU")]
        [TestCase("ExO")]
        [TestCase("ExU")]
        [TestCase("")]
        [TestCase(null)]
        public void BuildBetTeam_TicketBetTeamIsNotInExpectedValueList_BetTeamIsEmpty(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsEmpty(template.betTeam);
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