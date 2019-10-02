namespace Fanex.BetList.Core.UnitTest.Templates.Status
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Status_ShowIP_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperStatus_CashOutHistory_Block : Status_CashOutHistory_Block
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
    /// The Unit testing for class Status_ShowIP_Block.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Status_CashOutHistory_BlockTest
    {
        private const string StatusTemplate = "<div class=\"result cashout-history\" onclick=\"ViewCashOutHistory({0}, {1}, {2}, {3})\">Details</div>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlStringDefaultValue()
        {
            // Arrange
            var transId = "{transId}";
            var refNo = "{refNo}";
            var betTypeId = "{betType}";
            var betdaqId = "{betdaqId}";
            var statusHtml = new StringBuilder();
            var block = new Status_CashOutHistory_Block();

            // Act
            block.ToString(ref statusHtml);

            // Assert
            var expectedHtml = string.Format(StatusTemplate, transId, refNo, betTypeId, betdaqId);
            Assert.AreEqual(expectedHtml, statusHtml.ToString());
        }

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var transId = "1";
            var refNo = "2";
            var betTypeId = 3;
            var betdaqId = 1;
            var statusHtml = new StringBuilder();
            var block = new Status_CashOutHistory_Block();
            block.transId = transId;
            block.refNo = refNo;
            block.betType = betTypeId.ToString();
            block.betdaqId = betdaqId.ToString();

            // Act
            block.ToString(ref statusHtml);

            // Assert
            var expectedHtml = string.Format(StatusTemplate, transId, refNo, betTypeId, betdaqId);
            Assert.AreEqual(expectedHtml, statusHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsInvisible_ReturnEmptyString()
        {
            // Arrange
            var statusHtml = new StringBuilder();
            var block = new Status_CashOutHistory_Block();
            block.Visible = false;

            // Act
            block.ToString(ref statusHtml);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(statusHtml.ToString()));
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var transId = "{transId}";
            var refNo = "{refNo}";
            var betTypeId = "{betType}";
            var betdaqId = "{betdaqId}";
            var value = "Tennis";
            var statusHtml = new StringBuilder();
            var block = new WrapperStatus_CashOutHistory_Block();
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref statusHtml);

            // Assert
            var expectedHtml = string.Format(StatusTemplate, transId, refNo, betTypeId, betdaqId) + value;
            Assert.AreEqual(expectedHtml, statusHtml.ToString());
        }
    }
}