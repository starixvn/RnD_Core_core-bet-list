namespace Fanex.BetList.Bitcoin.Tests.Builder.ChoiceBuilder
{
    using System;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice7901Tests
    {

        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice7901();
            _ticket = Substitute.For<ITicket>();
            _ticket.MatchId = 1000;
            _ticket.EventDate = new DateTime(2021, 8, 13, 13, 00, 00);
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("o")]
        [TestCase("O")]
        public void BuildBetTeam_OverTicket_ReturnOver(string betteam)
        {
            // Arrange
            _ticket.BetTeam = betteam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("Over", template.betTeam);
        }

        [TestCase("u")]
        [TestCase("U")]
        public void BuildBetTeam_UnderTicket_ReturnUnder(string betteam)
        {
            // Arrange
            _ticket.BetTeam = betteam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("Under", template.betTeam);
        }

        [TestCase("o")]
        [TestCase("O")]
        public void BuildBetTeamClassNameAndHandicap_OverTicket_ReturnFavoriteClass(string betteam)
        {
            // Arrange
            _ticket.BetTeam = betteam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("favorite", template.betTeamClassName);
        }

        [TestCase("u")]
        [TestCase("U")]
        public void BuildBetTeamClassNameAndHandicap_UnderTicket_ReturnUnderdogClass(string betteam)
        {
            // Arrange
            _ticket.BetTeam = betteam;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("underdog", template.betTeamClassName);
        }

        [Test]
        public void BuildScore_Always_ReturnEmpty()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Score);
        }

        [Test]
        public void BuildLeague_Always_ReturnEmpty()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.League.LeagueName);
        }

        [Test]
        public void BuildBetTeamInfo_BreakDonwn_RenderForExcel()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            Assert.AreEqual($"No. 1000", linesInActualText[1]);
            Assert.AreEqual("8/13/2021 1:00:00 PM", linesInActualText[2]);
        }

    }
}