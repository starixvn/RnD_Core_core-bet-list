namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Entities;
    using System;
    using System.Text.RegularExpressions;
    using Templates;

    internal static class VirtualSportsChoiceUtils
    {
        private const string MatchInfoPattern = "m=\"([^\"]*)\"";
        private const string ExcludingScoresPattern = "s=\"([^\"]*)\"";
        internal const string VS3MatchCode = "LVS_MatchCode";

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


        public static string ParseMatchInfo3(string transDesc, ITicketHelper ticketHelper)
        {
            var matchCode = ParseNeccessaryInfoFromTransDesc(transDesc, MatchInfoPattern);
            
            return ticketHelper.GetVirtualSportMatchCodeFormat(matchCode, VS3MatchCode);
        }
        public static string ParseMatchInfo(string transDesc, ITicketHelper ticketHelper)
        {
            var matchCode = ParseNeccessaryInfoFromTransDesc(transDesc, MatchInfoPattern);

            return ticketHelper.GetBetRadarMatchCodeFormat(matchCode);
        }

        public static string ParseMatchInfo(string refId, string transDesc, ITicketHelper ticketHelper)
        {
            var matchCode = ParseNeccessaryInfoFromTransDesc(transDesc, MatchInfoPattern);

            return ticketHelper.GetVirtualSportMatchCodeFormat(matchCode, refId);
        }

        public static string ParseExcludingScoresInfo(string transDesc)
            => ParseNeccessaryInfoFromTransDesc(transDesc, ExcludingScoresPattern);

        public static string ParseNeccessaryInfoFromTransDesc(string transDesc)
        => ParseNeccessaryInfoFromTransDesc(transDesc, MatchInfoPattern);

        public static string ParseNeccessaryInfoFromTransDesc(string transDesc, string pattern)
        {
            if (!string.IsNullOrEmpty(transDesc))
            {
                const int expectedNumberOfTransDescParts = 2;

                var match = Regex.Match(transDesc, pattern, RegexOptions.IgnoreCase);

                if (match?.Groups.Count >= expectedNumberOfTransDescParts)
                {
                    return match.Groups[1].Value;
                }
            }

            return string.Empty;
        }
    }
}