namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Text.RegularExpressions;
    using Entities;
    using Templates;

    internal static class VirtualSportsChoiceUtils
    {
        private const string MatchInfoPattern = "m=\"([^\"]*)\"";
        private const string ExcludingScoresPattern = "s=\"([^\"]*)\"";

        public static void AppendKickOffTime(this Choice_Template template, ITicket ticket)
            => template.Match.Append($"<div class=\"match\">{ParseKickOffTime(ticket)}</div>");

        public static string ParseKickOffTime(ITicket ticket)
        {
            const string ExpectedDateTimeFormat = "M/d/yyyy hh:mm tt";
            DateTime eventTime;

            return DateTime.TryParse(ticket.ShowTime, out eventTime) ? eventTime.ToString(ExpectedDateTimeFormat) : ticket.ShowTime;
        }

        public static void AppendMatchOrRoundInfo(this Choice_Template template, string matchRoundInfo)
            => template.Match.Append($"<div class=\"match\">{matchRoundInfo}</div>");

        public static string ParseMatchInfo(string transDesc, ITicketHelper ticketHelper)
        {
            string matchCode = ParseNeccessaryInfoFromTransDesc(transDesc, MatchInfoPattern);

            return ticketHelper.GetBetRadarMatchCodeFormat(matchCode);
        }

        public static string ParseExcludingScoresInfo(string transDesc)
            => ParseNeccessaryInfoFromTransDesc(transDesc, ExcludingScoresPattern);

        private static string ParseNeccessaryInfoFromTransDesc(string transDesc, string pattern)
        {
            if (!string.IsNullOrEmpty(transDesc))
            {
                Match match = Regex.Match(transDesc, pattern, RegexOptions.IgnoreCase);

                if (match?.Groups.Count >= 2)
                {
                    return match.Groups[1].Value;
                }
            }

            return string.Empty;
        }
    }
}