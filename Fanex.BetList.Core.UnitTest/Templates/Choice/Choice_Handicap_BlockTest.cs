namespace Fanex.BetList.Core.UnitTest.Templates.Choice
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Choice_Handicap_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperChoice_Handicap_Block : Choice_Handicap_Block
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
    /// The Unit testing for class Choice_Handicap_Block.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Choice_Handicap_BlockTest
    {
        private const string HandicapTemplate = "<span class='handicap'> {0}{1}</span>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var handicap = "1-2";
            var odds = "1";
            var handicapHtml = new StringBuilder();
            var block = new Choice_Handicap_Block();
            block.handicap = handicap;
            block.Odds.odds = odds;

            // Act
            block.ToString(ref handicapHtml);

            // Assert
            var expectedHtml = string.Format(HandicapTemplate, handicap, block.Odds.ToString());
            Assert.AreEqual(expectedHtml, handicapHtml.ToString());
        }

        /// <summary>
        /// The block Odds is null return html string with Odds block is empty.
        /// </summary>
        [Test]
        public void ToString_BlockOddsIsNull_ReturnHtmlStringWithOddsBlockIsEmpty()
        {
            // Arrange
            var handicap = "1-2";
            var handicapHtml = new StringBuilder();
            var block = new Choice_Handicap_Block();
            block.handicap = handicap;
            block.Odds = null;

            // Act
            block.ToString(ref handicapHtml);

            // Assert
            var expectedHtml = string.Format(HandicapTemplate, handicap, string.Empty);
            Assert.AreEqual(expectedHtml, handicapHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var handicapHtml = new StringBuilder();
            var block = new Choice_Handicap_Block();
            block.Visible = false;

            // Act
            block.ToString(ref handicapHtml);

            // Assert
            Assert.IsNullOrEmpty(handicapHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var handicap = "1-2";
            var odds = "1";
            var value = "Tennis";
            var handicapHtml = new StringBuilder();
            var block = new WrapperChoice_Handicap_Block();
            block.handicap = handicap;
            block.Odds.odds = odds;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref handicapHtml);

            // Assert
            var expectedHtml = string.Format(HandicapTemplate, handicap, block.Odds.ToString()) + value;
            Assert.AreEqual(expectedHtml, handicapHtml.ToString());
        }
    }
}