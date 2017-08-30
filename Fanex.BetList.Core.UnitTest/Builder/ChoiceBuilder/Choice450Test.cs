namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
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
        public void BuildBetTeam_BetTeamIsOO_BetTeamIsOddAndOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string ExpectedBetOption = "Odd <span class=\"underdog\">&</span> Over";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [TestCase("OU")]
        [TestCase("Ou")]
        [TestCase("oU")]
        [TestCase("ou")]
        public void BuildBetTeam_BetTeamIsOU_BetTeamIsOddAndUnder(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string ExpectedBetOption = "Odd <span class=\"underdog\">&</span> Under";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [TestCase("EO")]
        [TestCase("Eo")]
        [TestCase("eO")]
        [TestCase("eo")]
        public void BuildBetTeam_BetTeamIsEO_BetTeamIsEvenAndOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string ExpectedBetOption = "Even <span class=\"underdog\">&</span> Over";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [TestCase("EU")]
        [TestCase("Eu")]
        [TestCase("eU")]
        [TestCase("eu")]
        public void BuildBetTeam_BetTeamIsEU_BetTeamIsEvenAndUnder(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string ExpectedBetOption = "Even <span class=\"underdog\">&</span> Under";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
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
        public void BuildBetTeam_BetTeamIsNotInExpectedValueList_BetTeamIsEmpty(string betTeam)
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