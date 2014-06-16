namespace Fanex.BetList.Core.UnitTest.Templates.Status
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Status_Template class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperStatus_Template : Status_Template
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
    /// The Unit testing for class Status_Template.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Status_TemplateTest
    {
        private const string StatusTemplate = "<div class=\"status\">{0}</div>{1}{2}";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var result = "void";
            var statusHtml = string.Empty;
            var block = new Status_Template();
            block.result = result;

            // Act
            statusHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(StatusTemplate, result, block.StatusResult.ToString(), block.ShowIP.ToString());
            Assert.AreEqual(expectedHtml, statusHtml);
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var statusHtml = string.Empty;
            var block = new Status_Template();
            block.Visible = false;

            // Act
            statusHtml = block.ToString();

            // Assert
            Assert.IsNullOrEmpty(statusHtml);
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var result = "void";
            var value = "Tennis";
            var statusHtml = string.Empty;
            var block = new WrapperStatus_Template();
            block.result = result;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            statusHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(StatusTemplate, result, block.StatusResult.ToString(), block.ShowIP.ToString()) + value;
            Assert.AreEqual(expectedHtml, statusHtml);
        }
    }
}