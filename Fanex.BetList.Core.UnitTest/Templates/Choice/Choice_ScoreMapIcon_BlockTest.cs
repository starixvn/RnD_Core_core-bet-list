namespace Fanex.BetList.Core.UnitTest.Templates.Choice
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Choice_ScoreMapIcon_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperChoice_ScoreMapIcon_Block : Choice_ScoreMapIcon_Block
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
    /// The Unit testing for class Choice_ScoreMapIcon_Block.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2100:CodeLineMustNotBeLongerThan", Justification = "Reviewed.")]
    public class Choice_ScoreMapIcon_BlockTest
    {
        private const string ScoreMapIconTemplate = "<span class='scoremap'><a href=\"javascript:OpenScoreMap({0},{1},{2});\"title=\"Score Map\"><div class='scoremapIcon'></div></a></span>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var matchId = "1000";
            var betTypeId = "1";
            var liveIndicator = "1";
            var scoreMapIconHtml = new StringBuilder();
            var block = new Choice_ScoreMapIcon_Block();
            block.matchId = matchId;
            block.betTypeId = betTypeId;
            block.liveindicator = liveIndicator;

            // Act
            block.ToString(ref scoreMapIconHtml);

            // Assert
            var expectedHtml = string.Format(ScoreMapIconTemplate, matchId, betTypeId, liveIndicator);
            Assert.AreEqual(expectedHtml, scoreMapIconHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var scoreMapIconHtml = new StringBuilder();
            var block = new Choice_ScoreMapIcon_Block();
            block.Visible = false;

            // Act
            block.ToString(ref scoreMapIconHtml);

            // Assert
            Assert.IsNullOrEmpty(scoreMapIconHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var matchId = "1000";
            var betTypeId = "1";
            var liveIndicator = "1";
            var value = "Tennis";
            var scoreMapIconHtml = new StringBuilder();
            var block = new WrapperChoice_ScoreMapIcon_Block();
            block.matchId = matchId;
            block.betTypeId = betTypeId;
            block.liveindicator = liveIndicator;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref scoreMapIconHtml);

            // Assert
            var expectedHtml = string.Format(ScoreMapIconTemplate, matchId, betTypeId, liveIndicator) + value;
            Assert.AreEqual(expectedHtml, scoreMapIconHtml.ToString());
        }
    }
}