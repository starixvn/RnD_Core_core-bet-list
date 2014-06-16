namespace Fanex.BetList.Core.UnitTest.Templates.Odds
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Odds_Template class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperOdds_Template : Odds_Template
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
    /// The Unit testing for class Odds_Template.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Odds_TemplateTest
    {
        private const string OddsTemplate = "<span class=\"underdog\">{0}</span><br /><span class=\"oddstype\">{1}</span>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var odds = "1";
            var oddType = "MY";
            var oddsHtml = string.Empty;
            var block = new Odds_Template();
            block.odds = odds;
            block.oddsType = oddType;

            // Act
            oddsHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(OddsTemplate, odds, oddType);
            Assert.AreEqual(expectedHtml, oddsHtml);
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var oddsHtml = string.Empty;
            var block = new Odds_Template();
            block.Visible = false;

            // Act
            oddsHtml = block.ToString();

            // Assert
            Assert.IsNullOrEmpty(oddsHtml);
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var odds = "1";
            var oddType = "MY";
            var value = "Tennis";
            var oddsHtml = string.Empty;
            var block = new WrapperOdds_Template();
            block.odds = odds;
            block.oddsType = oddType;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            oddsHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(OddsTemplate, odds, oddType) + value;
            Assert.AreEqual(expectedHtml, oddsHtml);
        }
    }
}