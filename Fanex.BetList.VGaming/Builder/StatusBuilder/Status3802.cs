namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System;
    using System.Globalization;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status3802 : Status1
    {
        private const int dayAgoSupportResultPage = -10;
        private const string siteIdDefault = "4100300";

        protected override void BuildStatusResult(ITicket ticket)
        {
            if (ticket.IsSettled() || ticket.IsRefunded())
            {
                if (IsSupportViewResultLink(ticket))
                {
                    var siteId = string.IsNullOrEmpty(ticket.MatchCode) ? siteIdDefault : ticket.MatchCode;

                    Template.StatusResult.matchId = ticket.MatchId.ToString(CultureInfo.InvariantCulture);
                    Template.StatusResult.betType = ticket.BetTypeId.ToString(CultureInfo.InvariantCulture);
                    Template.StatusResult.race = string.IsNullOrEmpty(ticket.Race) ? "0" : ticket.Race;
                    Template.StatusResult.refNo = ticket.TransId.ToString(CultureInfo.InvariantCulture);
                    Template.StatusResult.sportType = ticket.SportTypeId.ToString(CultureInfo.InvariantCulture);
                    Template.StatusResult.league = ticket.LeagueId.ToString(CultureInfo.InvariantCulture);
                    Template.StatusResult.userName = ticket.Currency;
                    Template.StatusResult.winlostDate = Formatter.FullDateFormat(ticket.WinlostDate);
                    Template.StatusResult.betId = $"{ticket.BetId ?? 0}, '{siteId}', {ticket.CustId}, '{ticket.BetCheck}'";
                }
                else
                {
                    Template.StatusResult.Hide();
                }
            }
            else
            {
                Template.StatusResult.Hide();
            }
        }

        private static bool IsSupportViewResultLink(ITicket ticket)
        {
            var minDate = DateTime.Today.AddDays(dayAgoSupportResultPage);

            return ticket.WinlostDate >= minDate && !string.IsNullOrWhiteSpace(ticket.BetCheck);
        }
    }
}