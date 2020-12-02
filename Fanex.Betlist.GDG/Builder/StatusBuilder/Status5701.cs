namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Status5701 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            if (string.IsNullOrEmpty(ticket.TransDesc))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                Template.StatusResult.betId = $"{ticket.BetId ?? 0}, '{ticket.MatchCode}', {ticket.CustId}, '{ticket.BetCheck}'";
            }
        }
    }
}