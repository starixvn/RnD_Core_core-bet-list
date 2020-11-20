namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public class MatchInfo
    {
        public MatchInfo(string transdesc)
        {
            var items = ParseTransDesc(transdesc);

            MatchDate = items.ContainsKey("md") ? items["md"] : string.Empty;
            MatchId = items.ContainsKey("mid") ? items["mid"] : string.Empty;
            Season = items.ContainsKey("se") ? items["se"] : string.Empty;
            Odds = items.ContainsKey("o") ? Convert.ToDecimal(items["o"]) : 0;
            Status = items.ContainsKey("mo") ? items["mo"] : string.Empty;

            var selection = items.ContainsKey("s") ? items["s"] : string.Empty;
            selectionItems = Split(selection, ':');

            var match = items.ContainsKey("m") ? items["m"] : string.Empty;
            matchItems = Split(match, ':');
        }

        public string MatchDate { get; }

        public string MatchId { get; }

        public string Season { get; }

        public string Match { get; }

        public string HomeTeam => matchItems.Length > 0 ? matchItems[0].Trim() : string.Empty;

        public string AwayTeam => matchItems.Length > 1 ? matchItems[1].Trim() : string.Empty;

        public string BetTeam => selectionItems.Length > 1 ? selectionItems[1].Trim() : string.Empty;

        public string BetChoice => selectionItems.Length > 0 ? selectionItems[0].Trim() : string.Empty;

        public decimal Odds { get; }

        public string Status { get; }

        private readonly string[] selectionItems;

        private readonly string[] matchItems;
        private static IDictionary<string, string> ParseTransDesc(string transDesc)
        {
            const int numberOfExpressionsOfEachProperty = 2;

            var transDescItems = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var splittedTransDescParts = Split(transDesc, ';');

            foreach (var part in splittedTransDescParts)
            {
                var keyValuePair = Split(part, '=');

                if (keyValuePair.Length == numberOfExpressionsOfEachProperty)
                {
                    transDescItems.Add(keyValuePair[0].Trim(), keyValuePair[1].Trim());
                }
            }

            return transDescItems;
        }

        private static string[] Split(string value, char separator)
            => value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
    }

    public class LeapGamingResource
    {
        private const string RefResource = "LG_STMT";

        public LeapGamingResource(ITicketHelper ticketHelper)
        {
            MixParlay = ticketHelper.GetResourceData(RefResource, "mix");
            MatchDate = ticketHelper.GetResourceData(RefResource, "md");
            MatchId = ticketHelper.GetResourceData(RefResource, "mid");
            Season = ticketHelper.GetResourceData(RefResource, "se");
        }

        public string MixParlay { get; }
        public string MatchDate { get; }
        public string MatchId { get; }
        public string Season { get; }

    }
}