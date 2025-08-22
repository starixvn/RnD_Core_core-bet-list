namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using BetListNPOI.SS.UserModel;
    using Templates;
    using Utils;
    using Utils.StringExt;
    using Utils.TicketExt;

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
        private readonly int[] _liveScoreSports = { SportsbookSportTypes.Soccer, SportsbookSportTypes.Futsal, SportsbookSportTypes.BeachSoccer, SportsbookSportTypes.VirtuallySoccer };
        private const string ESportsXmlTag = "p16";
        private const string InPlayXmlAttribute = "in";
        private const string MaxPayoutXmlTag = "l255";

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
            homeTeamName = string.Join(null, homeTeamName, ticket.IsNeutral ? neutral : string.Empty);

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
        /// <returns>A tuple containing the home and away FGLG values.</returns>
        protected virtual (string homeFGLG, string awayFGLG) BuildFG_LGLabel(string betStatus)
        {
            if (string.IsNullOrWhiteSpace(betStatus))
            {
                return (string.Empty, string.Empty);
            }

            string[] betStatusParts = betStatus.ToLowerInvariant().Split(';');
            string homeFGLG = string.Empty;
            string awayFGLG = string.Empty;

            string[] fglg = GetBetStatusPart(betStatusParts, BetStatusFl);
            var (homeFGLG1, awayFGLG1) = CalculateFG_LGLabel(FirstGoalCssClass, LastGoalCssClass, fglg);
            homeFGLG += homeFGLG1;
            awayFGLG += awayFGLG1;

            string[] fhfglg = GetBetStatusPart(betStatusParts, BetStatus1Fl);
            var (homeFGLG2, awayFGLG2) = CalculateFG_LGLabel(FirstHalfFirstGoalCssClass, FirstHalfLastGoalCssClass, fhfglg);
            homeFGLG += homeFGLG2;
            awayFGLG += awayFGLG2;

            if (!string.IsNullOrEmpty(homeFGLG))
            {
                homeFGLG = string.Join(null, HtmlCharacters.NoneBreakingSpace, homeFGLG);
            }

            if (!string.IsNullOrEmpty(awayFGLG))
            {
                awayFGLG = string.Join(null, HtmlCharacters.NoneBreakingSpace, awayFGLG);
            }

            return (homeFGLG, awayFGLG);
        }



        /// <summary>
        /// Calculates the (F) (L) (1F) (1L) label for Excel.
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
        /// <returns>A tuple containing the home and away FGLG values.</returns>
        protected virtual (string homeFGLG, string awayFGLG) CalculateFG_LG_Excel(string betStatus)
        {
            if (betStatus == null)
            {
                return (string.Empty, string.Empty);
            }

            string[] betStatusParts = betStatus.ToLowerInvariant().Split(';');
            string homeFGLG = string.Empty;
            string awayFGLG = string.Empty;

            string[] fglg = GetBetStatusPart(betStatusParts, BetStatusFl);
            var (homeFGLG1, awayFGLG1) = UpdateFG_LG_Excel(FirstGoalTextName, LastGoalTextName, fglg);
            homeFGLG += homeFGLG1;
            awayFGLG += awayFGLG1;

            string[] fhfglg = GetBetStatusPart(betStatusParts, BetStatus1Fl);
            var (homeFGLG2, awayFGLG2) = UpdateFG_LG_Excel(FirstHalfFirstGoalTextName, FirstHalfLastGoalTextName, fhfglg);
            homeFGLG += homeFGLG2;
            awayFGLG += awayFGLG2;

            return (homeFGLG, awayFGLG);
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

            // the 6th line
            BuildMaxPayoutInfo(rtfHelper);

            // the 7th line
            BuildBookedInfo(rtfHelper);

            // the 8th line
            BuildVoucherIdInfo(rtfHelper);

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

            BuildMaxPayout(ticket);

            BuildBooked(ticket);

            BuildVoucherId(ticket);
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

            if (IsShowLiveText(ticket.SportTypeId, ticket.TransDate))
            {
                var scoreStatus = ticket.SportTypeId == SportsbookSportTypes.ESports ? GetScoreStatus(ticket) : CoreBetList.Live;

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

        private bool IsShowLiveText(int sportTypeId, DateTime transDate)
        {
            if (LiveScoreSportType.DateTimeSupportLiveScoreBySports.ContainsKey(sportTypeId))
            {
                return transDate >= LiveScoreSportType.DateTimeSupportLiveScoreBySports[sportTypeId];
            }

            return sportTypeId >= SportsbookSportTypes.Soccer &&
                sportTypeId <= SportsbookSportTypes.MaxSportsbookSportType &&
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

            var (homeFGLG, awayFGLG) = BuildFG_LGLabel(ticket.EventStatus);
            Template.Match.home_firstGoal_lastGoal = homeFGLG;
            Template.Match.away_firstGoal_lastGoal = awayFGLG;
        }

        protected virtual void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            if (Template.League == null
                || Template.League.LeagueName == null)
            {
                return;
            }

            Template.League.LeagueName.leagueName = ticketHelper.HideSabaText ?
            ticketHelper.GetLeagueNameById(ticket.LeagueId, ticket.LeagueGroupId) :
            ticketHelper.GetLeagueNameById(ticket.LeagueId);
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
            if (ticket.IsSportsbook() && ticket.EventDate.HasValue)
            {
                Template.EventDate.Show();
                Template.EventDate.eventDate = Formatter.FormatDateTimeEn12ShortMonthNoSec(ticket.EventDate);
            }
            else
            {
                Template.EventDate.Hide();
            }
        }

        protected virtual void BuildMaxPayout(ITicket ticket)
        {
            if (string.Equals(ticket.Status, BetStatus.Won, StringComparison.OrdinalIgnoreCase)
                && ticket.ExtraInfo.HasXmlTag(MaxPayoutXmlTag))
            {
                Template.MaxPayout.maxPayout = string.Format("({0})", CoreBetList.MaxPayoutAlert);
            }
            else
            {
                Template.MaxPayout.Hide();
            }
        }

        protected virtual void BuildVoucherId(ITicket ticket)
        {
            if (ticket.ExtraInfo.HasAttributeInXmlTag(CommonValue.SportsbookTagName, "bb")
                    && ticket.ExtraInfo.HasAttributeInXmlTag(CommonValue.SportsbookTagName, "obt")
                    && ticket.ExtraInfo.GetXmlTagAttributeValue(CommonValue.SportsbookTagName, "obt") == "3")
            {
                Template.VoucherId.voucherId = ticket.ExtraInfo.GetXmlTagAttributeValue(CommonValue.SportsbookTagName, "vid");
            }
            else
            {
                Template.VoucherId.Hide();
            }
        }

        protected virtual void BuildBooked(ITicket ticket)
        {
            if (ticket.ExtraInfo.HasAttributeInXmlTag(CommonValue.SportsbookTagName, CommonValue.BookedBetAttribute))
            {
                Template.Booked.Show();
            }
            else
            {
                Template.Booked.Hide();
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
                string odds = ConvertByBetType.Odds(ticket.Odds, ticket.BetTypeId, ticket.SportTypeId, oddsType);

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
                    BuildMyanmarOdds(ticket);
                }
            }
        }

        protected virtual void BuildBetFigure(ITicket ticket, ref RTFHelper rtfHelper, bool isMixParlay)
        {
            string choice = Template.betTeam;
            var htmlTagMatching = new Regex("</?[^<>]*>");

            if (Template.Handicap != null && Template.Handicap.Visible && Template.Handicap.handicap != null)
            {
                Template.Handicap.handicap = Template.Handicap.handicap.Replace("<span class='favorite'>", string.Empty);
                Template.Handicap.handicap = Template.Handicap.handicap.Replace("<span class='sport'>", string.Empty);
                Template.Handicap.handicap = Template.Handicap.handicap.Replace("</span>", string.Empty);
                choice += string.Join(null, Space, Template.Handicap.handicap);
            }

            if (Template.Score != null && Template.Score.Visible)
            {
                choice += htmlTagMatching.Replace(Template.Score.ToString(), string.Empty);
            }

            if (isMixParlay)
            {
                choice += string.Join(null, " @ ", Formatter.FormatNumber3(ticket.Odds, null));
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

                BuildHomeTeam(rtfHelper);

                BuildTicketFG_LG(ticket);

                BuildHomeTeamFG_LG(rtfHelper);

                var isAddNewLine = BuildAwayTeam(rtfHelper);

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

        protected virtual void BuildMaxPayoutInfo(RTFHelper rtfHelper)
        {
            if (Template.MaxPayout != null
                && Template.MaxPayout.Visible
                && !string.IsNullOrEmpty(Template.MaxPayout.maxPayout))
            {
                rtfHelper.RTFRenderer.AddText("\n" + Template.MaxPayout.maxPayout);
            }
        }

        protected virtual void BuildVoucherIdInfo(RTFHelper rtfHelper)
        {
            if (Template.VoucherId != null && Template.VoucherId.Visible)
            {
                rtfHelper.RTFRenderer.AddText("\n" + $"{CoreBetList.voucherid} {Template.VoucherId.voucherId}");
            }
        }

        protected virtual void BuildBookedInfo(RTFHelper rtfHelper)
        {
            if (Template.Booked != null
                && Template.Booked.Visible)
            {
                rtfHelper.RTFRenderer.AddText("\n" + CoreBetList.Booked);
            }
        }

        private void BuildHomeTeam(RTFHelper rtfHelper)
        {
            var choice = string.Empty;

            if (!string.IsNullOrEmpty(Template.Match.homeTeam))
            {
                choice = Template.Match.homeTeam.Replace(HtmlCharacters.NoneBreakingSpace, Space);
            }

            if (!string.IsNullOrEmpty(choice))
            {
                rtfHelper.RTFRenderer.AddText("\n" + choice + Space);
            }
        }

        private void BuildTicketFG_LG(ITicket ticket)
        {
            if (ticket.EventStatus != null && Template.Match != null)
            {
                var (homeFGLG, awayFGLG) = CalculateFG_LG_Excel(ticket.EventStatus);
                Template.Match.home_firstGoal_lastGoal = homeFGLG;
                Template.Match.away_firstGoal_lastGoal = awayFGLG;
            }
        }

        private void BuildHomeTeamFG_LG(RTFHelper rtfHelper)
        {
            var choice = string.Empty;

            if (!string.IsNullOrEmpty(Template.Match.home_firstGoal_lastGoal))
            {
                choice = string.Join(null, Template.Match.home_firstGoal_lastGoal, Space);
            }

            if (!string.IsNullOrEmpty(choice))
            {
                choice = choice.Replace(HtmlCharacters.NoneBreakingSpace, Space);
                rtfHelper.RTFRenderer.AddText(choice, rtfHelper.PosFont);
            }
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
                choice += string.Join(null, Space, Template.Match.awayTeam);
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
            var part = source != null ? Array.Find(source, item => item.StartsWith(partName, StringComparison.InvariantCulture)) ?? string.Empty : string.Empty;
            var partValue = part.Split(':', '-');

            return partValue;
        }

        private static (string homeFGLG, string awayFGLG) CalculateFG_LGLabel(string firstClassName, string lastClassName, string[] statusPart)
        {
            const string goalIndicatorHtmlFormat = "<div class='{0}'>&nbsp;</div>";
            const int numberOfExpectedIndicators = 3;

            if (statusPart.Length != numberOfExpectedIndicators)
            {
                return (string.Empty, string.Empty);
            }

            string homeFGLG = string.Empty;
            string awayFGLG = string.Empty;

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

            return (homeFGLG, awayFGLG);
        }

        private static (string homeFGLG, string awayFGLG) UpdateFG_LG_Excel(string firstTextName, string lastTextName, string[] statusPart)
        {
            const int numberOfExpectedIndicators = 3;

            if (statusPart.Length != numberOfExpectedIndicators)
            {
                return (string.Empty, string.Empty);
            }

            string homeFGLG = string.Empty;
            string awayFGLG = string.Empty;

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

            return (homeFGLG, awayFGLG);
        }

        private static bool IsCashOutBetTicket(ITicket ticket) => ticket.BetdaqId == 1 && !IsBetTrade(ticket.BetTypeId);

        private static bool IsOutRight(int betTypeId) => betTypeId == BetTypes.Outright;

        private static bool IsBetTrade(int betTypeId) => betTypeId == BetTypes.Sold || betTypeId == BetTypes.Bought;

        private void BuildMyanmarOdds(ITicket ticket)
        {
            var oddsType = string.IsNullOrEmpty(ticket.OddsType) ? "0" : ticket.OddsType;
            var result = Int32.TryParse(oddsType, out int oddsTypeInt);
            if (result && oddsTypeInt == OddsTypes.Myanmar)
            {
                const string percentageFormat = " <span class='{0}'>({1})</span>";

                var percentageClass = ticket.OddsSpread < 0 ? Favorite : "sport";
                var percentageValue = Formatter.DecFormat(ticket.OddsSpread, 0);
                Template.Handicap.handicap += string.Format(percentageFormat, percentageClass, percentageValue);
            }
        }

        #endregion Private function
    }
}