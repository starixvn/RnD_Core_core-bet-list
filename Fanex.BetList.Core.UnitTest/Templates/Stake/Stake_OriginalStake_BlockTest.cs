namespace Fanex.BetList.Core.UnitTest.Templates.Stake
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Status_ShowIP_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperStake_OriginalStake_Block : Stake_OriginalStake_Block
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
    public class Stake_OriginalStake_BlockTest
    {
        private const string StakeTemplate = "<strike class=\"originalStake\">{0}</strike>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var originalStake = "10";
            var block = new Stake_OriginalStake_Block();
            block.stake = originalStake;

            // Act
            var stakeHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(StakeTemplate, originalStake);
            Assert.AreEqual(expectedHtml, stakeHtml);
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var block = new Stake_OriginalStake_Block();
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
            var block = new WrapperStake_OriginalStake_Block();
            block.stake = stake;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            var stakeHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(StakeTemplate, stake) + value;
            Assert.AreEqual(expectedHtml, stakeHtml);
        }
    }
}