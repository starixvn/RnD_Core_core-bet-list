namespace Fanex.BetList.Core.UnitTest.Templates.Stake
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Core.Templates;
    using NUnit.Framework;

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Stake_Bonus_BonusIcon_BlockWrapper : Stake_Bonus_BonusIcon_Block
    {
        public void SetAssigned(bool isAssigned)
        {
            IsAssigned = isAssigned;
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Stake_Bonus_BonusIcon_BlockTest
    {
        private const string BonusIconTemplate = "<span class=\"bonus-icon\" onclick=\"javascript:ShowBonusDetails({0},{1});\" title=\"{2}\"></span>";

        [Test]
        public void ToString_BlockIsInvisible_ReturnEmptyString()
        {
            // Arrange
            var bonusIconBlock = new Stake_Bonus_BonusIcon_Block();

            bonusIconBlock.Visible = false;

            // Act
            var bonusIconHtml = new StringBuilder();

            bonusIconBlock.ToString(ref bonusIconHtml);

            // Assert.
            Assert.IsEmpty(bonusIconHtml.ToString());
        }

        [Test]
        public void ToString_DoNothing_ReturnDefaultHtml()
        {
            // Arrange
            var bonusIconBlock = new Stake_Bonus_BonusIcon_Block();

            // Act
            var bonusIconHtml = new StringBuilder();

            bonusIconBlock.ToString(ref bonusIconHtml);

            // Assert.
            var refCode = "{refCode}";
            var transId = "{transId}";
            var bonusIconTitle = "{bonusIconTitle}";
            var expectedHtml = string.Format(BonusIconTemplate, refCode, transId, bonusIconTitle);

            Assert.AreEqual(expectedHtml, bonusIconHtml.ToString());
        }

        [Test]
        public void ToString_AssignBlockProperties_ReturnHtmlWithPassedData()
        {
            // Arrange
            var refCode = "refCode";
            var transId = "transId";
            var bonusIconTitle = "bonusIconTitle";
            var bonusIconBlock = new Stake_Bonus_BonusIcon_Block();

            bonusIconBlock.refCode = refCode;
            bonusIconBlock.transId = transId;
            bonusIconBlock.bonusIconTitle = bonusIconTitle;

            // Act
            var bonusIconHtml = new StringBuilder();

            bonusIconBlock.ToString(ref bonusIconHtml);

            // Assert.
            var expectedHtml = string.Format(BonusIconTemplate, refCode, transId, bonusIconTitle);

            Assert.AreEqual(expectedHtml, bonusIconHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallAppend_ReturnHtmlWithAppendedText()
        {
            // Arrange
            var extendedValue = "extended";
            var bonusIconBlock = new Stake_Bonus_BonusIcon_BlockWrapper();

            bonusIconBlock.SetAssigned(true);
            bonusIconBlock.Append(extendedValue);

            // Act
            var bonusIconHtml = new StringBuilder();

            bonusIconBlock.ToString(ref bonusIconHtml);

            // Assert.
            var refCode = "{refCode}";
            var transId = "{transId}";
            var bonusIconTitle = "{bonusIconTitle}";
            var expectedHtml = string.Format(BonusIconTemplate, refCode, transId, bonusIconTitle) + extendedValue;

            Assert.AreEqual(expectedHtml, bonusIconHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallSetValue_ReturnSetText()
        {
            // Arrange
            var extendedValue = "extended";
            var bonusIconBlock = new Stake_Bonus_BonusIcon_BlockWrapper();

            bonusIconBlock.SetAssigned(true);
            bonusIconBlock.SetValue(extendedValue);

            // Act
            var bonusIconHtml = new StringBuilder();

            bonusIconBlock.ToString(ref bonusIconHtml);

            // Assert.
            var expectedHtml = extendedValue;

            Assert.AreEqual(expectedHtml, bonusIconHtml.ToString());
        }
    }
}