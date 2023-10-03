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

            return GetMatchInfoOfVS3(matchCode, ticketHelper);
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

        private static string GetMatchInfoOfVS3(string matchCode, ITicketHelper ticketHelper)
        {
            var str1 = matchCode;
            var chArray = new[] { '{' };
            foreach (string str2 in str1.Split(chArray))
            {
                var length = str2.IndexOf('}');
                if (length != -1)
                {
                    var ResourceId = str2.Substring(0, length);
                    var refIdAndResourceId = ticketHelper.GetResourceData("LVS_MatchCode", ResourceId);
                    matchCode = matchCode.Replace("{" + ResourceId + "}", refIdAndResourceId);
                }
            }

            return matchCode;
        }
    }
}