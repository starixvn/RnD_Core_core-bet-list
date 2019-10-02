namespace Fanex.BetList.Core.UnitTest.Templates.Stake
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Core.Templates;
    using NUnit.Framework;

    public class Stake_Bonus_NormalBonus_BonusIcon_BlockWrapper : Stake_Bonus_NormalBonus_BonusIcon_Block
    {
        public void SetAssigned(bool isAssigned)
        {
            IsAssigned = isAssigned;
        }
    }

    public class Stake_Bonus_NormalBonus_BonusIcon_BlockTest
    {
        private const string BonusIconTemplate = "<span class=\"bonus-icon\" onclick=\"javascript:ShowBonusDetails({0},{1});\" title=\"{2}\"></span>";

        [Test]
        public void ToString_BlockIsInvisible_ReturnEmptyString()
        {
            // Arrange.
            var bonusIconBlock = new Stake_Bonus_NormalBonus_BonusIcon_Block();

            bonusIconBlock.Visible = false;

            // Act.
            var bonusIconHtml = new StringBuilder();

            bonusIconBlock.ToString(ref bonusIconHtml);

            // Assert.
            Assert.IsEmpty(bonusIconHtml.ToString());
        }

        [Test]
        public void ToString_DoNothing_ReturnDefaultHtml()
        {
            // Arrange.
            var bonusIconBlock = new Stake_Bonus_NormalBonus_BonusIcon_Block();

            // Act.
            var bonusIconHtml = new StringBuilder();

            bonusIconBlock.ToString(ref bonusIconHtml);

            // Assert.
            const string refCode = "{refCode}";
            const string transId = "{transId}";
            const string bonusIconTitle = "{bonusIconTitle}";

            var expectedHtml = string.Format(BonusIconTemplate, refCode, transId, bonusIconTitle);

            Assert.AreEqual(expectedHtml, bonusIconHtml.ToString());
        }

        [Test]
        public void ToString_AssignBlockProperties_ReturnHtmlWithPassedData()
        {
            // Arrange.
            const string refCode = "refCode";
            const string transId = "transId";
            const string bonusIconTitle = "bonusIconTitle";

            var bonusIconBlock = new Stake_Bonus_NormalBonus_BonusIcon_Block();

            bonusIconBlock.refCode = refCode;
            bonusIconBlock.transId = transId;
            bonusIconBlock.bonusIconTitle = bonusIconTitle;

            // Act.
            var bonusIconHtml = new StringBuilder();

            bonusIconBlock.ToString(ref bonusIconHtml);

            // Assert.
            var expectedHtml = string.Format(BonusIconTemplate, refCode, transId, bonusIconTitle);

            Assert.AreEqual(expectedHtml, bonusIconHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallAppend_ReturnHtmlWithAppendedText()
        {
            // Arrange.
            const string extendedValue = "extended";

            var bonusIconBlock = new Stake_Bonus_NormalBonus_BonusIcon_BlockWrapper();

            bonusIconBlock.SetAssigned(true);
            bonusIconBlock.Append(extendedValue);

            // Act.
            var bonusIconHtml = new StringBuilder();

            bonusIconBlock.ToString(ref bonusIconHtml);

            // Assert.
            const string refCode = "{refCode}";
            const string transId = "{transId}";
            const string bonusIconTitle = "{bonusIconTitle}";

            var expectedHtml = string.Format(BonusIconTemplate, refCode, transId, bonusIconTitle) + extendedValue;

            Assert.AreEqual(expectedHtml, bonusIconHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallSetValue_ReturnSetText()
        {
            // Arrange.
            const string newText = "extended";

            var bonusIconBlock = new Stake_Bonus_NormalBonus_BonusIcon_BlockWrapper();

            bonusIconBlock.SetAssigned(true);
            bonusIconBlock.SetValue(newText);

            // Act.
            var bonusIconHtml = new StringBuilder();

            bonusIconBlock.ToString(ref bonusIconHtml);

            // Assert.
            Assert.AreEqual(newText, bonusIconHtml.ToString());
        }
    }
}