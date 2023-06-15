namespace Fanex.BetList.NumberGame.Tests.Builder.TransBuilder
{
    using Core.Builder.TransBuilder;
    using Core.Entities;
    using NSubstitute;
    using NUnit.Framework;
    public class Trans81Tests
    {
        private Trans81 _trans;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _trans = new Trans81();
            _ticket = Substitute.For<ITicket>();
        }

        [Test]
        public void BuildLicenseeTxId_HasLicenseeTxId_ShowTxId()
        {
            // Arrange
            var txid = "1212124343434";
            var licensee = $"<div class=\"licenseeTxid\">LicTxID: {txid}</div>";
            _ticket.ExtraInfo = $"<p3 lt='{txid}'/>";

            // Act
            _trans.Render(_ticket);

            // Assert
            Assert.AreEqual(licensee, _trans.Template.LicenseeTxId.ToString());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("<p99 lt='123123'/>")]
        [TestCase("<p3 ab='jdjdjd'/>")]
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
