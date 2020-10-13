namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    /// <summary>
    /// Bet Type: Time Machine.
    /// </summary>
    public class Status228 : Status1
    {
        protected override void BuildStatusResult(ITicket ticket)
        {
            base.BuildStatusResult(ticket);
            Template.StatusResult.betId = ticket.Handicap1.ToString();

            if (ticket.IsRefunded())
            {
                Template.StatusResult.Hide();
            }
        }
    }
}