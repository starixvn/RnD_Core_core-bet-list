namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice3913Test
    {
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private IChoice _choice;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticket = new Ticket();
            _choice = new Choice3913();
            _ticket.HomeId = 1;
            _ticket.AwayId = 2;
            _ticketHelper.GetTeamNameById(_ticket.HomeId).Returns("Team Home");
            _ticketHelper.GetTeamNameById(_ticket.AwayId).Returns("Team Away");
        }

        [TestCase(801, "Set 1 Winner & Total Points")]
        public void BuildBetType_BetChoice_CorrectBetTypeName(long betId, string betTypeName)
        {
            //Arrange
            _ticket.BetId = betId;
            _ticket.BetTypeId = 3913;
            _ticket.BetTeam = "au";
            _ticketHelper.GetBetTypeNameById(3913, Arg.Any<string>(), betId: betId).Returns<string>(betTypeName);

            //Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(betTypeName, _choice.Template.BetType.betTypeName);
        }

        [TestCase("ao", "favorite")]
        [TestCase("hu", "underdog")]
        public void BuildBetTeamClassNameAndHandicap_BetTeamValue_CorrectBetTeamClassName(string betTeamValue, string className)
        {
            //Arrange
            _ticket.BetTeam = betTeamValue;

            //Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(className, _choice.Template.betTeamClassName);
        }
        
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Handicap1_CorrectHandicap()
        {
            //Arrange
            _ticket.BetTeam = "au";
            _ticket.Handicap1 = 12;

            //Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("12", _choice.Template.Handicap.handicap);
        }

        [TestCase("ho", "<span class=\"underdog\">Team Home &</span> Over")]
        [TestCase("hu", "<span class=\"underdog\">Team Home &</span> Under")]
        [TestCase("ao", "<span class=\"underdog\">Team Away &</span> Over")]
        [TestCase("au", "<span class=\"underdog\">Team Away &</span> Under")]
        [TestCase("a", "")]
        public void BuildBetTeam_BetTeamValue_CorrectBetChoice(string betTeamValue, string betChoice)
        {
            //Arrange
            _ticket.BetTeam = betTeamValue;

            //Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(betChoice, _choice.Template.betTeam);
        }
    }
}
