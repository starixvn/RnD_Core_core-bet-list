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
                var key = keyValuePair[0].Trim();
                if (keyValuePair.Length == numberOfExpressionsOfEachProperty &&
                    !transDescItems.ContainsKey(key))
                {
                    transDescItems.Add(key, keyValuePair[1].Trim());
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
            var betChoiceSection = ticket.BetCheck.Split('&');
            var betcheck = betChoiceSection[0].Replace("[", string.Empty).Replace("]", string.Empty);
            var splittedTransDescParts = betcheck.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in splittedTransDescParts)
            {
                var keyValuePair = part.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var key = keyValuePair[0].Trim();
                if (keyValuePair.Length == numberOfExpressionsOfEachProperty &&
                    !transDescItems.ContainsKey(key))
                {
                    transDescItems.Add(key, keyValuePair[1].Trim());
                }
            }

            return transDescItems;
        }

        public static Dictionary<string, string> ParseBetChoice(this ITicket ticket)
        {
            var transDescItems = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (string.IsNullOrEmpty(ticket.BetCheck))
            {
                return transDescItems;
            }

            var betcheck = ticket.BetCheck.Replace("[", string.Empty).Replace("]", string.Empty);
            var splittedTransDescParts = betcheck.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            var betChoiceSection = ticket.BetCheck.Split('&');
            var betChoiceIds = betChoiceSection.Length > 1 ? betChoiceSection[1].Split(';') : new string[splittedTransDescParts.Length];

            for (var i = 0; i < splittedTransDescParts.Length; i++)
            {
                var keyValuePair = splittedTransDescParts[i].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var key = keyValuePair[0].Trim();
                if(!transDescItems.ContainsKey(key))
                {
                    transDescItems.Add(key, i < betChoiceIds.Length ? betChoiceIds[i] : string.Empty);
                }                
            }

            return transDescItems;
        }
    }
}