namespace Fanex.BetList.Core.UnitTest.Templates.Choice
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Choice_Match_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperChoice_Match_BlockTest : Choice_Match_Block
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
    /// The Unit testing for class Choice_Match_Block.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Choice_Match_BlockTest
    {
        private const string MatchTemplate = "<div class=\"match\"><span>{0}</span>{1}{2}<span>{3}</span>{4}</div>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var homeTeamName = "Luton Hats";
            var awayTeamName = "Milan Reds";
            var firstGoal = "first-goal";
            var lastGoal = "last-goal";
            var matchHtml = new StringBuilder();
            var block = new Choice_Match_Block();
            block.homeTeam = homeTeamName;
            block.home_firstGoal_lastGoal = firstGoal;
            block.awayTeam = awayTeamName;
            block.away_firstGoal_lastGoal = lastGoal;

            // Act
            block.ToString(ref matchHtml);

            // Assert
            var expectedHtml = string.Format(MatchTemplate, homeTeamName, firstGoal, block.VS.ToString(), awayTeamName, lastGoal);
            Assert.AreEqual(expectedHtml, matchHtml.ToString());
        }

        /// <summary>
        /// The block VS is null return html string with VS block is empty.
        /// </summary>
        [Test]
        public void ToString_BlockVSIsNull_ReturnHtmlStringWithVSBlockIsEmpty()
        {
            // Arrange
            var homeTeamName = "Luton Hats";
            var awayTeamName = "Milan Reds";
            var firstGoal = "first-goal";
            var lastGoal = "last-goal";
            var matchHtml = new StringBuilder();
            var block = new Choice_Match_Block();
            block.homeTeam = homeTeamName;
            block.home_firstGoal_lastGoal = firstGoal;
            block.awayTeam = awayTeamName;
            block.away_firstGoal_lastGoal = lastGoal;
            block.VS = null;

            // Act
            block.ToString(ref matchHtml);

            // Assert
            var expectedHtml = string.Format(MatchTemplate, homeTeamName, firstGoal, string.Empty, awayTeamName, lastGoal);
            Assert.AreEqual(expectedHtml, matchHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var matchHtml = new StringBuilder();
            var block = new Choice_Match_Block();
            block.Visible = false;

            // Act
            block.ToString(ref matchHtml);

            // Assert
            Assert.IsNullOrEmpty(matchHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var homeTeamName = "Luton Hats";
            var awayTeamName = "Milan Reds";
            var firstGoal = "first-goal";
            var lastGoal = "last-goal";
            var value = "Tennis";
            var matchHtml = new StringBuilder();
            var block = new WrapperChoice_Match_BlockTest();
            block.homeTeam = homeTeamName;
            block.home_firstGoal_lastGoal = firstGoal;
            block.awayTeam = awayTeamName;
            block.away_firstGoal_lastGoal = lastGoal;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref matchHtml);

            // Assert
            var expectedHtml = string.Format(MatchTemplate, homeTeamName, firstGoal, block.VS.ToString(), awayTeamName, lastGoal) + value;
            Assert.AreEqual(expectedHtml, matchHtml.ToString());
        }
    }
}