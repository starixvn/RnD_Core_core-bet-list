namespace Fanex.BetList.RNGCore.Tests.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;
    using System;

    public class ChoiceRNGTests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;
        private readonly string bettypeRNG = "Bettype RNG";

        [SetUp]
        public void Setup()
        {
            _choice = new ChoiceRNG();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();

            _ticket.TransId = 1;
            _ticket.WinlostDate = DateTime.Today;
            _ticket.CustId = 2;
            _ticket.Status = BetStatus.Won;
            _ticket.BetTypeId = 1201;

            _ticketHelper.GetBetTypeNameById(_ticket.BetTypeId).Returns(bettypeRNG);
        }

        [Test]
        public void BuildBetTeam_Always_BeTeamStarstsWithHtmlContainingExpectedBetOption()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBetOptionHtml = $"<span class=\"handicap\">{bettypeRNG}</span>";

            StringAssert.StartsWith(expectedBetOptionHtml, template.betTeam);
        }

        [TestCase(null)]
        [TestCase("")]
        public void BuildBetTeam_TicketBetCheckIsNullOrEmpty_BeTeamOnlyContainsExpectedBetOption(string betCheck)
        {
            // Arrange.
            _ticket.BetCheck = betCheck;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedBetOptionHtml = $"<span class=\"handicap\">{bettypeRNG}</span>";

            Assert.AreEqual(expectedBetOptionHtml, template.betTeam);
        }

        [TestCase("BetCheck", "BetCheck")]
        [TestCase("BetCheck|&|displayBetCheck", "displayBetCheck")]
        public void BuildBetTeam_TicketBetCheckIsNotNullOrEmpty_BeTeamEndsWithBetCheck(string betCheck, string displayBetCheck)
        {
            // Arrange.
            _ticket.BetCheck = betCheck;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            StringAssert.EndsWith(betCheck, template.betTeam);
        }

        [Test]
        public void BuildLeague_Always_SetLeaugeNameIsNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.League.LeagueName);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetHandicapToNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.Null(template.Handicap);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameToUnderdog()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectedCssClassName = "underdog";

            Assert.AreEqual(expectedCssClassName, template.betTeamClassName);
        }

        [Test]
        public void BuildBetType_Always_BetTypeIsSetToNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.Null(template.BetType);
        }

        [Test]
        public void BuildMatch_Always_BetTypeIsSetToNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.Null(template.Match);
        }

        [Test]
        public void BuildScore_Always_BetTypeIsSetToNull()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.Null(template.Score);
        }
    }
}