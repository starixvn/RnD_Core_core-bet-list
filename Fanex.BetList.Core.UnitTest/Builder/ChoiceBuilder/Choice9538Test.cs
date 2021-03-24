namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice9538Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice9538();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.OneXTwoBackLay;
            _ticket.HomeId = 1;
            _ticket.AwayId = 2;
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketHelper.GetTeamNameById((long)1).Returns("home 1");
            _ticketHelper.GetTeamNameById((long)2).Returns("away 1");
        }

        [TestCase("1b", " <span class='underdog'>home 1</span> Back")]
        [TestCase("xb", " <span class='underdog'>Draw</span> Back")]
        [TestCase("2b", " <span class='underdog'>away 1</span> Back")]
        [TestCase("1l", " <span class='underdog'>home 1</span> Lay")]
        [TestCase("xl", " <span class='underdog'>Draw</span> Lay")]
        [TestCase("2l", " <span class='underdog'>away 1</span> Lay")]
        public void BuildBetTeam_Always_ShowCorrectText(string betteam, string text)
        {
            // Arrange
            _ticket.BetTeam = betteam;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(text, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }
    }
}