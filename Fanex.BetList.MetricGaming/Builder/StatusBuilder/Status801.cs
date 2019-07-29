namespace Fanex.BetList.Core.Builder.StatusBuilder
{
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils.TicketExt;

    /// <summary>
    /// Goal Within 60 Seconds
    /// </summary>
    public class Status801 : Status1
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
                    var betId = ticket.BetId ?? 0;

                    Template.StatusResult.betId = betId.ToString();
                }
            }
        }
    }
}