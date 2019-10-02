namespace Fanex.BetList.Core.UnitTest.Templates.Stake
{
    using System.Text;
    using Core.Templates;
    using NUnit.Framework;

    public class Stake_Bonus_NormalBonus_Wallet_BlockWrapper : Stake_Bonus_NormalBonus_Wallet_Block
    {
        public void SetAssigned(bool isAssigned)
        {
            IsAssigned = isAssigned;
        }
    }

    public class Stake_Bonus_NormalBonus_Wallet_BlockTest
    {
        private const string BonusIconTemplate = "<span class=\"light-blue\">{0}</span>";

        [Test]
        public void ToString_BlockIsInvisible_ReturnEmptyString()
        {
            // Arrange.
            var walletBlock = new Stake_Bonus_NormalBonus_Wallet_Block();

            walletBlock.Visible = false;

            // Act.
            var walletHtml = new StringBuilder();

            walletBlock.ToString(ref walletHtml);

            // Assert.
            Assert.IsEmpty(walletHtml.ToString());
        }

        [Test]
        public void ToString_DoNothing_ReturnDefaultHtml()
        {
            // Arrange.
            var walletBlock = new Stake_Bonus_NormalBonus_Wallet_Block();

            // Act.
            var walletHtml = new StringBuilder();

            walletBlock.ToString(ref walletHtml);

            // Assert.
            const string type = "{type}";

            var expectedHtml = string.Format(BonusIconTemplate, type);

            Assert.AreEqual(expectedHtml, walletHtml.ToString());
        }

        [Test]
        public void ToString_AssignBlockProperties_ReturnHtmlWithPassedData()
        {
            // Arrange.
            const string type = "type";

            var walletBlock = new Stake_Bonus_NormalBonus_Wallet_Block();

            walletBlock.type = type;

            // Act.
            var walletHtml = new StringBuilder();

            walletBlock.ToString(ref walletHtml);

            // Assert.
            var expectedHtml = string.Format(BonusIconTemplate, type);

            Assert.AreEqual(expectedHtml, walletHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallAppend_ReturnHtmlWithAppendedText()
        {
            // Arrange.
            const string extendedValue = "extended";

            var walletBlock = new Stake_Bonus_NormalBonus_Wallet_BlockWrapper();

            walletBlock.SetAssigned(true);
            walletBlock.Append(extendedValue);

            // Act.
            var walletHtml = new StringBuilder();

            walletBlock.ToString(ref walletHtml);

            // Assert.
            const string type = "{type}";

            var expectedHtml = string.Format(BonusIconTemplate, type) + extendedValue;

            Assert.AreEqual(expectedHtml, walletHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallSetValue_ReturnSetText()
        {
            // Arrange.
            const string newText = "extended";

            var walletBlock = new Stake_Bonus_NormalBonus_Wallet_BlockWrapper();

            walletBlock.SetAssigned(true);
            walletBlock.SetValue(newText);

            // Act.
            var walletHtml = new StringBuilder();

            walletBlock.ToString(ref walletHtml);

            // Assert.
            Assert.AreEqual(newText, walletHtml.ToString());
        }
    }
}