namespace Fanex.BetList.Core.UnitTest.Templates.Stake
{
    using System.Diagnostics.CodeAnalysis;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Stake_Template class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Stake_TemplateWrapper : Stake_Template
    {
        public void SetAssigned(bool isAssigned)
        {
            this.IsAssigned = isAssigned;
        }
    }

    /// <summary>
    /// The Unit testing for class Stake_Template.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Stake_TemplateTest
    {
        private const string StakeTemplate = "{0}<div class=\"stake\">{1}</div>{2}";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_ChangeBlockProperty_ReturnHtmlWithData()
        {
            // Arrange
            var stake = "10";
            var block = new Stake_Template();

            block.stake = stake;

            // Act
            var stakeHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(StakeTemplate, block.OriginalStake, stake, block.Bonus);
            Assert.AreEqual(expectedHtml, stakeHtml);
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsInvisible_ReturnEmptyString()
        {
            // Arrange
            var block = new Stake_Template();

            block.Visible = false;

            // Act
            var stakeHtml = block.ToString();

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(stakeHtml));
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AppendBlockWithText_ReturnHtmlWithAppendedValue()
        {
            // Arrange
            var stake = "10";
            var appendedText = "appendedText";
            var block = new Stake_TemplateWrapper();

            block.stake = stake;
            block.Append(appendedText);
            block.SetAssigned(true);

            // Act
            var stakeHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(StakeTemplate, block.OriginalStake, stake, block.Bonus) + appendedText;
            Assert.AreEqual(expectedHtml, stakeHtml);
        }

        [Test]
        public void ToString_WhenCallSetValue_ReturnNewValue()
        {
            // Arrange
            var newText = "appendedText";
            var block = new Stake_TemplateWrapper();

            block.SetValue(newText);
            block.SetAssigned(true);

            // Act
            var stakeHtml = block.ToString();

            // Assert
            Assert.AreEqual(newText, stakeHtml);
        }
    }
}