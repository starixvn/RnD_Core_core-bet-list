namespace Fanex.BetList.Core.Builder.OddsBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Entities;
    using Templates;
    using Utils;

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
                List<ITicketData> foundTicketData = GetReferenceData(ticket, ticketData);

                if (foundTicketData != null && foundTicketData.Count > 1)
                {
                    ITicketData winTicket = foundTicketData.Find(item => item.BetTypeId == WinBetId);
                    ITicketData placeTicket = foundTicketData.Find(item => item.BetTypeId == PlaceBetId);

                    string oddsWin = Formatter.FormatNumber3(winTicket.Odds);
                    string oddsPlace = Formatter.FormatNumber3(placeTicket.Odds);

                    var oddsWinMarkup = string.Join(null, new string[] { CoreBetList.win, ": ", "<b>", oddsWin, "</b>" });
                    var oddsPlaceMarkup = string.Join(null, new string[] { CoreBetList.place, ": ", "<b>", oddsPlace, "</b>" });

                    var oddsWinContent = string.Join(null, new string[] { "<span class='blue' style='font-weight:normal;'>", oddsWinMarkup, "</span>" });
                    var oddsPlaceContent = string.Join(null, new string[] { "<span class='' style='font-weight:normal;'>", oddsPlaceMarkup, "</span>" });

                    Template.odds = string.Join(null, new string[] { oddsWinContent, "<br>", oddsPlaceContent });
                }
                else
                {
                    Template.odds = "-";
                }
            }
            else
            {
                Template.odds = "-";
            }

            Template.oddsType = funcGetOddsTypeName(ConvertByBetType.OddsType(ticket.BetTypeId, ticket.OddsType));

            return Template;
        }

        protected override List<ITicketData> GetReferenceData(ITicket ticket, List<ITicketData> ticketData)
        {
            return ticketData.FindAll(item => item.RefNo.Equals(ticket.RefNo));
        }
    }
}