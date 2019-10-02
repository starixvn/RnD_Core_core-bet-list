namespace Fanex.BetList.Core.UnitTest.Templates.Stake
{
    using System.Text;
    using Core.Templates;
    using NUnit.Framework;

    public class Stake_OriginalStake_BlockWrapper : Stake_OriginalStake_Block
    {
        public void SetAssigned(bool isAssigned)
        {
            IsAssigned = isAssigned;
        }

        public void SetValueExtend(StringBuilder value)
        {
            Value = value;
        }
    }

    public class Stake_OriginalStake_BlockTest
    {
        private const string StakeTemplate = "<strike class=\"originalStake\">{0}</strike>";

        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange.
            const string originalStake = "10";

            var block = new Stake_OriginalStake_Block();

            block.stake = originalStake;

            // Act.
            var stakeHtml = block.ToString();

            // Assert.
            var expectedHtml = string.Format(StakeTemplate, originalStake);

            Assert.AreEqual(expectedHtml, stakeHtml);
        }

        [Test]
        public void ToString_BlockIsInvisible_ReturnEmptyString()
        {
            // Arrange.
            var block = new Stake_OriginalStake_Block();

            block.Visible = false;

            // Act.
            var stakeHtml = block.ToString();

            // Assert.
            Assert.IsTrue(string.IsNullOrEmpty(stakeHtml));
        }

        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange.
            const string stake = "10";
            const string value = "Tennis";

            var block = new Stake_OriginalStake_BlockWrapper();

            block.stake = stake;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act.
            var stakeHtml = block.ToString();

            // Assert.
            var expectedHtml = string.Format(StakeTemplate, stake) + value;

            Assert.AreEqual(expectedHtml, stakeHtml);
        }
    }
}