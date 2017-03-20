namespace Fanex.BetList.Core.UnitTest.Templates.Stake
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Stake_Template class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperStake_Template : Stake_Template
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
    /// The Unit testing for class Stake_Template.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Stake_TemplateTest
    {
        private const string StakeTemplate = "{0}<div class=\"stake\">{1}</div>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var stake = "10";
            var block = new Stake_Template();
            block.stake = stake;

            // Act
            var stakeHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(StakeTemplate, block.OriginalStake, stake);
            Assert.AreEqual(expectedHtml, stakeHtml);
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
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
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var stake = "10";
            var value = "Tennis";
            var block = new WrapperStake_Template();
            block.stake = stake;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            var stakeHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(StakeTemplate, block.OriginalStake, stake) + value;
            Assert.AreEqual(expectedHtml, stakeHtml);
        }
    }
}