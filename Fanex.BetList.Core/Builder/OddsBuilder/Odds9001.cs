namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System;
    using System.Collections.Generic;
    using Entities;
    using Fanex.BetList.Core.Utils;
    using Templates;

    /// <summary>
    /// E-Sports 2.0: Map X Moneyline.
    /// </summary>
    public class Odds9001 : Odds1
    {
        private const byte DecimalOddsTypeId = 1;
        private const byte USOddsTypeId = 5;

        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            var oddsType = string.IsNullOrEmpty(ticket.OddsType) ? "0" : ticket.OddsType;
            var actualOdds = CalculateOddsByOddsType(Convert.ToByte(oddsType), ticket.Odds);

            Template.odds = FormatOdds(actualOdds);
            Template.oddsType = funcGetOddsTypeName(oddsType);

            return Template;
        }

        protected static string FormatOdds(decimal odds)
        {
            if (odds < 0)
            {
                return string.Format(ConvertByBetType.Minus, string.Format(Formatter.DECIMALFORMAT3, odds));
            }

            if (odds == 0)
            {
                return "-";
            }

            return string.Format(Formatter.DECIMALFORMAT3, odds);
        }

        protected static decimal CalculateOddsByOddsType(byte oddsType, decimal odds)
        {
            if (oddsType == DecimalOddsTypeId)
            {
                return odds + 1;
            }

            if (oddsType == USOddsTypeId)
            {
                return odds * 100;
            }

            return odds;
        }
    }
}