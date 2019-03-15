namespace Fanex.BetList.Core.UnitTest.Builder.TransBuilder
{
    using System;
    using Core.Builder.TransBuilder;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// The Unit testing for Trans1 class.
    /// </summary>
    public class Trans1Test
    {
        private Trans1 _trans;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _trans = new Trans1();
            _ticket = Substitute.For<ITicket>();
        }

        /// <summary>
        /// Builds the trans time always set trans time is trans date.
        /// </summary>
        [Test]
        public void BuildTransTime_Always_SetTransTimeIsTransDate()
        {
            // Arrange
            _ticket.TransDate = new DateTime(2012, 12, 12, 12, 12, 12);

            // Act
            _trans.Render(_ticket);

            // Assert
            const string TRANS_TIME = "12/12/2012 12:12:12 PM";
            Assert.AreEqual(TRANS_TIME, _trans.Template.transTime);
        }

        [Test]
        public void BuildRefNo_Always_SetRefNoIsTranId()
        {
            // Arrange
            _ticket.TransId = 100;

            // Act
            _trans.Render(_ticket);

            // Assert
            Assert.AreEqual(_ticket.TransId.ToString(), _trans.Template.TransTime.refNo);
        }

        [Test]
        public void RenderTranstime_Always_GetTransTimeBlock()
        {
            // Arrange
            _ticket.TransDate = new DateTime(2012, 12, 12, 12, 12, 12);

            // Act
            var transTime = _trans.RenderTranstime(_ticket);

            // Assert
            Assert.AreEqual("<div class=\"time\">12/12/2012 12:12:12 PM</div>", transTime);
        }

        [Test]
        public void RenderRefNo_Always_GetTransTimeBlock()
        {
            // Arrange
            _ticket.TransId = 1243432423;

            // Act
            var refNo = _trans.RenderRefNo(_ticket);

            // Assert
            Assert.AreEqual("1243432423", refNo);
        }

        [Test]
        public void RenderEventDate_Always_GetEventDate()
        {
            // Arrange
            _ticket.EventDate = new DateTime(2012, 12, 12, 12, 12, 12);

            // Act
            var eventDate = _trans.RenderEventDate(_ticket);

            // Assert
            Assert.AreEqual("<div class=\"event-date\">12/12/2012 12:12 PM</div>", eventDate);
        }

        [Test]
        public void RenderRTF_Always_GetExpectedRTFText()
        {
            // Arrange
            _ticket.TransId = 1243432423;
            _ticket.TransDate = new DateTime(2012, 12, 12, 12, 12, 12);

            // Act
            var excelText = _trans.RenderRTF(_ticket, null);

            // Assert
            Assert.AreEqual("Ref No: 1243432423\n12/12/2012 12:12:12 PM", excelText.ToString());
        }

        [Test]
        public void RenderTransTimeRTF_Always_GetExpectedRTFText()
        {
            // Arrange
            _ticket.TransDate = new DateTime(2012, 12, 12, 12, 12, 12);

            // Act
            var excelText = _trans.RenderTransTimeRTF(_ticket, null);

            // Assert
            Assert.AreEqual("12/12/2012 12:12:12 PM", excelText.ToString());
        }

        [Test]
        public void RenderRefNoRTF_Always_GetExpectedRTFText()
        {
            // Arrange
            _ticket.TransId = 1243432423;

            // Act
            var excelText = _trans.RenderRefNoRTF(_ticket, null);

            // Assert
            Assert.AreEqual("1243432423", excelText.ToString());
        }
    }
}