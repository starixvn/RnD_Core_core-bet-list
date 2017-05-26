namespace Fanex.BetList.Core.UnitTest.Templates.Stake
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Core.Templates;
    using NUnit.Framework;

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Stake_Bonus_BlockWrapper : Stake_Bonus_Block
    {
        public void SetAssigned(bool isAssigned)
        {
            IsAssigned = isAssigned;
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Stake_Bonus_BlockTest
    {
        private const string BonusIconTemplate = "{0}{1}";

        [Test]
        public void ToString_BlockIsInvisible_ReturnEmptyString()
        {
            // Arrange
            var bonusBlock = new Stake_Bonus_Block();

            bonusBlock.Visible = false;

            // Act
            var bonusIconHtml = new StringBuilder();

            bonusBlock.ToString(ref bonusIconHtml);

            // Assert.
            Assert.IsEmpty(bonusIconHtml.ToString());
        }

        [Test]
        public void ToString_DoNothing_ReturnDefaultHtml()
        {
            // Arrange
            var bonusBlock = new Stake_Bonus_Block();

            // Act
            var bonusIconHtml = new StringBuilder();

            bonusBlock.ToString(ref bonusIconHtml);

            // Assert.
            var expectedHtml = string.Format(BonusIconTemplate, bonusBlock.BonusIcon, bonusBlock.Wallet);

            Assert.AreEqual(expectedHtml, bonusIconHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallAppend_ReturnHtmlWithAppendedText()
        {
            // Arrange
            var extendedValue = "extended";
            var bonusBlock = new Stake_Bonus_BlockWrapper();

            bonusBlock.SetAssigned(true);
            bonusBlock.Append(extendedValue);

            // Act
            var bonusIconHtml = new StringBuilder();

            bonusBlock.ToString(ref bonusIconHtml);

            // Assert.
            var expectedHtml = string.Format(BonusIconTemplate, bonusBlock.BonusIcon, bonusBlock.Wallet) + extendedValue;

            Assert.AreEqual(expectedHtml, bonusIconHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallSetValue_ReturnSetText()
        {
            // Arrange
            var extendedValue = "extended";
            var bonusBlock = new Stake_Bonus_BlockWrapper();

            bonusBlock.SetAssigned(true);
            bonusBlock.SetValue(extendedValue);

            // Act
            var bonusIconHtml = new StringBuilder();

            bonusBlock.ToString(ref bonusIconHtml);

            // Assert.
            var expectedHtml = extendedValue;

            Assert.AreEqual(expectedHtml, bonusIconHtml.ToString());
        }
    }
}