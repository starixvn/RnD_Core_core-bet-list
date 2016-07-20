namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice417Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice417();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("YH", "Yes/HomeTeam")]
        [TestCase("YD", "Yes/Draw")]
        [TestCase("YA", "Yes/AwayTeam")]
        [TestCase("NH", "No/HomeTeam")]
        [TestCase("ND", "No/Draw")]
        [TestCase("NA", "No/AwayTeam")]
        public void BuildBetTeam_BetTeam_TemplateBetTeam(string betTeam, string expected)
        {
            //Arrange
            _ticket.HomeId = 123;
            _ticket.AwayId = 456;
            _ticket.BetTeam = betTeam;
            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns<string>("HomeTeam");
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns<string>("AwayTeam");

            //Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            //Assert
            Assert.AreEqual(expected, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }
    }
}
