namespace Fanex.BetList.Core.UnitTest.Templates.Stake
{
    using System.Text;
    using Core.Templates;
    using NUnit.Framework;

    public class Stake_Bonus_NormalBonus_BlockWrapper : Stake_Bonus_NormalBonus_Block
    {
        public void SetAssigned(bool isAssigned)
        {
            IsAssigned = isAssigned;
        }
    }

    public class Stake_Bonus_NormalBonus_BlockTest
    {
        private const string BonusTemplate = "{0}{1}";

        [Test]
        public void ToString_BlockIsInvisible_ReturnEmptyString()
        {
            // Arrange
            var normalBonusBlock = new Stake_Bonus_NormalBonus_Block();

            normalBonusBlock.Visible = false;

            // Act
            var normalBonusHtml = new StringBuilder();

            normalBonusBlock.ToString(ref normalBonusHtml);

            // Assert.
            Assert.IsEmpty(normalBonusHtml.ToString());
        }

        [Test]
        public void ToString_DoNothing_ReturnDefaultHtml()
        {
            // Arrange
            var normalBonusBlock = new Stake_Bonus_NormalBonus_Block();

            // Act
            var normalBonusHtml = new StringBuilder();

            normalBonusBlock.ToString(ref normalBonusHtml);

            // Assert.
            var expectedHtml = string.Format(BonusTemplate, normalBonusBlock.BonusIcon, normalBonusBlock.Wallet);

            Assert.AreEqual(expectedHtml, normalBonusHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallAppend_ReturnHtmlWithAppendedText()
        {
            // Arrange
            const string extendedValue = "extended";

            var normalBonusBlock = new Stake_Bonus_NormalBonus_BlockWrapper();

            normalBonusBlock.SetAssigned(true);
            normalBonusBlock.Append(extendedValue);

            // Act
            var normalBonusHtml = new StringBuilder();

            normalBonusBlock.ToString(ref normalBonusHtml);

            // Assert.
            var originalGeneratedHtml = string.Format(BonusTemplate, normalBonusBlock.BonusIcon, normalBonusBlock.Wallet);
            var expectedHtml = $"{originalGeneratedHtml}{extendedValue}";

            Assert.AreEqual(expectedHtml, normalBonusHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallSetValue_ReturnSetText()
        {
            // Arrange
            const string newText = "extended";

            var normalBonusBlock = new Stake_Bonus_NormalBonus_BlockWrapper();

            normalBonusBlock.SetAssigned(true);
            normalBonusBlock.SetValue(newText);

            // Act
            var normalBonusHtml = new StringBuilder();

            normalBonusBlock.ToString(ref normalBonusHtml);

            // Assert.
            Assert.AreEqual(newText, normalBonusHtml.ToString());
        }
    }
}