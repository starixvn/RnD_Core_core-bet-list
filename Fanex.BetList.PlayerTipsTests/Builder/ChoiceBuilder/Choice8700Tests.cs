namespace Fanex.BetList.PlayerTips.Tests.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;
    using System;

    public class Choice8700Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice8700();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildBetTeam_DontHaveP74Tag_ShowEmpty()
        {
            // Arrange
            _ticket.ExtraInfo = string.Empty;

            // Act.
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(act.betTeam);
        }

        [Test]
        public void BuildBetTeam_DontHaveStreamingSrc_ShowEmpty()
        {
            // Arrange
            _ticket.ExtraInfo = "<p74 m=\"123456\" h=\"456\" a=\"895\" l=\"489\" lt=\"10000000\"/>";

            // Act.
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(act.betTeam);
        }

        [Test]
        public void BuildBetTeam_StreamingSrcIsEmpty_ShowEmpty()
        {
            // Arrange
            _ticket.ExtraInfo = "<p74 m=\"123456\" ss=\"\" h=\"456\" a=\"895\" l=\"489\" lt=\"10000000\"/>";

            // Act.
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsEmpty(act.betTeam);
        }

        [Test]
        public void BuildBetTeam_ValidData_ShowStreamingSrc()
        {
            // Arrange
            _ticket.ExtraInfo = "<p74 m=\"123456\" ss=\"22 - (TH)\" h=\"456\" a=\"895\" l=\"489\" lt=\"10000000\"/>";

            // Act.
            var act = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("22 - (TH)", act.betTeam);
        }

        [Test]
        public void BuildBetTeamClassNameAndHandicap_Always_SetBetTeamClassNameToUnderdog()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            const string expectedCssClassName = "favorite";

            Assert.AreEqual(expectedCssClassName, template.betTeamClassName);
        }

        [Test]
        public void BuildScore_Always_Hide()
        {
            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsNull(template.Score);
        }

        [Test]
        public void BuildEventKickoffDateTime_Always_ShowEventDate()
        {
            // Arrange
            var today = new DateTime(2022, 11, 18, 14, 18, 0);
            _ticket.EventDate = today;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual("11/18/2022 02:18 PM", template.EventDate.eventDate);
        }

        [Test]
        public void BuildEventKickoffDateTime_Always_Show()
        {
            // Arrange
            var today = new DateTime(2022, 11, 18, 14, 18, 0);
            _ticket.EventDate = today;

            // Act.
            var template = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.IsTrue(template.EventDate.Visible);
        }
    }
}