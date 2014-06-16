namespace Fanex.BetList.Core.UnitTest.Templates.SystemParlayDetail
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for SystemParlayDetail_TotalContents_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperSystemParlayDetail_TotalContents_Block : SystemParlayDetail_TotalContents_Block
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
    /// The Unit testing for class SystemParlayDetail_TotalContents_Block.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class SystemParlayDetail_TotalContents_BlockTest
    {
        private const string TotalContentsTemplate = "<tr class=\"system-parlay-total\"><td colspan=\"4\">Total:</td><td>{0}</td><td>&nbsp;</td></tr>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var totalWinloss = "20000";
            var totalContentsHtml = new StringBuilder();
            var block = new SystemParlayDetail_TotalContents_Block();
            block.TotalWinloss = totalWinloss;

            // Act
            block.ToString(ref totalContentsHtml);

            // Assert
            var expectedHtml = string.Format(TotalContentsTemplate, totalWinloss);
            Assert.AreEqual(expectedHtml, totalContentsHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var totalContentsHtml = new StringBuilder();
            var block = new SystemParlayDetail_TotalContents_Block();
            block.Visible = false;

            // Act
            block.ToString(ref totalContentsHtml);

            // Assert
            Assert.IsNullOrEmpty(totalContentsHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var totalWinloss = "20000";
            var totalContentsHtml = new StringBuilder();
            var value = "Tennis";
            var block = new WrapperSystemParlayDetail_TotalContents_Block();
            block.TotalWinloss = totalWinloss;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref totalContentsHtml);

            // Assert
            var expectedHtml = string.Format(TotalContentsTemplate, totalWinloss) + value;
            Assert.AreEqual(expectedHtml, totalContentsHtml.ToString());
        }
    }
}