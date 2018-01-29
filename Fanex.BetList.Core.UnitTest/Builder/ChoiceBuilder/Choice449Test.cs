namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System.Globalization;
    using App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Templates;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice449Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice449();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("1XO")]
        [TestCase("1xO")]
        [TestCase("1Xo")]
        [TestCase("1xo")]
        public void BuildBetTeam_TicketBetTeamIs1XO_BetTeamIsHomeDrawAndOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetOption = $"{CoreBetList.Home}/{CoreBetList.draw} <span class=\"underdog\">&</span> {CoreBetList.over}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("1XU")]
        [TestCase("1xU")]
        [TestCase("1Xu")]
        [TestCase("1xu")]
        public void BuildBetTeam_TicketBetTeamIs1XU_BetTeamIsHomeDrawAndUnder(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetOption = $"{CoreBetList.Home}/{CoreBetList.draw} <span class=\"underdog\">&</span> {CoreBetList.under}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("12O")]
        [TestCase("12o")]
        public void BuildBetTeam_TicketBetTeamIs12O_BetTeamIsHomeAwayAndOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetOption = $"{CoreBetList.Home}/{CoreBetList.Away} <span class=\"underdog\">&</span> {CoreBetList.over}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("12U")]
        [TestCase("12u")]
        public void BuildBetTeam_TicketBetTeamIs12U_BetTeamIsHomeAwayAndUnder(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetOption = $"{CoreBetList.Home}/{CoreBetList.Away} <span class=\"underdog\">&</span> {CoreBetList.under}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("2XO")]
        [TestCase("2xO")]
        [TestCase("2Xo")]
        [TestCase("2xo")]
        public void BuildBetTeam_TicketBetTeamIs2XO_BetTeamIsAwayDrawAndOver(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetOption = $"{CoreBetList.Away}/{CoreBetList.draw} <span class=\"underdog\">&</span> {CoreBetList.over}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("2XU")]
        [TestCase("2xU")]
        [TestCase("2Xu")]
        [TestCase("2xu")]
        public void BuildBetTeam_TicketBetTeamIs2XU_BetTeamIsAwayDrawAndUnder(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetOption = $"{CoreBetList.Away}/{CoreBetList.draw} <span class=\"underdog\">&</span> {CoreBetList.under}";

            Assert.AreEqual(expectedBetOption, template.betTeam);
        }

        [TestCase("abc")]
        [TestCase("1x2")]
        [TestCase("1XOabc")]
        [TestCase("abc1XO")]
        [TestCase("")]
        [TestCase(null)]
        public void BuildBetTeam_TicketBetTeamIsNotInExpectedValueList_BetTeamIsEmpty(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
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