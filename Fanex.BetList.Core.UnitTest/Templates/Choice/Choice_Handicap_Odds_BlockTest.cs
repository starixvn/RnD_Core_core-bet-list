namespace Fanex.BetList.Core.UnitTest.Templates.Choice
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Choice_Handicap_Odds_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperChoice_Handicap_Odds_Block : Choice_Handicap_Odds_Block
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
    /// The Unit testing for class Choice_Handicap_Odds_Block.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Choice_Handicap_Odds_BlockTest
    {
        private const string BetTeamOddsHandicap = "<span class=\"odds\"> @ {0}</span>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var odds = "1";
            var oddsHtml = new StringBuilder();
            var block = new Choice_Handicap_Odds_Block();
            block.odds = odds;

            // Act
            block.ToString(ref oddsHtml);

            // Assert
            var expectedHtml = string.Format(BetTeamOddsHandicap, odds);
            Assert.AreEqual(expectedHtml, oddsHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var oddsHtml = new StringBuilder();
            var block = new Choice_Handicap_Odds_Block();
            block.Visible = false;

            // Act
            block.ToString(ref oddsHtml);

            // Assert
            Assert.IsNullOrEmpty(oddsHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var odds = "1";
            var value = "Tennis";
            var oddsHtml = new StringBuilder();
            var block = new WrapperChoice_Handicap_Odds_Block();
            block.odds = odds;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref oddsHtml);

            // Assert
            var expectedHtml = string.Format(BetTeamOddsHandicap, odds) + value;
            Assert.AreEqual(expectedHtml, oddsHtml.ToString());
        }
    }
}