namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;
    using Utils.TicketExt;

    /// <summary>
    /// Fast Markets: Next 1 minute.
    /// </summary>
    public class Status221 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);

            if (Template.StatusResult.Visible)
            {
                if (ticket.IsRefunded())
                {
                    Template.StatusResult.Hide();
                }
                else
                {
                    Template.StatusResult.betId = $"{ticket.Handicap1}, {ticket.Handicap2}";
                }
            }
        }
    }
}