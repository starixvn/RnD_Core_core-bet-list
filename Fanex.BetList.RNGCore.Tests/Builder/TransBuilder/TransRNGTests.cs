namespace Fanex.BetList.RNGCore.Tests.Builder.TransBuilder
{
    using Fanex.BetList.Core.Builder.TransBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class TransRNGTests
    {
        private TransRNG _trans;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _trans = new TransRNG();
            _ticket = Substitute.For<ITicket>();
        }

        [Test]
        public void BuildLicenseeTxId_HasLicenseeTxId_ShowTxId()
        {
            // Arrange
            var txid = "1212124343434";
            var licensee = $"<div class=\"licenseeTxid\">LicTxID: {txid}</div>";
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
    }
}
