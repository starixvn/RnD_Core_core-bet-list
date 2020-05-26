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
        public void BuildLicenseeTxId_HasLicenseeTxId_ShowTxId()
        {
            // Arrange
            var txid = "1212124343434";
            var licensee = $"<div class=\"licenseeTxid\">TxID: {txid}</div>";
            _ticket.ExtraInfo = $"<p1 lt='{txid}'/>";

            // Act
            _trans.Render(_ticket);

            // Assert
            Assert.AreEqual(licensee, _trans.Template.LicenseeTxId.ToString());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("<p12 lt='jdjdjd'/>")]
        [TestCase("<p1 ab='jdjdjd'/>")]
        public void BuildLicenseeTxId_HasNotLicenseeTxId_HideTxId(string extraInfo)
        {
            // Arrange
            _ticket.ExtraInfo = extraInfo;

            // Act
            _trans.Render(_ticket);

            // Assert
            Assert.IsEmpty(_trans.Template.LicenseeTxId.Value);
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
            Assert.AreEqual("12/12/2012 12:12:12 PM", transTime);
        }

        [Test]
        public void RenderRefNo_Always_GetTransTimeBlock()
        {
            // Arrange
            _ticket.TransId = 1_243_432_423;

            // Act
            var refNo = _trans.RenderRefNo(_ticket);

            // Assert
            Assert.AreEqual("1243432423", refNo);
        }

        [Test]
        public void RenderRTF_Always_GetExpectedRTFText()
        {
            // Arrange
            _ticket.TransId = 1_243_432_423;
            _ticket.TransDate = new DateTime(2012, 12, 12, 12, 12, 12);

            // Act
            var excelText = _trans.RenderRTF(_ticket, null);

            // Assert
            Assert.AreEqual("Ref No: 1243432423\n12/12/2012 12:12:12 PM", excelText.ToString());
        }

        [Test]
        public void RenderRTF_HasNotLicenseeTxId_HideLicenseeTxIdText()
        {
            // Arrange
            _ticket.TransId = 1_243_432_423;
            _ticket.ExtraInfo = string.Empty;
            _ticket.TransDate = new DateTime(2012, 12, 12, 12, 12, 12);

            // Act
            var excelText = _trans.RenderRTF(_ticket, null);

            // Assert
            Assert.AreEqual("Ref No: 1243432423\n12/12/2012 12:12:12 PM", excelText.ToString());
        }

        [Test]
        public void RenderRTF_HasLicenseeTxId_ShowLicenseeTxIdText()
        {
            // Arrange
            _ticket.TransId = 1_243_432_423;
            _ticket.ExtraInfo = "<p1 lt=\"123456789\" />";
            _ticket.TransDate = new DateTime(2012, 12, 12, 12, 12, 12);

            // Act
            var excelText = _trans.RenderRTF(_ticket, null);

            // Assert
            Assert.AreEqual("TxID: 123456789\nRef No: 1243432423\n12/12/2012 12:12:12 PM", excelText.ToString());
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
            _ticket.TransId = 1_243_432_423;

            // Act
            var excelText = _trans.RenderRefNoRTF(_ticket, null);

            // Assert
            Assert.AreEqual("1243432423", excelText.ToString());
        }

        [Test]
        public void RenderLicenseeTxIdRTF_Always_GetExpectedRTFText()
        {
            // Arrange
            _ticket.ExtraInfo = "<p1 lt=\"232323\" />";

            // Act
            var excelText = _trans.RenderLicenseeTxIdRTF(_ticket, null);

            // Assert
            Assert.AreEqual("232323", excelText.ToString());
        }
    }
}