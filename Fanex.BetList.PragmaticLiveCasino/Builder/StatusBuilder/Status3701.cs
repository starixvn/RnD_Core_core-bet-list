namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    public class Status3701 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            if (ticket.IsSettled())
            {
                Template.StatusResult.betId = $"{ticket.BetId ?? 0}, '', {ticket.CustId}, '{ticket.BetCheck}'";
            }
            else
            {
                Template.StatusResult.Hide();
            }
        }
    }
}