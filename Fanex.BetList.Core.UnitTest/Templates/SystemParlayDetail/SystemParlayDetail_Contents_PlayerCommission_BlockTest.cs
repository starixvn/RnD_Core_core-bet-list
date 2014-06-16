namespace Fanex.BetList.Core.UnitTest.Templates.SystemParlayDetail
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for SystemParlayDetail_Contents_PlayerCommission_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperSystemParlayDetail_Contents_PlayerCommission_Block : SystemParlayDetail_Contents_PlayerCommission_Block
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
    /// The Unit testing for class SystemParlayDetail_Contents_PlayerCommission_Block.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class SystemParlayDetail_Contents_PlayerCommission_BlockTest
    {
        private const string ContentsPlayerCommissionTemplate = "<div class=\"player-commission\">{0}</div>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var playerCommission = "0.625";
            var contentsPlayerCommission = new StringBuilder();
            var block = new SystemParlayDetail_Contents_PlayerCommission_Block();
            block.PlayerCommission = playerCommission;

            // Act
            block.ToString(ref contentsPlayerCommission);

            // Assert
            var expectedHtml = string.Format(ContentsPlayerCommissionTemplate, playerCommission);
            Assert.AreEqual(expectedHtml, contentsPlayerCommission.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var contentsPlayerCommission = new StringBuilder();
            var block = new WrapperSystemParlayDetail_Contents_PlayerCommission_Block();
            block.Visible = false;

            // Act
            block.ToString(ref contentsPlayerCommission);

            // Assert
            Assert.IsNullOrEmpty(contentsPlayerCommission.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var playerCommission = "0.625";
            var contentsPlayerCommission = new StringBuilder();
            var value = "Tennis";
            var block = new WrapperSystemParlayDetail_Contents_PlayerCommission_Block();
            block.PlayerCommission = playerCommission;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref contentsPlayerCommission);

            // Assert
            var expectedHtml = string.Format(ContentsPlayerCommissionTemplate, playerCommission) + value;
            Assert.AreEqual(expectedHtml, contentsPlayerCommission.ToString());
        }
    }
}