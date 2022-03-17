namespace Fanex.BetList.Core.Utils.Parsers
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public static class TicketParser
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
                    transDescItems.Add(keyValuePair[0].ToLowerInvariant(), keyValuePair[1]);
                }
            }

            return transDescItems;
        }
    }
}