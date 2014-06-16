namespace Fanex.BetList.Core.UnitTest.Templates.Choice
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Choice_League_LeagueName_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperChoice_League_LeagueName_Block : Choice_League_LeagueName_Block
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
    /// The Unit testing for class Choice_League_LeagueName_Block.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Choice_League_LeagueName_BlockTest
    {
        private const string LeagueNameTemplate = "<span class=\"leagueName\">&nbsp;{0}</span>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var leagueName = "WTA - Australian Open (First Set - Game Winner)";
            var leagueNameHtml = new StringBuilder();
            var block = new Choice_League_LeagueName_Block();
            block.leagueName = leagueName;

            // Act
            block.ToString(ref leagueNameHtml);

            // Assert
            var expectedHtml = string.Format(LeagueNameTemplate, leagueName);
            Assert.AreEqual(expectedHtml, leagueNameHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var leagueNameHtml = new StringBuilder();
            var block = new Choice_League_LeagueName_Block();
            block.Visible = false;

            // Act
            block.ToString(ref leagueNameHtml);

            // Assert
            Assert.IsNullOrEmpty(leagueNameHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var leagueName = "WTA - Australian Open (First Set - Game Winner)";
            var value = "(First Set - Game Winner)";
            var leagueNameHtml = new StringBuilder();
            var block = new WrapperChoice_League_LeagueName_Block();
            block.leagueName = leagueName;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref leagueNameHtml);

            // Assert
            var expectedHtml = string.Format(LeagueNameTemplate, leagueName) + value;
            Assert.AreEqual(expectedHtml, leagueNameHtml.ToString());
        }
    }
}