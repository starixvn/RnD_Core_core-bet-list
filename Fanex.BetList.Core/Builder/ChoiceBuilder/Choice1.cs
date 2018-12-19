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
        /// <summary>
        /// The Underdog = "underdog".
        /// </summary>
        protected const string Underdog = "underdog";

        /// <summary>
        /// The Favorite = "favorite".
        /// </summary>
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
        private const string ESportXmlTag = "p16";
        private const string InPlayXmlAttribute = "in";

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

        /// <summary>
        /// Updates all match member to null.
        /// </summary>
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

        /// <summary>
        /// Builds the RTF choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <returns>Return RichTextString.</returns>
        protected virtual IRichTextString BuildRTFChoice(ITicket ticket, RTFHelper rtfHelper, ITicketHelper ticketHelper)
        {
            var isMixParlay = false;

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

        #endregion Render Line Excel

        #region Build Html Block

        /// <summary>
        /// Builds the choice.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
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
        }

        /// <summary>
        /// Builds the bet team.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        protected virtual void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var awayTeamName = ticketHelper.GetTeamNameById(ticket.AwayId);
            var homeTeamName = GetHomeTeamName(ticket, ticketHelper);

            var isBetHomeTeam = IsBetHomeTeam(ticket);
            Template.betTeam = isBetHomeTeam ? homeTeamName : awayTeamName;
        }

        /// <summary>
        /// Builds the bet team class name and handicap.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
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

        /// <summary>
        /// Builds the score.
        /// Bettype 210, 413, 416 only support soccer
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        protected virtual void BuildScore(ITicket ticket)
        {
            if (Template.Score == null)
            {
                return;
            }

            if (IsShowLiveText(ticket.SportTypeId))
            {
                Template.Score.SetValue($"<span class=\"{Favorite}\"> [{GetScoreText(ticket.ExtraInfo)}]</span>");
            }
            else
            {
                Template.Score.homeScore = ticket.LiveHomeScore.ToString();
                Template.Score.awayScore = ticket.LiveAwayScore.ToString();
            }

            Template.Score.Visible = ticket.IsLive;
        }

        private static string GetScoreText(string extraInfo)
        {
            var scoreText = CoreBetList.Live;

            if (extraInfo.HasAttributeInXmlTag(ESportXmlTag, InPlayXmlAttribute))
            {
                var isInPlay = extraInfo.GetXmlTagAttributeValue(ESportXmlTag, InPlayXmlAttribute) == "1";

                if (isInPlay)
                {
                    scoreText = CoreBetList.InPlay;
                }
            }

            return scoreText;
        }

        private bool IsShowLiveText(int sportTypeId)
        {
            const int MaxSportsbookSportTypeId = 99;

            return sportTypeId > 0 &&
                sportTypeId <= MaxSportsbookSportTypeId &&
                !_liveScoreSports.Contains(sportTypeId);
        }

        /// <summary>
        /// Builds the type of the bet.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
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

        /// <summary>
        /// Builds the match.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
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

        /// <summary>
        /// Builds the league.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        protected virtual void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (Template.League == null
                || Template.League.LeagueName == null)
            {
                return;
            }

            Template.League.LeagueName.leagueName = ticketHelper.GetLeagueNameById(ticket.LeagueId);
        }

        /// <summary>
        /// Builds the sport.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        protected virtual void BuildSport(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (Template.League == null)
            {
                return;
            }

            Template.League.sportTypeName = ticketHelper.GetSportNameById(ticket.SportTypeId);
        }

        /// <summary>
        /// Builds the ticket status.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
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
        /// Builds the odds.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
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

        /// <summary>
        /// Builds the bet figure.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <param name="isMixParlay">If set to <c>true</c> [is mix parlay].</param>
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

        /// <summary>
        /// Builds the name of the bet type.
        /// </summary>
        /// <param name="rtfHelper">The RTF helper.</param>
        protected virtual void BuildBetTypeName(ref RTFHelper rtfHelper)
        {
            var choice = string.Empty;

            if (Template.BetType != null && !string.IsNullOrEmpty(Template.BetType.betTypeName))
            {
                choice = Template.BetType.betTypeName.Replace(HtmlCharacters.NoneBreakingSpace, Space);
                rtfHelper.RTFRenderer.AddText("\n" + choice);
            }
        }

        /// <summary>
        /// Builds the bet teams information.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
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

        /// <summary>
        /// Builds the match information.
        /// </summary>
        /// <param name="rtfHelper">The RTF helper.</param>
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

        /// <summary>
        /// Gets the bet status part.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="partName">Name of the part.</param>
        /// <returns>Return the bet status array.</returns>
        private static string[] GetBetStatusPart(string[] source, string partName)
        {
            var part = source?.FirstOrDefault(item => item.StartsWith(partName, StringComparison.InvariantCulture)) ?? string.Empty;
            var partValue = part.Split(new[] { ':', '-' });

            return partValue;
        }

        /// <summary>
        /// Calculates the FGLG label.
        /// </summary>
        /// <param name="firstClassName">First name of the class.</param>
        /// <param name="lastClassName">Last name of the class.</param>
        /// <param name="statusPart">The status part.</param>
        /// <param name="homeFGLG">The home FGLG.</param>
        /// <param name="awayFGLG">The away FGLG.</param>
        private static void CalculateFG_LGLabel(string firstClassName, string lastClassName, string[] statusPart, ref string homeFGLG, ref string awayFGLG)
        {
            if (statusPart.Length != 3)
            {
                return;
            }

            if (statusPart[1] == BetTeamValue.NumberOne)
            {
                homeFGLG += string.Join(null, new[] { "<div class='", firstClassName, "'>&nbsp;</div>" });
            }

            if (statusPart[1] == BetTeamValue.NumberTwo)
            {
                awayFGLG += string.Join(null, new[] { "<div class='", firstClassName, "'>&nbsp;</div>" });
            }

            if (statusPart[2] == BetTeamValue.NumberOne)
            {
                homeFGLG += string.Join(null, new[] { "<div class='", lastClassName, "'>&nbsp;</div>" });
            }

            if (statusPart[2] == BetTeamValue.NumberTwo)
            {
                awayFGLG += string.Join(null, new[] { "<div class='", lastClassName, "'>&nbsp;</div>" });
            }
        }

        /// <summary>
        /// Updates the f g_ l g_ excel.
        /// </summary>
        /// <param name="firstTextName">First name of the text.</param>
        /// <param name="lastTextName">Last name of the text.</param>
        /// <param name="statusPart">The status part.</param>
        /// <param name="homeFGLG">The home FGLG.</param>
        /// <param name="awayFGLG">The away FGLG.</param>
        private static void UpdateFG_LG_Excel(string firstTextName, string lastTextName, string[] statusPart, ref string homeFGLG, ref string awayFGLG)
        {
            if (statusPart.Length != 3)
            {
                return;
            }

            if (statusPart[1] == "1")
            {
                homeFGLG += firstTextName;
            }

            if (statusPart[1] == "2")
            {
                awayFGLG += firstTextName;
            }

            if (statusPart[2] == "1")
            {
                homeFGLG += lastTextName;
            }

            if (statusPart[2] == "2")
            {
                awayFGLG += lastTextName;
            }
        }

        private static bool IsCashOutBetTicket(ITicket ticket) => ticket.BetdaqId == 1 && !IsBetTrade(ticket.BetTypeId);

        private static bool IsOutRight(int betTypeId) => betTypeId == 10;

        private static bool IsBetTrade(int betTypeId) => betTypeId == 901 || betTypeId == 902;

        #endregion Private function
    }
}