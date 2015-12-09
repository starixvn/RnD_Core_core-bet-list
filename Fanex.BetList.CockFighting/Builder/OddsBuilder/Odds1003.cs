namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;

    public class Odds1003 : Odds1
    {
        private const string FTDOddsValue = "1:88";
        private const string BDDOddsValue = "1:8";

        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            if (ticket == null)
            {
                return Template;
            }

            Template.oddsType = string.Empty;
            var transDescData = Choice1003.ParseTransDesc(ticket.TransDesc);

            switch (transDescData[Choice1003.BetChoiceKey])
            {
                case Choice1003.FTDChoice:
                    Template.odds = FTDOddsValue;
                    return Template;

                case Choice1003.BDDChoice:
                    Template.odds = BDDOddsValue;
                    return Template;

                default:
                    return base.Render(ticket, ticketData, funcGetOddsTypeName);
            }
        }
    }
}