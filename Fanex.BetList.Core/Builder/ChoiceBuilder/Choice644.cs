namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Quarter Correct Score.
    /// </summary>
    public class Choice644 : Choice1
    {
        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var choice = string.IsNullOrEmpty(ticket.BetTeam) ? string.Empty : ticket.BetTeam.Replace(":", "-");

            // "&nbsp" because export format
            Template.betTeam = choice.ToUpperInvariant();
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
            var choice = Template.betTeam;

            if (Template.Score != null && Template.Score.Visible && ticket.IsLive)
            {
                choice += $" [{CoreBetList.Live}]";
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

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.Handicap.handicap = null;
            Template.betTeamClassName = Favorite;
        }

        private static bool IsAOS(ITicket ticket)
            => BetTeamValue.AOS == ticket.BetTeam.ToLowerInvariant() && !string.IsNullOrEmpty(ticket.TransDesc);

        private static string FormatTransDesc(string transDesc)
            => Regex.Replace(transDesc, ",\\s*", ", ").Replace(":", "-");

        private static string BuildExcludingBlock(string transDesc)
            => $"<div class=\"excluding-choices\">{CoreBetList.Excluding}: {FormatTransDesc(transDesc)}</div>";
    }
}