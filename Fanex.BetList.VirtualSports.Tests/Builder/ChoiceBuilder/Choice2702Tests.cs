namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using System;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2702Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2702();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = 2702;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("1", "HT.1")]
        [TestCase("x", "HT.X")]
        [TestCase("X", "HT.X")]
        [TestCase("2", "HT.2")]
        public void BuildTeamName_Always_RenderCorrectName(string betTeam, string expectedBetTeam)
        {
            // Arrange.
            _ticket.BetTeam = betTeam;

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            Assert.AreEqual(expectedBetTeam, builtChoice.betTeam);
        }

        [Test]
        public void BuildMatch_ShowTimeIsInValidFormat_AppendShowTimeInCorrectFormatRightAfterMatch()
        {
            // Arrange.
            _ticket.ShowTime = "June 2 2017 12:00AM";

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            string expectedHtmlOfKickOffTime = "<div class=\"match\">6/2/2017 12:00 AM</div>";
            Assert.IsTrue(builtChoice.Match.ToString().EndsWith(expectedHtmlOfKickOffTime, StringComparison.InvariantCulture));
        }

        [Test]
        public void BuildMatch_ShowTimeIsInInvalidFormat_AppendShowTimeAsWhatItHasRightAfterMatch()
        {
            // Arrange.
            _ticket.ShowTime = "Ju 2 17 12:00AM";

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            string expectedHtmlOfKickOffTime = "<div class=\"match\">Ju 2 17 12:00AM</div>";
            Assert.IsTrue(builtChoice.Match.ToString().EndsWith(expectedHtmlOfKickOffTime, StringComparison.InvariantCulture));
        }

        [Test]
        public void BuildBetTeamsInfo_ShowTimeIsInValidFormat_KickOffTimeInCorrectIsAtTheThirdRow()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.ShowTime = "June 2 2017 12:00AM";
            _ticketHelper.GetBetTypeNameById(Arg.Is(2702), Arg.Any<object>()).Returns("1H 1X2");

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            var expectedShowTime = "6/2/2017 12:00 AM";

            Assert.AreEqual(linesInActualText[2], expectedShowTime);
        }

        [Test]
        public void BuildBetTeamsInfo_ShowTimeIsInInValidFormat_KickOffTimeIsAtTheThirdRowAsWhatItHas()
        {
            // Arrange.
            var rtfHelper = new RTFHelper().Clone();

            _ticket.ShowTime = "Ju 2 17 12:00AM";
            _ticketHelper.GetBetTypeNameById(Arg.Is(2702), Arg.Any<object>()).Returns("1H 1X2");

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            var expectedShowTime = "Ju 2 17 12:00AM";

            Assert.AreEqual(linesInActualText[2], expectedShowTime);
        }
    }
}