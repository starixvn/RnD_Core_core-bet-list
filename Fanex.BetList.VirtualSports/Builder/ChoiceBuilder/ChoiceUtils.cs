namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using Entities;
    using Templates;

    internal static class VirtualSportsChoiceUtils
    {
        public static void AppendKickOffTime(this Choice_Template template, ITicket ticket)
            => template.Match.Append($"<div class=\"match\">{ParseKickOffTime(ticket)}</div>");

        public static void AppendMatchOrRoundInfo(this Choice_Template template, ITicket ticket)
            => template.Match.Append($"<div class=\"match\">{ticket.TransDesc}</div>");

        public static string ParseKickOffTime(ITicket ticket)
        {
            const string ExpectedDateTimeFormat = "M/d/yyyy hh:mm tt";
            DateTime eventTime;

            return DateTime.TryParse(ticket.ShowTime, out eventTime) ? eventTime.ToString(ExpectedDateTimeFormat) : ticket.ShowTime;
        }
    }
}