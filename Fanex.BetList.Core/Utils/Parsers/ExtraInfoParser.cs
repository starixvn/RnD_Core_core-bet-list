namespace Fanex.BetList.Core.Utils.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization.Json;
    using Fanex.BetList.Core.Entities;

    public static class ExtraInfoParser
    {
        public static Dictionary<string, string> ParseExtraInfo(string extraInfo)
            => ParseExtraInfo(extraInfo, '*');

        public static Dictionary<string, string> ParseExtraInfo(string extraInfo, char splitChar)
        {
            const int numberOfExpressionsOfEachProperty = 2;

            var extraInfoItems = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (string.IsNullOrEmpty(extraInfo))
            {
                return extraInfoItems;
            }

            var splittedExtraInfoParts = extraInfo.Split(new[] { splitChar }, StringSplitOptions.RemoveEmptyEntries);

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

        public static LegOddsTransDesc ParseLegOddsTransDesc(string transDesc)
        {
            if (string.IsNullOrEmpty(transDesc))
            {
                return new LegOddsTransDesc();
            }

            try
            {
                var serializer = new DataContractJsonSerializer(typeof(LegOddsTransDesc));

                var result = serializer.SafeDeserializeFromString<LegOddsTransDesc>(transDesc);
                if (result != null && result.LegOddsbyMatch != null)
                {
                    return result;
                }

                return new LegOddsTransDesc();
            }
            catch
            {
                return new LegOddsTransDesc();
            }
        }
    }
}