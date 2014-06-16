namespace Fanex.BetList.Core.UnitTest.Templates.SystemParlayDetail
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for SystemParlayDetail_Template class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperSystemParlayDetail_Template : SystemParlayDetail_Template
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
    /// The Unit testing for class SystemParlayDetail_Template.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2100:CodeLineMustNotBeLongerThan", Justification = "Reviewed.")]
    public class SystemParlayDetail_TemplateTest
    {
        private const string SystemParlayDetailTemplate = "<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" class=\"system-parlay-detail\" style=\"text-align: right;white-space: nowrap; width: 100%; font-weight: bold\"><tr class=\"system-parlay-header\"><th width=\"100\">Date</th><th>Event</th><th width=\"80\">Odds</th><th width=\"80\">Stake</th><th width=\"80\">Win Loss</th><th width=\"80\">Status</th></tr>{0}{1}{2}{3}</table>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var systemParlayDetailsHtml = string.Empty;
            var block = new SystemParlayDetail_Template();

            // Act
            systemParlayDetailsHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(SystemParlayDetailTemplate, block.Contents.ToString(), block.SubTotalContents.ToString(), block.Outstanding.ToString(), block.TotalContents.ToString());
            Assert.AreEqual(expectedHtml, systemParlayDetailsHtml);
        }

        /// <summary>
        /// The all child block is null return html string with all child block is empty.
        /// </summary>
        [Test]
        public void ToString_AllChildBlockIsNull_ReturnHtmlStringWithAllChildBlockIsEmpty()
        {
            // Arrange
            var systemParlayDetailsHtml = string.Empty;
            var block = new SystemParlayDetail_Template();
            block.Contents = null;
            block.SubTotalContents = null;
            block.Outstanding = null;
            block.TotalContents = null;

            // Act
            systemParlayDetailsHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(SystemParlayDetailTemplate, string.Empty, string.Empty, string.Empty, string.Empty);
            Assert.AreEqual(expectedHtml, systemParlayDetailsHtml);
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var systemParlayDetailsHtml = string.Empty;
            var block = new SystemParlayDetail_Template();
            block.Visible = false;

            // Act
            systemParlayDetailsHtml = block.ToString();

            // Assert
            Assert.IsNullOrEmpty(systemParlayDetailsHtml);
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var systemParlayDetailsHtml = string.Empty;
            var value = "Tennis";
            var block = new WrapperSystemParlayDetail_Template();
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            systemParlayDetailsHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(SystemParlayDetailTemplate, block.Contents.ToString(), block.SubTotalContents.ToString(), block.Outstanding.ToString(), block.TotalContents.ToString()) + value;
            Assert.AreEqual(expectedHtml, systemParlayDetailsHtml);
        }
    }
}