namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Templates;

    /// <summary>
    /// BetRadar Virtual Sports - Correct Score.
    /// </summary>
    public class Choice2707 : Choice4
    {
        private string _matchRoundInfo;
        private string _excludingScores;

        public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            _matchRoundInfo = VirtualSportsChoiceUtils.ParseMatchInfo(ticket.TransDesc, ticketHelper);
            _excludingScores = VirtualSportsChoiceUtils.ParseExcludingScoresInfo(ticket.TransDesc);

            return base.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        }

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            string choice = ticket.BetTeam;

            choice = BetTeamValue.AOS == choice.ToLowerInvariant() ? CoreBetList.AOS : choice.Replace(':', '-');
            Template.betTeam = choice;
        }

        protected override void BuildScore(ITicket ticket)
        {
            if (Template.Score == null)
            {
                return;
            }

            base.BuildScore(ticket);

            if (BetTeamValue.AOS == ticket.BetTeam.ToLowerInvariant())
            {
                if (string.IsNullOrEmpty(_excludingScores))
                {
                    return;
                }

                if (Template.Score.Visible)
                {
                    Template.Score.Append(BuildExcludingBlock(_excludingScores));
                }
                else
                {
                    Template.Score.Visible = true;
                    Template.Score.SetValue(BuildExcludingBlock(_excludingScores));
                }
            }
        }

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildMatch(ticket, ticketHelper);

            Template.AppendMatchOrRoundInfo(_matchRoundInfo);
        }

        protected override void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            base.BuildBetTeamsInfo(ticket, ref rtfHelper);

            rtfHelper.RTFRenderer.AddText($"\n{_matchRoundInfo}");
        }

        protected override void BuildBetFigure(ITicket ticket, ref RTFHelper rtfHelper, bool isMixParlay)
        {
            rtfHelper.RTFRenderer.AddText(Template.betTeam, rtfHelper.PosFont);

            if (BetTeamValue.AOS == ticket.BetTeam.ToLowerInvariant() && !string.IsNullOrEmpty(_excludingScores))
            {
                rtfHelper.RTFRenderer.AddText($"\n{CoreBetList.Excluding}: {FormatExcludingList(_excludingScores)}", rtfHelper.PosFont);
            }
        }

        private static string BuildExcludingBlock(string excludingList)
            => $"<div class=\"excluding-choices\">{CoreBetList.Excluding}: {FormatExcludingList(excludingList)}</div>";

        private static string FormatExcludingList(string excludingList)
            => Regex.Replace(excludingList, ",\\s*", ", ").Replace(":", "-");
    }
}