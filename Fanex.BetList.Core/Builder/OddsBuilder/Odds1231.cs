// ---------------------------------------------------------------------------------
// <copyright file="Odds1231.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2013.03.11">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;

    public class Odds1231 : Odds1
    {
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById FuncGetOddsTypeName)
        {
            object oddsType = new object();

            if (string.IsNullOrEmpty(ticket.OddsType))
            {
                oddsType = 0;
            }
            else
            {
                oddsType = ticket.OddsType;
            }

            this.Template.odds = ConvertByBetType.VirtualHorseRacingOdds(ticket.Odds, ticket.BetTypeId, oddsType);
            this.Template.oddsType = FuncGetOddsTypeName(ConvertByBetType.OddsType(ticket.BetTypeId, oddsType));

            return this.Template;
        }
    }
}