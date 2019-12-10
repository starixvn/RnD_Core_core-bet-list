namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1328Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1328();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.SetXGameYTotalPointsExact;
            _ticketHelper = Substitute.For<ITicketHelper>();
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
        public void BuildBetTeam_BetTeamIs01_BetTeamAreSpaceAnd4AndPointResource()
        {
            // Arrange
            _ticket.BetTeam = "01";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = string.Format("&nbsp;4 {0}", CoreBetList.points);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs02_BetTeamAreSpaceAnd5AndPointResource()
        {
            // Arrange
            _ticket.BetTeam = "02";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = string.Format("&nbsp;5 {0}", CoreBetList.points);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs03_BetTeamAreSpaceAnd6AndPointResource()
        {
            // Arrange
            _ticket.BetTeam = "03";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = string.Format("&nbsp;6 {0}", CoreBetList.points);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs04_BetTeamAreSpaceAnd7AndUpPointResource()
        {
            // Arrange
            _ticket.BetTeam = "04";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = string.Format("&nbsp;7 {0}", CoreBetList.upPoints);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamNotAre01Or02Or03Or04_BetTeamIsEmpty()
        {
            // Arrange
            _ticket.BetTeam = "bet team";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("&nbsp;", _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetType_Always_SetBetTypeNameIsBetTypeName()
        {
            // Arrange
            _ticketHelper.GetBetTypeNameById(Arg.Any<object>(), Arg.Any<object>(), Arg.Any<object>(), Arg.Any<object>()).Returns("Bet type name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual("Bet type name", _choice.Template.BetType.betTypeName);
        }
    }
}