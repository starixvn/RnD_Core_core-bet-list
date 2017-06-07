namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using System;
    using Core.App_GlobalResources;
    using Core.Builder.ChoiceBuilder;
    using Core.Constants;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Choice2707Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice2707();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = 2707;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("0:0")]
        [TestCase("1:0")]
        [TestCase("0:1")]
        [TestCase("2:0")]
        [TestCase("0:2")]
        [TestCase("3:0")]
        [TestCase("0:3")]
        [TestCase("2:1")]
        [TestCase("1:2")]
        [TestCase("3:1")]
        [TestCase("3:1")]
        [TestCase("2:2")]
        [TestCase("3:2")]
        [TestCase("2:3")]
        [TestCase("3:3")]
        public void BuildBetTeam_BetOptionIsOneOfSupportedList_ShowBetChoiceExactlyAsWhatItIs(string betOption)
        {
            // Arrange.
            _ticket.BetTeam = betOption;

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedText = HtmlCharacters.NoneBreakingSpace + betOption;

            Assert.AreEqual(expectedText, builtChoice.betTeam);
        }

        [TestCase("aos")]
        [TestCase("AOS")]
        public void BuildBetTeam_BetOptionIsAOS_BetChoiceIsAOS(string betOption)
        {
            // Arrange.
            _ticket.BetTeam = betOption;

            // Act.
            var builtChoice = _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert.
            var expectedText = HtmlCharacters.NoneBreakingSpace + CoreBetList.AOS;

            Assert.AreEqual(expectedText, builtChoice.betTeam);
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
            _ticketHelper.GetBetTypeNameById(Arg.Is(2707), Arg.Any<object>()).Returns("Correct Score");

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
            _ticketHelper.GetBetTypeNameById(Arg.Is(2707), Arg.Any<object>()).Returns("Correct Score");

            // Act
            var actualText = _choice.RenderRTF(_ticket, _ticketHelper, null, false, rtfHelper);
            var linesInActualText = actualText[0].String.Split('\n');

            // Assert
            var expectedShowTime = "Ju 2 17 12:00AM";

            Assert.AreEqual(linesInActualText[2], expectedShowTime);
        }
    }
}