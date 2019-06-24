namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice405Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice405();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.SecondHalfCorrectScore;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("AOS")]
        [TestCase("aos")]
        public void BuildBetTeam_BetTeamIsAOS_BetTeamIsAOS(string betTeam)
        {
            // Arrange
            _ticket.BetTeam = betTeam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.AreEqual("&nbsp;AOS", _choice.Template.betTeam);
        }

        [Test]
        public void BuildChoice_BetTeamIsNotAOS_BetTeamAreSpaceAndBetTeam()
        {
            // Arrange
            _ticket.BetTeam = "4:4";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            string expectedBetTeam = "&nbsp;4-4";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
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

        [Test]
        public void BuildBetType_Always_ShowBetTypeNameFromRemotingRef()
        {
            // Arrange.
            const string BET_TYPE_NAME = "Bet type name";
            _ticketHelper.GetBetTypeNameById(405, Arg.Any<object>()).Returns(BET_TYPE_NAME);

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(BET_TYPE_NAME, _choice.Template.BetType.betTypeName);
        }
    }
}