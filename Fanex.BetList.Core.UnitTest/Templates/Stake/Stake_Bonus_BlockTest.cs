namespace Fanex.BetList.Core.UnitTest.Templates.Stake
{
    using System.Text;
    using Core.Templates;
    using NUnit.Framework;

    public class Stake_Bonus_BlockWrapper : Stake_Bonus_Block
    {
        public void SetAssigned(bool isAssigned)
        {
            IsAssigned = isAssigned;
        }
    }

    public class Stake_Bonus_BlockTest
    {
        private const string BonusTemplate = "{0}{1}{2}{3}";

        [Test]
        public void ToString_BlockIsInvisible_ReturnEmptyString()
        {
            // Arrange
            var bonusBlock = new Stake_Bonus_Block();

            bonusBlock.Visible = false;

            // Act
            var bonusHtml = new StringBuilder();

            bonusBlock.ToString(ref bonusHtml);

            // Assert.
            Assert.IsEmpty(bonusHtml.ToString());
        }

        [Test]
        public void ToString_DoNothing_ReturnDefaultHtml()
        {
            // Arrange
            var bonusBlock = new Stake_Bonus_Block();

            // Act
            var bonusHtml = new StringBuilder();

            bonusBlock.ToString(ref bonusHtml);

            // Assert.
            var expectedHtml = string.Format(BonusTemplate, bonusBlock.NormalBonus, bonusBlock.RebateBonusIcon, bonusBlock.NonPeakHoursPromotionIcon, bonusBlock.GodOfFortune);

            Assert.AreEqual(expectedHtml, bonusHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallAppend_ReturnHtmlWithAppendedText()
        {
            // Arrange
            const string extendedValue = "extended";

            var bonusBlock = new Stake_Bonus_BlockWrapper();

            bonusBlock.SetAssigned(true);
            bonusBlock.Append(extendedValue);

            // Act
            var bonusHtml = new StringBuilder();

            bonusBlock.ToString(ref bonusHtml);

            // Assert.
            var originalGeneratedHtml = string.Format(BonusTemplate, bonusBlock.NormalBonus, bonusBlock.RebateBonusIcon, bonusBlock.NonPeakHoursPromotionIcon, bonusBlock.GodOfFortune);
            var expectedHtml = $"{originalGeneratedHtml}{extendedValue}";

            Assert.AreEqual(expectedHtml, bonusHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallSetValue_ReturnSetText()
        {
            // Arrange
            const string newText = "extended";

            var bonusBlock = new Stake_Bonus_BlockWrapper();

            bonusBlock.SetAssigned(true);
            bonusBlock.SetValue(newText);

            // Act
            var bonusHtml = new StringBuilder();

            bonusBlock.ToString(ref bonusHtml);

            // Assert.
            Assert.AreEqual(newText, bonusHtml.ToString());
        }
    }
}