namespace Fanex.BetList.Core.UnitTest.Templates.Stake
{
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    public class Stake_TemplateWrapper : Stake_Template
    {
        public void SetAssigned(bool isAssigned)
        {
            IsAssigned = isAssigned;
        }
    }

    public class Stake_TemplateTest
    {
        private const string StakeTemplate = "{0}<div class=\"stake\">{1}</div>{2}";

        [Test]
        public void ToString_ChangeBlockProperty_ReturnHtmlWithData()
        {
            // Arrange.
            const string stake = "10";

            var block = new Stake_Template();

            block.stake = stake;

            // Act.
            var stakeHtml = block.ToString();

            // Assert.
            var expectedHtml = string.Format(StakeTemplate, block.OriginalStake, stake, block.Bonus);

            Assert.AreEqual(expectedHtml, stakeHtml);
        }

        [Test]
        public void ToString_BlockIsInvisible_ReturnEmptyString()
        {
            // Arrange.
            var block = new Stake_Template();

            block.Visible = false;

            // Act.
            var stakeHtml = block.ToString();

            // Assert.
            Assert.IsTrue(string.IsNullOrEmpty(stakeHtml));
        }

        [Test]
        public void ToString_AppendBlockWithText_ReturnHtmlWithAppendedValue()
        {
            // Arrange.
            const string stake = "10";
            const string appendedText = "appendedText";

            var block = new Stake_TemplateWrapper();

            block.stake = stake;
            block.Append(appendedText);
            block.SetAssigned(true);

            // Act.
            var stakeHtml = block.ToString();

            // Assert.
            var expectedHtml = string.Format(StakeTemplate, block.OriginalStake, stake, block.Bonus) + appendedText;

            Assert.AreEqual(expectedHtml, stakeHtml);
        }

        [Test]
        public void ToString_WhenCallSetValue_ReturnNewValue()
        {
            // Arrange.
            const string newText = "appendedText";

            var block = new Stake_TemplateWrapper();

            block.SetValue(newText);
            block.SetAssigned(true);

            // Act.
            var stakeHtml = block.ToString();

            // Assert.
            Assert.AreEqual(newText, stakeHtml);
        }
    }
}