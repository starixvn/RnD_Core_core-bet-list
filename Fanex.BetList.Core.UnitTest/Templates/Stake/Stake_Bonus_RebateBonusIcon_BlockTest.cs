namespace Fanex.BetList.Core.UnitTest.Templates.Stake
{
    using System.Text;
    using Core.Templates;
    using NUnit.Framework;

    public class Stake_Bonus_RebateBonusIcon_BlockWrapper : Stake_Bonus_RebateBonusIcon_Block
    {
        public void SetAssigned(bool isAssigned)
        {
            IsAssigned = isAssigned;
        }
    }

    public class Stake_Bonus_RebateBonusIcon_BlockTest
    {
        private static readonly string RebateBonusIconTemplate = $"<div class=\"rebate-bonus\"><span class=\"rebate-bonus-icon\"></span>{App_GlobalResources.CoreBetList.Rebate}</div>";

        [Test]
        public void ToString_BlockIsInvisible_ReturnEmptyString()
        {
            // Arrange.
            var rebateBonusIconBlock = new Stake_Bonus_RebateBonusIcon_Block();

            rebateBonusIconBlock.Visible = false;

            // Act.
            var rebateBonusIconHtml = new StringBuilder();

            rebateBonusIconBlock.ToString(ref rebateBonusIconHtml);

            // Assert.
            Assert.IsEmpty(rebateBonusIconHtml.ToString());
        }

        [Test]
        public void ToString_DoNothing_ReturnDefaultHtml()
        {
            // Arrange.
            var rebateBonusIconBlock = new Stake_Bonus_RebateBonusIcon_Block();

            // Act.
            var rebateBonusIconHtml = new StringBuilder();

            rebateBonusIconBlock.ToString(ref rebateBonusIconHtml);

            // Assert.
            Assert.AreEqual(RebateBonusIconTemplate, rebateBonusIconHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallAppend_ReturnHtmlWithAppendedText()
        {
            // Arrange.
            const string extendedValue = "extended";

            var rebateBonusIconBlock = new Stake_Bonus_RebateBonusIcon_BlockWrapper();

            rebateBonusIconBlock.SetAssigned(true);
            rebateBonusIconBlock.Append(extendedValue);

            // Act.
            var rebateBonusIconHtml = new StringBuilder();

            rebateBonusIconBlock.ToString(ref rebateBonusIconHtml);

            // Assert.
            var expectedHtml = RebateBonusIconTemplate + extendedValue;

            Assert.AreEqual(expectedHtml, rebateBonusIconHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallSetValue_ReturnSetText()
        {
            // Arrange.
            const string newText = "extended";

            var rebateBonusIconBlock = new Stake_Bonus_RebateBonusIcon_BlockWrapper();

            rebateBonusIconBlock.SetAssigned(true);
            rebateBonusIconBlock.SetValue(newText);

            // Act.
            var rebateBonusIconHtml = new StringBuilder();

            rebateBonusIconBlock.ToString(ref rebateBonusIconHtml);

            // Assert.
            Assert.AreEqual(newText, rebateBonusIconBlock.ToString());
        }
    }
}