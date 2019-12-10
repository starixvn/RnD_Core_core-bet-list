namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Constants;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Choice28Test
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice28();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.ThreeWayHandicap;
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
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs1_BetTeamAreHomeTeamNameAndHandicapIsHandicap2SubtractHandicap1()
        {
            // Arrange
            _ticket.BetTeam = "1";
            _ticket.Handicap2 = 2;
            _ticket.Handicap1 = 1;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Home team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedBetTeam = "Home team name&nbsp;<span class=\"underdog\">(+1)</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamIs2_BetTeamAreAwayTeamNameAndHandicapIsHandicap1SubtractHandicap2()
        {
            // Arrange
            _ticket.BetTeam = "2";
            _ticket.Handicap2 = 1;
            _ticket.Handicap1 = 2;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Away team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedBetTeam = "Away team name&nbsp;<span class=\"underdog\">(+1)</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamNotIs1AndNotIs2AndHandicap1GreaterThanHandicap2_BetTeamAreDrawResourceAndaResource()
        {
            // Arrange
            _ticket.BetTeam = "bet team";
            _ticket.Handicap2 = 1;
            _ticket.Handicap1 = 2;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedBetTeam = string.Format("{0}&nbsp;<span class=\"underdog\">({1}+1)</span>", CoreBetList.draw, CoreBetList.a);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamNotIs1AndNotIs2AndHandicap2GreaterThanHandicap1_BetTeamAreDrawResourceAndaResource()
        {
            // Arrange
            _ticket.BetTeam = "bet team";
            _ticket.Handicap2 = 2;
            _ticket.Handicap1 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedBetTeam = string.Format("{0}&nbsp;<span class=\"underdog\">({1}+1)</span>", CoreBetList.draw, CoreBetList.h);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        [Test]
        public void BuildBetTeam_BetTeamNotIs1AndNotIs2AndHandicap2EqualHandicap1_BetTeamAreDrawResourceAndaResource()
        {
            // Arrange
            _ticket.BetTeam = "bet team";
            _ticket.Handicap2 = 1;
            _ticket.Handicap1 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var expectedBetTeam = string.Format("{0}&nbsp;<span class=\"underdog\">(0)</span>", CoreBetList.draw);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }
    }
}