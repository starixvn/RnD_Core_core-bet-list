namespace Fanex.BetList.Core.UnitTest.Templates.Choice
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Choice_Score_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperChoice_Score_Block : Choice_Score_Block
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
    /// The Unit testing for class Choice_Score_Block.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Choice_Score_BlockTest
    {
        private const string ScoreTemplate = "<span class=\"favorite\"> [{0}-{1}]</span>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var homeScore = "1";
            var awayScore = "2";
            var scoreHtml = new StringBuilder();
            var block = new Choice_Score_Block();
            block.homeScore = homeScore;
            block.awayScore = awayScore;

            // Act
            block.ToString(ref scoreHtml);

            // Assert
            var expectedHtml = string.Format(ScoreTemplate, homeScore, awayScore);
            Assert.AreEqual(expectedHtml, scoreHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var scoreHtml = new StringBuilder();
            var block = new Choice_Score_Block();
            block.Visible = false;

            // Act
            block.ToString(ref scoreHtml);

            // Assert
            Assert.IsNullOrEmpty(scoreHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var homeScore = "1";
            var awayScore = "2";
            var value = "Tennis";
            var scoreHtml = new StringBuilder();
            var block = new WrapperChoice_Score_Block();
            block.homeScore = homeScore;
            block.awayScore = awayScore;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref scoreHtml);

            // Assert
            var expectedHtml = string.Format(ScoreTemplate, homeScore, awayScore) + value;
            Assert.AreEqual(expectedHtml, scoreHtml.ToString());
        }
    }
}