namespace Fanex.BetList.VirtualSports.Tests.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using Core.App_GlobalResources;
    using Core.Builder.StatusBuilder;
    using Core.Entities;
    using Core.UnitTest.Common;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class Status1233Tests
    {
        private const int WinBetId = 1231;
        private const int PlaceBetId = 1232;
        private Status1233 _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status1233();
            _ticket = Substitute.For<ITicket>();
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCase("running", "Running")]
        [TestCase("waiting", "Waiting")]
        [TestCase("void", "Void")]
        [TestCase("reject", "Reject")]
        [TestCase("refund", "Refund")]
        public void BuildResult_Status_Result(string status, string expectedResult)
        {
            // Arrange
            var ticketData = new List<ITicketData>();
            ticketData.Add(TestData.CreateTicketData(WinBetId, _ticket.RefNo.ToString(), "draw"));
            ticketData.Add(TestData.CreateTicketData(PlaceBetId, _ticket.RefNo.ToString(), "draw"));
            _ticket.Status = status;

            // Act
            _status.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            Assert.AreEqual(expectedResult, _status.Template.result);
        }

        [TestCase("draw", "Draw")]
        [TestCase("lose", "Lose")]
        [TestCase("refund", "Refund")]
        [TestCase("reject", "Reject")]
        [TestCase("running", "Running")]
        [TestCase("void", "Void")]
        [TestCase("waiting", "Waiting")]
        [TestCase("won", "Won")]
        [TestCase("", "")]
        public void BuildResult_WinStatus_ReturnResult(string status, string expectedResult)
        {
            // Arrange
            var ticketData = new List<ITicketData>();
            ticketData.Add(TestData.CreateTicketData(WinBetId, _ticket.RefNo, status));
            ticketData.Add(TestData.CreateTicketData(PlaceBetId, _ticket.RefNo, "draw"));
            _ticket.Status = "lose";

            // Act
            _status.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            var winStatus = string.Format("<span class='blue' style='font-weight:normal;'>Win: <b>{0}</b></span>", expectedResult);
            var placeStatus = "<span class='' style='font-weight:normal;'>Place: <b>Draw</b></span>";
            var expected = string.Format("{0}<br/>{1}", winStatus, placeStatus);
            Assert.AreEqual(expected, _status.Template.result);
        }

        [TestCase("draw", "Draw")]
        [TestCase("lose", "Lose")]
        [TestCase("refund", "Refund")]
        [TestCase("reject", "Reject")]
        [TestCase("running", "Running")]
        [TestCase("void", "Void")]
        [TestCase("waiting", "Waiting")]
        [TestCase("won", "Won")]
        [TestCase("", "")]
        public void BuildResult_PlaceStatus_ReturnResult(string status, string expectedResult)
        {
            // Arrange
            var ticketData = new List<ITicketData>();
            ticketData.Add(TestData.CreateTicketData(WinBetId, _ticket.RefNo, "draw"));
            ticketData.Add(TestData.CreateTicketData(PlaceBetId, _ticket.RefNo, status));
            _ticket.Status = "lose";

            // Act
            _status.Render(_ticket, _ticketHelper, ticketData, false);

            // Assert
            var winStatus = "<span class='blue' style='font-weight:normal;'>Win: <b>Draw</b></span>";
            var placeStatus = string.Format("<span class='' style='font-weight:normal;'>Place: <b>{0}</b></span>", expectedResult);
            var expected = string.Format("{0}<br/>{1}", winStatus, placeStatus);
            Assert.AreEqual(expected, _status.Template.result);
        }

        [Test]
        public void RenderRTF_ResultIsNotWinLoseAndIPIsNotVisible_ShowResult()
        {
            // Arrange
            _ticket.Status = "lose";
            _ticketHelper.IsVnIp(Arg.Any<string>()).Returns(true);

            // Act
            var actualStatus = _status.RenderRTF(_ticket, _ticketHelper, null, false, new RTFHelper().Clone());

            // Assert
            Assert.AreEqual(CoreBetList.lose, actualStatus.ToString());
        }

        [Test]
        public void RenderRTF_ResultIsNotWinLoseAndIPIsVisible_ShowResult()
        {
            // Arrange
            _ticket.Status = "lose";
            _ticketHelper.IsVnIp(Arg.Any<string>()).Returns(false);

            // Act
            var actualStatus = _status.RenderRTF(_ticket, _ticketHelper, null, false, new RTFHelper().Clone());

            // Assert
            Assert.AreEqual($"{CoreBetList.lose}\n", actualStatus.ToString());
        }

        [Test]
        public void RenderRTF_ResultIsWinLoseAndIPIsVisible_ShowResult()
        {
            // Arrange
            var ticketData = new List<ITicketData>();
            var winTicketData = TestData.CreateTicketData(WinBetId, _ticket.RefNo, "draw");
            var placeTicketData = TestData.CreateTicketData(PlaceBetId, _ticket.RefNo, "draw");
            ticketData.Add(winTicketData);
            ticketData.Add(placeTicketData);
            _ticket.Status = "lose";
            _ticketHelper.IsVnIp(Arg.Any<string>()).Returns(true);

            // Act
            var actualStatus = _status.RenderRTF(_ticket, _ticketHelper, ticketData, false, new RTFHelper().Clone());

            // Assert
            Assert.AreEqual($"{CoreBetList.win}: Draw\n" + $"{CoreBetList.place}: Draw", actualStatus.ToString());
        }
    }
}