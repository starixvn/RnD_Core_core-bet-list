namespace Fanex.BetList.Core.UnitTest.Builder.ChoiceBuilder
{
    using App_GlobalResources;
    using Common.Enums;
    using Core.Builder.ChoiceBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice28 class.
    /// </summary>
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
            _ticket.BetTypeId = BetTypes._3_Way_Handicap;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        /// <summary>
        /// The value of Template.Handicap.handicap is always null.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapIsNull()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            Assert.IsNull(_choice.Template.Handicap.handicap);
        }

        /// <summary>
        /// The value of Template.betTeamClassName is always favorite.
        /// </summary>
        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameIsFavorite()
        {
            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            const string CSS_CLASS_FAVORITE = "favorite";
            Assert.AreEqual(CSS_CLASS_FAVORITE, _choice.Template.betTeamClassName);
        }

        /// <summary>
        /// The value of Template.betTeam is always home team name and handicap 2 - handicap 1.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs1_BetTeamAreHomeTeamNameAndHandicapIsHandicap2SubtractHandicap1()
        {
            // Arrange
            _ticket.BetTeam = "1";
            _ticket.Handicap2 = 2;
            _ticket.Handicap1 = 1;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Home team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedBetTeam = "Home team name&nbsp;<span class=\"underdog\">(+1)</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always away team name and handicap 1 - handicap 2.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamIs2_BetTeamAreAwayTeamNameAndHandicapIsHandicap1SubtractHandicap2()
        {
            // Arrange
            _ticket.BetTeam = "2";
            _ticket.Handicap2 = 1;
            _ticket.Handicap1 = 2;
            _ticketHelper.GetTeamNameById(Arg.Any<long>()).Returns("Away team name");

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedBetTeam = "Away team name&nbsp;<span class=\"underdog\">(+1)</span>";
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always draw and handicap is a if handicap 1 > handicap 2.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamNotIs1AndNotIs2AndHandicap1GreaterThanHandicap2_BetTeamAreDrawResourceAndaResource()
        {
            // Arrange
            _ticket.BetTeam = "bet team";
            _ticket.Handicap2 = 1;
            _ticket.Handicap1 = 2;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedBetTeam = string.Format("{0}&nbsp;<span class=\"underdog\">({1}&nbsp;+1)</span>", CoreBetList.lblDraw, CoreBetList.a);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always draw and handicap is h if handicap 2> handicap 1.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamNotIs1AndNotIs2AndHandicap2GreaterThanHandicap1_BetTeamAreDrawResourceAndaResource()
        {
            // Arrange
            _ticket.BetTeam = "bet team";
            _ticket.Handicap2 = 2;
            _ticket.Handicap1 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedBetTeam = string.Format("{0}&nbsp;<span class=\"underdog\">({1}&nbsp;+1)</span>", CoreBetList.lblDraw, CoreBetList.h);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }

        /// <summary>
        /// The value of Template.betTeam is always draw and handicap is empty if handicap 2 = handicap 1.
        /// </summary>
        [Test]
        public void BuildBetTeam_BetTeamNotIs1AndNotIs2AndHandicap2EqualHandicap1_BetTeamAreDrawResourceAndaResource()
        {
            // Arrange
            _ticket.BetTeam = "bet team";
            _ticket.Handicap2 = 1;
            _ticket.Handicap1 = 1;

            // Act
            _choice.Render(_ticket, _ticketHelper, null, Arg.Any<bool>());

            // Assert
            var expectedBetTeam = string.Format("{0}&nbsp;<span class=\"underdog\">(0)</span>", CoreBetList.lblDraw);
            Assert.AreEqual(expectedBetTeam, _choice.Template.betTeam);
        }
    }
}