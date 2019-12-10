namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using System.Globalization;
    using App_GlobalResources;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice1334Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1334();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.SetXGameYAndGameYAdd1Points;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Arrange
            _ticket.Handicap1 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.AreEqual(_ticket.Handicap1.ToString(CultureInfo.InvariantCulture), _choice.Template.Handicap.handicap);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs1_BetTeamIsUnderResource()
        {
            // Arrange
            _ticket.BetTeam = "1";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = string.Format("<span class=\"underdog\">{0}</span>", CoreBetList.under);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs2_BetTeamIsOverResource()
        {
            // Arrange
            _ticket.BetTeam = "2";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = string.Format("<span class=\"favorite\">{0}</span>", CoreBetList.over);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamNotIs1AndNotIs2_BetTeamIsExactlyResource()
        {
            // Arrange
            _ticket.BetTeam = string.Empty;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            string expectedBetTeam = string.Format("<span class=\"underdog\">{0}</span>", CoreBetList.exactly);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
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