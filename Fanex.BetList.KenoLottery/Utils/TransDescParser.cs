namespace Fanex.BetList.Core.Utils
{
    using System;

    internal static class TransDescParser
    {
        public static Tuple<string, string> ParseTransDesc(string transDesc)
        {
            const string gameIdIndicator = "betChoice=";
            const int expectedNumberOfInfoParts = 2;

            var lengthOfNeededInfo = transDesc.IndexOf(';');
            var neededInfo = transDesc.Substring(0, lengthOfNeededInfo < 0 ? transDesc.Length : lengthOfNeededInfo);
            var parts = neededInfo.Split(',');
            var gameId = string.Empty;
            var choiceId = string.Empty;

            if (parts.Length == expectedNumberOfInfoParts)
            {
                if (parts[0].StartsWith(gameIdIndicator, StringComparison.InvariantCultureIgnoreCase))
                {
                    gameId = parts[0].Substring(parts[0].IndexOf('=') + 1).Trim();
                }

                choiceId = parts[1].Trim();
            }

            return new Tuple<string, string>(choiceId, gameId);
        }
    }
}