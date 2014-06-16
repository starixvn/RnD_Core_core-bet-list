namespace Fanex.BetList.Core.UnitTest.Templates.SystemParlayDetail
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for SystemParlayDetail_SubTotalContents_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperSystemParlayDetail_SubTotalContents_Block : SystemParlayDetail_SubTotalContents_Block
    {
        public void SetAssigned(bool isAssigned)
        {
            this.IsAssigned = isAssigned;
        }

        public void SetValueExtend(StringBuilder value)
        {
            this.Value = value;
        }
    }

    /// <summary>
    /// The Unit testing for class SystemParlayDetail_SubTotalContents_Block.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2100:CodeLineMustNotBeLongerThan", Justification = "Reviewed.")]
    public class SystemParlayDetail_SubTotalContents_BlockTest
    {
        private const string SubTotalContentsTemplate = "<tr class=\"system-parlay-subtotal\"><td colspan=\"4\">Sub Total:</td><td style=\"border-left: none;\">{0}</td><td style=\"border-left: none;\">&nbsp;</td></tr>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var totalWinloss = "20000";
            var subTotalContentsHtml = new StringBuilder();
            var block = new SystemParlayDetail_SubTotalContents_Block();
            block.TotalWinloss = totalWinloss;

            // Act
            block.ToString(ref subTotalContentsHtml);

            // Assert
            var expectedHtml = string.Format(SubTotalContentsTemplate, totalWinloss);
            Assert.AreEqual(expectedHtml, subTotalContentsHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var subTotalContentsHtml = new StringBuilder();
            var block = new SystemParlayDetail_SubTotalContents_Block();
            block.Visible = false;

            // Act
            block.ToString(ref subTotalContentsHtml);

            // Assert
            Assert.IsNullOrEmpty(subTotalContentsHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var totalWinloss = "20000";
            var subTotalContentsHtml = new StringBuilder();
            var value = "Tennis";
            var block = new WrapperSystemParlayDetail_SubTotalContents_Block();
            block.TotalWinloss = totalWinloss;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref subTotalContentsHtml);

            // Assert
            var expectedHtml = string.Format(SubTotalContentsTemplate, totalWinloss) + value;
            Assert.AreEqual(expectedHtml, subTotalContentsHtml.ToString());
        }
    }
}