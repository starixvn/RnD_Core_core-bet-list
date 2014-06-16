namespace Fanex.BetList.Core.UnitTest.Templates.SystemParlayDetail
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for SystemParlayDetail_Outstanding_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperSystemParlayDetail_Outstanding_Block : SystemParlayDetail_Outstanding_Block
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
    /// The Unit testing for class SystemParlayDetail_Outstanding_Block.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2100:CodeLineMustNotBeLongerThan", Justification = "Reviewed.")]
    public class SystemParlayDetail_Outstanding_BlockTest
    {
        private const string OutstandingTemplate = "<tr class=\"system-parlay-outstanding\"><td colspan=\"4\">Outstanding:</td><td style=\"border-left: none;\">{0}</td><td style=\"border-left: none;\">&nbsp;</td></tr>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var outstanding = "2";
            var outstandingHtml = new StringBuilder();
            var block = new SystemParlayDetail_Outstanding_Block();
            block.Outstanding = outstanding;

            // Act
            block.ToString(ref outstandingHtml);

            // Assert
            var expectedHtml = string.Format(OutstandingTemplate, outstanding);
            Assert.AreEqual(expectedHtml, outstandingHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var outstandingHtml = new StringBuilder();
            var block = new SystemParlayDetail_Outstanding_Block();
            block.Visible = false;

            // Act
            block.ToString(ref outstandingHtml);

            // Assert
            Assert.IsNullOrEmpty(outstandingHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var outstanding = "2";
            var outstandingHtml = new StringBuilder();
            var value = "Tennis";
            var block = new WrapperSystemParlayDetail_Outstanding_Block();
            block.Outstanding = outstanding;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref outstandingHtml);

            // Assert
            var expectedHtml = string.Format(OutstandingTemplate, outstanding) + value;
            Assert.AreEqual(expectedHtml, outstandingHtml.ToString());
        }
    }
}