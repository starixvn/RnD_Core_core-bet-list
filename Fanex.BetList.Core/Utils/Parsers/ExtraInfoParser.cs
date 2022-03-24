namespace Fanex.BetList.Core.Utils.Parsers
{
    using System;
    using System.Collections.Generic;

    public static class ExtraInfoParser
    {
        public static Dictionary<string, string> ParseExtraInfo(string extraInfo)
        {
            const int numberOfExpressionsOfEachProperty = 2;

            var extraInfoItems = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (string.IsNullOrEmpty(extraInfo))
            {
                return extraInfoItems;
            }

            var splittedExtraInfoParts = extraInfo.Split(new[] { '*' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in splittedExtraInfoParts)
            {
                var keyValuePair = part.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                if (keyValuePair.Length == numberOfExpressionsOfEachProperty)
                {
                    extraInfoItems.Add(keyValuePair[0].ToLowerInvariant(), keyValuePair[1]);
                }
            }

            return extraInfoItems;
        }
    }
}