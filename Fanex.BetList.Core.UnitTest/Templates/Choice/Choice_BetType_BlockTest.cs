namespace Fanex.BetList.Core.UnitTest.Templates.Choice
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper of Choice_BetType_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperChoice_BetType_BlockTest : Choice_BetType_Block
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
    /// The test Choice_BetType_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Choice_BetType_BlockTest
    {
        private const string BetTypeTemplate = "<div class=\"bettype\"> {0}</div>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var betTypeName = "Handicap";
            var betTypeHtml = new StringBuilder();
            var block = new Choice_BetType_Block();
            block.betTypeName = betTypeName;

            // Act
            block.ToString(ref betTypeHtml);

            // Assert
            string expectedHtml = string.Format(BetTypeTemplate, betTypeName);
            Assert.AreEqual(expectedHtml, betTypeHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var betTypeHtml = new StringBuilder();
            var block = new Choice_BetType_Block();
            block.Visible = false;

            // Act
            block.ToString(ref betTypeHtml);

            // Assert
            Assert.IsNullOrEmpty(betTypeHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var betTypeName = "Handicap";
            var betTypeHtml = new StringBuilder();
            var value = "Tennis";
            var block = new WrapperChoice_BetType_BlockTest();
            block.betTypeName = betTypeName;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref betTypeHtml);

            // Assert
            var expectedHtml = string.Format(BetTypeTemplate, betTypeName) + value;
            Assert.AreEqual(expectedHtml, betTypeHtml.ToString());
        }
    }
}