// ---------------------------------------------------------------------------------
// <copyright file="Odds1233.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------
// <history>
//     <change who="Marc.Bui" date="2013.02.27">Create</change>
//     <change who="Marc.Bui" date="2013.03.11">Update always show odds</change>
// </history>
// ---------------------------------------------------------------------------------

namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Templates;
    using Fanex.BetList.Core.Utils;

    /// <summary>
    /// Virtual Horse Racing - Win/Place.
    /// </summary>
    public class Odds1233 : Odds33
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Odds1233" /> class.
        /// </summary>
        public Odds1233()
        {
            WinBetId = 1231;
            PlaceBetId = 1232;
        }

        /// <summary>
        /// Renders the specified ticket.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <param name="funcGetOddsTypeName">Name of the function get odds type.</param>
        /// <returns>Odds_Template object.</returns>
        public override Odds_Template Render(ITicket ticket, List<ITicketData> ticketData, GetCachePropertyById funcGetOddsTypeName)
        {
            if (ticketData != null && ticketData.Count > 0)
            {
                string odds_win = string.Empty;
                string odds_place = string.Empty;
                string oddsWinMarkup, oddsPlaceMarkup;               

                List<ITicketData> foundTicketData = this.GetReferenceData(ticket, ticketData);

                if (foundTicketData != null && foundTicketData.Count > 1)
                {
                    ITicketData winTicket = foundTicketData.Find(item => item.BetTypeId == WinBetId);
                    ITicketData placeTicket = foundTicketData.Find(item => item.BetTypeId == PlaceBetId);

                    string oddsWin = Formatter.FormatNumber3(winTicket.Odds);
                    string oddsPlace = Formatter.FormatNumber3(placeTicket.Odds);

                    oddsWinMarkup = string.Join(null, new string[] { CoreBetList.win, ": ", "<b>", oddsWin, "</b>" });
                    oddsPlaceMarkup = string.Join(null, new string[] { CoreBetList.place, ": ", "<b>", oddsPlace, "</b>" });

                    string status = ticket.Status.ToLower();

                    odds_win = string.Join(null, new string[] { "<span class='blue' style='font-weight:normal;'>", oddsWinMarkup, "</span>" });

                    odds_place = string.Join(null, new string[] { "<span class='' style='font-weight:normal;'>", oddsPlaceMarkup, "</span>" });

                    this.Template.odds = string.Join(null, new string[] { odds_win, "<br>", odds_place });
                }
                else
                {
                    this.Template.odds = "-";
                }
            }
            else
            {
                this.Template.odds = "-";
            }

            this.Template.oddsType = funcGetOddsTypeName(ConvertByBetType.OddsType(ticket.BetTypeId, ticket.OddsType));

            return this.Template;
        }

        /// <summary>
        /// Gets the reference data.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketData">The ticket data.</param>
        /// <returns>List of reference data.</returns>
        protected override List<ITicketData> GetReferenceData(ITicket ticket, List<ITicketData> ticketData)
        {
            return ticketData.FindAll(item => item.RefNo.Equals(ticket.RefNo));
        }
    }
}