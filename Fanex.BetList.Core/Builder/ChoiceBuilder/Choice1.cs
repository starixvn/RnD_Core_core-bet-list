namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Fanex.BetList.Core.Utils.StringExt;
    using NPOI.SS.UserModel;
    using Templates;
    using Utils;

    /// <summary>
    /// Bet type name: Handicap.
    /// </summary>
    public class Choice1 : IChoice
    {
        protected const string Underdog = "underdog";
        protected const string Favorite = "favorite";

        private const string VoidStatus = "void";
        private const string RejectStatus = "reject";
        private const string RefundStatus = "refund";
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
        private const int MuayThaiSportId = 44;
        private const int HandicapBetTypeId = 1;
        private const string LightBlue = " light-blue";
        private readonly int[] _liveScoreSports = { 1, 3, 4, 8, 26, 28, 49, 51 };
        private const string ESportsXmlTag = "p16";
        private const string InPlayXmlAttribute = "in";
        private const int ESportsSportType = 43;

        #region Constructors

        public Choice1()
        {
            Template = new Choice_Template();
        }

        #endregion Constructors

        #region Properties

        public Choice_Template Template { get; set; }

        #endregion Properties

        #region Render

        public virtual Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            BuildChoice(ticket, ticketHelper, ticketData);
            BuildOdds(ticket, ticketHelper);
            BuildScoreMap(ticket, isShowScoreMap);

            return Template;
        }

        public virtual List<IRichTextString> RenderRTF(
            ITicket ticket,
            ITicketHelper ticketHelper,
            List<ITicketData> ticketData,
            bool isShowScoreMap,
            RTFHelper rtfHelper)
        {
            Render(ticket, ticketHelper, ticketData, isShowScoreMap);

            var choiceData = BuildRTFChoice(ticket, rtfHelper, ticketHelper);
            var rtfChoice = new List<IRichTextString> { choiceData };

            return rtfChoice;
        }

        #endregion Render

        #region Common utils

        protected virtual string GetHomeTeamName(ITicket ticket, ITicketHelper ticketHelper)
        {
            string homeTeamName = ticketHelper.GetTeamNameById(ticket.HomeId);
            string neutral = string.Format(" {0}", CoreBetList.Neutral);
            homeTeamName = string.Join(null, new[] { homeTeamName, ticket.IsNeutral ? neutral : string.Empty });

            return homeTeamName;
        }

        /// <summary>
        /// Calculates the (F) (L) (1F) (1L) label.
        /// </summary>
        /// <param name="betStatus">The bet status.
        /// <![CDATA[Refund HT;1FL:1-2;FL:2-0]]>
        /// <![CDATA[1FL:1-2;FL:2-0]]>
        /// <![CDATA[Refund HT;FL:2-0]]>
        /// <![CDATA[Refund HT;1FL:1-2]]>
        /// <![CDATA[FL:2-0]]>
        /// <![CDATA[Refund HT]]>
        /// <![CDATA[1FL:1-2]]>
        /// </param>
        /// <param name="homeFGLG">The home FGLG.</param>
        /// <param name="awayFGLG">The away FGLG.</param>
        protected virtual void BuildFG_LGLabel(string betStatus, ref string homeFGLG, ref string awayFGLG)
        {
            if (string.IsNullOrWhiteSpace(betStatus))
            {
                return;
            }

            string[] betStatusParts = betStatus.ToLowerInvariant().Split(';');

            string[] fglg = GetBetStatusPart(betStatusParts, BetStatusFl);
            CalculateFG_LGLabel(FirstGoalCssClass, LastGoalCssClass, fglg, ref homeFGLG, ref awayFGLG);

            string[] fhfglg = GetBetStatusPart(betStatusParts, BetStatus1Fl);
            CalculateFG_LGLabel(FirstHalfFirstGoalCssClass, FirstHalfLastGoalCssClass, fhfglg, ref homeFGLG, ref awayFGLG);

            if (!string.IsNullOrEmpty(homeFGLG))
            {
                homeFGLG = string.Join(null, new[] { HtmlCharacters.NoneBreakingSpace, homeFGLG });
            }

            if (!string.IsNullOrEmpty(awayFGLG))
            {
                awayFGLG = string.Join(null, new[] { HtmlCharacters.NoneBreakingSpace, awayFGLG });
            }
        }

        /// <summary>
        /// Calculates the (F) (L) (1F) (1L) label.
        /// </summary>
        /// <param name="betStatus">The bet status.
        /// <![CDATA[Refund HT;1FL:1-2;FL:2-0]]>
        /// <![CDATA[1FL:1-2;FL:2-0]]>
        /// <![CDATA[Refund HT;FL:2-0]]>
        /// <![CDATA[Refund HT;1FL:1-2]]>
        /// <![CDATA[FL:2-0]]>
        /// <![CDATA[Refund HT]]>
        /// <![CDATA[1FL:1-2]]>
        /// </param>
        /// <param name="homeFGLG">The home FGLG.</param>
        /// <param name="awayFGLG">The away FGLG.</param>
        protected virtual void CalculateFG_LG_Excel(string betStatus, ref string homeFGLG, ref string awayFGLG)
        {
            if (betStatus == null)
            {
                return;
            }

            string[] betStatusParts = betStatus.ToLowerInvariant().Split(';');

            string[] fglg = GetBetStatusPart(betStatusParts, BetStatusFl);
            UpdateFG_LG_Excel(FirstGoalTextName, LastGoalTextName, fglg, ref homeFGLG, ref awayFGLG);

            string[] fhfglg = GetBetStatusPart(betStatusParts, BetStatus1Fl);
            UpdateFG_LG_Excel(FirstHalfFirstGoalTextName, FirstHalfLastGoalTextName, fhfglg, ref homeFGLG, ref awayFGLG);
        }

        protected void UpdateAllMatchMemberToNull()
        {
            if (Template.Match == null)
            {
                return;
            }

            Template.Match.homeTeam = null;
            Template.Match.awayTeam = null;
            Template.Match.VS = null;
            Template.Match.home_firstGoal_lastGoal = null;
            Template.Match.away_firstGoal_lastGoal = null;
        }

        #endregion Common utils

        #region Render Line Excel

        protected virtual IRichTextString BuildRTFChoice(ITicket ticket, RTFHelper rtfHelper, ITicketHelper ticketHelper)
        {
            const bool isMixParlay = false;

            AdjustBetTeamToRTF();

            // the 1st line
            BuildBetFigure(ticket, ref rtfHelper, isMixParlay);

            // the 2nd line
            BuildBetTypeName(ref rtfHelper);

            // the 3rd line
            BuildBetTeamsInfo(ticket, ref rtfHelper);

            // the 4th line
            BuildMatchInfo(ref rtfHelper);

            // the 5th line
            BuildEventKickoffDateTimeInfo(rtfHelper);

            var choiceRtf = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();
            return choiceRtf;
        }

        protected virtual void AdjustBetTeamToRTF()
        {
            Template.betTeam = Template.betTeam.Replace("<span class=\"favorite\">", string.Empty);
            Template.betTeam = Template.betTeam.Replace("<span class=\"underdog\">", string.Empty);
            Template.betTeam = Template.betTeam.Replace("<span style='color:#555555'>", string.Empty);
            Template.betTeam = Template.betTeam.Replace("</span>", string.Empty);
        }

        #endregion Render Line Excel

        #region Build Html Block

        protected virtual void BuildChoice(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            // The first build match information get home team, away team name, label (F)(L)(1F)(1L)
            BuildMatch(ticket, ticketHelper);

            // Optional ordering these function
            BuildBetTeam(ticket, ticketHelper, ticketData);

            BuildScore(ticket);

            BuildBetType(ticket, ticketHelper, ticketData);

            BuildLeague(ticket, ticketHelper);

            BuildSport(ticket, ticketHelper);

            BuildTicketStatus(ticket);

            BuildBetTeamClassNameAndHandicap(ticket);

            BuildEventKickoffDateTime(ticket);
        }

        protected virtual void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var awayTeamName = ticketHelper.GetTeamNameById(ticket.AwayId);
            var homeTeamName = GetHomeTeamName(ticket, ticketHelper);

            var isBetHomeTeam = IsBetHomeTeam(ticket);
            Template.betTeam = isBetHomeTeam ? homeTeamName : awayTeamName;
        }

        protected virtual void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            var isBetHomeTeam = IsBetHomeTeam(ticket);

            string betTeamClassName;

            if (isBetHomeTeam)
            {
                betTeamClassName = ticket.Handicap2 >= ticket.Handicap1 ? Underdog : Favorite;
            }
            else
            {
                betTeamClassName = ticket.Handicap2 <= ticket.Handicap1 ? Underdog : Favorite;
            }

            Template.betTeamClassName = betTeamClassName;

            ChangeClassNameWhenSportIsMuayThai(ticket.SportTypeId, ticket.BetTypeId, isBetHomeTeam);

            decimal handicap = ticket.Handicap2 > ticket.Handicap1 ? ticket.Handicap2 : ticket.Handicap1;
            handicap = betTeamClassName == Favorite ? -handicap : handicap;

            Template.Handicap.handicap = ConvertByBetType.Hdp(handicap);
        }

        private void ChangeClassNameWhenSportIsMuayThai(int sportId, int betTypeId, bool isBetHomeTeam)
        {
            if (sportId == MuayThaiSportId && betTypeId == HandicapBetTypeId)
            {
                Template.betTeamClassName = Favorite;

                if (!isBetHomeTeam)
                {
                    Template.betTeamClassName += LightBlue;
                }
            }
        }

        protected virtual void BuildScore(ITicket ticket)
        {
            if (Template.Score == null)
            {
                return;
            }

            if (IsShowLiveText(ticket.SportTypeId))
            {
                var scoreStatus = ticket.SportTypeId == ESportsSportType ? GetScoreStatus(ticket) : CoreBetList.Live;

                Template.Score.SetValue($"<span class=\"{Favorite}\"> [{scoreStatus}]</span>");
            }
            else
            {
                Template.Score.homeScore = ticket.LiveHomeScore.ToString();
                Template.Score.awayScore = ticket.LiveAwayScore.ToString();
            }

            Template.Score.Visible = ticket.IsLive;
        }

        private static string GetScoreStatus(ITicket ticket)
        {
            var inPlayStatus = GetInPlayStatus(ticket.ExtraInfo);

            if (string.IsNullOrEmpty(inPlayStatus))
            {
                inPlayStatus = GetInPlayStatus(ticket.TransDesc);
            }

            return string.IsNullOrEmpty(inPlayStatus) ? CoreBetList.Live : inPlayStatus;
        }

        private static string GetInPlayStatus(string data)
        {
            try
            {
                if (data.HasAttributeInXmlTag(ESportsXmlTag, InPlayXmlAttribute))
                {
                    var isInPlay = data.GetXmlTagAttributeValue(ESportsXmlTag, InPlayXmlAttribute) == "1";

                    if (isInPlay)
                    {
                        return CoreBetList.InPlay;
                    }
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        private bool IsShowLiveText(int sportTypeId)
        {
            const int MaxSportsbookSportTypeId = 99;

            return sportTypeId > 0 &&
                sportTypeId <= MaxSportsbookSportTypeId &&
                !_liveScoreSports.Contains(sportTypeId);
        }

        protected virtual void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            if (Template.BetType == null)
            {
                return;
            }

            var refNo = ticket.RefNo;

            if (IsCashOutBetTicket(ticket) && !IsOutRight(ticket.BetTypeId))
            {
                refNo = CommonValue.RefNoDefault;
            }

            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, refNo);
        }

        protected virtual void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (Template.Match == null)
            {
                return;
            }

            Template.Match.homeTeam = GetHomeTeamName(ticket, ticketHelper);
            Template.Match.awayTeam = ticketHelper.GetTeamNameById(ticket.AwayId);

            Template.Match.home_firstGoal_lastGoal = string.Empty;
            Template.Match.away_firstGoal_lastGoal = string.Empty;
            BuildFG_LGLabel(ticket.EventStatus, ref Template.Match.home_firstGoal_lastGoal, ref Template.Match.away_firstGoal_lastGoal);
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

        protected virtual void BuildTicketStatus(ITicket ticket)
        {
            string statusId = ticket.StatusId != null ? ticket.StatusId.ToString() : string.Empty;
            string status = ticket.Status ?? string.Empty;

            status = status.Trim().ToLowerInvariant();

            if (statusId == BetStatusId.Void || status == BetStatus.Void)
            {
                Template.ticketStatus = VoidStatus;
            }

            if (statusId == BetStatusId.Reject || status == BetStatus.Reject)
            {
                Template.ticketStatus = RejectStatus;
            }

            if (statusId == BetStatusId.Refund || status == BetStatus.Refund)
            {
                Template.ticketStatus = RefundStatus;
            }
        }

        protected virtual void BuildEventKickoffDateTime(ITicket ticket)
        {
            if (IsSportsbook(ticket.SportTypeId))
            {
                Template.EventDate.Visible = true;
                Template.EventDate.eventDate = Formatter.FormatDateTimeEn12ShortMonthNoSec(ticket.EventDate);
            }
            else
            {
                Template.EventDate.Visible = false;
            }
        }

        #endregion Build Html Block

        public virtual string GetBetTypeName(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData = null)
        {
            BuildBetType(ticket, ticketHelper, ticketData);

            var betTypeName = Template.BetType?.betTypeName;

            if (string.IsNullOrEmpty(betTypeName))
            {
                betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.RefNo);
            }

            return betTypeName;
        }

        #region Private function

        protected bool IsBetHomeTeam(ITicket ticket)
        {
            bool betHomeTeam = ticket.BetTeam != null && ticket.BetTeam.ToLowerInvariant().Contains(BetTeamValue.H);

            return betHomeTeam;
        }

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

        private void BuildOdds(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (ticketHelper.IsShowOddsWithChoice)
            {
                object oddsType = string.IsNullOrEmpty(ticket.OddsType) ? (object)0 : ticket.OddsType;
                string odds = ConvertByBetType.Odds(ticket.Odds, ticket.BetTypeId, oddsType);

                if (odds == "-")
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

        protected virtual void BuildBetFigure(ITicket ticket, ref RTFHelper rtfHelper, bool isMixParlay)
        {
            string choice = Template.betTeam;
            var htmlTagMatching = new Regex("</?[^<>]*>");

            if (Template.Handicap != null && Template.Handicap.Visible && Template.Handicap.handicap != null)
            {
                choice += string.Join(null, new[] { Space, Template.Handicap.handicap });
            }

            if (Template.Score != null && Template.Score.Visible)
            {
                choice += htmlTagMatching.Replace(Template.Score.ToString(), string.Empty);
            }

            if (isMixParlay)
            {
                choice += string.Join(null, new[] { " @ ", Formatter.FormatNumber3(ticket.Odds, null) });
            }

            if (!string.IsNullOrEmpty(choice))
            {
                choice = choice.Replace(HtmlCharacters.NoneBreakingSpace, Space);
                rtfHelper.RTFRenderer.AddText(choice, rtfHelper.PosFont);
            }
        }

        protected virtual void BuildBetTypeName(ref RTFHelper rtfHelper)
        {
            var choice = string.Empty;

            if (Template.BetType != null && !string.IsNullOrEmpty(Template.BetType.betTypeName))
            {
                choice = Template.BetType.betTypeName.Replace(HtmlCharacters.NoneBreakingSpace, Space);
                rtfHelper.RTFRenderer.AddText("\n" + choice);
            }
        }

        protected virtual void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            if (Template.Match != null)
            {
                var choice = string.Empty;

                var isAddNewLine = BuildHomeTeam(rtfHelper);

                BuildTicketFG_LG(ticket);

                isAddNewLine = BuildHomeTeamFG_LG(rtfHelper);

                isAddNewLine = BuildAwayTeam(rtfHelper);

                if (!string.IsNullOrEmpty(Template.Match.away_firstGoal_lastGoal))
                {
                    choice = Space + Template.Match.away_firstGoal_lastGoal;
                }

                if (string.Empty != choice || isAddNewLine)
                {
                    choice = choice.Replace(HtmlCharacters.NoneBreakingSpace, Space);
                    rtfHelper.RTFRenderer.AddText(choice, rtfHelper.PosFont);
                }
            }
        }

        protected virtual void BuildMatchInfo(ref RTFHelper rtfHelper)
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

        protected virtual void BuildEventKickoffDateTimeInfo(RTFHelper rtfHelper)
        {
            if (Template.EventDate != null
                && Template.EventDate.Visible
                && !string.IsNullOrEmpty(Template.EventDate.eventDate))
            {
                rtfHelper.RTFRenderer.AddText("\n" + Template.EventDate.eventDate);
            }
        }

        private bool BuildHomeTeam(RTFHelper rtfHelper)
        {
            var choice = string.Empty;

            if (!string.IsNullOrEmpty(Template.Match.homeTeam))
            {
                choice = Template.Match.homeTeam.Replace(HtmlCharacters.NoneBreakingSpace, Space);
            }

            if (!string.IsNullOrEmpty(choice))
            {
                rtfHelper.RTFRenderer.AddText("\n" + choice + Space);

                return true;
            }

            return false;
        }

        private void BuildTicketFG_LG(ITicket ticket)
        {
            if (ticket.EventStatus != null && Template.Match != null)
            {
                Template.Match.home_firstGoal_lastGoal = string.Empty;
                Template.Match.away_firstGoal_lastGoal = string.Empty;
                CalculateFG_LG_Excel(ticket.EventStatus, ref Template.Match.home_firstGoal_lastGoal, ref Template.Match.away_firstGoal_lastGoal);
            }
        }

        private bool BuildHomeTeamFG_LG(RTFHelper rtfHelper)
        {
            var choice = string.Empty;

            if (!string.IsNullOrEmpty(Template.Match.home_firstGoal_lastGoal))
            {
                choice = string.Join(null, new[] { Template.Match.home_firstGoal_lastGoal, Space });
            }

            if (!string.IsNullOrEmpty(choice))
            {
                choice = choice.Replace(HtmlCharacters.NoneBreakingSpace, Space);
                rtfHelper.RTFRenderer.AddText(choice, rtfHelper.PosFont);

                return true;
            }

            return false;
        }

        private bool BuildAwayTeam(RTFHelper rtfHelper)
        {
            var choice = string.Empty;

            if (Template.Match.VS != null && Template.Match.VS.Visible)
            {
                choice = "- vs -";
            }

            if (!string.IsNullOrEmpty(Template.Match.awayTeam))
            {
                choice += string.Join(null, new[] { Space, Template.Match.awayTeam });
            }

            if (!string.IsNullOrEmpty(choice))
            {
                choice = choice.Replace(HtmlCharacters.NoneBreakingSpace, Space);
                rtfHelper.RTFRenderer.AddText(choice);

                return true;
            }

            return false;
        }

        private static string[] GetBetStatusPart(string[] source, string partName)
        {
            var part = source?.FirstOrDefault(item => item.StartsWith(partName, StringComparison.InvariantCulture)) ?? string.Empty;
            var partValue = part.Split(new[] { ':', '-' });

            return partValue;
        }

        private static void CalculateFG_LGLabel(string firstClassName, string lastClassName, string[] statusPart, ref string homeFGLG, ref string awayFGLG)
        {
            const string goalIndicatorHtmlFormat = "<div class='{0}'>&nbsp;</div>";
            const int numberOfExpectedIndicators = 3;

            if (statusPart.Length != numberOfExpectedIndicators)
            {
                return;
            }

            var firstGoalIndicator = statusPart[1];
            var lastGoalIndicator = statusPart[2];

            if (firstGoalIndicator == BetTeamValue.NumberOne)
            {
                homeFGLG += string.Format(goalIndicatorHtmlFormat, firstClassName);
            }

            if (firstGoalIndicator == BetTeamValue.NumberTwo)
            {
                awayFGLG += string.Format(goalIndicatorHtmlFormat, firstClassName);
            }

            if (lastGoalIndicator == BetTeamValue.NumberOne)
            {
                homeFGLG += string.Format(goalIndicatorHtmlFormat, lastClassName);
            }

            if (lastGoalIndicator == BetTeamValue.NumberTwo)
            {
                awayFGLG += string.Format(goalIndicatorHtmlFormat, lastClassName);
            }
        }

        private static void UpdateFG_LG_Excel(string firstTextName, string lastTextName, string[] statusPart, ref string homeFGLG, ref string awayFGLG)
        {
            const int numberOfExpectedIndicators = 3;

            if (statusPart.Length != numberOfExpectedIndicators)
            {
                return;
            }

            var firstGoalIndicator = statusPart[1];
            var lastGoalIndicator = statusPart[2];

            if (firstGoalIndicator == BetTeamValue.NumberOne)
            {
                homeFGLG += firstTextName;
            }

            if (firstGoalIndicator == BetTeamValue.NumberTwo)
            {
                awayFGLG += firstTextName;
            }

            if (lastGoalIndicator == BetTeamValue.NumberOne)
            {
                homeFGLG += lastTextName;
            }

            if (lastGoalIndicator == BetTeamValue.NumberTwo)
            {
                awayFGLG += lastTextName;
            }
        }

        private static bool IsCashOutBetTicket(ITicket ticket) => ticket.BetdaqId == 1 && !IsBetTrade(ticket.BetTypeId);

        private static bool IsOutRight(int betTypeId) => betTypeId == BetTypes.Outright;

        private static bool IsBetTrade(int betTypeId) => betTypeId == BetTypes.Sold || betTypeId == BetTypes.Bought;

        private static bool IsSportsbook(int sportTypeId)
        {
            const int soccer = 1;
            const int otherSports = 99;

            return sportTypeId >= soccer && sportTypeId <= otherSports;
        }

        #endregion Private function
    }
}