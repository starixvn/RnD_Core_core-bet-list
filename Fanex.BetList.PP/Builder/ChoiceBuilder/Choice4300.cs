namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System;
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;

    public class Choice4300 : Choice4101
    {
        private const string MainTicketGameTypeId = "MAINGAMEID";

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            var mainTicketGameType = GetMainTicketGameTypeName(ticket, ticketHelper);
            var gameType = ticketHelper.GetResourceData(GameTypeRefId, ticket.BetTypeId.ToString());

            if (string.IsNullOrEmpty(ticket.BetCheck))
            {
                Template.betTeam = $"<span class=\"handicap\">{gameType}</span>";
                return;
            }

            Template.betTeam = $"<span class=\"handicap\">{mainTicketGameType} - {gameType}</span> {ticket.BetCheck}";
        }

        private static string GetMainTicketGameTypeName(ITicket ticket, ITicketHelper ticketHelper)
        {
            var additionalInfo = DeserializeAdditionalInfo(ticket.MatchCode.ToUpperInvariant());

            if (additionalInfo.ContainsKey(MainTicketGameTypeId))
            {
                return ticketHelper.GetResourceData(GameTypeRefId, additionalInfo[MainTicketGameTypeId]);
            }

            return string.Empty;
        }

        private static Dictionary<string, string> DeserializeAdditionalInfo(string additionalInfo)
        {
            const int requiredNumberOfParts = 2;

            var transDescItems = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(additionalInfo))
            {
                return transDescItems;
            }

            var keyValuePairs = additionalInfo.Split(new[] { '*' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var keyValuePair in keyValuePairs)
            {
                var keyValue = keyValuePair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (keyValue.Length == requiredNumberOfParts)
                {
                    transDescItems.Add(keyValue[0], keyValue[1].Trim());
                }
            }

            return transDescItems;
        }
    }
}