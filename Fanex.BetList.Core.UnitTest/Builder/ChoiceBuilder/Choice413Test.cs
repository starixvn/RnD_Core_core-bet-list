namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice413Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice413();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_HomeIdNotZero_BetTeamAreSpaceAndBetTeam()
        {
            // Arrange.
            _ticket.BetTeam = "4:4";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectedBetTeam = "4-4";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [TestCase("AOS")]
        [TestCase("aos")]
        public void BuildBetTeam_BetTeamIsAOS_BetTeamIsAOS(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.AreEqual(CoreBetList.AOS, _choice.Template.betTeam);
        }

        public void BuildBetTeam_BetTeamIsNotAOS_NotShowExcludingChoices()
        {
            // Arrange.
            _ticket.BetTeam = "4:4";

            // Act.
            var choiceHtml = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>()).ToString();

            // Assert.
            StringAssert.DoesNotContain(CoreBetList.Excluding, choiceHtml);
        }

        [TestCase("AOS")]
        [TestCase("aos")]
        public void BuildBetTeam_BetTeamIsAOSAndTransDescIsNull_NotShowExcludingChoices(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var choiceHtml = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>()).ToString();

            // Assert.
            StringAssert.DoesNotContain(CoreBetList.Excluding, choiceHtml);
        }

        [TestCase("AOS")]
        [TestCase("aos")]
        public void BuildBetTeam_BetTeamIsAOSAndTransDescIsNotNull_ShowExcludingChoices(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;
            _ticket.TransDesc = "1:1,1:2,2:1";

            // Act.
            var choiceHtml = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>()).ToString();

            // Assert.
            string expectedChoiceList = $"{CoreBetList.Excluding}: 1-1, 1-2, 2-1";
            StringAssert.Contains(expectedChoiceList, choiceHtml);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }
    }
}