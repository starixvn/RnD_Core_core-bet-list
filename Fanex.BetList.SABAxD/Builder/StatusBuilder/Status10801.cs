namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Globalization;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status10801 : StatusRNG
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            if (!IsHideRNGResult(ticket) && (ticket.IsSettled() || ticket.IsRefunded()))
            {
                Template.StatusResult.matchId = ticket.MatchId.ToString(CultureInfo.InvariantCulture);
                Template.StatusResult.betType = ticket.BetTypeId.ToString(CultureInfo.InvariantCulture);
                Template.StatusResult.race = string.IsNullOrEmpty(ticket.Race) ? "0" : ticket.Race;
                Template.StatusResult.refNo = ticket.TransId.ToString(CultureInfo.InvariantCulture);
                Template.StatusResult.sportType = ticket.SportTypeId.ToString(CultureInfo.InvariantCulture);
                Template.StatusResult.league = ticket.LeagueId.ToString(CultureInfo.InvariantCulture);
                Template.StatusResult.userName = ticket.UserName;
                Template.StatusResult.winlostDate = Formatter.FullDateFormat(ticket.WinlostDate);                
                Template.StatusResult.betId = $"{ticket.CurrencyId}, '{ticket.BetTeam}', {ticket.CustId}, '{ticket.BetCheck}', '{ticket.SiteId}'";                
            }
            else
            {
                Template.StatusResult.Hide();
            }
        }

        protected override bool IsHideRNGResult(ITicket ticket)
        {
            const int daySupportResultLink = 10;

            return base.IsHideRNGResult(ticket) || ticket.IsTicketHide3rdResultLink(daySupportResultLink);
        }
    }
}