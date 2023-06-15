namespace Fanex.BetList.Bitcoin.UnitTest.Builder.TransBuilder
{
    using Core.Builder.TransBuilder;
    using Fanex.BetList.Core.Entities;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// The Unit testing for Trans1 class.
    /// </summary>
    public class Trans7901Test
    {
        private Trans7901 _trans;
        private ITicket _ticket;

        [SetUp]
        public void Setup()
        {
            _trans = new Trans7901();
            _ticket = Substitute.For<ITicket>();
        }

        [Test]
        public void BuildLicenseeTxId_HasLicenseeTxId_ShowTxId()
        {
            // Arrange
            var txid = "1212124343434";
            var licensee = $"<div class=\"licenseeTxid\">LicTxID: {txid}</div>";
            _ticket.ExtraInfo = $"<p69 lt='{txid}'/>";

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