
namespace Fanex.BetList.SabaClub.Tests.Builder.ChoiceBuilder
{
    using System;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice1612Test
    {

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1612();
            _ticket = TestData.CreateTicket(SabaClubBetTypes.MinRange);
            _ticketHelper = Substitute.For<ITicketHelper>();
            _ticketHelper.GetBetTypeNameById(SabaClubBetTypes.MinRange).Returns("Game name 1");
        }

        [Test]
        public void BuildLeague_DetailTicketHasJackpotPrize_ShowJackpotDetailLink()
        {
            // Arrange
            _ticket.JackpotInfo = "jp=1;jpw=xxxxxxxxxxx";
            _ticket.BetCheck = "betcheck";
            _ticket.CustId = 123;
            _ticket.BetTypeId = SabaClubBetTypes.FourGuardians;
            _ticket.WinlostDate = new DateTime(2022, 8, 24);
            var expectedJSFunction = "showSabaClubJackpotWonDetails('xxxxxxxxxxx', '123', '1640', '08/24/2022 00:00:00')";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            StringAssert.Contains(expectedJSFunction, template.League.ToString());
        }
        [Test]
        public void BuildLeague_BetCheckIsEmpty_ShowDetailLink()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;
            var expectedJSFunction = "showSabaClubDetails";

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
            var expectedJSFunction = "showSabaClubDetails";

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsFalse(template.League.ToString().Contains(expectedJSFunction));
        }

        [TestCase("")]
        [TestCase("121")]
        public void BuildLeague_Always_SetLeagueNameIsNull(string betCheck)
        {
            // Arrange
            _ticket.BetCheck = betCheck;

            // Act
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            Assert.IsNull(template.League.LeagueName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Alway_NullAndUnderdogClass()
        {
            // Arrange
            var expectedClass = "underdog";

            // Act.
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(_choice.Template.Handicap.handicap);
            Assert.AreEqual(expectedClass, _choice.Template.betTeamClassName);
        }

        [Test]
        public void BuildBetTeam_IsSummaryTicket_NotContainBetcheck()
        {
            // Arrange
            _ticket.BetCheck = string.Empty;
            _ticket.BetTypeId = SabaClubBetTypes.MinRange;
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
            _ticket.BetTypeId = SabaClubBetTypes.MinRange;
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

        public void BuildMatch_Alway_Null()
        {
            // Arrange

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Match);
        }
    }
}
