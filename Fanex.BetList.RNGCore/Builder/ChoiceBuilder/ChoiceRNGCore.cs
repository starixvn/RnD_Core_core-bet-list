namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.Parsers;
    using Fanex.BetList.Core.Utils.TicketExt;
    using System.Collections.Generic;
    using System.Globalization;

    public class ChoiceRNGCore : ChoiceRNG
    {
        private const string JackpotTransIdInfo = "jpw";
        protected virtual string RefIdForGameGroup => string.Empty;

        protected override void BuildLeague(ITicket ticket, ITicketHelper ticketHelper)
        {
            Template.League.LeagueName.SetValue($"<span class=\"gamegroupname\"> {ticketHelper.GetResourceData(RefIdForGameGroup, ticket.BetTypeId.ToString())}</span>");

            if (IsSummaryTicket(ticket.BetCheck))
            {
                Template.League.Append(BuildDetailsLink(ticket, ticketHelper));
            }
            else
            {
                if (ticket.TicketHasJackpotInfo())
                {
                    Template.League.Append(BuildShowJackpotWonDetails(ticket, ticketHelper));
                }
            }
        }

        protected virtual string BuildShowJackpotWonDetails(ITicket ticket, ITicketHelper ticketHelper)
        {
            var transId = ExtraInfoParser.ParseExtraInfo(ticket.JackpotInfo, ',')[JackpotTransIdInfo];
            var sportTypeName = ticketHelper.GetSportNameById(ticket.SportTypeId);
            var detailLinkMarkup =
                $"<div class=\"detail jackpot-detail\" onclick=\"showRNGJackpotWonDetails('{transId}', '{ticket.CustId}', '{ticket.BetTypeId}', '{ticket.WinlostDate}', '{sportTypeName}');\">" +
                $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.JackpotWonDetails}</a>" +
                $"</div>";

            return detailLinkMarkup;
        }
        private static string BuildDetailsLink(ITicket ticket, ITicketHelper ticketHelper)
        {
            var detailLink = $"<a class=\"detail-link\" href=\"javascript:void('');\">{CoreBetList.details}</a>";
            var parames = new List<object>
            {
                $"'{ticket.TransId}'",
                $"'{ticket.WinlostDate.ToString(CultureInfo.InvariantCulture)}'",
                ticket.CustId,
                ticket.GetTransactionType(),
                ticket.SportTypeId,
                $"'{ticketHelper.GetSportNameById(ticket.SportTypeId)}'"
            };
            var parameterList = string.Join(", ", parames);

            return $"<div class=\"detail rng-detail\" onclick=\"showRNGBetDetails({parameterList});\">{detailLink}</div>";
        }
    }
}