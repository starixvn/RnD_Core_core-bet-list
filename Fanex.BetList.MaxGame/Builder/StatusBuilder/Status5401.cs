namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;

    public class Status5401 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            if (string.IsNullOrEmpty(ticket.BetCheck))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                base.BuildStatusResult(ticket);

                var siteId = string.IsNullOrEmpty(ticket.BetTeam) ? "0" : ticket.BetTeam;
                var transId3rd = ticket.BetCheck;
                var transDate3rd = Formatter.FullDateFormat(ticket.EventDate.Value);

                Template.StatusResult.betId = $"{siteId}, '{transDate3rd}', {ticket.CustId}, '{transId3rd}'";
            }
        }
    }
}