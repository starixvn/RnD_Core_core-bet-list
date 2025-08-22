namespace Fanex.BetList.Core.Utils
{
    using System;

    public static class TransDescParser
    {
        public static Tuple<string, string> ParseTransDesc(string transDesc)
        {
            var neededInfo = RemoveExtraInfo(transDesc);

            return ParseTransDesc(Split(neededInfo));
        }

        private static string RemoveExtraInfo(string transDesc)
        {
            var lengthOfNeededInfo = transDesc.IndexOf(';');

            return transDesc.Substring(0, lengthOfNeededInfo < 0 ? transDesc.Length : lengthOfNeededInfo);
        }

        private static string[] Split(string value)
            => value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        private static Tuple<string, string> ParseTransDesc(string[] parts)
        {
            const string gameIdIndicator = "betChoice=";
            const int expectedNumberOfInfoParts = 2;
            var gameId = string.Empty;
            var choiceId = string.Empty;

            if (parts.Length == expectedNumberOfInfoParts)
            {
                if (parts[0].StartsWith(gameIdIndicator, StringComparison.OrdinalIgnoreCase))
                {
                    gameId = parts[0].Substring(parts[0].IndexOf('=') + 1).Trim();
                }

                choiceId = parts[1].Trim();
            }

            return new Tuple<string, string>(choiceId, gameId);
        }
    }
}
