
namespace Fanex.BetList.VGaming.Tests.Builder.ChoiceBuilder
{
    using System;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice10801Test
    {

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice10801();
            _ticket = TestData.CreateTicket(SABAxDBetTypes.LowestBetTypeIdInRange);
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketHelper.GetBetTypeNameById(SABAxDBetTypes.LowestBetTypeIdInRange).Returns("Game name 1");
        }

        
        [Test]
        public void BuildLeague_BetCheckIsEmpty_ShowDetailLink()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;
            var expectedJSFunction = "showSabaxDDetails";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsTrue(template.League.ToString().Contains(expectedJSFunction));
        }

        [Test]
        public void BuildLeague_BetCheckHasValue_HideDetailLink()
        {
            // Arrange
            _ticket.BetCheck = "1212";
            var expectedJSFunction = "showSabaxDDetails";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(template.League.ToString().Contains(expectedJSFunction));
        }
        

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Alway_NullAndUnderdogClass()
        {
            // Arrange
            var expectedClass = "underdog";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(_choice.Template.Handicap);
            Assert.AreEqual(expectedClass, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeam_IsSummaryTicket_NotContainBetcheck()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;
            _ticket.BetTypeId = SABAxDBetTypes.LowestBetTypeIdInRange;
            var expectedBetTeam = $"<span class=\"handicap\">Game name 1</span>";

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetTeam, template.betTeam);
        }

        [Test]
        public void BuildBetTeam_IsDetailTicket_ContainBetcheck()
        {
            // Arrange
            _ticket.BetCheck = "123456";
            _ticket.BetTypeId = SABAxDBetTypes.LowestBetTypeIdInRange;
            var expectedBetTeam = $"<span class=\"handicap\">Game name 1</span> {_ticket.BetCheck}";

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetTeam, template.betTeam);
        }


        [Test]
        public void BuildBetType_Alway_Null()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(_choice.Template.BetType);
        }

        [Test]
        public void BuildScore_Alway_Null()
        {
            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(_choice.Template.Score);
        }

        [Test]
        public void BuildMatch_Alway_Null()
        {
            // Arrange

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Match);
        }

        [Test]
        public void BuildSport_ShowSabaText_ReturnSabaName()
        {
            // Arrange
            _ticketHelper.HideSabaText = false;
            _ticketHelper.GetSportNameById(Arg.Any<int>()).Returns<string>("Saba sport");

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("Saba sport", _choice.Template.League.sportTypeName);
        }

        [Test]
        public void BuildSport_HideSabaText_RemoveSabaName()
        {
            // Arrange
            _ticketHelper.HideSabaText = true;
            _ticketHelper.GetSportNameById(Arg.Any<int>()).Returns<string>("Saba sport");
            _ticketHelper.GetResourceData("Sports_Saba", Arg.Any<string>()).Returns<string>("xxx sport");

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("xxx sport", _choice.Template.League.sportTypeName);
        }
    }
}
