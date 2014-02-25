// ---------------------------------------------------------------------------------
// <copyright file="Choice1.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2012.11.16">Create</change>
//     <change who="Marc.Bui" date="2012.11.26">Add Odds to show in Mix Parlay bettypes (ones with RefNo != 100000000000)</change> 
//     <change who="Marc.Bui" date="2012.12.04">Add Excel rendering </change> 
//     <change who="Marc.Bui" date="2013.01.18">Update ScoreClassName to BLFAVORITE (red) for all bettypes</change>
//     <change who="Marc.Bui" date="2013.02.05">Update betteam when the match is Neutral </change> 
//     <change who="Marc.Bui" date="2013.05.06">Assign ticketStatus to void if tickets is void, use for Sub-tickets markup (Mix Parlay & System Parlay)</change>
//     <change who="Marc.Bui" date="2013.05.31">Implement export excel with detail data for *Parlay tickets</change>
//     <change who="Marc.Bui" date="2013.10.09">Check stylecop & refactor</change>
// </history>
// ---------------------------------------------------------------------------------

/// <summary>
/// The ChoiceBuilder namespace.
/// </summary>
namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Bet type name: Handicap.
    /// </summary>
    public class Choice1 : IChoice
    {
        #region Attributes

        protected const string BLUNDERDOG = "underdog";
        protected const string BLFAVORITE = "favorite";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Choice1" /> class.
        /// </summary>
        public Choice1()
        {
            this.Template = new Choice_Template();
        }

        #endregion

        #region Properties

        public Choice_Template Template { get; set; }

        #endregion

        /// <summary>
        /// Renders the specified ticket.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="isShowScoreMap">If set to <c>true</c> [the score map icon is shown].</param>
        /// <returns>Choice_Template: the whole Choice block built for input data.</returns>
        public virtual Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            this.BuildChoice(ticket, ticketHelper, ticketData);
            this.BuildHandicapOdds(ticket, ticketHelper);
            this.BuildScoreMap(ticket, isShowScoreMap);

            return this.Template;
        }

        /// <summary>
        /// Renders the RTF.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="isShowScoreMap">If set to <c>true</c> [is show score map].</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>List of IRichTextString for input data.</returns>
        public virtual List<IRichTextString> RenderRTF(
                                ITicket ticket,
                                ITicketHelper ticketHelper,
                                List<ITicketData> ticketData,
                                bool isShowScoreMap,
                                RTFHelper rtfHelper)
        {
            this.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
            var choiceData = BuildRTFChoice(ticket, rtfHelper, ticketHelper);
            List<IRichTextString> rtfChoice;

            if (choiceData is List<IRichTextString>)
            {
                rtfChoice = (List<IRichTextString>)choiceData;
            }
            else
            {
                rtfChoice = new List<IRichTextString>() { (IRichTextString)choiceData };
            }

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
            this.Template.Score.homeScore = ticket.LiveHomeScore.ToString();
            this.Template.Score.awayScore = ticket.LiveAwayScore.ToString();

            this.Template.League.LeagueName.leagueName = ticketHelper.GetLeagueNameById(ticket.LeagueId);

            string homeName = ticketHelper.GetTeamNameById(ticket.HomeId);
            string awayName = ticketHelper.GetTeamNameById(ticket.AwayId);

            this.Template.Score.Visible = ticket.IsLive;
            this.Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);
            this.Template.Match.homeTeam = string.Join(null, new string[] { homeName, ticket.IsNeutral ? " (N) " : string.Empty });

            this.Template.Match.awayTeam = awayName;
            this.Template.League.sportTypeName = ticketHelper.GetSportNameById(ticket.SportTypeId);

            this.Template.Match.home_firstGoal_lastGoal = string.Empty;
            this.Template.Match.away_firstGoal_lastGoal = string.Empty;

            this.CalculateFG_LG(ticket.EventStatus, ref this.Template.Match.home_firstGoal_lastGoal, ref this.Template.Match.away_firstGoal_lastGoal);

            bool betHomeTeam = ticket.BetTeam != null && ticket.BetTeam.Contains("h");

            this.Template.betTeam = betHomeTeam ? this.Template.Match.homeTeam : awayName;

            if (betHomeTeam)
            {
                this.Template.betTeamClassName = ticket.Handicap2 >= ticket.Handicap1 ? BLUNDERDOG : BLFAVORITE;
            }
            else
            {
                this.Template.betTeamClassName = ticket.Handicap2 <= ticket.Handicap1 ? BLUNDERDOG : BLFAVORITE;
            }

            this.Template.Score.scoreClassName = BLFAVORITE;

            decimal handicap = ticket.Handicap2 > ticket.Handicap1 ? ticket.Handicap2 : ticket.Handicap1;

            this.Template.Handicap.handicap = ConvertByBetType.Hdp((this.Template.betTeamClassName == BLFAVORITE) ? -handicap : handicap);

            if ((ticket.StatusId != null && ticket.StatusId.ToString().Trim() == BetStatusId.Void)
                || (ticket.Status != null && ticket.Status.Trim().ToLower() == BetStatus.Void))
            {
                this.Template.ticketStatus = "void";
            }
        }

        /// <summary>
        /// Calculates the F g_ LG.
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

            // Format: FL:1-2
            string[] args = betStatus.Split(new char[] { ':', '-' });

            if (args.Length != 3)
            {
                return;
            }

            if (args[1] == "1")
            {
                homeFGLG += "<div class=\"firstGoal\">&nbsp;</div>";
            }
            else if (args[1] == "2")
            {
                awayFGLG += "<div class=\"firstGoal\">&nbsp;</div>";
            }

            if (args[2] == "1")
            {
                homeFGLG += "<div class=\"lastGoal\">&nbsp;</div>";
            }
            else if (args[2] == "2")
            {
                awayFGLG += "<div class=\"lastGoal\">&nbsp;</div>";
            }

            if (!homeFGLG.Equals(string.Empty))
            {
                homeFGLG = string.Join(null, new string[] { "&nbsp;", homeFGLG });
            }

            if (!awayFGLG.Equals(string.Empty))
            {
                awayFGLG = string.Join(null, new string[] { "&nbsp;", awayFGLG });
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

            this.AdjustBetTeamToRTF();

            // the 1st line
            this.BuildBetFigure(ticket, ref rtfHelper, isMixParlay);

            // the 2nd line
            this.BuildBetTypeName(ref rtfHelper);

            // the 3rd line
            this.BuildBetTeamsInfo(ticket, ref rtfHelper);

            // the 4th line
            this.BuildMatchInfo(ref rtfHelper);

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

            // Format: FL:1-2
            string[] args = betStatus.Split(new char[] { ':', '-' });

            if (args.Length != 3)
            {
                return;
            }

            if (args[1] == "1")
            {
                homeFGLG += "(F)";
            }
            else if (args[1] == "2")
            {
                awayFGLG += "(F)";
            }

            if (args[2] == "1")
            {
                homeFGLG += "(L)";
            }
            else if (args[2] == "2")
            {
                awayFGLG += "(L)";
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
                this.Template.ScoreMapIcon.matchId = ticket.MatchId.ToString();
                this.Template.ScoreMapIcon.betTypeId = ticket.BetTypeId.ToString();
                this.Template.ScoreMapIcon.liveindicator = Convert.ToInt32(ticket.IsLive).ToString();
            }
            else
            {
                this.Template.ScoreMapIcon = null;
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
                    this.Template.Handicap.Odds = null;
                }
                else
                {
                    this.Template.Handicap.Odds.odds = odds;
                }
            }
            else
            {
                if (this.Template.Handicap != null)
                {
                    this.Template.Handicap.Odds = null;
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
                choice += string.Join(null, new string[] { " ", Template.Handicap.handicap });
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
                choice = choice.Replace("&nbsp;", " ");
                rtfHelper.RTFRenderer.AddText(choice, rtfHelper.PosFont);
            }
        }

        /// <summary>
        /// Builds the name of the bet type.
        /// </summary>
        /// <param name="rtfHelper">The RTF helper.</param>
        private void BuildBetTypeName(ref RTFHelper rtfHelper)
        {
            string choice = string.Empty;

            if (!string.IsNullOrEmpty(Template.BetType.betTypeName))
            {
                choice = Template.BetType.betTypeName.Replace("&nbsp;", " ");
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
            Justification = "The logic is simple so seperating to reduce fewer than 10 lines is not necessary.")]
        private void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            bool isAddNewLine = false;
            string choice = string.Empty;
            if (!string.IsNullOrEmpty(Template.Match.homeTeam))
            {
                choice = Template.Match.homeTeam.Replace("&nbsp;", " ");
            }

            if (!string.IsNullOrEmpty(choice))
            {
                isAddNewLine = true;
                rtfHelper.RTFRenderer.AddText("\n" + choice);
                choice = string.Empty;
            }

            this.BuildTicketFG_LG(ticket);

            if (!string.IsNullOrEmpty(Template.Match.home_firstGoal_lastGoal))
            {
                choice = string.Join(null, new string[] { " ", Template.Match.home_firstGoal_lastGoal });
            }

            if (!string.IsNullOrEmpty(choice))
            {
                isAddNewLine = true;
                choice = choice.Replace("&nbsp;", " ");
                rtfHelper.RTFRenderer.AddText(choice, rtfHelper.PosFont);
                choice = string.Empty;
            }
            
            if (Template.Match.VS != null && Template.Match.VS.Visible)
            {
                choice = "- vs -";
            }

            if (!string.IsNullOrEmpty(Template.Match.awayTeam))
            {
                choice += string.Join(null, new string[] { " ", Template.Match.awayTeam });
            }

            if (!string.IsNullOrEmpty(choice))
            {
                isAddNewLine = true;
                choice = choice.Replace("&nbsp;", " ");
                rtfHelper.RTFRenderer.AddText(choice);
                choice = string.Empty;
            }
            
            if (!string.IsNullOrEmpty(Template.Match.away_firstGoal_lastGoal))
            {
                choice = " " + Template.Match.away_firstGoal_lastGoal;
            }

            if (!string.IsNullOrEmpty(choice) || isAddNewLine)
            {
                choice = choice.Replace("&nbsp;", " ");
                rtfHelper.RTFRenderer.AddText(choice, rtfHelper.PosFont);
            }
        }

        /// <summary>
        /// Builds the match information.
        /// </summary>
        /// <param name="rtfHelper">The RTF helper.</param>
        private void BuildMatchInfo(ref RTFHelper rtfHelper)
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
                    choice += " ";
                }

                choice += Template.League.LeagueName.leagueName;
            }

            if (!string.IsNullOrEmpty(choice))
            {
                choice = choice.Replace("&nbsp;", " ");
                rtfHelper.RTFRenderer.AddText("\n" + choice);
            }
        }

        /// <summary>
        /// Builds First Goal, Last Goal information.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        private void BuildTicketFG_LG(ITicket ticket)
        {
            if (ticket.EventStatus != null)
            {
                Template.Match.home_firstGoal_lastGoal = string.Empty;
                Template.Match.away_firstGoal_lastGoal = string.Empty;
                this.CalculateFG_LG_Excel(ticket.EventStatus, ref Template.Match.home_firstGoal_lastGoal, ref Template.Match.away_firstGoal_lastGoal);
            }
        }
    }
}