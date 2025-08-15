namespace Fanex.BetList.Core.Utils
{
    using System;

    public static class TransDescParser
    {
        public static string GetChoiceKey(string transDesc)
        {
            const int numberOfExpressionsOfEachProperty = 2;

            if (string.IsNullOrEmpty(transDesc))
            {
                return string.Empty;
            }

            var splittedTransDescParts = transDesc.Split(new[] { '*' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in splittedTransDescParts)
            {
                var keyValuePair = part.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                if (keyValuePair.Length == numberOfExpressionsOfEachProperty &&
                    keyValuePair[0].Equals("choice", StringComparison.OrdinalIgnoreCase))
                {
                    return keyValuePair[1];
                }
            }

            return string.Empty;
        }
    }
}