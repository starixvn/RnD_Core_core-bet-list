namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using Fanex.BetList.Core.Utils.TicketExt;
    using System.Globalization;

    public class Status1612 : StatusRNGExpireDate
    {
        public override int ExpireDateResultLink => 10;
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
                    var externalLink = $"{ticket.CurrencyId}, '{ticket.BetTeam}', {ticket.CustId}, '{ticket.BetCheck}', '{ticket.SiteId}', '{sportName}'";

                    Template.StatusResult.matchId = ticket.MatchId.ToString(CultureInfo.InvariantCulture);
                    Template.StatusResult.betType = ticket.BetTypeId.ToString(CultureInfo.InvariantCulture);
                    Template.StatusResult.race = string.IsNullOrEmpty(ticket.Race) ? "0" : ticket.Race;
                    Template.StatusResult.refNo = ticket.TransId.ToString(CultureInfo.InvariantCulture);
                    Template.StatusResult.sportType = ticket.SportTypeId.ToString(CultureInfo.InvariantCulture);
                    Template.StatusResult.league = ticket.LeagueId.ToString(CultureInfo.InvariantCulture);
                    Template.StatusResult.userName = ticket.UserName;
                    Template.StatusResult.winlostDate = Formatter.FullDateFormat(ticket.WinlostDate);
                    Template.StatusResult.betId = externalLink;
                }
            }
            else
            {
                Template.StatusResult.Hide();
            }
        }
    }
}