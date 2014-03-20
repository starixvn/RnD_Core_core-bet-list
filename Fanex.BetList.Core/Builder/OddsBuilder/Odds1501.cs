// ---------------------------------------------------------------------------------
// <copyright file="Odds1501.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2014.03.05">Create</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Keno games.
    /// </summary>
    public class Odds1501 : IOdds
    {
        public Odds1501()
        {
            this.Template = new Odds_Template();
        }

        public Odds_Template Template
        {
            get;
            set;
        }

        /// <summary>
        /// Renders the specified ticket.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="funcGetOddsTypeName">Name of the function get odds type.</param>
        /// <returns>Odds Template.</returns>
        public Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
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

            this.Template.odds = ConvertByBetType.Odds(ticket.Odds, ticket.BetTypeId, oddsType);
            this.Template.oddsType = string.Empty;

            return this.Template;
        }

        /// <summary>
        /// Renders the RTF.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="funcGetOddsTypeName">Name of the function get odds type.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString object.</returns>
        public IRichTextString RenderRTF(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName, RTFHelper rtfHelper)
        {
            Render(ticket, ticketData, funcGetOddsTypeName);
            var rtfOdds = BuildRTFOdds(rtfHelper);

            return rtfOdds;
        }

        /// <summary>
        /// Builds the RTF odds.
        /// </summary>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString object.</returns>
        protected virtual IRichTextString BuildRTFOdds(RTFHelper rtfHelper)
        {
            string odds = Template.odds;
            var rtfNumberRenderer = new RTFNumber(rtfHelper.RTFRenderer, rtfHelper.PosFont, rtfHelper.NegFont);

            rtfNumberRenderer.Render(odds);

            var rtfOdds = rtfHelper.RTFRenderer.Render();
            rtfHelper.RTFRenderer.Clear();
            return rtfOdds;
        }
    }
}