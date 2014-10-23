namespace Fanex.BetList.Core.UnitTest.Templates.Status
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Status_StatusResult_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperStatus_StatusResult_Block : Status_StatusResult_Block
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
    /// The Unit testing for class Status_StatusResult_Block.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Status_StatusResult_BlockTest
    {
        private const string StatusTemplate = "<div class=\"result\" onclick=\"ViewResult({0}, {1}, {2}, {3}, {4}, '{5}', '{6}', {7}, {8}, {9}, {10});\">Results</div>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlStringDefaultValue()
        {
            // Arrange
            var matchId = "1100";
            var race = "0";
            var betType = "1";
            var sportType = "1";
            var refNo = "1000000000";
            var username = "TESTCS0101002";
            var winlostDate = "11/20/2013 12:00:00 AM";
            var refNoMixParlay = "0";
            var league = "NBA BASKETBALL - SPECIALS (Most 3-Point Shots Scored)";
            var isOutright = "0";
            var betId = "801";
            var statusHtml = new StringBuilder();
            var block = new Status_StatusResult_Block();
            block.matchId = matchId;
            block.betType = betType;
            block.sportType = sportType;
            block.refNo = refNo;
            block.refNo_MixParlay = refNoMixParlay;
            block.userName = username;
            block.winlostDate = winlostDate;
            block.league = league;
            block.betId = betId;

            // Act
            block.ToString(ref statusHtml);

            // Assert
            var expectedHtml = string.Format(
                                        StatusTemplate,
                                        matchId,
                                        race,
                                        betType,
                                        sportType,
                                        refNo,
                                        username,
                                        winlostDate,
                                        refNoMixParlay,
                                        league,
                                        isOutright,
                                        betId);
            Assert.AreEqual(expectedHtml, statusHtml.ToString());
        }

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var matchId = "1100";
            var race = "10";
            var betType = "1";
            var sportType = "1";
            var refNo = "1000000000";
            var username = "TESTCS0101002";
            var winlostDate = "11/20/2013 12:00:00 AM";
            var refNoMixParlay = "1000000000";
            var league = "NBA BASKETBALL - SPECIALS (Most 3-Point Shots Scored)";
            var isOutright = "1";
            var betId = "80101";
            var statusHtml = new StringBuilder();
            var block = new Status_StatusResult_Block();
            block.matchId = matchId;
            block.race = race;
            block.betType = betType;
            block.sportType = sportType;
            block.refNo = refNo;
            block.refNo_MixParlay = refNoMixParlay;
            block.userName = username;
            block.winlostDate = winlostDate;
            block.league = league;
            block.isoutright = isOutright;
            block.betId = betId;

            // Act
            block.ToString(ref statusHtml);

            // Assert
            var expectedHtml = string.Format(
                                        StatusTemplate,
                                        matchId,
                                        race,
                                        betType,
                                        sportType,
                                        refNo,
                                        username,
                                        winlostDate,
                                        refNoMixParlay,
                                        league,
                                        isOutright,
                                        betId);
            Assert.AreEqual(expectedHtml, statusHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var statusHtml = new StringBuilder();
            var block = new Status_StatusResult_Block();
            block.Visible = false;

            // Act
            block.ToString(ref statusHtml);

            // Assert
            Assert.IsNullOrEmpty(statusHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var matchId = "1100";
            var race = "0";
            var betType = "1";
            var sportType = "1";
            var refNo = "1000000000";
            var username = "TESTCS0101002";
            var winlostDate = "11/20/2013 12:00:00 AM";
            var refNoMixParlay = "0";
            var league = "NBA BASKETBALL - SPECIALS (Most 3-Point Shots Scored)";
            var isOutright = "0";
            var betId = "801";
            var value = "Tennis";
            var statusHtml = new StringBuilder();
            var block = new WrapperStatus_StatusResult_Block();
            block.matchId = matchId;
            block.betType = betType;
            block.sportType = sportType;
            block.refNo = refNo;
            block.refNo_MixParlay = refNoMixParlay;
            block.userName = username;
            block.winlostDate = winlostDate;
            block.league = league;
            block.betId = betId;
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref statusHtml);

            // Assert
            var expectedHtml = string.Format(
                                        StatusTemplate,
                                        matchId,
                                        race,
                                        betType,
                                        sportType,
                                        refNo,
                                        username,
                                        winlostDate,
                                        refNoMixParlay,
                                        league,
                                        isOutright,
                                        betId) + value;
            Assert.AreEqual(expectedHtml, statusHtml.ToString());
        }
    }
}