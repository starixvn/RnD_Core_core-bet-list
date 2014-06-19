namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Constants;
    using Entities;
    using NPOI.SS.UserModel;
    using Templates;
    using Utils;

    /// <summary>
    /// Bet type name: Handicap.
    /// </summary>
    public class Choice1 : IChoice
    {
        #region Attributes

        /// <summary>
        /// String underdog.
        /// </summary>
        protected const string Underdog = "underdog";

        /// <summary>
        /// String favorite.
        /// </summary>
        protected const string Favorite = "favorite";

        #endregion Attributes

        private const string VoidStatus = "void";
        private const string Neutral = " (N) ";
        private const string FirstGoalCssClass = "firstGoal ";
        private const string LastGoalCssClass = "lastGoal";
        private const string FirstHalfFirstGoalCssClass = "fhFirstGoal";
        private const string FirstHalfLastGoalCssClass = "fhLastGoal";
        private const string Space = " ";
        private const string FirstGoalTextName = "(F)";
        private const string LastGoalTextName = "(L)";
        private const string FirstHalfFirstGoalTextName = "(1F)";
        private const string FirstHalfLastGoalTextName = "(1L)";
        private const string BetStatusFl = "fl";
        private const string BetStatus1Fl = "1fl";

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Choice1" /> class.
        /// </summary>
        public Choice1()
        {
            Template = new Choice_Template();
        }

        #endregion Constructors

        #region Properties

        public Choice_Template Template { get; set; }

        #endregion Properties

        public virtual Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            BuildChoice(ticket, ticketHelper, ticketData);
            BuildHandicapOdds(ticket, ticketHelper);
            BuildScoreMap(ticket, isShowScoreMap);

            return Template;
        }
        
        public virtual List<IRichTextString> RenderRTF(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap, RTFHelper rtfHelper)
        {
            Render(ticket, ticketHelper, ticketData, isShowScoreMap);
            var choiceData = BuildRTFChoice(ticket, rtfHelper, ticketHelper);

            var rtfChoice = new List<IRichTextString>() { (IRichTextString)choiceData };

            return rtfChoice;
        }

        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected virtual void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            BuildBetTeam(ticket, ticketHelper, ticketData);

            BuildBetTeamClassNameAndHandicap(ticket);

            BuildScore(ticket);

            BuildBetType(ticket, ticketHelper, ticketData);

            BuildMatch(ticket, ticketHelper);

            BuildLeague(ticket, ticketHelper);

            BuildSport(ticket, ticketHelper);

            BuildTicketStatus(ticket);
        }

        protected virtual void BuildScore(ITicket ticket)
        {
            if (Template.Score == null)
            {
                return;
            }

            Template.Score.homeScore = ticket.LiveHomeScore.ToString();
            Template.Score.awayScore = ticket.LiveAwayScore.ToString();
            Template.Score.Visible = ticket.IsLive;
            Template.Score.scoreClassName = Favorite;
        }

        protected virtual void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (Template.League == null
                || Template.League.LeagueName == null)
            {
                return;
            }

            Template.League.LeagueName.leagueName = ticketHelper.GetLeagueNameById(ticket.LeagueId);

        }

        protected virtual void BuildSport(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (Template.League == null)
            {
                return;
            }

            Template.League.sportTypeName = ticketHelper.GetSportNameById(ticket.SportTypeId);
        }


        protected virtual void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            string awayName = ticketHelper.GetTeamNameById(ticket.AwayId);
            string homeName = GetHomeTeamName(ticket, ticketHelper);

            Template.Match.homeTeam = homeName;
            Template.Match.awayTeam = awayName;

            Template.Match.home_firstGoal_lastGoal = string.Empty;
            Template.Match.away_firstGoal_lastGoal = string.Empty;
            CalculateFG_LG(ticket.EventStatus, ref Template.Match.home_firstGoal_lastGoal, ref Template.Match.away_firstGoal_lastGoal);
        }

        protected virtual void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string awayName = ticketHelper.GetTeamNameById(ticket.AwayId);
            string homeName = GetHomeTeamName(ticket, ticketHelper);

            bool betHomeTeam = IsBetHomeTeam(ticket);
            Template.betTeam = betHomeTeam ? homeName : awayName;
        }

        protected virtual void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            bool betHomeTeam = IsBetHomeTeam(ticket);

            if (betHomeTeam)
            {
                Template.betTeamClassName = ticket.Handicap2 >= ticket.Handicap1 ? Underdog : Favorite;
            }
            else
            {
                Template.betTeamClassName = ticket.Handicap2 <= ticket.Handicap1 ? Underdog : Favorite;
            }

            decimal handicap = ticket.Handicap2 > ticket.Handicap1 ? ticket.Handicap2 : ticket.Handicap1;

            Template.Handicap.handicap = ConvertByBetType.Hdp((Template.betTeamClassName == Favorite) ? -handicap : handicap);
        }

        protected virtual void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            if (Template.BetType == null)
            {
                return;
            }

            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);
        }

        protected virtual void BuildTicketStatus(ITicket ticket)
        {
            if ((ticket.StatusId != null && ticket.StatusId.ToString().Trim() == BetStatusId.Void)
                || (ticket.Status != null && ticket.Status.Trim().ToLower() == BetStatus.Void))
            {
                Template.ticketStatus = VoidStatus;
            }
        }

        private bool IsBetHomeTeam(ITicket ticket)
        {
            bool betHomeTeam = ticket.BetTeam != null && ticket.BetTeam.Contains(BetTeamValue.H);

            return betHomeTeam;
        }

        protected virtual string GetHomeTeamName(ITicket ticket, ITicketHelper ticketHelper)
        {
            string homeName = ticketHelper.GetTeamNameById(ticket.HomeId);
            homeName = string.Join(null, new string[] { homeName, ticket.IsNeutral ? Neutral : string.Empty });

            return homeName;
        }

        /// <summary>
        /// Calculates the FG _ LG.
        /// </summary>
        /// <param name="betStatus">The bet status.</param>
        /// <param name="homeFGLG">The home FGLG.</param>
        /// <param name="awayFGLG">The away FGLG.</param>
        protected virtual void CalculateFG_LG(string betStatus, ref string homeFGLG, ref string awayFGLG)
        {
            if (betStatus == null)
            {
                return;
            }

            /*
            // Possible formats:
            // Refund HT;1FL:1-2;FL:2-0
            // 1FL:1-2;FL:2-0
            // Refund HT;FL:2-0
            // Refund HT;1FL:1-2
            // FL:2-0
            // Refund HT
            // 1FL:1-2
            */

            string[] betStatusParts = betStatus.ToLower().Split(';');

            string[] fglg = GetBetStatusPart(betStatusParts, BetStatusFl);
            UpdateFG_LG(FirstGoalCssClass, LastGoalCssClass, fglg, ref homeFGLG, ref awayFGLG);

            string[] fhfglg = GetBetStatusPart(betStatusParts, BetStatus1Fl);
            UpdateFG_LG(FirstHalfFirstGoalCssClass, FirstHalfLastGoalCssClass, fhfglg, ref homeFGLG, ref awayFGLG);

            if (!homeFGLG.Equals(string.Empty))
            {
                homeFGLG = string.Join(null, new string[] { HtmlCharacters.NoneBreakingSpace, homeFGLG });
            }

            if (!awayFGLG.Equals(string.Empty))
            {
                awayFGLG = string.Join(null, new string[] { HtmlCharacters.NoneBreakingSpace, awayFGLG });
            }
        }

        /// <summary>
        /// Builds the RTF choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <returns>IRichTextString: the rich text string of input data.</returns>
        protected virtual IRichTextString BuildRTFChoice(ITicket ticket, RTFHelper rtfHelper, ITicketHelper ticketHelper)
        {
            bool isMixParlay = false;

            AdjustBetTeamToRTF();

            // the 1st line
            BuildBetFigure(ticket, ref rtfHelper, isMixParlay);

            // the 2nd line
            BuildBetTypeName(ref rtfHelper);

            // the 3rd line
            BuildBetTeamsInfo(ticket, ref rtfHelper);

            // the 4th line
            BuildMatchInfo(ref rtfHelper);

            var choiceRtf = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();
            return choiceRtf;
        }

        /// <summary>
        /// Adjusts the bet team to RTF.
        /// </summary>
        protected virtual void AdjustBetTeamToRTF()
        {
            Template.betTeam = Template.betTeam.Replace("<span class=\"favorite\">", string.Empty);
            Template.betTeam = Template.betTeam.Replace("<span class=\"underdog\">", string.Empty);
            Template.betTeam = Template.betTeam.Replace("<span style='color:#555555'>", string.Empty);
            Template.betTeam = Template.betTeam.Replace("</span>", string.Empty);
        }

        /// <summary>
        /// Calculates the F g_ L g_ excel.
        /// </summary>
        /// <param name="betStatus">The bet status.</param>
        /// <param name="homeFGLG">The home FGLG.</param>
        /// <param name="awayFGLG">The away FGLG.</param>
        protected virtual void CalculateFG_LG_Excel(string betStatus, ref string homeFGLG, ref string awayFGLG)
        {
            if (betStatus == null)
            {
                return;
            }

            /*
            // Possible formats:
            // Refund HT;1FL:1-2;FL:2-0
            // 1FL:1-2;FL:2-0
            // Refund HT;FL:2-0
            // Refund HT;1FL:1-2
            // FL:2-0
            // Refund HT
            // 1FL:1-2
            */

            string[] betStatusParts = betStatus.ToLower().Split(';');

            string[] fglg = GetBetStatusPart(betStatusParts, BetStatusFl);
            UpdateFG_LG_Excel(FirstGoalTextName, LastGoalTextName, fglg, ref homeFGLG, ref awayFGLG);

            string[] fhfglg = GetBetStatusPart(betStatusParts, BetStatus1Fl);
            UpdateFG_LG_Excel(FirstHalfFirstGoalTextName, FirstHalfLastGoalTextName, fhfglg, ref homeFGLG, ref awayFGLG);
        }

        protected void UpdateAllMatchMemberToNull()
        {
            if (Template.Match != null)
            {
                Template.Match.homeTeam = null;
                Template.Match.awayTeam = null;
                Template.Match.VS = null;
                Template.Match.home_firstGoal_lastGoal = null;
                Template.Match.away_firstGoal_lastGoal = null;
            }
        }

        /// <summary>
        /// Builds the score map.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="isShowScoreMap">If set to <c>true</c> [is show score map].</param>
        private void BuildScoreMap(ITicket ticket, bool isShowScoreMap)
        {
            if (isShowScoreMap)
            {
                Template.ScoreMapIcon.matchId = ticket.MatchId.ToString();
                Template.ScoreMapIcon.betTypeId = ticket.BetTypeId.ToString();
                Template.ScoreMapIcon.liveindicator = Convert.ToInt32(ticket.IsLive).ToString();
            }
            else
            {
                Template.ScoreMapIcon = null;
            }
        }

        /// <summary>
        /// Builds the handicap odds.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        private void BuildHandicapOdds(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (ticketHelper.IsShowOddsWithChoice)
            {
                object oddsType = new object();

                if (string.IsNullOrEmpty(ticket.OddsType))
                {
                    oddsType = 0;
                }
                else
                {
                    oddsType = ticket.OddsType;
                }

                string odds = ConvertByBetType.Odds(ticket.Odds, ticket.BetTypeId, oddsType);

                if (odds.Equals("-"))
                {
                    Template.Handicap.Odds = null;
                }
                else
                {
                    Template.Handicap.Odds.odds = odds;
                }
            }
            else
            {
                if (Template.Handicap != null)
                {
                    Template.Handicap.Odds = null;
                }
            }
        }

        /// <summary>
        /// Builds the bet figure.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <param name="isMixParlay">If set to <c>true</c> [current ticket is mix parlay].</param>
        private void BuildBetFigure(ITicket ticket, ref RTFHelper rtfHelper, bool isMixParlay)
        {
            string choice = Template.betTeam;

            if (Template.Handicap != null && Template.Handicap.Visible && Template.Handicap.handicap != null)
            {
                choice += string.Join(null, new string[] { Space, Template.Handicap.handicap });
            }

            if (Template.Score != null && Template.Score.Visible)
            {
                choice += string.Join(null, new string[] { " [", Template.Score.homeScore, "-", Template.Score.awayScore, "]" });
            }

            if (isMixParlay)
            {
                choice += string.Join(null, new string[] { " @ ", Formatter.FormatNumber3(ticket.Odds, null) });
            }

            if (!string.IsNullOrEmpty(choice))
            {
                choice = choice.Replace(HtmlCharacters.NoneBreakingSpace, Space);
                rtfHelper.RTFRenderer.AddText(choice, rtfHelper.PosFont);
            }
        }

        /// <summary>
        /// Builds the name of the bet type.
        /// </summary>
        /// <param name="rtfHelper">The RTF helper.</param>
        private void BuildBetTypeName(ref RTFHelper rtfHelper)
        {
            var choice = string.Empty;

            if (Template.BetType != null && !string.IsNullOrEmpty(Template.BetType.betTypeName))
            {
                choice = Template.BetType.betTypeName.Replace(HtmlCharacters.NoneBreakingSpace, Space);
                rtfHelper.RTFRenderer.AddText("\n" + choice);
            }
        }

        /// <summary>
        /// Builds the bet teams' information.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "The logic is simple so separating to reduce fewer than 10 lines is not necessary.")]
        private void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            if (Template.Match != null)
            {
                bool isAddNewLine = false;
                string choice = string.Empty;
                if (!string.IsNullOrEmpty(Template.Match.homeTeam))
                {
                    choice = Template.Match.homeTeam.Replace(HtmlCharacters.NoneBreakingSpace, Space);
                }

                if (!string.IsNullOrEmpty(choice))
                {
                    isAddNewLine = true;
                    rtfHelper.RTFRenderer.AddText("\n" + choice);
                    choice = string.Empty;
                }

                BuildTicketFG_LG(ticket);

                if (!string.IsNullOrEmpty(Template.Match.home_firstGoal_lastGoal))
                {
                    choice = string.Join(null, new string[] { Space, Template.Match.home_firstGoal_lastGoal });
                }

                if (!string.IsNullOrEmpty(choice))
                {
                    isAddNewLine = true;
                    choice = choice.Replace(HtmlCharacters.NoneBreakingSpace, Space);
                    rtfHelper.RTFRenderer.AddText(choice, rtfHelper.PosFont);
                    choice = string.Empty;
                }

                if (Template.Match.VS != null && Template.Match.VS.Visible)
                {
                    choice = "- vs -";
                }

                if (!string.IsNullOrEmpty(Template.Match.awayTeam))
                {
                    choice += string.Join(null, new string[] { Space, Template.Match.awayTeam });
                }

                if (!string.IsNullOrEmpty(choice))
                {
                    isAddNewLine = true;
                    choice = choice.Replace(HtmlCharacters.NoneBreakingSpace, Space);
                    rtfHelper.RTFRenderer.AddText(choice);
                    choice = string.Empty;
                }

                if (!string.IsNullOrEmpty(Template.Match.away_firstGoal_lastGoal))
                {
                    choice = Space + Template.Match.away_firstGoal_lastGoal;
                }

                if (!string.IsNullOrEmpty(choice) || isAddNewLine)
                {
                    choice = choice.Replace(HtmlCharacters.NoneBreakingSpace, Space);
                    rtfHelper.RTFRenderer.AddText(choice, rtfHelper.PosFont);
                }
            }
        }

        /// <summary>
        /// Builds the match information.
        /// </summary>
        /// <param name="rtfHelper">The RTF helper.</param>
        private void BuildMatchInfo(ref RTFHelper rtfHelper)
        {
            if (Template.League != null)
            {
                string choice = string.Empty;

                if (!string.IsNullOrEmpty(Template.League.sportTypeName))
                {
                    choice = Template.League.sportTypeName;
                }

                if (Template.League.LeagueName != null && !string.IsNullOrEmpty(Template.League.LeagueName.leagueName))
                {
                    if (!string.IsNullOrEmpty(choice))
                    {
                        choice += Space;
                    }

                    choice += Template.League.LeagueName.leagueName;
                }

                if (!string.IsNullOrEmpty(choice))
                {
                    choice = choice.Replace(HtmlCharacters.NoneBreakingSpace, Space);
                    rtfHelper.RTFRenderer.AddText("\n" + choice);
                }
            }
        }

        /// <summary>
        /// Builds First Goal, Last Goal information.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        private void BuildTicketFG_LG(ITicket ticket)
        {
            if (ticket.EventStatus != null && Template.Match != null)
            {
                Template.Match.home_firstGoal_lastGoal = string.Empty;
                Template.Match.away_firstGoal_lastGoal = string.Empty;
                CalculateFG_LG_Excel(ticket.EventStatus, ref Template.Match.home_firstGoal_lastGoal, ref Template.Match.away_firstGoal_lastGoal);
            }
        }

        /// <summary>
        /// Get part of bet status by name (1FL or FL).
        /// </summary>
        /// <param name="source">The source string array.</param>
        /// <param name="partName">The part name (1FL or FL).</param>
        /// <returns>The targeted part.</returns>
        private string[] GetBetStatusPart(string[] source, string partName)
        {
            string part = string.Empty;

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i].StartsWith(partName))
                {
                    part = source[i];
                    break;
                }
            }

            string[] partValue = part.Split(new char[] { ':', '-' });
            return partValue;
        }

        /// <summary>
        /// Update First Goal, Last Goal icon group.
        /// </summary>
        /// <param name="firstClassName">CSS class name of first case.</param>
        /// <param name="lastClassName">CSS class name of last case.</param>
        /// <param name="statusPart">The data of currently processed part.</param>
        /// <param name="homeFGLG">Reference to home FGLG.</param>
        /// <param name="awayFGLG">Reference to away FGLG.</param>
        private void UpdateFG_LG(string firstClassName, string lastClassName, string[] statusPart, ref string homeFGLG, ref string awayFGLG)
        {
            if (statusPart.Length != 3)
            {
                return;
            }

            if (statusPart[1] == "1")
            {
                homeFGLG += string.Join(null, new string[] { "<div class='", firstClassName, "'>&nbsp;</div>" });
            }
            else if (statusPart[1] == "2")
            {
                awayFGLG += string.Join(null, new string[] { "<div class='", firstClassName, "'>&nbsp;</div>" });
            }

            if (statusPart[2] == "1")
            {
                homeFGLG += string.Join(null, new string[] { "<div class='", lastClassName, "'>&nbsp;</div>" });
            }
            else if (statusPart[2] == "2")
            {
                awayFGLG += string.Join(null, new string[] { "<div class='", lastClassName, "'>&nbsp;</div>" });
            }
        }

        /// <summary>
        /// Update First Goal, Last Goal text group.
        /// </summary>
        /// <param name="firstTextName">Text name of first case.</param>
        /// <param name="lastTextName">Text class name of last case.</param>
        /// <param name="statusPart">The data of currently processed part.</param>
        /// <param name="homeFGLG">Reference to home FGLG.</param>
        /// <param name="awayFGLG">Reference to away FGLG.</param>
        private void UpdateFG_LG_Excel(string firstTextName, string lastTextName, string[] statusPart, ref string homeFGLG, ref string awayFGLG)
        {
            if (statusPart.Length != 3)
            {
                return;
            }

            if (statusPart[1] == "1")
            {
                homeFGLG += firstTextName;
            }
            else if (statusPart[1] == "2")
            {
                awayFGLG += firstTextName;
            }

            if (statusPart[2] == "1")
            {
                homeFGLG += lastTextName;
            }
            else if (statusPart[2] == "2")
            {
                awayFGLG += lastTextName;
            }
        }
    }
}