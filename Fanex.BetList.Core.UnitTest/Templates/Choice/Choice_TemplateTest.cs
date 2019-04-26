namespace Fanex.BetList.Core.UnitTest.Templates.Choice
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Choice_Template class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperChoice_Template : Choice_Template
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

    /// <summary>
    /// The Unit testing for Choice_Template class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Choice_TemplateTest
    {
        private const string ChoiceTemplate = "<div class='{0}'>{1}<span class=\"{2}\">{3}{4}{5}{6}</span>{7}{8}{9}{10}</div>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var ticketStatus = "void";
            var betTeamClass = "favorite";
            var betTeam = "Milan Reds";
            var choiceHtml = string.Empty;
            var block = new Choice_Template();
            block.ticketStatus = ticketStatus;
            block.betTeamClassName = betTeamClass;
            block.betTeam = betTeam;

            // Act
            choiceHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(
                                                ChoiceTemplate,
                                                ticketStatus,
                                                block.ScoreMapIcon,
                                                betTeamClass,
                                                betTeam,
                                                block.Handicap,
                                                block.OddMixParlay,
                                                block.Score,
                                                block.BetType,
                                                block.Match,
                                                block.League,
                                                block.EventDate);
            Assert.AreEqual(expectedHtml, choiceHtml);
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var choiceHtml = string.Empty;
            var block = new Choice_Template
            {
                Visible = false
            };

            // Act
            choiceHtml = block.ToString();

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(choiceHtml));
        }

        /// <summary>
        /// The all child block is null return html string with all child block is empty.
        /// </summary>
        [Test]
        public void ToString_AllChildBlockIsNull_ReturnHtmlStringWithAllChildBlockIsEmpty()
        {
            // Arrange
            var ticketStatus = "void";
            var betTeamClass = "favorite";
            var betTeam = "Milan Reds";
            var choiceHtml = string.Empty;
            var block = new Choice_Template
            {
                ScoreMapIcon = null,
                Handicap = null,
                OddMixParlay = null,
                Score = null,
                BetType = null,
                Match = null,
                League = null,
                EventDate = null,
                ticketStatus = ticketStatus,
                betTeamClassName = betTeamClass,
                betTeam = betTeam
            };

            // Act
            choiceHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(
                                                ChoiceTemplate,
                                                ticketStatus,
                                                string.Empty,
                                                betTeamClass,
                                                betTeam,
                                                string.Empty,
                                                string.Empty,
                                                string.Empty,
                                                string.Empty,
                                                string.Empty,
                                                string.Empty,
                                                string.Empty);
            Assert.AreEqual(expectedHtml, choiceHtml);
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var ticketStatus = "void";
            var betTeamClass = "favorite";
            var betTeam = "Milan Reds";
            var value = "Tennis";
            var choiceHtml = string.Empty;
            var block = new WrapperChoice_Template
            {
                ticketStatus = ticketStatus,
                betTeamClassName = betTeamClass,
                betTeam = betTeam
            };
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            choiceHtml = block.ToString();

            // Assert
            var expectedHtml = string.Format(
                                                ChoiceTemplate,
                                                ticketStatus,
                                                block.ScoreMapIcon,
                                                betTeamClass,
                                                betTeam,
                                                block.Handicap,
                                                block.OddMixParlay,
                                                block.Score,
                                                block.BetType,
                                                block.Match,
                                                block.League,
                                                block.EventDate) + value;
            Assert.AreEqual(expectedHtml, choiceHtml);
        }
    }
}