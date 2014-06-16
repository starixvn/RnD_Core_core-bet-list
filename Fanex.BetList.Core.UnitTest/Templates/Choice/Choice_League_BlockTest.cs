namespace Fanex.BetList.Core.UnitTest.Templates.Choice
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Choice_League_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperChoice_League_Block : Choice_League_Block
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
    /// The Unit testing for class Choice_League_Block.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Choice_League_BlockTest
    {
        private const string LeagueTemplate = "<div class=\"league\"><span class=\"sport\">{0}</span>{1}</div>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var sportName = "Tennis";
            var leagueHtml = new StringBuilder();
            var block = new Choice_League_Block();
            block.sportTypeName = sportName;

            // Act
            block.ToString(ref leagueHtml);

            // Assert
            var expectedHtml = string.Format(LeagueTemplate, sportName, block.LeagueName.ToString());
            Assert.AreEqual(expectedHtml, leagueHtml.ToString());
        }

        /// <summary>
        /// The block league name is null return html string with league name block is empty.
        /// </summary>
        [Test]
        public void ToString_BlockLeagueNameIsNull_ReturnHtmlStringWithLeagueNameBlockIsEmpty()
        {
            // Arrange
            var sportName = "Tennis";
            var leagueHtml = new StringBuilder();
            var block = new Choice_League_Block();
            block.sportTypeName = sportName;
            block.LeagueName = null;

            // Act
            block.ToString(ref leagueHtml);

            // Assert
            var expectedHtml = string.Format(LeagueTemplate, sportName, string.Empty);
            Assert.AreEqual(expectedHtml, leagueHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var leagueHtml = new StringBuilder();
            var block = new Choice_League_Block();
            block.Visible = false;

            // Act
            block.ToString(ref leagueHtml);

            // Assert
            Assert.IsNullOrEmpty(leagueHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var sportName = "Tennis";
            var value = "Tennis";
            var leagueHtml = new StringBuilder();
            var block = new WrapperChoice_League_Block();
            block.sportTypeName = sportName;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref leagueHtml);

            // Assert
            var expectedHtml = string.Format(LeagueTemplate, sportName, block.LeagueName.ToString()) + value;
            Assert.AreEqual(expectedHtml, leagueHtml.ToString());
        }
    }
}