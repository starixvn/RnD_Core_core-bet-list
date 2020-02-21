namespace Fanex.BetList.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    internal static class TicketParser
    {
        public static Dictionary<string, string> ParseTransDesc(this ITicket ticket)
        {
            const int numberOfExpressionsOfEachProperty = 2;

            var transDescItems = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (string.IsNullOrEmpty(ticket.TransDesc))
            {
                return transDescItems;
            }

            var splittedTransDescParts = ticket.TransDesc.Split(new[] { '*' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in splittedTransDescParts)
            {
                var keyValuePair = part.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                if (keyValuePair.Length == numberOfExpressionsOfEachProperty)
                {
                    transDescItems.Add(keyValuePair[0].Trim(), keyValuePair[1].Trim());
                }
            }

            return transDescItems;
        }

        public static Dictionary<string, string> ParseBetcheck(this ITicket ticket)
        {
            const int numberOfExpressionsOfEachProperty = 2;

            var transDescItems = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (string.IsNullOrEmpty(ticket.BetCheck))
            {
                return transDescItems;
            }

            var betcheck = ticket.BetCheck.Replace("[", string.Empty).Replace("]", string.Empty);
            var splittedTransDescParts = betcheck.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in splittedTransDescParts)
            {
                var keyValuePair = part.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (keyValuePair.Length == numberOfExpressionsOfEachProperty)
                {
                    transDescItems.Add(keyValuePair[0].Trim(), keyValuePair[1].Trim());
                }
            }

            return transDescItems;
        }
    }
}