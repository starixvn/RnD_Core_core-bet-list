namespace Fanex.BetList.Core.UnitTest.Templates.Stake
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Core.Templates;
    using NUnit.Framework;

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Stake_Bonus_Wallet_BlockWrapper : Stake_Bonus_Wallet_Block
    {
        public void SetAssigned(bool isAssigned)
        {
            IsAssigned = isAssigned;
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Stake_Bonus_Wallet_BlockTest
    {
        private const string BonusIconTemplate = "<span class=\"light-blue\">{0}</span>";

        [Test]
        public void ToString_BlockIsInvisible_ReturnEmptyString()
        {
            // Arrange
            var walletBlock = new Stake_Bonus_Wallet_Block();

            walletBlock.Visible = false;

            // Act
            var bonusIconHtml = new StringBuilder();

            walletBlock.ToString(ref bonusIconHtml);

            // Assert.
            Assert.IsEmpty(bonusIconHtml.ToString());
        }

        [Test]
        public void ToString_DoNothing_ReturnDefaultHtml()
        {
            // Arrange
            var walletBlock = new Stake_Bonus_Wallet_Block();

            // Act
            var bonusIconHtml = new StringBuilder();

            walletBlock.ToString(ref bonusIconHtml);

            // Assert.
            var type = "{type}";
            var expectedHtml = string.Format(BonusIconTemplate, type);

            Assert.AreEqual(expectedHtml, bonusIconHtml.ToString());
        }

        [Test]
        public void ToString_AssignBlockProperties_ReturnHtmlWithPassedData()
        {
            // Arrange
            var type = "type";
            var walletBlock = new Stake_Bonus_Wallet_Block();

            walletBlock.type = type;

            // Act
            var bonusIconHtml = new StringBuilder();

            walletBlock.ToString(ref bonusIconHtml);

            // Assert.
            var expectedHtml = string.Format(BonusIconTemplate, type);

            Assert.AreEqual(expectedHtml, bonusIconHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallAppend_ReturnHtmlWithAppendedText()
        {
            // Arrange
            var extendedValue = "extended";
            var walletBlock = new Stake_Bonus_Wallet_BlockWrapper();

            walletBlock.SetAssigned(true);
            walletBlock.Append(extendedValue);

            // Act
            var bonusIconHtml = new StringBuilder();

            walletBlock.ToString(ref bonusIconHtml);

            // Assert.
            var type = "{type}";
            var expectedHtml = string.Format(BonusIconTemplate, type) + extendedValue;

            Assert.AreEqual(expectedHtml, bonusIconHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallSetValue_ReturnSetText()
        {
            // Arrange
            var extendedValue = "extended";
            var walletBlock = new Stake_Bonus_Wallet_BlockWrapper();

            walletBlock.SetAssigned(true);
            walletBlock.SetValue(extendedValue);

            // Act
            var bonusIconHtml = new StringBuilder();

            walletBlock.ToString(ref bonusIconHtml);

            // Assert.
            var expectedHtml = extendedValue;

            Assert.AreEqual(expectedHtml, bonusIconHtml.ToString());
        }
    }
}