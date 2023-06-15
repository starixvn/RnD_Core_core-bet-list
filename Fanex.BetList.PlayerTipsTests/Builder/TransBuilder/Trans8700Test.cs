namespace Fanex.BetList.PlayerTips.UnitTest.Builder.TransBuilder
{
    using Core.Builder.TransBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class Trans8700Test
    {
        private Trans8700 _trans;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _trans = new Trans8700();
            _ticket = Substitute.For<ITicket>();
        }

        [Test]
        public void BuildLicenseeTxId_HasLicenseeTxId_ShowTxId()
        {
            // Arrange
            var txid = "1212124343434";
            var licensee = $"<div class=\"licenseeTxid\">LicTxID: {txid}</div>";
            _ticket.ExtraInfo = $"<p74 lt='{txid}'/>";

            // Act
            _trans.Render(_ticket);

            // Assert
            Assert.AreEqual(licensee, _trans.Template.LicenseeTxId.ToString());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("<p12 lt='jdjdjd'/>")]
        [TestCase("<p69 ab='jdjdjd'/>")]
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