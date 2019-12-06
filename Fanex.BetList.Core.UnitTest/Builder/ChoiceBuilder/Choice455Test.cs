namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Templates;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice455Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice455();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("NG")]
        [TestCase("ng")]
        [TestCase("Ng")]
        [TestCase("nG")]
        public void BuildBetTeam_TicketBetTeamIsNone_BetTeamIsNoGoal(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedDisplayingBetTeam = CoreBetList.NoGoal14;

            Assert.AreEqual(expectedDisplayingBetTeam, template.betTeam);
        }

        [TestCase("1-10", "00:01-10:00")]
        [TestCase("11-20", "10:01-20:00")]
        [TestCase("21-30", "20:01-30:00")]
        [TestCase("31-40", "30:01-40:00")]
        [TestCase("41-50", "40:01-50:00")]
        [TestCase("51-60", "50:01-60:00")]
        [TestCase("61-70", "60:01-70:00")]
        [TestCase("71-80", "70:01-80:00")]
        [TestCase("81-90", "80:01-FT")]
        public void BuildBetTeam_TicketBetTeamIsOneOfAcceptedIntervalsOf10Minutes_BetTeamIsAssociateIntervalOfTimeInSeconds(string betTeam, string displayingInterval)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual(displayingInterval, template.betTeam);
        }

        [TestCase("1-10 ")]
        [TestCase(" 1-10")]
        public void BuildBetTeam_TicketBetTeamHasTrailingSpaces_IgnoreTrailingSpaces(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            Choice_Template template = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedDisplayingBetTeam = "00:01-10:00";

            Assert.AreEqual(expectedDisplayingBetTeam, template.betTeam);
        }

        [TestCase("abc")]
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