namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using System.Collections.Generic;
    using System.Globalization;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;

    /// <summary>
    /// Progressive Jackpot
    /// </summary>
    public class Status2199 : Status2101
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            var result = ticket.Status.ToLowerInvariant();

            if (result.Equals(BetStatus.Running) && !string.IsNullOrEmpty(ticket.BetCheck))
            {
                Template.StatusResult.matchId = ticket.MatchId.ToString(CultureInfo.InvariantCulture);
                Template.StatusResult.betType = ticket.BetTypeId.ToString(CultureInfo.InvariantCulture);
                Template.StatusResult.race = string.IsNullOrEmpty(ticket.Race) ? "0" : ticket.Race;
                Template.StatusResult.sportType = ticket.SportTypeId.ToString(CultureInfo.InvariantCulture);
                Template.StatusResult.league = ticket.LeagueId.ToString(CultureInfo.InvariantCulture);
                Template.StatusResult.userName = ticket.UserName ?? string.Empty;
                Template.StatusResult.winlostDate = Formatter.FullDateFormat(ticket.WinlostDate);

                Template.StatusResult.Show();
            }
        }

        protected override void BuildResult(ITicket ticket, List<ITicketData> ticketData)
        {
            var status = ticket.Status.ToLowerInvariant();
            var result = GetResultString(ticket.Status);

            Template.result = !string.IsNullOrEmpty(result) ? result : status;
        }
    }
}