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
            IsAssigned = isAssigned;
        }

        public void SetValueExtend(StringBuilder value)
        {
            Value = value;
        }
    }

    /// <summary>
    /// The Unit testing for class Status_Template.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Status_TemplateTest
    {
        private const string StatusTemplate = "<div class=\"status\">{0}</div>{1}{2}{3}";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var result = "void";
            var block = new Status_Template
            {
                result = result
            };

            // Act
            var statusHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(StatusTemplate, result, block.CashOutHistory, block.StatusResult, block.ShowIP);
            Assert.AreEqual(expectedHtml, statusHtml);
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var block = new Status_Template
            {
                Visible = false
            };

            // Act
            var statusHtml = block.ToString();

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(statusHtml));
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
            var block = new WrapperStatus_Template
            {
                result = result
            };
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            var statusHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(StatusTemplate, result, block.CashOutHistory, block.StatusResult, block.ShowIP) + value;
            Assert.AreEqual(expectedHtml, statusHtml);
        }
    }
}