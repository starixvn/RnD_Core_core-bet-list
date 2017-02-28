namespace Fanex.BetList.VirtualSports.Tests.Builder.ChoiceBuilder
{
    using System;
    using Core.Builder.ChoiceBuilder;
    using Core.Entities;
    using Core.UnitTest.Common.Enums;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for Choice1220 class.
    /// </summary>
    [TestFixture]
    public class Choice1203Tests
    {
        private IChoice _choice;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _choice = new Choice1203();
            _ticket = Substitute.For<ITicket>();
            _ticket.BetTypeId = BetTypes.VT_PlayerWin;
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [Test]
        public void BuildMatch_ShowTimeIsValidFormat_AddShowTimeBeFormatted()
        {
            // Arrange
            _ticket.ShowTime = "01/01/2014 15:25";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var kickOffTimeFormat = "<div class=\"match\">{0}</div>";
            var expectedShowTime = DateTime.Parse(_ticket.ShowTime).ToString("M/d/yyyy hh:mm tt");
            Assert.IsTrue(_choice.Template.Match.ToString().Contains(string.Format(kickOffTimeFormat, expectedShowTime)));
        }

        [Test]
        public void BuildMatch_ShowTimeIsInvalidFormat_AddShowTime()
        {
            // Arrange
            _ticket.ShowTime = "1/1--14";

            // Act
            _choice.Render(_ticket, _ticketHelper, null, false);

            // Assert
            var kickOffTimeFormat = "<div class=\"match\">{0}</div>";
            Assert.IsTrue(_choice.Template.Match.ToString().Contains(string.Format(kickOffTimeFormat, _ticket.ShowTime)));
        }

        [Test]
        public void BuildBetTeamsInfo_Always_RenderKickOffTime()
        {
            // Arrange
            _ticket.ShowTime = "01/01/2014 15:25";

            // Act
            var actual = _choice.RenderRTF(_ticket, _ticketHelper, null, false, new RTFHelper().Clone());

            // Assert
            var expectedShowTime = DateTime.Parse(_ticket.ShowTime).ToString("M/d/yyyy hh:mm tt");
            Assert.IsTrue(actual[0].ToString().Contains(expectedShowTime));
        }
    }
}