namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using Entities;

    /// <summary>
    /// Virtual Soccer - Over/Under
    /// </summary>
    public class Choice1203 : Choice3
    {
        private const string DateTimeFormat = "M/d/yyyy hh:mm tt";
        private const string KickOffTimeFormat = "<div class=\"match\">{0}</div>";

        protected override void BuildMatch(ITicket ticket, ITicketHelper ticketHelper)
        {
            base.BuildMatch(ticket, ticketHelper);

            Template.Match.Append(string.Format(KickOffTimeFormat, GetKickOffTime(ticket)));
        }

        protected override void BuildBetTeamsInfo(ITicket ticket, ref RTFHelper rtfHelper)
        {
            base.BuildBetTeamsInfo(ticket, ref rtfHelper);

            rtfHelper.RTFRenderer.AddText($"\n{GetKickOffTime(ticket)}");
        }

        private string GetKickOffTime(ITicket ticket)
        {
            DateTime eventTime;
            var kickOffTime = DateTime.TryParse(ticket.ShowTime, out eventTime)
                ? eventTime.ToString(DateTimeFormat)
                : ticket.ShowTime;

            return kickOffTime;
        }
    }
}