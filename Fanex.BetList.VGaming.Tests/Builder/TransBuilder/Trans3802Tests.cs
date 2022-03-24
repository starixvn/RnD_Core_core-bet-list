using System;
using Fanex.BetList.Core.Builder.TransBuilder;
using Fanex.BetList.Core.Entities;
using NSubstitute;
using NUnit.Framework;

namespace Fanex.BetList.VGaming.Tests.Builder.TransBuilder
{
    public class Trans3802Tests
    {
        private Trans3802 _trans;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _trans = new Trans3802();
            _ticket = Substitute.For<ITicket>();
        }

        [Test]
        public void BuildLicenseeTxId_HasLicenseeTxId_ShowTxId()
        {
            // Arrange
            var txid = "1212124343434";
            var licensee = $"<div class=\"licenseeTxid\">TxID: {txid}</div>";
            _ticket.MatchCode = $"JPType=0*lt={txid}";

            // Act
            _trans.Render(_ticket);

            // Assert
            Assert.AreEqual(licensee, _trans.Template.LicenseeTxId.ToString());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("ab=jdjdjd")]
        public void BuildLicenseeTxId_HasNotLicenseeTxId_HideTxId(string extraInfo)
        {
            // Arrange
            _ticket.MatchCode = extraInfo;

            // Act
            _trans.Render(_ticket);

            // Assert
            Assert.IsEmpty(_trans.Template.LicenseeTxId.Value);
        }

        [Test]
        public void RenderRTF_HasNotLicenseeTxId_HideLicenseeTxIdText()
        {
            // Arrange
            _ticket.TransId = 1_243_432_423;
            _ticket.MatchCode = string.Empty;
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
            _ticket.MatchCode = "lt=123456789";
            _ticket.TransDate = new DateTime(2012, 12, 12, 12, 12, 12);

            // Act
            var excelText = _trans.RenderRTF(_ticket, null);

            // Assert
            Assert.AreEqual("TxID: 123456789\nRef No: 1243432423\n12/12/2012 12:12:12 PM", excelText.ToString());
        }
    }
}
