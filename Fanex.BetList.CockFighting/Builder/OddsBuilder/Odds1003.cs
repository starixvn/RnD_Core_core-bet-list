namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Utils;
    public class Odds1003 : Odds1
    {
        private const string ExceptionOddsValueFormat = "1:{0}";

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
                case Choice1003.BDDChoice:
                    Template.odds = string.Format(ExceptionOddsValueFormat, Formatter.DecFormat(ticket.Odds, 0));
                    return Template;

                default:
                    return base.Render(ticket, ticketData, funcGetOddsTypeName);
            }
        }
    }
}