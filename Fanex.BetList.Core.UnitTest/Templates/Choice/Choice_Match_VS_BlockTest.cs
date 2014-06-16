namespace Fanex.BetList.Core.UnitTest.Templates.Choice
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Choice_Match_VS_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperChoice_Match_VS_Block : Choice_Match_VS_Block
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
    /// The Unit testing for class Choice_Match_VS_Block.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Choice_Match_VS_BlockTest
    {
        private const string MatchVSTemplate = "<span>&nbsp;-&nbsp;vs&nbsp;-&nbsp;</span>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var versusHtml = new StringBuilder();
            var block = new Choice_Match_VS_Block();

            // Act
            block.ToString(ref versusHtml);

            // Assert
            Assert.AreEqual(MatchVSTemplate, versusHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var versusHtml = new StringBuilder();
            var block = new Choice_Match_VS_Block();
            block.Visible = false;

            // Act
            block.ToString(ref versusHtml);

            // Assert
            Assert.IsNullOrEmpty(versusHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var versusHtml = new StringBuilder();
            var value = "Tennis";
            var block = new WrapperChoice_Match_VS_Block();
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref versusHtml);

            // Assert
            var expectedHtml = MatchVSTemplate + value;
            Assert.AreEqual(expectedHtml, versusHtml.ToString());
        }
    }
}