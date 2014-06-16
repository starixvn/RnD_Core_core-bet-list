namespace Fanex.BetList.Core.UnitTest.Templates.SystemParlayDetail
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for SYSTEMPARLAYDETAIL_CONTENTS_WINLOSSDATE_BLOCK class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperSystemParlayDetail_Contents_WinlossDate_Block : SystemParlayDetail_Contents_WinlossDate_Block
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
    /// The Unit testing for class SYSTEMPARLAYDETAIL_CONTENTS_WINLOSSDATE_BLOCK.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class SystemParlayDetail_Contents_WinlossDate_BlockTest
    {
        private const string ContentsWinlossDateTemplate = "<td style=\"text-align: center; font-weight: normal\" rowspan=\"{0}\">{1}</td>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var rowSpan = "2";
            var winlossDate = "1/27/2014 4:40:00 AM";
            var contentsWinlossDateHtml = new StringBuilder();
            var block = new SystemParlayDetail_Contents_WinlossDate_Block();
            block.RowSpan = rowSpan;
            block.WinlossDate = winlossDate;

            // Act
            block.ToString(ref contentsWinlossDateHtml);

            // Assert
            var expectedHtml = string.Format(ContentsWinlossDateTemplate, rowSpan, winlossDate);
            Assert.AreEqual(expectedHtml, contentsWinlossDateHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var contentsWinlossDateHtml = new StringBuilder();
            var block = new SystemParlayDetail_Contents_WinlossDate_Block();
            block.Visible = false;

            // Act
            block.ToString(ref contentsWinlossDateHtml);

            // Assert
            Assert.IsNullOrEmpty(contentsWinlossDateHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var rowSpan = "2";
            var winlossDate = "1/27/2014 4:40:00 AM";
            var contentsWinlossDateHtml = new StringBuilder();
            var value = "Tennis";
            var block = new WrapperSystemParlayDetail_Contents_WinlossDate_Block();
            block.RowSpan = rowSpan;
            block.WinlossDate = winlossDate;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref contentsWinlossDateHtml);

            // Assert
            var expectedHtml = string.Format(ContentsWinlossDateTemplate, rowSpan, winlossDate) + value;
            Assert.AreEqual(expectedHtml, contentsWinlossDateHtml.ToString());
        }
    }
}