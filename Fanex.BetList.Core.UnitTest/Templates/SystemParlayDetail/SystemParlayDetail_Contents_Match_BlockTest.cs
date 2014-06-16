namespace Fanex.BetList.Core.UnitTest.Templates.SystemParlayDetail
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for SystemParlayDetail_Contents_Match_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperSystemParlayDetail_Contents_Match_Block : SystemParlayDetail_Contents_Match_Block
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
    /// The Unit testing for SystemParlayDetail_Contents_Match_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class SystemParlayDetail_Contents_Match_BlockTest
    {
        private const string ContentsMatchTemplate = "<div style=\"padding: 3px 0px\">{0} - vs - {1}</div>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var homeTeam = "Portland Trail Blazers";
            var awayTeam = "Denver Nuggets";
            var contentsMatchHtml = new StringBuilder();
            var block = new SystemParlayDetail_Contents_Match_Block();
            block.HomeTeam = homeTeam;
            block.AwayTeam = awayTeam;

            // Act
            block.ToString(ref contentsMatchHtml);

            // Assert
            var expectedHtml = string.Format(ContentsMatchTemplate, homeTeam, awayTeam);
            Assert.AreEqual(expectedHtml, contentsMatchHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var contentsMatchHtml = new StringBuilder();
            var block = new SystemParlayDetail_Contents_Match_Block();
            block.Visible = false;

            // Act
            block.ToString(ref contentsMatchHtml);

            // Assert
            Assert.IsNullOrEmpty(contentsMatchHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var homeTeam = "Portland Trail Blazers";
            var awayTeam = "Denver Nuggets";
            var contentsMatchHtml = new StringBuilder();
            var value = "Tennis";
            var block = new WrapperSystemParlayDetail_Contents_Match_Block();
            block.HomeTeam = homeTeam;
            block.AwayTeam = awayTeam;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref contentsMatchHtml);

            // Assert
            var expectedHtml = string.Format(ContentsMatchTemplate, homeTeam, awayTeam) + value;
            Assert.AreEqual(expectedHtml, contentsMatchHtml.ToString());
        }
    }
}