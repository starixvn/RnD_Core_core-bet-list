namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Templates;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice454Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice454();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("1XH")]
        [TestCase("1xH")]
        [TestCase("1Xh")]
        [TestCase("1xh")]
        public void BuildBetTeam_BetTeamIs1XH_BetTeamIsHomeDrawAndHome(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string ExpectedBetOption = "Home/Draw <span class=\"underdog\">&</span> Home";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [TestCase("12H")]
        [TestCase("12h")]
        public void BuildBetTeam_BetTeamIs12H_BetTeamIsHomeAwayAndHome(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string ExpectedBetOption = "Home/Away <span class=\"underdog\">&</span> Home";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [TestCase("2XH")]
        [TestCase("2xH")]
        [TestCase("2Xh")]
        [TestCase("2xh")]
        public void BuildBetTeam_BetTeamIs2XH_BetTeamIsAwayDrawAndHome(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string ExpectedBetOption = "Away/Draw <span class=\"underdog\">&</span> Home";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [TestCase("1XA")]
        [TestCase("1xA")]
        [TestCase("1Xa")]
        [TestCase("1xa")]
        public void BuildBetTeam_BetTeamIs1XA_BetTeamIsHomeDrawAndAway(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string ExpectedBetOption = "Home/Draw <span class=\"underdog\">&</span> Away";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [TestCase("12A")]
        [TestCase("12a")]
        public void BuildBetTeam_BetTeamIs12A_BetTeamIsHomeAwayAndAway(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string ExpectedBetOption = "Home/Away <span class=\"underdog\">&</span> Away";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [TestCase("2XA")]
        [TestCase("2xA")]
        [TestCase("2Xa")]
        [TestCase("2xa")]
        public void BuildBetTeam_BetTeamIs2XA_BetTeamIsAwayDrawAndAway(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string ExpectedBetOption = "Away/Draw <span class=\"underdog\">&</span> Away";

            Assert.AreEqual(ExpectedBetOption, template.betTeam);
        }

        [TestCase("NG")]
        [TestCase("nG")]
        [TestCase("Ng")]
        [TestCase("ng")]
        public void BuildBetTeam_BetTeamIsNG_BetTeamIsNoGoal(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(CoreBetList.nogoal, template.betTeam);
        }

        [TestCase("abc")]
        [TestCase("1x2")]
        [TestCase("1XHabc")]
        [TestCase("abc1XH")]
        [TestCase("NGabc")]
        [TestCase("")]
        [TestCase(null)]
        public void BuildBetTeam_BetTeamIsNotInExpectedValueList_BetTeamIsEmpty(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsEmpty(template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapToNull()
        {
            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsNull(template.Handicap.handicap);
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