namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;

    /// <summary>
    /// Video Poker
    /// </summary>
    public class Status1901 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket.BetCheck))
            {
                Template.StatusResult.Hide();
            }
            else
            {
                base.BuildStatusResult(ticket);
                Template.StatusResult.refNo = $"'{ticket.BetCheck}'";
                Template.StatusResult.betId = $"{ticket.BetId ?? 0}, '{ticket.BetCheck}', {ticket.CustId}";
            }
        }
    }
}