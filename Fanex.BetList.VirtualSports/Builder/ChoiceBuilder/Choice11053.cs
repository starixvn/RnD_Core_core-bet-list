namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Constants;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Utils;
    using System.Text.RegularExpressions;
    using Entities;
    using Fanex.BetList.Core.Templates;

    public class Choice11053 : Choice11050
    {
        private string _excludingScores;

         public override Choice_Template Render(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData, bool isShowScoreMap)
        {
            _excludingScores = VirtualSportsChoiceUtils.ParseExcludingScoresInfo(ticket.TransDesc);

            return base.Render(ticket, ticketHelper, ticketData, isShowScoreMap);
        } 

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var choice = ticket.BetTeam.ToLowerInvariant();

            Template.betTeam = BetTeamValue.AOS == choice ? CoreBetList.AOS : choice.Replace(":", "-");
        }

        protected override void BuildScore(ITicket ticket)
        {
            if (Template.Score == null)
            {
                return;
            }

            base.BuildScore(ticket);

            if (IsAOS(ticket))
            {
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

        protected override void BuildBetFigure(ITicket ticket, ref RTFHelper rtfHelper, bool isMixParlay)
        {
            string choice = Template.betTeam;

            if (Template.Handicap != null && Template.Handicap.Visible && Template.Handicap.handicap != null)
            {
                choice += $" {Template.Handicap.handicap}";
            }

            if (Template.Score != null && Template.Score.Visible && ticket.IsLive)
            {
                choice += $" [{Template.Score.homeScore}-{Template.Score.awayScore}]";
            }

            if (isMixParlay)
            {
                choice += $" @{Formatter.FormatNumber3(ticket.Odds, null)}";
            }

            if (IsAOS(ticket))
            {
                choice += $"\n{CoreBetList.Excluding}: {BuildExcludingBlock(_excludingScores)}";
            }

            if (!string.IsNullOrEmpty(choice))
            {
                rtfHelper.RTFRenderer.AddText(choice, rtfHelper.PosFont);
            }
        }

        protected virtual bool IsAOS(ITicket ticket)
            => BetTeamValue.AOS == ticket.BetTeam.ToLowerInvariant() && !string.IsNullOrEmpty(ticket.TransDesc);

        private static string FormatTransDesc(string transDesc)
            => Regex.Replace(transDesc, ",\\s*", ", ").Replace(":", "-");

        private static string BuildExcludingBlock(string transDesc)
        => $"<div class=\"excluding-choices\">{CoreBetList.Excluding}: {FormatTransDesc(transDesc)}</div>";
    }
}