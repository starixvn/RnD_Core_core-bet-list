namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;

    public class Odds3508 : Odds1
    {
        private static readonly List<string> ChoiceKeysHiddenOdds = new List<string> { "3489660930_2_1", "3489660936_2_2", "3489660960_2_3" };

        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            var choiceKey = TransDescParser.GetChoiceKey(ticket.TransDesc);

            if (!string.IsNullOrEmpty(choiceKey) && ChoiceKeysHiddenOdds.Contains(choiceKey))
            {
                Template.odds = "-";
                Template.oddsType = string.Empty;

                return Template;
            }

            return base.Render(ticket, ticketData, funcGetOddsTypeName);
        }
    }
}