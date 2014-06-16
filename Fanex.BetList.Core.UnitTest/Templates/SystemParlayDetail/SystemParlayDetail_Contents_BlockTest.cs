namespace Fanex.BetList.Core.UnitTest.Templates.SystemParlayDetail
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for SystemParlayDetail_Contents_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperSystemParlayDetail_Contents_Block : SystemParlayDetail_Contents_Block
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
    /// The Unit testing for class SystemParlayDetail_Contents_Block.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2100:CodeLineMustNotBeLongerThan", Justification = "Reviewed.")]
    public class SystemParlayDetail_Contents_BlockTest
    {
        private const string ContentsTemplate = "<tr>{0}<td style=\"text-align: left; font-weight: normal\">{1}</td><td>{2}</td><td>{3}</td><td>{4}{5}</td><td style=\"text-align: center;\">{6}</td></tr>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var odds = "0.78";
            var stake = "1";
            var winloss = "1000";
            var status = "Lose";
            var contentsHtml = new StringBuilder();
            var block = new SystemParlayDetail_Contents_Block();
            block.Odds = odds;
            block.Stake = stake;
            block.Winloss = winloss;
            block.Status = status;

            // Act
            block.ToString(ref contentsHtml);

            // Assert
            var expectedHtml = string.Format(ContentsTemplate, block.WinlossDate.ToString(), block.Match.ToString(), odds, stake, winloss, block.PlayerCommission.ToString(), status);
            Assert.AreEqual(expectedHtml, contentsHtml.ToString());
        }

        /// <summary>
        /// The block content is null return html string with block content is empty.
        /// </summary>
        [Test]
        public void ToString_BlockContentsIsNull_ReturnHtmlStringWithContentsBlockIsEmpty()
        {
            // Arrange
            var odds = "0.78";
            var stake = "1";
            var winloss = "1000";
            var status = "Lose";
            var contentsHtml = new StringBuilder();
            var block = new SystemParlayDetail_Contents_Block();
            block.WinlossDate = null;
            block.Match = null;
            block.Odds = odds;
            block.Stake = stake;
            block.Winloss = winloss;
            block.PlayerCommission = null;
            block.Status = status;

            // Act
            block.ToString(ref contentsHtml);

            // Assert
            var expectedHtml = string.Format(ContentsTemplate, string.Empty, string.Empty, odds, stake, winloss, string.Empty, status);
            Assert.AreEqual(expectedHtml, contentsHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var contentsHtml = new StringBuilder();
            var block = new SystemParlayDetail_Contents_Block();
            block.Visible = false;

            // Act
            block.ToString(ref contentsHtml);

            // Assert
            Assert.IsNullOrEmpty(contentsHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var odds = "0.78";
            var stake = "1";
            var winloss = "1000";
            var status = "Lose";
            var contentsHtml = new StringBuilder();
            var value = "Tennis";
            var block = new WrapperSystemParlayDetail_Contents_Block();
            block.Odds = odds;
            block.Stake = stake;
            block.Winloss = winloss;
            block.Status = status;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref contentsHtml);

            // Assert
            var expectedHtml = string.Format(ContentsTemplate, block.WinlossDate.ToString(), block.Match.ToString(), odds, stake, winloss, block.PlayerCommission.ToString(), status) + value;
            Assert.AreEqual(expectedHtml, contentsHtml.ToString());
        }
    }
}