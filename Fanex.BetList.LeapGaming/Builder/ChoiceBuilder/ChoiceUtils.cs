namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public class MatchInfo
    {
        public MatchInfo(string transdesc, ITicketHelper ticketHelper)
        {
            var items = ParseTransDesc(transdesc);
           
            Odds = items.ContainsKey("o") ? Convert.ToDecimal(items["o"]) : 0;
            Status = items.ContainsKey("mo") ? items["mo"] : string.Empty;
            Status = ticketHelper.GetResourceData("LG_STMT", Status);

            BetTeam = items.ContainsKey("b") ? items["b"] : string.Empty;
            BetTeam = ticketHelper.GetResourceData("LG_BC", BetTeam);

            BetChoice = items.ContainsKey("s") ? items["s"] : string.Empty;
            BetChoice = ticketHelper.GetResourceData("LG_SEL", BetChoice);

            var match = items.ContainsKey("m") ? items["m"] : string.Empty;
            matchItems = Split(match, '-');
        }
       
        public string HomeTeam => matchItems.Length > 0 ? matchItems[0].Trim() : string.Empty;

        public string AwayTeam => matchItems.Length > 1 ? matchItems[1].Trim() : string.Empty;

        public string BetTeam { get; }

        public string BetChoice { get; }

        public decimal Odds { get; }

        public string Status { get; }

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
        }

        public string MixParlay { get; }
    }
}