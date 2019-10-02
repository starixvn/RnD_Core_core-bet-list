namespace Fanex.BetList.Core.UnitTest.Templates.Stake
{
    using System.Text;
    using Core.Templates;
    using NUnit.Framework;

    public class Stake_Bonus_NonPeakHoursPromotionIcon_BlockWrapper : Stake_Bonus_NonPeakHoursPromotionIcon_Block
    {
        public void SetAssigned(bool isAssigned)
        {
            IsAssigned = isAssigned;
        }
    }

    public class Stake_Bonus_NonPeakHoursPromotionIcon_BlockTest
    {
        private static readonly string NonPeakHoursPromotionIconTemplate = $"<div class=\"nph-promotion\"><span class=\"nph-promotion-icon\"></span>{App_GlobalResources.CoreBetList.Promotion}</div>";

        [Test]
        public void ToString_BlockIsInvisible_ReturnEmptyString()
        {
            // Arrange.
            var nonPeakHoursPromotionIconBlock = new Stake_Bonus_NonPeakHoursPromotionIcon_Block();

            nonPeakHoursPromotionIconBlock.Visible = false;

            // Act.
            var nonPeakHoursPromotionIconHtml = new StringBuilder();

            nonPeakHoursPromotionIconBlock.ToString(ref nonPeakHoursPromotionIconHtml);

            // Assert.
            Assert.IsEmpty(nonPeakHoursPromotionIconHtml.ToString());
        }

        [Test]
        public void ToString_DoNothing_ReturnDefaultHtml()
        {
            // Arrange.
            var nonPeakHoursPromotionIconBlock = new Stake_Bonus_NonPeakHoursPromotionIcon_Block();

            // Act.
            var nonPeakHoursPromotionIconHtml = new StringBuilder();

            nonPeakHoursPromotionIconBlock.ToString(ref nonPeakHoursPromotionIconHtml);

            // Assert.
            Assert.AreEqual(NonPeakHoursPromotionIconTemplate, nonPeakHoursPromotionIconHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallAppend_ReturnHtmlWithAppendedText()
        {
            // Arrange.
            const string extendedValue = "extended";

            var nonPeakHoursPromotionIconBlock = new Stake_Bonus_NonPeakHoursPromotionIcon_BlockWrapper();

            nonPeakHoursPromotionIconBlock.SetAssigned(true);
            nonPeakHoursPromotionIconBlock.Append(extendedValue);

            // Act.
            var nonPeakHoursPromotionIconHtml = new StringBuilder();

            nonPeakHoursPromotionIconBlock.ToString(ref nonPeakHoursPromotionIconHtml);

            // Assert.
            var expectedHtml = NonPeakHoursPromotionIconTemplate + extendedValue;

            Assert.AreEqual(expectedHtml, nonPeakHoursPromotionIconHtml.ToString());
        }

        [Test]
        public void ToString_WhenCallSetValue_ReturnSetText()
        {
            // Arrange.
            const string newText = "extended";

            var nonPeakHoursPromotionIconBlock = new Stake_Bonus_NonPeakHoursPromotionIcon_BlockWrapper();

            nonPeakHoursPromotionIconBlock.SetAssigned(true);
            nonPeakHoursPromotionIconBlock.SetValue(newText);

            // Act.
            var nonPeakHoursPromotionIconHtml = new StringBuilder();

            nonPeakHoursPromotionIconBlock.ToString(ref nonPeakHoursPromotionIconHtml);

            // Assert.
            Assert.AreEqual(newText, nonPeakHoursPromotionIconBlock.ToString());
        }
    }
}