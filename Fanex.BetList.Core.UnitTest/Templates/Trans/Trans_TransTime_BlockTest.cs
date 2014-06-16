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
    public class WrapperTrans_TransTime_Block : Trans_TransTime_Block
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
    public class Trans_TransTime_BlockTest
    {
        private const string TransTimeTemplate = "Ref No: {0}";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var refNo = "1000000";
            var transTimeHtml = new StringBuilder();
            var block = new Trans_TransTime_Block();
            block.refNo = refNo;

            // Act
            block.ToString(ref transTimeHtml);

            // Assert
            var expectedHtml = string.Format(TransTimeTemplate, refNo);
            Assert.AreEqual(expectedHtml, transTimeHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var transTimeHtml = new StringBuilder();
            var block = new Trans_TransTime_Block();
            block.Visible = false;

            // Act
            block.ToString(ref transTimeHtml);

            // Assert
            Assert.IsNullOrEmpty(transTimeHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var refNo = "1000000";
            var transTimeHtml = new StringBuilder();
            var value = "Tennis";
            var block = new WrapperTrans_TransTime_Block();
            block.refNo = refNo;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref transTimeHtml);

            // Assert
            var expectedHtml = string.Format(TransTimeTemplate, refNo) + value;
            Assert.AreEqual(expectedHtml, transTimeHtml.ToString());
        }
    }
}