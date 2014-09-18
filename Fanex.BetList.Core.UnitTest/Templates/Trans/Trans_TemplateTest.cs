namespace Fanex.BetList.Core.UnitTest.Templates.Trans
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Trans_TransTime_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperTrans_Template : Trans_Template
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
    /// The Unit testing for class Trans_TransTime_Block.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Trans_TemplateTest
    {
        private const string TransTemplate = "{0}<div class=\"time\">{1}</div>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var transTime = "11/20/2013 6:54:00 AM";
            var transHtml = string.Empty;
            var block = new Trans_Template();
            block.transTime = transTime;

            // Act
            transHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(TransTemplate, block.TransTime.ToString(), transTime);
            Assert.AreEqual(expectedHtml, transHtml);
        }

        /// <summary>
        /// The all child block is null return html string with all child block is empty.
        /// </summary>
        [Test]
        public void ToString_AllChildBlockIsNull_ReturnHtmlStringWithAllChildBlockIsEmpty()
        {
            // Arrange
            var transTime = "11/20/2013 6:54:00 AM";
            var transHtml = string.Empty;
            var block = new Trans_Template();
            block.transTime = transTime;
            block.TransTime = null;

            // Act
            transHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(TransTemplate, string.Empty, transTime);
            Assert.AreEqual(expectedHtml, transHtml);
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var transHtml = string.Empty;
            var block = new Trans_Template();
            block.Visible = false;

            // Act
            transHtml = block.ToString();

            // Assert
            Assert.IsNullOrEmpty(transHtml);
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var transTime = "11/20/2013 6:54:00 AM";
            var transHtml = string.Empty;
            var value = "Tennis";
            var block = new WrapperTrans_Template();
            block.transTime = transTime;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            transHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(TransTemplate, block.TransTime.ToString(), transTime) + value;
            Assert.AreEqual(expectedHtml, transHtml);
        }
    }
}