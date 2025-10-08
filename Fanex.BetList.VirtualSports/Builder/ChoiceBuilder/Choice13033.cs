namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using App_GlobalResources;
    using Constants;
    using Entities;
    using Fanex.BetList.Core.Utils;
    public class Choice13033 : Choice13000
    {
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
                    Template.Score.Append(BuildExcludingBlock(ticket.TransDesc));
                }
                else
                {
                    Template.Score.Visible = true;
                    Template.Score.SetValue(BuildExcludingBlock(ticket.TransDesc));
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
                choice += $"\n{CoreBetList.Excluding}: {FormatTransDesc(ticket.TransDesc)}";
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