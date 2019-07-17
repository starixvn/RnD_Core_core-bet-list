namespace Fanex.BetList.Core.UnitTest.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using Common;
    using Core.Builder.StatusBuilder;
    using Core.Constants;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Status33Test
    {
        private Status33 _status;
        private ITicket _ticket;
        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _status = new Status33();
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
            var ticketData = Substitute.For<List<ITicketData>>();
            var winTicketData = Substitute.For<ITicketData>();
            var placeTicketData = Substitute.For<ITicketData>();

            _ticket.TransId = 1000;
            _ticket.Status = status;

            winTicketData.BetTypeId = BetTypes.Win;
            winTicketData.RefNo = _ticket.TransId.ToString();
            winTicketData.Status = "draw";
            ticketData.Add(winTicketData);

            placeTicketData.BetTypeId = BetTypes.Place;
            placeTicketData.RefNo = _ticket.TransId.ToString();
            placeTicketData.Status = "draw";
            ticketData.Add(placeTicketData);

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
        public void WinStatus_ReturnResult(string status, string expectedResult)
        {
            // Arrange
            var ticketData = Substitute.For<List<ITicketData>>();
            var winTicketData = Substitute.For<ITicketData>();
            var placeTicketData = Substitute.For<ITicketData>();

            _ticket.TransId = 1000;
            _ticket.Status = "lose";

            winTicketData.BetTypeId = BetTypes.Win;
            winTicketData.RefNo = _ticket.TransId.ToString();
            winTicketData.Status = status;
            ticketData.Add(winTicketData);

            placeTicketData.BetTypeId = BetTypes.Place;
            placeTicketData.RefNo = _ticket.TransId.ToString();
            placeTicketData.Status = "draw";
            ticketData.Add(placeTicketData);

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
        public void PlaceStatus_ReturnResult(string status, string expectedResult)
        {
            // Arrange
            var ticketDatas = new List<ITicketData>();

            ticketDatas.Add(TestData.CreateTicketData(BetTypes.Win, _ticket.TransId.ToString(), "draw"));
            ticketDatas.Add(TestData.CreateTicketData(BetTypes.Place, _ticket.TransId.ToString(), status));
            _ticket.Status = "lose";

            // Act
            var template = _status.Render(_ticket, _ticketHelper, ticketDatas, false);

            // Assert
            var winStatus = "<span class='blue' style='font-weight:normal;'>Win: <b>Draw</b></span>";
            var placeStatus = string.Format("<span class='' style='font-weight:normal;'>Place: <b>{0}</b></span>", expectedResult);
            var expected = string.Format("{0}<br/>{1}", winStatus, placeStatus);

            Assert.AreEqual(expected, template.result);
        }
    }
}