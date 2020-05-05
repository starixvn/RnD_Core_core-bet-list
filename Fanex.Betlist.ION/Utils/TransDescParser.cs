namespace Fanex.BetList.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    internal static class TransDescParser
    {
        public static IDictionary<string, string> ParseTransDesc(this ITicket ticket)
            => ticket.IsSummaryTicket()
            ? new Dictionary<string, string>()
            : ParseTransDesc(ticket.TransDesc);

        public static bool IsSummaryTicket(this ITicket ticket)
            => string.IsNullOrWhiteSpace(ticket.BetCheck);

        private static IDictionary<string, string> ParseTransDesc(string transDesc)
        {
            const int numberOfExpressionsOfEachProperty = 2;

            var transDescItems = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var splittedTransDescParts = Split(transDesc, '*');

            foreach (var part in splittedTransDescParts)
            {
                var keyValuePair = Split(part, '=');

                if (keyValuePair.Length == numberOfExpressionsOfEachProperty)
                {
                    transDescItems.Add(keyValuePair[0], keyValuePair[1]);
                }
            }

            return transDescItems;
        }

        private static string[] Split(string value, char separator)
            => value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
    }
}