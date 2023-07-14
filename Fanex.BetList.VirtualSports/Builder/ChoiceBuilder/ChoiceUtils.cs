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


        public static string ParseMatchInfo3(string transDesc, ITicketHelper ticketHelper)
        {
            var matchCode = ParseNeccessaryInfoFromTransDesc(transDesc, MatchInfoPattern);
            var matchDay = ticketHelper.GetResourceData("LVS_MatchCode", "2");
            var cup = ticketHelper.GetResourceData("LVS_MatchCode", "1");
            var items = matchCode.Split(',');            
            if (items.Length >= 3)
            {
                var code = items[1].Trim();
                var groupCode = code.Replace("{", string.Empty).Replace("}", string.Empty);
                var groupName = ticketHelper.GetResourceData("LVS_MatchCode", groupCode);
                matchCode = matchCode.Replace(code, groupName);
            }

            return matchCode.Replace("{1}", cup).Replace("{2}", matchDay);
        }
        public static string ParseMatchInfo(string transDesc, ITicketHelper ticketHelper)
        {
            var matchCode = ParseNeccessaryInfoFromTransDesc(transDesc, MatchInfoPattern);

            return ticketHelper.GetBetRadarMatchCodeFormat(matchCode);
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