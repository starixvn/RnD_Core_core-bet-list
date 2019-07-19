namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice416Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice416();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_HomeIdNotZero_BetTeamAreSpaceAndBetTeam()
        {
            // Arrange.
            _ticket.BetTeam = "4:4/4:4";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectedBetTeam = "&nbsp;Half Time 4-4 / Full Time 4-4";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [TestCase("AOS/0:0", "Half Time AOS / Full Time 0-0")]
        [TestCase("aos/0:0", "Half Time AOS / Full Time 0-0")]
        [TestCase("0:0/AOS", "Half Time 0-0 / Full Time AOS")]
        [TestCase("0:0/aos", "Half Time 0-0 / Full Time AOS")]
        [TestCase("AOS/AOS", "Half Time AOS / Full Time AOS")]
        [TestCase("aos/aos", "Half Time AOS / Full Time AOS")]
        public void BuildBetTeam_BetTeamIsAOS_BetTeamIsAOS(string betTeam, string expected)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert.
            string expectedBetTeam = $"&nbsp;{expected}";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIsNotAOS_NotShowFTExcludingChoices()
        {
            // Arrange.
            _ticket.BetTeam = "4:4/4:4";

            // Act.
            var choiceHtml = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>()).ToString();

            // Assert.
            StringAssert.DoesNotContain(CoreBetList.Excluding, choiceHtml);
        }

        [TestCase("AOS/0:0")]
        [TestCase("aos/0:0")]
        [TestCase("0:0/AOS")]
        [TestCase("0:0/aos")]
        [TestCase("AOS/AOS")]
        [TestCase("aos/aos")]
        public void BuildBetTeam_BetTeamIsAOSAndTransDescIsNull_NotShowExcludingChoices(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var choiceHtml = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>()).ToString();

            // Assert.
            StringAssert.DoesNotContain(CoreBetList.Excluding, choiceHtml);
        }

        [TestCase("AOS/0:0")]
        [TestCase("aos/0:0")]
        public void BuildBetTeam_BetTeamIsHTAOSAndTransDescIsNotNull_ShowHTExcludingChoices(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;
            _ticket.TransDesc = "1:1,1:2,2:1/";

            // Act.
            var choiceHtml = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>()).ToString();

            // Assert.
            string expectedChoiceList = $"{CoreBetList.AOSHTExcluding}: 1-1, 1-2, 2-1";
            StringAssert.Contains(expectedChoiceList, choiceHtml);
        }

        [TestCase("0:0/AOS")]
        [TestCase("0:0/aos")]
        public void BuildBetTeam_BetTeamIsFTAOSAndTransDescIsNotNull_ShowFTExcludingChoices(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;
            _ticket.TransDesc = "/1:1,1:2,2:1";

            // Act.
            var choiceHtml = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>()).ToString();

            // Assert.
            string expectedChoiceList = $"{CoreBetList.AOSFTExcluding}: 1-1, 1-2, 2-1";
            StringAssert.Contains(expectedChoiceList, choiceHtml);
        }

        [TestCase("AOS/AOS")]
        [TestCase("aos/aos")]
        public void BuildBetTeam_BetTeamIsHTFTAOSAndTransDescIsNotNull_ShowHTExcludingChoices(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;
            _ticket.TransDesc = "0:0/1:1,1:2,2:1";

            // Act.
            var choiceHtml = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>()).ToString();

            // Assert.
            string expectedChoiceList = $"{CoreBetList.AOSHTExcluding}: 0-0";
            StringAssert.Contains(expectedChoiceList, choiceHtml);
        }

        [TestCase("AOS/AOS")]
        [TestCase("aos/aos")]
        public void BuildBetTeam_BetTeamIsHTFTAOSAndTransDescIsNotNull_ShowFTExcludingChoices(string betTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;
            _ticket.TransDesc = "0:0/1:1,1:2,2:1";

            // Act.
            var choiceHtml = _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>()).ToString();

            // Assert.
            string expectedChoiceList = $"{CoreBetList.AOSFTExcluding}: 1-1, 1-2, 2-1";
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