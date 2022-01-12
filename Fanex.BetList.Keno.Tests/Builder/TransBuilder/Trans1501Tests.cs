namespace Fanex.BetList.Keno.Tests.Builder.TransBuilder
{
    using Core.Builder.TransBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Trans1501Tests
    {
        private Trans1501 _trans;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _trans = new Trans1501();
            _ticket = Substitute.For<ITicket>();
        }

        [Test]
        public void Render_Always_RefNoIsTicketRefNo()
        {
            // Arrange.
            _ticket.RefNo = "100";

            // Act.
            var template = _trans.Render(_ticket);

            // Assert.
            Assert.AreEqual(_ticket.RefNo, template.TransTime.refNo);
        }

        [Test]
        public void BuildLicenseeTxId_HasLicenseeTxId_ShowTxId()
        {
            // Arrange
            var txid = "1212124343434";
            var licensee = $"<div class=\"licenseeTxid\">TxID: {txid}</div>";
            _ticket.ExtraInfo = $"<p8 lt='{txid}'/>";

            // Act
            _trans.Render(_ticket);

            // Assert
            Assert.AreEqual(licensee, _trans.Template.LicenseeTxId.ToString());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("<p99 lt='1123123'/>")]
        [TestCase("<p8 ab='jdjdjd'/>")]
        public void BuildLicenseeTxId_HasNotLicenseeTxId_HideTxId(string extraInfo)
        {
            // Arrange
            _ticket.ExtraInfo = extraInfo;

            // Act
            _trans.Render(_ticket);

            // Assert
            Assert.IsEmpty(_trans.Template.LicenseeTxId.Value);
        }
    }
}