namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    public class Status6401 : Status1
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

                Template.StatusResult.betId = $"{ticket.BetId ?? 0}, '{ticket.MatchCode}', '{ticket.CustId}', '{ticket.BetCheck}'";
            }
        }
    }
}