namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System;
    using System.Globalization;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status3802 : StatusRNG
    {
        private const string siteIdDefault = "4100300";

        protected override void BuildStatusResult(ITicket ticket)
        {
            if (ticket.IsSettled() || ticket.IsRefunded())
            {
                if (IsHideRNGResult(ticket))
                {
                    Template.StatusResult.Hide();
                }
                else
                {
                    var siteId = string.IsNullOrEmpty(ticket.SiteId) ? siteIdDefault : ticket.SiteId;

                    Template.StatusResult.matchId = ticket.MatchId.ToString(CultureInfo.InvariantCulture);
                    Template.StatusResult.betType = ticket.BetTypeId.ToString(CultureInfo.InvariantCulture);
                    Template.StatusResult.race = string.IsNullOrEmpty(ticket.Race) ? "0" : ticket.Race;
                    Template.StatusResult.refNo = ticket.TransId.ToString(CultureInfo.InvariantCulture);
                    Template.StatusResult.sportType = ticket.SportTypeId.ToString(CultureInfo.InvariantCulture);
                    Template.StatusResult.league = ticket.LeagueId.ToString(CultureInfo.InvariantCulture);
                    Template.StatusResult.userName = ticket.Currency;
                    Template.StatusResult.winlostDate = Formatter.FullDateFormat(ticket.WinlostDate);
                    Template.StatusResult.betId = $"{ticket.CurrencyId}, '{ticket.BetTeam}', {ticket.CustId}, '{ticket.BetCheck}', '{siteId}'";
                }
            }
            else
            {
                Template.StatusResult.Hide();
            }
        }

        protected override bool IsHideRNGResult(ITicket ticket)
        {
            const int daySupportResultLink = 10;

            return string.IsNullOrEmpty(ticket.BetCheck) ||
                ticket.IsTicketHide3rdResultLink(daySupportResultLink);
        }
    }
}